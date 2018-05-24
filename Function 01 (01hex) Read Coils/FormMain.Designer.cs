namespace Function_01__01hex__Read_Coils
{
    partial class FormMain
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
            this.btnRequestMsg = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRequestMsg = new System.Windows.Forms.TextBox();
            this.txtReceivedMsg = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnRequestMsg
            // 
            this.btnRequestMsg.Location = new System.Drawing.Point(207, 145);
            this.btnRequestMsg.Name = "btnRequestMsg";
            this.btnRequestMsg.Size = new System.Drawing.Size(142, 40);
            this.btnRequestMsg.TabIndex = 0;
            this.btnRequestMsg.Text = "Request";
            this.btnRequestMsg.UseVisualStyleBackColor = true;
            this.btnRequestMsg.Click += new System.EventHandler(this.btnSendMsg_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "RequestMsg:";
            // 
            // txtRequestMsg
            // 
            this.txtRequestMsg.Location = new System.Drawing.Point(207, 30);
            this.txtRequestMsg.Name = "txtRequestMsg";
            this.txtRequestMsg.Size = new System.Drawing.Size(396, 30);
            this.txtRequestMsg.TabIndex = 2;
            // 
            // txtReceivedMsg
            // 
            this.txtReceivedMsg.Location = new System.Drawing.Point(207, 82);
            this.txtReceivedMsg.Name = "txtReceivedMsg";
            this.txtReceivedMsg.Size = new System.Drawing.Size(396, 30);
            this.txtReceivedMsg.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "ReceivedMsg:";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 458);
            this.Controls.Add(this.txtReceivedMsg);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtRequestMsg);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRequestMsg);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Function 01 (01hex) Read Coils";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRequestMsg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRequestMsg;
        private System.Windows.Forms.TextBox txtReceivedMsg;
        private System.Windows.Forms.Label label2;
    }
}

