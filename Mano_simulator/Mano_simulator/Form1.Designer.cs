namespace Mano_simulator
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.runButton = new System.Windows.Forms.Button();
            this.debug_button = new System.Windows.Forms.Button();
            this.stepoverButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.acLable = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.drLable = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pcLable = new System.Windows.Forms.Label();
            this.arLable = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.sbrLable = new System.Windows.Forms.Label();
            this.carLable = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(45, 45);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(239, 423);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(576, 12);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(96, 29);
            this.runButton.TabIndex = 1;
            this.runButton.Text = "Run";
            this.runButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.runButton.UseVisualStyleBackColor = true;
            // 
            // debug_button
            // 
            this.debug_button.Location = new System.Drawing.Point(678, 12);
            this.debug_button.Name = "debug_button";
            this.debug_button.Size = new System.Drawing.Size(94, 29);
            this.debug_button.TabIndex = 2;
            this.debug_button.Text = "Debug";
            this.debug_button.UseVisualStyleBackColor = true;
            // 
            // stepoverButton
            // 
            this.stepoverButton.Location = new System.Drawing.Point(476, 12);
            this.stepoverButton.Name = "stepoverButton";
            this.stepoverButton.Size = new System.Drawing.Size(94, 29);
            this.stepoverButton.TabIndex = 3;
            this.stepoverButton.Text = "Step Over";
            this.stepoverButton.UseVisualStyleBackColor = true;
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(376, 12);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(94, 29);
            this.stopButton.TabIndex = 4;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(438, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "AC: ";
            // 
            // acLable
            // 
            this.acLable.AutoSize = true;
            this.acLable.Location = new System.Drawing.Point(479, 93);
            this.acLable.Name = "acLable";
            this.acLable.Size = new System.Drawing.Size(137, 20);
            this.acLable.TabIndex = 6;
            this.acLable.Text = "0000000000000000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(438, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "DR:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // drLable
            // 
            this.drLable.AutoSize = true;
            this.drLable.Location = new System.Drawing.Point(479, 133);
            this.drLable.Name = "drLable";
            this.drLable.Size = new System.Drawing.Size(137, 20);
            this.drLable.TabIndex = 8;
            this.drLable.Text = "0000000000000000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(438, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "PC:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(438, 212);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "AR:";
            // 
            // pcLable
            // 
            this.pcLable.AutoSize = true;
            this.pcLable.Location = new System.Drawing.Point(479, 172);
            this.pcLable.Name = "pcLable";
            this.pcLable.Size = new System.Drawing.Size(89, 20);
            this.pcLable.TabIndex = 11;
            this.pcLable.Text = "0000000000";
            // 
            // arLable
            // 
            this.arLable.AutoSize = true;
            this.arLable.Location = new System.Drawing.Point(479, 212);
            this.arLable.Name = "arLable";
            this.arLable.Size = new System.Drawing.Size(89, 20);
            this.arLable.TabIndex = 12;
            this.arLable.Text = "0000000000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(435, 285);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "SBR:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(433, 251);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 20);
            this.label6.TabIndex = 14;
            this.label6.Text = "CAR:";
            // 
            // sbrLable
            // 
            this.sbrLable.AutoSize = true;
            this.sbrLable.Location = new System.Drawing.Point(479, 285);
            this.sbrLable.Name = "sbrLable";
            this.sbrLable.Size = new System.Drawing.Size(57, 20);
            this.sbrLable.TabIndex = 15;
            this.sbrLable.Text = "000000";
            // 
            // carLable
            // 
            this.carLable.AutoSize = true;
            this.carLable.Location = new System.Drawing.Point(479, 251);
            this.carLable.Name = "carLable";
            this.carLable.Size = new System.Drawing.Size(57, 20);
            this.carLable.TabIndex = 16;
            this.carLable.Text = "000000";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(877, 521);
            this.Controls.Add(this.carLable);
            this.Controls.Add(this.sbrLable);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.arLable);
            this.Controls.Add(this.pcLable);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.drLable);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.acLable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.stepoverButton);
            this.Controls.Add(this.debug_button);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RichTextBox richTextBox1;
        private Button runButton;
        private Button debug_button;
        private Button stepoverButton;
        private Button stopButton;
        private Label label1;
        private Label acLable;
        private Label label2;
        private Label drLable;
        private Label label3;
        private Label label4;
        private Label pcLable;
        private Label arLable;
        private Label label5;
        private Label label6;
        private Label sbrLable;
        private Label carLable;
    }
}