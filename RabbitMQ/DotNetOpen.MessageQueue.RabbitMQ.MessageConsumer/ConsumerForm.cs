using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RabbitMQ = global::RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;

namespace DotNetOpen.MessageQueue.RabbitMQ.MessageConsumer
{
    public enum MQSendType
    {
        HelloWorld,
        WorkQueue,
        PubSub,
        Routing,
        Topics,
        TopicsRoundRobin,
        RPC
    }

    public partial class ConsumerForm : Form
    {
        const string Default_Queue = "Queue";
        const string Default_Exchange = "Exchange";
        const string Default_RoutingKey = "RoutingKey";

        Stopwatch stopWatcher;
        ConnectionFactory factory;
        IConnection connection;
        IModel channel;

        public ConsumerForm()
        {
            InitializeComponent();
            
            UsernameTextBox.Text = "guest";
            PasswordTextBox.Text = "guest";

            queueTextbox.Text = Default_Queue;
            exchangeTextBox.Text = Default_Exchange;
            routingKeyTextbox.Text = Default_RoutingKey;

            typeComboBox.SelectedIndexChanged += TypeComboBox_SelectedIndexChanged;
            typeComboBox.Items.Clear();
            typeComboBox.Items.AddRange(Enum.GetValues(typeof(MQSendType)).Cast<object>().ToArray());
            typeComboBox.SelectedIndex = 0;
        }

        private void TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sendType = (MQSendType)typeComboBox.SelectedItem;
            switch (sendType)
            {
                case MQSendType.HelloWorld:
                    SendTypeMessageLabel.Text = "The simplest thing that does something";
                    queueTextbox.Enabled = true;
                    queueTextbox.Text = Default_Queue;
                    exchangeTextBox.Enabled = false;
                    exchangeTextBox.Text = "";
                    routingKeyTextbox.Enabled = false;
                    routingKeyTextbox.Text = "";
                    break;
                case MQSendType.WorkQueue:
                    SendTypeMessageLabel.Text = "Distributing tasks among workers (the competing consumers pattern)";
                    queueTextbox.Enabled = true;
                    queueTextbox.Text = Default_Queue;
                    exchangeTextBox.Enabled = false;
                    exchangeTextBox.Text = "";
                    routingKeyTextbox.Enabled = false;
                    routingKeyTextbox.Text = "";
                    break;
                case MQSendType.PubSub:
                    SendTypeMessageLabel.Text = "Sending messages to many consumers at once";
                    queueTextbox.Enabled = false;
                    queueTextbox.Text = "";
                    exchangeTextBox.Enabled = true;
                    exchangeTextBox.Text = Default_Exchange;
                    routingKeyTextbox.Enabled = false;
                    routingKeyTextbox.Text = "";
                    break;
                case MQSendType.Routing:
                    SendTypeMessageLabel.Text = "Receiving messages selectively";
                    queueTextbox.Enabled = false;
                    queueTextbox.Text = "";
                    exchangeTextBox.Enabled = true;
                    exchangeTextBox.Text = Default_Exchange;
                    routingKeyTextbox.Enabled = true;
                    routingKeyTextbox.Text = $"{Default_RoutingKey}";
                    break;
                case MQSendType.Topics:
                    SendTypeMessageLabel.Text = "Receiving messages based on a pattern (topics)";
                    queueTextbox.Enabled = false;
                    queueTextbox.Text = "";
                    exchangeTextBox.Enabled = true;
                    exchangeTextBox.Text = Default_Exchange;
                    routingKeyTextbox.Enabled = true;
                    routingKeyTextbox.Text = $"{Default_RoutingKey}.*";
                    break;
                case MQSendType.TopicsRoundRobin:
                    SendTypeMessageLabel.Text = "Receiving messages based on a pattern (topics) - RoundRobin for same queues";
                    queueTextbox.Enabled = true;
                    queueTextbox.Text = Default_Queue;
                    exchangeTextBox.Enabled = true;
                    exchangeTextBox.Text = Default_Exchange;
                    routingKeyTextbox.Enabled = true;
                    routingKeyTextbox.Text = $"{Default_RoutingKey}.*";
                    break;
                case MQSendType.RPC:
                    SendTypeMessageLabel.Text = "Request/reply pattern example (NOT IMPLEMENTED YET)";
                    queueTextbox.Enabled = true;
                    queueTextbox.Text = Default_Queue;
                    exchangeTextBox.Enabled = false;
                    exchangeTextBox.Text = "";
                    routingKeyTextbox.Enabled = false;
                    routingKeyTextbox.Text = "";
                    break;
                default:
                    break;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            Reset();
            base.OnClosed(e);

        }

        private void ConsumerForm_Load(object sender, EventArgs e)
        {
            stopWatcher = new Stopwatch();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            Reset();
            
            factory = new ConnectionFactory() { HostName = HostTextBox.Text, UserName = UsernameTextBox.Text,Password = PasswordTextBox.Text };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();

            var queueName = queueTextbox.Text;
            var exchange = exchangeTextBox.Text;
            var routingKey = routingKeyTextbox.Text;

            var sendType = (MQSendType)typeComboBox.SelectedItem;
            switch (sendType)
            {
                case MQSendType.HelloWorld:
                    ReceiveHelloWorld(connection, channel, queueName, exchange, routingKey);
                    break;
                case MQSendType.WorkQueue:
                    ReceiveWorkQueue(connection, channel, queueName, exchange, routingKey);
                    break;
                case MQSendType.PubSub:
                    ReceivePubSub(connection, channel, queueName, exchange, routingKey);
                    break;
                case MQSendType.Routing:
                    ReceiveRouting(connection, channel, queueName, exchange, routingKey);
                    break;
                case MQSendType.Topics:
                    ReceiveTopics(connection, channel, queueName, exchange, routingKey);
                    break;
                case MQSendType.TopicsRoundRobin:
                    ReceiveTopics(connection, channel, queueName, exchange, routingKey, true);
                    break;
                case MQSendType.RPC:
                    ReceiveHelloWorld(connection, channel, queueName, exchange, routingKey);
                    break;
                default:
                    break;
            }

            MessageBox.Show("Connected");
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            MessageTextBox.Text = "";
        }

