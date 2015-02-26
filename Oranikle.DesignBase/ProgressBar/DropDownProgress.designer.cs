namespace Oranikle.Studio.Controls.ProgressBar
{
    partial class ProgressBar
    {
        partial class DropDownProgress
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
                if (disposing)
                {
                    if (components != null)
                        components.Dispose();

                    foreach (System.Windows.Forms.Control control in Controls)
                        control.Dispose();
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
                this.redTrackBar = new System.Windows.Forms.TrackBar();
                this.greenTrackBar = new System.Windows.Forms.TrackBar();
                this.blueTrackBar = new System.Windows.Forms.TrackBar();
                this.alphaTrackBar = new System.Windows.Forms.TrackBar();
                this.closeDialogBtn = new System.Windows.Forms.Button();
                this.label1 = new System.Windows.Forms.Label();
                this.label2 = new System.Windows.Forms.Label();
                this.label3 = new System.Windows.Forms.Label();
                this.label4 = new System.Windows.Forms.Label();
                ((System.ComponentModel.ISupportInitialize)(this.redTrackBar)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.greenTrackBar)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.blueTrackBar)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.alphaTrackBar)).BeginInit();
                this.SuspendLayout();
                // 
                // redTrackBar
                // 
                this.redTrackBar.Location = new System.Drawing.Point(29, 27);
                this.redTrackBar.Maximum = 255;
                this.redTrackBar.Name = "redTrackBar";
                this.redTrackBar.Size = new System.Drawing.Size(288, 45);
                this.redTrackBar.TabIndex = 1;
                this.redTrackBar.TickFrequency = 15;
                this.redTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
                this.redTrackBar.ValueChanged += new System.EventHandler(this.TRACKBAR_VALUE_CHANGED);
                // 
                // greenTrackBar
                // 
                this.greenTrackBar.Location = new System.Drawing.Point(29, 78);
                this.greenTrackBar.Maximum = 255;
                this.greenTrackBar.Name = "greenTrackBar";
                this.greenTrackBar.Size = new System.Drawing.Size(288, 45);
                this.greenTrackBar.TabIndex = 2;
                this.greenTrackBar.TickFrequency = 15;
                this.greenTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
                this.greenTrackBar.ValueChanged += new System.EventHandler(this.TRACKBAR_VALUE_CHANGED);
                // 
                // blueTrackBar
                // 
                this.blueTrackBar.Location = new System.Drawing.Point(29, 129);
                this.blueTrackBar.Maximum = 255;
                this.blueTrackBar.Name = "blueTrackBar";
                this.blueTrackBar.Size = new System.Drawing.Size(288, 45);
                this.blueTrackBar.TabIndex = 3;
                this.blueTrackBar.TickFrequency = 15;
                this.blueTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
                this.blueTrackBar.ValueChanged += new System.EventHandler(this.TRACKBAR_VALUE_CHANGED);
                // 
                // alphaTrackBar
                // 
                this.alphaTrackBar.Location = new System.Drawing.Point(29, 180);
                this.alphaTrackBar.Maximum = 255;
                this.alphaTrackBar.Minimum = 50;
                this.alphaTrackBar.Name = "alphaTrackBar";
                this.alphaTrackBar.Size = new System.Drawing.Size(288, 45);
                this.alphaTrackBar.TabIndex = 4;
                this.alphaTrackBar.TickFrequency = 15;
                this.alphaTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
                this.alphaTrackBar.Value = 50;
                this.alphaTrackBar.ValueChanged += new System.EventHandler(this.TRACKBAR_VALUE_CHANGED);
                // 
                // closeDialogBtn
                // 
                this.closeDialogBtn.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                this.closeDialogBtn.Location = new System.Drawing.Point(121, 230);
                this.closeDialogBtn.Name = "closeDialogBtn";
                this.closeDialogBtn.Size = new System.Drawing.Size(105, 23);
                this.closeDialogBtn.TabIndex = 8;
                this.closeDialogBtn.Text = "Close Dialog";
                this.closeDialogBtn.UseVisualStyleBackColor = true;
                this.closeDialogBtn.Click += new System.EventHandler(this.closeDialogBtn_Click);
                // 
                // label1
                // 
                this.label1.AutoSize = true;
                this.label1.ForeColor = System.Drawing.Color.Red;
                this.label1.Location = new System.Drawing.Point(32, 11);
                this.label1.Name = "label1";
                this.label1.Size = new System.Drawing.Size(30, 13);
                this.label1.TabIndex = 9;
                this.label1.Text = "Red:";
                // 
                // label2
                // 
                this.label2.AutoSize = true;
                this.label2.ForeColor = System.Drawing.Color.Green;
                this.label2.Location = new System.Drawing.Point(32, 62);
                this.label2.Name = "label2";
                this.label2.Size = new System.Drawing.Size(40, 13);
                this.label2.TabIndex = 10;
                this.label2.Text = "Green:";
                // 
                // label3
                // 
                this.label3.AutoSize = true;
                this.label3.ForeColor = System.Drawing.Color.Blue;
                this.label3.Location = new System.Drawing.Point(32, 113);
                this.label3.Name = "label3";
                this.label3.Size = new System.Drawing.Size(31, 13);
                this.label3.TabIndex = 11;
                this.label3.Text = "Blue:";
                // 
                // label4
                // 
                this.label4.AutoSize = true;
                this.label4.ForeColor = System.Drawing.Color.Black;
                this.label4.Location = new System.Drawing.Point(32, 164);
                this.label4.Name = "label4";
                this.label4.Size = new System.Drawing.Size(38, 13);
                this.label4.TabIndex = 12;
                this.label4.Text = "Alpha:";
                // 
                // DropDownProgress
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.Controls.Add(this.label4);
                this.Controls.Add(this.label3);
                this.Controls.Add(this.label2);
                this.Controls.Add(this.label1);
                this.Controls.Add(this.closeDialogBtn);
                this.Controls.Add(this.alphaTrackBar);
                this.Controls.Add(this.blueTrackBar);
                this.Controls.Add(this.greenTrackBar);
                this.Controls.Add(this.redTrackBar);
                this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                this.Name = "DropDownProgress";
                this.Padding = new System.Windows.Forms.Padding(4);
                this.Size = new System.Drawing.Size(347, 261);
                this.Load += new System.EventHandler(this.DropDownProgress_Load);
                ((System.ComponentModel.ISupportInitialize)(this.redTrackBar)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.greenTrackBar)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.blueTrackBar)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.alphaTrackBar)).EndInit();
                this.ResumeLayout(false);
                this.PerformLayout();

            }

            #endregion

            private System.Windows.Forms.TrackBar redTrackBar;
            private System.Windows.Forms.TrackBar greenTrackBar;
            private System.Windows.Forms.TrackBar blueTrackBar;
            private System.Windows.Forms.TrackBar alphaTrackBar;
            private System.Windows.Forms.Button closeDialogBtn;
            private System.Windows.Forms.Label label1;
            private System.Windows.Forms.Label label2;
            private System.Windows.Forms.Label label3;
            private System.Windows.Forms.Label label4;
        }
    }
}