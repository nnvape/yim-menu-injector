namespace yimmenu_injector_headless
{
    partial class startgta
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
            label1 = new Label();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(25, 6);
            label1.Name = "label1";
            label1.Size = new Size(94, 21);
            label1.TabIndex = 0;
            label1.Text = "start GTA V?";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Click += label1_Click;
            // 
            // button1
            // 
            button1.Location = new Point(6, 30);
            button1.Name = "button1";
            button1.Size = new Size(57, 23);
            button1.TabIndex = 1;
            button1.Text = "steam";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(69, 30);
            button2.Name = "button2";
            button2.Size = new Size(84, 23);
            button2.TabIndex = 2;
            button2.Text = "epic games";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // startgta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(160, 57);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            Name = "startgta";
            Text = " ";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button button1;
        private Button button2;
    }
}