        private void Reset()
        {
            if (channel != null)
                channel.Dispose();
            if (connection != null)
                connection.Dispose();

            stopWatcher.Reset();
        }

        #region Receive Per Different Cases
        protected void ReceiveHelloWorld(IConnection connection, IModel channel, string queueName, string exchange, string routingKey)
        {
            var count = 0;
            channel.QueueDeclare(queue: queueName,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            stopWatcher.Start();
            var startTime = DateTime.Now;
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                count++;
                MessageTextBox.Text = $"{DateTime.Now.ToString("HH:mm:ss")} Received: {message}" + Environment.NewLine + MessageTextBox.Text;
                ReceivedCountLabel.Text = $"{startTime.ToString("HH:mm:ss")} - {count}";// $"{count} - {stopWatch.Elapsed.ToString("HH:mm:ss")}";
            };
            channel.BasicConsume(queue: queueName,
                                 noAck: true,
                                 consumer: consumer);
        }

        protected void ReceiveWorkQueue(IConnection connection, IModel channel, string queueName, string exchange, string routingKey)
        {
            var count = 0;
            channel.QueueDeclare(queue: queueName,
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);
            // Each Worker only get once by turn. (Robin)
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            stopWatcher.Start();
            var startTime = DateTime.Now;
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                count++;
                MessageTextBox.Text = $"{DateTime.Now.ToString("HH:mm:ss")} Received: {message}" + Environment.NewLine + MessageTextBox.Text;
                ReceivedCountLabel.Text = $"{startTime.ToString("HH:mm:ss")} - {count}";// $"{count} - {stopWatch.Elapsed.ToString("HH:mm:ss")}";

                // Tell Producer that message has been received.
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
            channel.BasicConsume(queue: queueName,
                                 noAck: false,
                                 consumer: consumer);
        }
        protected void ReceivePubSub(IConnection connection, IModel channel, string queueName, string exchange, string routingKey)
        {
            var count = 0;
            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);

            // Generate Random Queue
            var randomQueueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: randomQueueName,
                              exchange: exchange,
                              routingKey: "");

            stopWatcher.Start();
            var startTime = DateTime.Now;
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                count++;
                MessageTextBox.Text = $"{DateTime.Now.ToString("HH:mm:ss")} Received: {message}" + Environment.NewLine + MessageTextBox.Text;
                ReceivedCountLabel.Text = $"{startTime.ToString("HH:mm:ss")} - {count}";// $"{count} - {stopWatch.Elapsed.ToString("HH:mm:ss")}";
            };

            channel.BasicConsume(queue: randomQueueName,
                                 noAck: true,
                                 consumer: consumer);
        }
        protected void ReceiveRouting(IConnection connection, IModel channel, string queueName, string exchange, string routingKey)
        {
            var count = 0;
            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Direct);

            // Generate Random Queue
            var randomQueueName = channel.QueueDeclare().QueueName;
            var routingKeyArray = routingKey.Split(' ');
            foreach (var rKey in routingKeyArray)
            {
                channel.QueueBind(queue: randomQueueName,
                                  exchange: exchange,
                                  routingKey: rKey);
            }
            stopWatcher.Start();
            var startTime = DateTime.Now;
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                count++;
                MessageTextBox.Text = $"{DateTime.Now.ToString("HH:mm:ss")} Received: {message} from {ea.RoutingKey}" + Environment.NewLine + MessageTextBox.Text;
                ReceivedCountLabel.Text = $"{startTime.ToString("HH:mm:ss")} - {count}";// $"{count} - {stopWatch.Elapsed.ToString("HH:mm:ss")}";
            };
            channel.BasicConsume(queue: randomQueueName,
                                 noAck: true,
                                 consumer: consumer);
        }
        protected void ReceiveTopics(IConnection connection, IModel channel, string queueName, string exchange, string routingKey, bool roundRobin = false)
        {
            var durable = roundRobin;

            var count = 0;
            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Topic, durable: durable);

            if (durable)
            {
                // Each Worker only get once by turn. (Robin)
                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
            }

            // Generate Random Queue
            var randomQueueName = (durable ? channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false) : channel.QueueDeclare()).QueueName;

            var routingKeyArray = routingKey.Split(' ');
            foreach (var rKey in routingKeyArray)
            {
                channel.QueueBind(queue: randomQueueName,
                                  exchange: exchange,
                                  routingKey: rKey);
            }
            stopWatcher.Start();
            var startTime = DateTime.Now;
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                count++;
                MessageTextBox.Text = $"{DateTime.Now.ToString("HH:mm:ss")} Received: {message} from {ea.RoutingKey}" + Environment.NewLine + MessageTextBox.Text;
                ReceivedCountLabel.Text = $"{startTime.ToString("HH:mm:ss")} - {count}";// $"{count} - {stopWatch.Elapsed.ToString("HH:mm:ss")}";

                if (durable)
                {
                    // Tell Producer that message has been received.
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
            };
            channel.BasicConsume(queue: randomQueueName,
                                 noAck: !durable,
                                 consumer: consumer);
        }
        #endregion
    }
}
