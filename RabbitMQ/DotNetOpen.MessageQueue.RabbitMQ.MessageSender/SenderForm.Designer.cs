namespace DotNetOpen.MessageQueue.RabbitMQ.MessageSender
{
    partial class SenderForm
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
            this.MessageLabel = new System.Windows.Forms.Label();
            this.MessageTextBox = new System.Windows.Forms.TextBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.HostTextBox = new System.Windows.Forms.TextBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.UsernameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.RecursivelyButton = new System.Windows.Forms.Button();
            this.CountTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CountLabel = new System.Windows.Forms.Label();
            this.queueTextbox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.routingKeyTextbox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.exchangeTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // MessageLabel
            // 
            this.MessageLabel.AutoSize = true;
            this.MessageLabel.Location = new System.Drawing.Point(430, 132);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(102, 13);
            this.MessageLabel.TabIndex = 0;
            this.MessageLabel.Text = "Type your message:";
            // 
            // MessageTextBox
            // 
            this.MessageTextBox.Location = new System.Drawing.Point(15, 158);
            this.MessageTextBox.Multiline = true;
            this.MessageTextBox.Name = "MessageTextBox";
            this.MessageTextBox.Size = new System.Drawing.Size(538, 53);
            this.MessageTextBox.TabIndex = 8;
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(13, 220);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(75, 23);
            this.SendButton.TabIndex = 9;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Host:";
            // 
            // HostTextBox
            // 
            this.HostTextBox.Location = new System.Drawing.Point(77, 6);
            this.HostTextBox.Name = "HostTextBox";
            this.HostTextBox.Size = new System.Drawing.Size(476, 20);
            this.HostTextBox.TabIndex = 4;
            this.HostTextBox.Text = "localhost";
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(15, 127);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(75, 23);
            this.ConnectButton.TabIndex = 7;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(371, 42);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '*';
            this.PasswordTextBox.Size = new System.Drawing.Size(182, 20);
            this.PasswordTextBox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(305, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Password:";
            // 
            // UsernameTextBox
            // 
            this.UsernameTextBox.Location = new System.Drawing.Point(77, 42);
            this.UsernameTextBox.Name = "UsernameTextBox";
            this.UsernameTextBox.Size = new System.Drawing.Size(182, 20);
            this.UsernameTextBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "UserName:";
            // 
            // RecursivelyButton
            // 
            this.RecursivelyButton.Location = new System.Drawing.Point(443, 220);
            this.RecursivelyButton.Name = "RecursivelyButton";
            this.RecursivelyButton.Size = new System.Drawing.Size(110, 23);
            this.RecursivelyButton.TabIndex = 11;
            this.RecursivelyButton.Text = "Recursively Send";
            this.RecursivelyButton.UseVisualStyleBackColor = true;
            this.RecursivelyButton.Click += new System.EventHandler(this.RecursivelyButton_Click);
            // 
            // CountTextBox
            // 
            this.CountTextBox.Location = new System.Drawing.Point(323, 222);
            this.CountTextBox.Name = "CountTextBox";
            this.CountTextBox.Size = new System.Drawing.Size(100, 20);
            this.CountTextBox.TabIndex = 10;
            this.CountTextBox.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(267, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Minutes:";
            // 
            // CountLabel
            // 
            this.CountLabel.AutoSize = true;
            this.CountLabel.Location = new System.Drawing.Point(539, 132);
            this.CountLabel.Name = "CountLabel";
            this.CountLabel.Size = new System.Drawing.Size(13, 13);
            this.CountLabel.TabIndex = 20;
            this.CountLabel.Text = "0";
            // 
            // queueTextbox
            // 
            this.queueTextbox.Location = new System.Drawing.Point(77, 99);
            this.queueTextbox.Name = "queueTextbox";
            this.queueTextbox.Size = new System.Drawing.Size(182, 20);
            this.queueTextbox.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Queue:";
            // 
            // routingKeyTextbox
            // 
            this.routingKeyTextbox.Location = new System.Drawing.Point(371, 68);
            this.routingKeyTextbox.Name = "routingKeyTextbox";
            this.routingKeyTextbox.Size = new System.Drawing.Size(182, 20);
            this.routingKeyTextbox.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(305, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Routing Key:";
            // 
            // exchangeTextBox
            // 
            this.exchangeTextBox.Location = new System.Drawing.Point(77, 68);
            this.exchangeTextBox.Name = "exchangeTextBox";
            this.exchangeTextBox.Size = new System.Drawing.Size(182, 20);
            this.exchangeTextBox.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Exchange:";
            // 
            // SenderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 261);
            this.Controls.Add(this.queueTextbox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.routingKeyTextbox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.exchangeTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.CountLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CountTextBox);
            this.Controls.Add(this.RecursivelyButton);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.UsernameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.HostTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.MessageTextBox);
            this.Controls.Add(this.MessageLabel);
            this.Name = "SenderForm";
            this.Text = "Sender Form";
            this.Load += new System.EventHandler(this.SenderForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.TextBox MessageTextBox;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox HostTextBox;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox UsernameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button RecursivelyButton;
        private System.Windows.Forms.TextBox CountTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label CountLabel;
        private System.Windows.Forms.TextBox queueTextbox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox routingKeyTextbox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox exchangeTextBox;
        private System.Windows.Forms.Label label7;
    }
}

