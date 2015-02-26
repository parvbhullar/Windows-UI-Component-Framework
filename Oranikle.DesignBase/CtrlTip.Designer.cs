namespace Oranikle.Studio.Controls
{
    partial class CtrlTip
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
            this.lblText = new System.Windows.Forms.Label();
            this.ctrlUIBase = new Oranikle.Studio.Controls.CtrlStyledPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ctrlUIBase)).BeginInit();
            this.SuspendLayout();
            // 
            // lblText
            // 
            this.lblText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblText.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblText.Location = new System.Drawing.Point(3, 1);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(133, 13);
            this.lblText.TabIndex = 2;
            this.lblText.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ctrlUIBase
            // 
            this.ctrlUIBase.BorderColor = System.Drawing.Color.SkyBlue;
            this.ctrlUIBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlUIBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlUIBase.Location = new System.Drawing.Point(0, 0);
            this.ctrlUIBase.Name = "ctrlUIBase";
            this.ctrlUIBase.Size = new System.Drawing.Size(150, 21);
            this.ctrlUIBase.TabIndex = 0;
            this.ctrlUIBase.TabStop = false;
            // 
            // CtrlTip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.ctrlUIBase);
            this.Name = "CtrlTip";
            this.Size = new System.Drawing.Size(150, 21);
            ((System.ComponentModel.ISupportInitialize)(this.ctrlUIBase)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CtrlStyledPictureBox ctrlUIBase;
        private System.Windows.Forms.Label lblText;
    }
}
