namespace Net4
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MessageInputBox = new TextBox();
            SendButton = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // MessageInputBox
            // 
            MessageInputBox.Location = new Point(165, 52);
            MessageInputBox.Name = "MessageInputBox";
            MessageInputBox.Size = new Size(100, 23);
            MessageInputBox.TabIndex = 0;
            // 
            // SendButton
            // 
            SendButton.Location = new Point(165, 153);
            SendButton.Name = "SendButton";
            SendButton.Size = new Size(100, 23);
            SendButton.TabIndex = 1;
            SendButton.Text = "Send";
            SendButton.UseVisualStyleBackColor = true;
            SendButton.Click += SendButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(37, 55);
            label1.Name = "label1";
            label1.Size = new Size(98, 15);
            label1.TabIndex = 2;
            label1.Text = "Spare Part Name:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(369, 209);
            Controls.Add(label1);
            Controls.Add(SendButton);
            Controls.Add(MessageInputBox);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox MessageInputBox;
        private Button SendButton;
        private Label label1;
    }
}
