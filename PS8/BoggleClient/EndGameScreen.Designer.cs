namespace BoggleClient
{
    partial class EndGameScreen
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
            this.P2ScoretextBox = new System.Windows.Forms.TextBox();
            this.P1ScoretextBox = new System.Windows.Forms.TextBox();
            this.Player2textBox = new System.Windows.Forms.TextBox();
            this.Player1textBox = new System.Windows.Forms.TextBox();
            this.Player2ScoretextBox = new System.Windows.Forms.TextBox();
            this.Player1ScoretextBox = new System.Windows.Forms.TextBox();
            this.Player2label = new System.Windows.Forms.Label();
            this.Player1label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // P2ScoretextBox
            // 
            this.P2ScoretextBox.Location = new System.Drawing.Point(294, 288);
            this.P2ScoretextBox.Multiline = true;
            this.P2ScoretextBox.Name = "P2ScoretextBox";
            this.P2ScoretextBox.Size = new System.Drawing.Size(153, 46);
            this.P2ScoretextBox.TabIndex = 17;
            // 
            // P1ScoretextBox
            // 
            this.P1ScoretextBox.Location = new System.Drawing.Point(50, 288);
            this.P1ScoretextBox.Multiline = true;
            this.P1ScoretextBox.Name = "P1ScoretextBox";
            this.P1ScoretextBox.Size = new System.Drawing.Size(153, 46);
            this.P1ScoretextBox.TabIndex = 16;
            // 
            // Player2textBox
            // 
            this.Player2textBox.Enabled = false;
            this.Player2textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Player2textBox.Location = new System.Drawing.Point(365, 9);
            this.Player2textBox.Name = "Player2textBox";
            this.Player2textBox.Size = new System.Drawing.Size(82, 29);
            this.Player2textBox.TabIndex = 15;
            // 
            // Player1textBox
            // 
            this.Player1textBox.Enabled = false;
            this.Player1textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Player1textBox.Location = new System.Drawing.Point(121, 9);
            this.Player1textBox.Name = "Player1textBox";
            this.Player1textBox.Size = new System.Drawing.Size(82, 29);
            this.Player1textBox.TabIndex = 14;
            // 
            // Player2ScoretextBox
            // 
            this.Player2ScoretextBox.Enabled = false;
            this.Player2ScoretextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Player2ScoretextBox.Location = new System.Drawing.Point(294, 59);
            this.Player2ScoretextBox.Multiline = true;
            this.Player2ScoretextBox.Name = "Player2ScoretextBox";
            this.Player2ScoretextBox.Size = new System.Drawing.Size(153, 199);
            this.Player2ScoretextBox.TabIndex = 13;
            // 
            // Player1ScoretextBox
            // 
            this.Player1ScoretextBox.Enabled = false;
            this.Player1ScoretextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Player1ScoretextBox.Location = new System.Drawing.Point(50, 59);
            this.Player1ScoretextBox.Multiline = true;
            this.Player1ScoretextBox.Name = "Player1ScoretextBox";
            this.Player1ScoretextBox.Size = new System.Drawing.Size(153, 199);
            this.Player1ScoretextBox.TabIndex = 12;
            // 
            // Player2label
            // 
            this.Player2label.AutoSize = true;
            this.Player2label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Player2label.Location = new System.Drawing.Point(290, 9);
            this.Player2label.Name = "Player2label";
            this.Player2label.Size = new System.Drawing.Size(69, 20);
            this.Player2label.TabIndex = 11;
            this.Player2label.Text = "Player 2:";
            // 
            // Player1label
            // 
            this.Player1label.AutoSize = true;
            this.Player1label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Player1label.Location = new System.Drawing.Point(46, 9);
            this.Player1label.Name = "Player1label";
            this.Player1label.Size = new System.Drawing.Size(69, 20);
            this.Player1label.TabIndex = 10;
            this.Player1label.Text = "Player 1:";
            // 
            // EndGameScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 354);
            this.Controls.Add(this.P2ScoretextBox);
            this.Controls.Add(this.P1ScoretextBox);
            this.Controls.Add(this.Player2textBox);
            this.Controls.Add(this.Player1textBox);
            this.Controls.Add(this.Player2ScoretextBox);
            this.Controls.Add(this.Player1ScoretextBox);
            this.Controls.Add(this.Player2label);
            this.Controls.Add(this.Player1label);
            this.Name = "EndGameScreen";
            this.Text = "End Game Screen";
            this.Load += new System.EventHandler(this.EndGameScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox P2ScoretextBox;
        private System.Windows.Forms.TextBox P1ScoretextBox;
        private System.Windows.Forms.TextBox Player2textBox;
        private System.Windows.Forms.TextBox Player1textBox;
        private System.Windows.Forms.TextBox Player2ScoretextBox;
        private System.Windows.Forms.TextBox Player1ScoretextBox;
        private System.Windows.Forms.Label Player2label;
        private System.Windows.Forms.Label Player1label;
    }
}