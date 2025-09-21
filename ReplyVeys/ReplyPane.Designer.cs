namespace ReplyVeys
{
    partial class ReplyPane
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtDraft = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDraft
            // 
            this.txtDraft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDraft.Location = new System.Drawing.Point(0, 0);
            this.txtDraft.Multiline = true;
            this.txtDraft.Name = "txtDraft";
            this.txtDraft.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDraft.Size = new System.Drawing.Size(592, 455);
            this.txtDraft.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSend);
            this.panel1.Controls.Add(this.btnApply);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 407);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 48);
            this.panel1.TabIndex = 1;
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(13, 12);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(88, 33);
            this.btnApply.TabIndex = 0;
            this.btnApply.Text = "Uygula";
            this.btnApply.UseVisualStyleBackColor = true;
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(483, 12);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(88, 33);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Gönder!";
            this.btnSend.UseVisualStyleBackColor = true;
            // 
            // ReplyPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtDraft);
            this.Name = "ReplyPane";
            this.Size = new System.Drawing.Size(592, 455);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDraft;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnSend;
    }
}
