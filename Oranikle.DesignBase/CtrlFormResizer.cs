using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oranikle.Studio.Controls.Properties;

namespace Oranikle.Studio.Controls
{
    public class CtrlFormResizer : System.Windows.Forms.UserControl
    {

        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.PictureBox pictureBox1;
        private bool resizeMouseDown;
        private int resizemousex;
        private int resizemousey;

        public CtrlFormResizer()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            pictureBox1 = new System.Windows.Forms.PictureBox();
            SuspendLayout();
            pictureBox1.BackgroundImage = Resources.Icon_ResizeGrip_16;
            pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            pictureBox1.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            pictureBox1.Location = new System.Drawing.Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(16, 16);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(pictureBox1_MouseDown_1);
            pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(pictureBox1_MouseMove_1);
            pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(pictureBox1_MouseUp_1);
            AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            Controls.Add(pictureBox1);
            Name = "CtrlFormResizer";
            Size = new System.Drawing.Size(16, 16);
            ResumeLayout(false);
        }

        private void pictureBox1_MouseDown_1(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            resizeMouseDown = true;
            resizemousex = e.X;
            resizemousey = e.Y;
        }

        private void pictureBox1_MouseMove_1(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (ParentForm == null)
                return;
            if (resizeMouseDown)
            {
                int i1 = e.X - resizemousex + ParentForm.Width;
                int i2 = e.Y - resizemousey + ParentForm.Height;
                System.Drawing.Size size1 = ParentForm.MinimumSize;
                if (i1 < size1.Width)
                {
                    System.Drawing.Size size2 = ParentForm.MinimumSize;
                    i1 = size2.Width;
                }
                System.Drawing.Size size3 = ParentForm.MinimumSize;
                if (i2 < size3.Height)
                {
                    System.Drawing.Size size4 = ParentForm.MinimumSize;
                    i2 = size4.Height;
                }
                if ((ParentForm is Oranikle.Studio.Controls.ICustomResizeForm))
                {
                    ((Oranikle.Studio.Controls.ICustomResizeForm)ParentForm).CustomSetSize(i1, i2);
                    return;
                }
                ParentForm.Size = new System.Drawing.Size(i1, i2);
            }
        }

        private void pictureBox1_MouseUp_1(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            resizeMouseDown = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

    } 
}
