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
using System.Diagnostics;

namespace DotNetOpen.MessageQueue.RabbitMQ.MessageSender
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

    public partial class SenderForm : Form
    {
        const string Default_Queue = "Queue";
        const string Default_Exchange = "Exchange";
        const string Default_RoutingKey = "RoutingKey";
        
        public SenderForm()
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
            Reset();

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
                    routingKeyTextbox.Text = Default_RoutingKey;
                    break;
                case MQSendType.Topics:
                    SendTypeMessageLabel.Text = "Receiving messages based on a pattern (topics)";
                    queueTextbox.Enabled = false;
                    queueTextbox.Text = "";
                    exchangeTextBox.Enabled = true;
                    exchangeTextBox.Text = Default_Exchange;
                    routingKeyTextbox.Enabled = true;
                    routingKeyTextbox.Text = $"{Default_RoutingKey}.{Default_RoutingKey}1";
                    break;
                case MQSendType.TopicsRoundRobin:
                    SendTypeMessageLabel.Text = "Receiving messages based on a pattern (topics) - RoundRobin for same queues";
                    queueTextbox.Enabled = false;
                    queueTextbox.Text = "";
                    exchangeTextBox.Enabled = true;
                    exchangeTextBox.Text = Default_Exchange;
                    routingKeyTextbox.Enabled = true;
                    routingKeyTextbox.Text = $"{Default_RoutingKey}.{Default_RoutingKey}1";
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


        private void SenderForm_Load(object sender, EventArgs e)
        {
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = HostTextBox.Text, UserName = UsernameTextBox.Text, Password = PasswordTextBox.Text };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    MessageBox.Show("Connected");
                }
            }
            catch
            {
                MessageBox.Show("Failed");
            }
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            var queueName = queueTextbox.Text;
            var exchange = exchangeTextBox.Text;
            var routingKey = routingKeyTextbox.Text;

            var factory = new ConnectionFactory() { HostName = HostTextBox.Text, UserName = UsernameTextBox.Text, Password = PasswordTextBox.Text };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var sendType = (MQSendType)typeComboBox.SelectedItem;
                switch (sendType)
                {
                    case MQSendType.HelloWorld:
                        SendHelloWorld(connection, channel, queueName, exchange, routingKey);
                        break;
                    case MQSendType.WorkQueue:
                        SendWorkQueue(connection, channel, queueName, exchange, routingKey);
                        break;
                    case MQSendType.PubSub:
                        SendPubSub(connection, channel, queueName, exchange, routingKey);
                        break;
                    case MQSendType.Routing:
                        SendRouting(connection, channel, queueName, exchange, routingKey);
                        break;
                    case MQSendType.Topics:
                        SendTopics(connection, channel, queueName, exchange, routingKey);
                        break;
                    case MQSendType.TopicsRoundRobin:
                        SendTopics(connection, channel, queueName, exchange, routingKey, true);
                        break;
                    case MQSendType.RPC:
                        SendHelloWorld(connection, channel, queueName, exchange, routingKey);
                        break;
                    default:
                        break;
                }
            }
        }

        private void RecursivelyButton_Click(object sender, EventArgs e)
        {
            var queueName = queueTextbox.Text;
            var exchange = exchangeTextBox.Text;
            var routingKey = routingKeyTextbox.Text;
            
            var totalMS = int.Parse(CountTextBox.Text) * 60 * 1000;
            var stopWatcher = new Stopwatch();
            var factory = new ConnectionFactory() { HostName = HostTextBox.Text, UserName = UsernameTextBox.Text, Password = PasswordTextBox.Text };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var sendType = (MQSendType)typeComboBox.SelectedItem;
                switch (sendType)
                {
                    case MQSendType.HelloWorld:
                        SendHelloWorld(connection, channel, queueName, exchange, routingKey, stopWatcher, totalMS);
                        break;
                    case MQSendType.WorkQueue:
                        SendWorkQueue(connection, channel, queueName, exchange, routingKey, stopWatcher, totalMS);
                        break;
                    case MQSendType.PubSub:
                        SendPubSub(connection, channel, queueName, exchange, routingKey, stopWatcher, totalMS);
                        break;
                    case MQSendType.Routing:
                        SendRouting(connection, channel, queueName, exchange, routingKey, stopWatcher, totalMS);
                        break;
                    case MQSendType.Topics:
                        SendTopics(connection, channel, queueName, exchange, routingKey, stopWatcher, totalMS);
                        break;
                    case MQSendType.TopicsRoundRobin:
                        SendTopics(connection, channel, queueName, exchange, routingKey, stopWatcher, totalMS, true);
                        break;
                    case MQSendType.RPC:
                        SendHelloWorld(connection, channel, queueName, exchange, routingKey, stopWatcher, totalMS);
                        break;
                    default:
                        break;
                }
            }
        }

        protected void Reset()
        {
            var queueName = queueTextbox.Text;
            var exchangeName = exchangeTextBox.Text;
            var factory = new ConnectionFactory() { HostName = HostTextBox.Text, UserName = UsernameTextBox.Text, Password = PasswordTextBox.Text };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                if (!string.IsNullOrEmpty(queueName))
                {
                    channel.QueueDelete(queueName);
                }
                if (!string.IsNullOrEmpty(exchangeName))
                {
                    channel.ExchangeDelete(exchangeName);
                }
            }
        }

        #region Send Per Different Cases
        protected void SendHelloWorld(IConnection connection, IModel channel, string queueName, string exchange, string routingKey)
        {
            var body = Encoding.UTF8.GetBytes(MessageTextBox.Text);
            channel.QueueDeclare(queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            channel.BasicPublish(exchange: "",
                                 routingKey: queueName,
                                 basicProperties: null,
                                 body: body);
        }
        protected void SendHelloWorld(IConnection connection, IModel channel, string queueName, string exchange, string routingKey, Stopwatch stopWatcher, int totalMS)
        {
            var count = 0;
            stopWatcher.Start();

            var body = Encoding.UTF8.GetBytes(MessageTextBox.Text);
            channel.QueueDeclare(queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            while (stopWatcher.ElapsedMilliseconds < totalMS)
            {
                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: null,
                                     body: body);
                count++;
                CountLabel.Text = $"{count}";// $"{count} - {stopWatcher.Elapsed.ToString("HH:mm:ss")}";
            }
        }
        protected void SendWorkQueue(IConnection connection, IModel channel, string queueName, string exchange, string routingKey)
        {
            var body = Encoding.UTF8.GetBytes(MessageTextBox.Text);
            channel.QueueDeclare(queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: properties, body: body);
        }
        protected void SendWorkQueue(IConnection connection, IModel channel, string queueName, string exchange, string routingKey, Stopwatch stopWatcher, int totalMS)
        {
            var count = 0;
            stopWatcher.Start();

            var body = Encoding.UTF8.GetBytes(MessageTextBox.Text);
            channel.QueueDeclare(queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            while (stopWatcher.ElapsedMilliseconds < totalMS)
            {
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: properties, body: body);

                count++;
                CountLabel.Text = $"{count}";// $"{count} - {stopWatcher.Elapsed.ToString("HH:mm:ss")}";
            }
        }
        protected void SendPubSub(IConnection connection, IModel channel, string queueName, string exchange, string routingKey)
        {
            var body = Encoding.UTF8.GetBytes(MessageTextBox.Text);
            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);

            channel.BasicPublish(exchange: exchange,
                                 routingKey: "",
                                 basicProperties: null,
                                 body: body);
        }
        protected void SendPubSub(IConnection connection, IModel channel, string queueName, string exchange, string routingKey, Stopwatch stopWatcher, int totalMS)
        {
            var count = 0;
            stopWatcher.Start();

            var body = Encoding.UTF8.GetBytes(MessageTextBox.Text);
            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);

            while (stopWatcher.ElapsedMilliseconds < totalMS)
            {
                channel.BasicPublish(exchange: exchange,
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);

                count++;
                CountLabel.Text = $"{count}";// $"{count} - {stopWatcher.Elapsed.ToString("HH:mm:ss")}";
            }
        }
        protected void SendRouting(IConnection connection, IModel channel, string queueName, string exchange, string routingKey)
        {
            var body = Encoding.UTF8.GetBytes(MessageTextBox.Text);
            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Direct);

            channel.BasicPublish(exchange: exchange,
                                 routingKey: routingKey,
                                 basicProperties: null,
                                 body: body);
        }
        protected void SendRouting(IConnection connection, IModel channel, string queueName, string exchange, string routingKey, Stopwatch stopWatcher, int totalMS)
        {
            var count = 0;
            stopWatcher.Start();

            var body = Encoding.UTF8.GetBytes(MessageTextBox.Text);
            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Direct);

            while (stopWatcher.ElapsedMilliseconds < totalMS)
            {
                channel.BasicPublish(exchange: exchange,
                                     routingKey: routingKey,
                                     basicProperties: null,
                                     body: body);

                count++;
                CountLabel.Text = $"{count}";// $"{count} - {stopWatcher.Elapsed.ToString("HH:mm:ss")}";
            }
        }
        protected void SendTopics(IConnection connection, IModel channel, string queueName, string exchange, string routingKey, bool roundRobin = false)
        {
            var body = Encoding.UTF8.GetBytes(MessageTextBox.Text);
            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Topic, durable: roundRobin);

            var properties = channel.CreateBasicProperties();
            if (roundRobin)
                properties.Persistent = true;

            channel.BasicPublish(exchange: exchange,
                                 routingKey: routingKey,
                                 basicProperties: properties,
                                 body: body);
        }
        protected void SendTopics(IConnection connection, IModel channel, string queueName, string exchange, string routingKey, Stopwatch stopWatcher, int totalMS, bool roundRobin = false)
        {
            var count = 0;
            stopWatcher.Start();

            var body = Encoding.UTF8.GetBytes(MessageTextBox.Text);
            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Topic, durable: roundRobin);
            var properties = channel.CreateBasicProperties();
            if (roundRobin)
                properties.Persistent = true;

            while (stopWatcher.ElapsedMilliseconds < totalMS)
            {
                channel.BasicPublish(exchange: exchange,
                                     routingKey: routingKey,
                                     basicProperties: properties,
                                     body: body);

                count++;
                CountLabel.Text = $"{count}";// $"{count} - {stopWatcher.Elapsed.ToString("HH:mm:ss")}";
            }
        }
        #endregion
    }
}
