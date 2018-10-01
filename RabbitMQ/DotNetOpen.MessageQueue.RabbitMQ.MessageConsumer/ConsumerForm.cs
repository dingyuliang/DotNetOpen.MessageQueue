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
    public partial class ConsumerForm : Form
    {
        Stopwatch stopWatch;
        ConnectionFactory factory;
        IConnection connection;
        IModel channel;

        public ConsumerForm()
        {
            InitializeComponent();

            queueTextbox.Text = exchangeTextBox.Text = routingKeyTextbox.Text = "Hello";
        }
        
        protected override void OnClosed(EventArgs e)
        {
            Reset();
            base.OnClosed(e);

        }

        private void ConsumerForm_Load(object sender, EventArgs e)
        {
            stopWatch = new Stopwatch();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            Reset();

            var count = 0;
            factory = new ConnectionFactory() { HostName = HostTextBox.Text, UserName = UsernameTextBox.Text,Password = PasswordTextBox.Text };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();

            var queueName = queueTextbox.Text;
            var exchange = exchangeTextBox.Text;
            var routingKey = routingKeyTextbox.Text;

            channel.ExchangeDeclare(exchange, ExchangeType.Direct, false, false, null);
            channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            channel.QueueBind(queueName, exchange, routingKey, null);

            stopWatch.Start();
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

            stopWatch.Reset();
        }
    }
}
