namespace TcpClient
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
            OrderLabel = new Label();
            OrderNameBox = new TextBox();
            NewOrderButton = new Button();
            EditOrderButton = new Button();
            RemoveOrderButton = new Button();
            OrderListBox = new ListBox();
            OrderListLabel = new Label();
            SuspendLayout();
            // 
            // OrderLabel
            // 
            OrderLabel.AllowDrop = true;
            OrderLabel.BorderStyle = BorderStyle.FixedSingle;
            OrderLabel.Location = new Point(117, 71);
            OrderLabel.Name = "OrderLabel";
            OrderLabel.Size = new Size(83, 23);
            OrderLabel.TabIndex = 0;
            OrderLabel.Text = "Order Name:";
            // 
            // OrderNameBox
            // 
            OrderNameBox.Location = new Point(206, 71);
            OrderNameBox.Name = "OrderNameBox";
            OrderNameBox.Size = new Size(218, 23);
            OrderNameBox.TabIndex = 1;
            // 
            // NewOrderButton
            // 
            NewOrderButton.Location = new Point(35, 352);
            NewOrderButton.Name = "NewOrderButton";
            NewOrderButton.Size = new Size(132, 52);
            NewOrderButton.TabIndex = 2;
            NewOrderButton.Text = "New Order";
            NewOrderButton.UseVisualStyleBackColor = true;
            NewOrderButton.Click += NewOrderButton_Click;
            // 
            // EditOrderButton
            // 
            EditOrderButton.Location = new Point(252, 352);
            EditOrderButton.Name = "EditOrderButton";
            EditOrderButton.Size = new Size(120, 52);
            EditOrderButton.TabIndex = 3;
            EditOrderButton.Text = "Edit Order";
            EditOrderButton.UseVisualStyleBackColor = true;
            EditOrderButton.Click += EditOrderButton_Click;
            // 
            // RemoveOrderButton
            // 
            RemoveOrderButton.Location = new Point(464, 352);
            RemoveOrderButton.Name = "RemoveOrderButton";
            RemoveOrderButton.Size = new Size(102, 52);
            RemoveOrderButton.TabIndex = 4;
            RemoveOrderButton.Text = "Remove Order";
            RemoveOrderButton.UseVisualStyleBackColor = true;
            RemoveOrderButton.Click += RemoveOrderButton_Click;
            // 
            // OrderListBox
            // 
            OrderListBox.FormattingEnabled = true;
            OrderListBox.ItemHeight = 15;
            OrderListBox.Location = new Point(117, 181);
            OrderListBox.Name = "OrderListBox";
            OrderListBox.Size = new Size(329, 94);
            OrderListBox.TabIndex = 5;
            // 
            // OrderListLabel
            // 
            OrderListLabel.AutoSize = true;
            OrderListLabel.Location = new Point(12, 181);
            OrderListLabel.Name = "OrderListLabel";
            OrderListLabel.Size = new Size(58, 15);
            OrderListLabel.TabIndex = 6;
            OrderListLabel.Text = "Order List";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(636, 470);
            Controls.Add(OrderListLabel);
            Controls.Add(OrderListBox);
            Controls.Add(RemoveOrderButton);
            Controls.Add(EditOrderButton);
            Controls.Add(NewOrderButton);
            Controls.Add(OrderNameBox);
            Controls.Add(OrderLabel);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label OrderLabel;
        private TextBox OrderNameBox;
        private Button NewOrderButton;
        private Button EditOrderButton;
        private Button RemoveOrderButton;
        private ListBox OrderListBox;
        private Label OrderListLabel;
    }
}
