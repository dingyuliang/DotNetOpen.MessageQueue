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
    public partial class SenderForm : Form
    { 
        public SenderForm()
        {
            InitializeComponent();

            queueTextbox.Text = exchangeTextBox.Text = routingKeyTextbox.Text = "Hello";
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
                channel.QueueDeclare(queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(MessageTextBox.Text);
                channel.BasicPublish(exchange: exchange, routingKey: routingKey, basicProperties: null, body: body);
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
                }
                MessageBox.Show("Connected");
            }
            catch
            {
                MessageBox.Show("Failed");
            }
        }

        private void RecursivelyButton_Click(object sender, EventArgs e)
        {
            var count = 0;
            var stopWatcher = new Stopwatch();
            var totalMS = int.Parse(CountTextBox.Text) * 60 * 1000;
            var factory = new ConnectionFactory() { HostName = HostTextBox.Text, UserName = UsernameTextBox.Text, Password = PasswordTextBox.Text };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                stopWatcher.Start();
                channel.QueueDeclare("Hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                while (stopWatcher.ElapsedMilliseconds < totalMS)
                {
                    var body = Encoding.UTF8.GetBytes(MessageTextBox.Text);
                    channel.BasicPublish(exchange: "", routingKey: "Hello", basicProperties: null, body: body);
                    count++;
                    CountLabel.Text = $"{count}";// $"{count} - {stopWatcher.Elapsed.ToString("HH:mm:ss")}";
                }
            }
        }
    }
}
