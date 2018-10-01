namespace DotNetOpen.MessageQueue.RabbitMQ.MessageConsumer
{
    partial class ConsumerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MessageTextBox = new System.Windows.Forms.TextBox();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.HostTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.UsernameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ClearButton = new System.Windows.Forms.Button();
            this.ReceivedCountLabel = new System.Windows.Forms.Label();
            this.exchangeTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.routingKeyTextbox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.queueTextbox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // MessageTextBox
            // 
            this.MessageTextBox.Location = new System.Drawing.Point(15, 171);
            this.MessageTextBox.Multiline = true;
            this.MessageTextBox.Name = "MessageTextBox";
            this.MessageTextBox.Size = new System.Drawing.Size(538, 161);
            this.MessageTextBox.TabIndex = 3;
            // 
            // MessageLabel
            // 
            this.MessageLabel.AutoSize = true;
            this.MessageLabel.Location = new System.Drawing.Point(305, 147);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(107, 13);
            this.MessageLabel.TabIndex = 2;
            this.MessageLabel.Text = "Received Messages:";
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(13, 142);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(87, 23);
            this.ConnectButton.TabIndex = 10;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // HostTextBox
            // 
            this.HostTextBox.Location = new System.Drawing.Point(77, 12);
            this.HostTextBox.Name = "HostTextBox";
            this.HostTextBox.Size = new System.Drawing.Size(476, 20);
            this.HostTextBox.TabIndex = 7;
            this.HostTextBox.Text = "localhost";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Host:";
            // 
            // UsernameTextBox
            // 
            this.UsernameTextBox.Location = new System.Drawing.Point(77, 47);
            this.UsernameTextBox.Name = "UsernameTextBox";
            this.UsernameTextBox.Size = new System.Drawing.Size(182, 20);
            this.UsernameTextBox.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "UserName:";
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(371, 47);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '*';
            this.PasswordTextBox.Size = new System.Drawing.Size(182, 20);
            this.PasswordTextBox.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(305, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Password:";
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(180, 142);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(79, 23);
            this.ClearButton.TabIndex = 11;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // ReceivedCountLabel
            // 
            this.ReceivedCountLabel.AutoSize = true;
            this.ReceivedCountLabel.Location = new System.Drawing.Point(418, 147);
            this.ReceivedCountLabel.Name = "ReceivedCountLabel";
            this.ReceivedCountLabel.Size = new System.Drawing.Size(13, 13);
            this.ReceivedCountLabel.TabIndex = 14;
            this.ReceivedCountLabel.Text = "0";
            // 
            // exchangeTextBox
            // 
            this.exchangeTextBox.Location = new System.Drawing.Point(77, 80);
            this.exchangeTextBox.Name = "exchangeTextBox";
            this.exchangeTextBox.Size = new System.Drawing.Size(182, 20);
            this.exchangeTextBox.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Exchange:";
            // 
            // routingKeyTextbox
            // 
            this.routingKeyTextbox.Location = new System.Drawing.Point(371, 80);
            this.routingKeyTextbox.Name = "routingKeyTextbox";
            this.routingKeyTextbox.Size = new System.Drawing.Size(182, 20);
            this.routingKeyTextbox.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(305, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Routing Key:";
            // 
            // queueTextbox
            // 
            this.queueTextbox.Location = new System.Drawing.Point(77, 111);
            this.queueTextbox.Name = "queueTextbox";
            this.queueTextbox.Size = new System.Drawing.Size(182, 20);
            this.queueTextbox.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Queue:";
            // 
            // ConsumerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 347);
            this.Controls.Add(this.queueTextbox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.routingKeyTextbox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.exchangeTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ReceivedCountLabel);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.UsernameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.HostTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MessageTextBox);
            this.Controls.Add(this.MessageLabel);
            this.Name = "ConsumerForm";
            this.Text = "Consumer Form";
            this.Load += new System.EventHandler(this.ConsumerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox MessageTextBox;
        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.TextBox HostTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox UsernameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Label ReceivedCountLabel;
        private System.Windows.Forms.TextBox exchangeTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox routingKeyTextbox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox queueTextbox;
        private System.Windows.Forms.Label label6;
    }
}

