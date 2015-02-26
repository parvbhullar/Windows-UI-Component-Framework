using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Oranikle.Studio.Controls
{
    [System.ComponentModel.DefaultBindingProperty("Checked")]
    public class StyledCheckBox : System.Windows.Forms.CheckBox
    {

        protected Oranikle.Studio.Controls.BorderDrawer borderDrawer;
        private System.Nullable<System.Drawing.Color> originalBackColor;

        public override System.Drawing.Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = System.Drawing.Color.Black;
            }
        }

        public StyledCheckBox()
        {
            borderDrawer = new Oranikle.Studio.Controls.BorderDrawer();
            FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            base.ForeColor = System.Drawing.Color.Black;
            borderDrawer.RegisterFocusHoverControl(this);
        }

        protected override void OnBackColorChanged(System.EventArgs e)
        {
            base.OnBackColorChanged(e);
            if (!originalBackColor.HasValue)
                originalBackColor = new System.Nullable<System.Drawing.Color>(BackColor);
        }

        protected override void OnCheckedChanged(System.EventArgs e)
        {
            foreach (System.Windows.Forms.Binding binding in DataBindings)
            {
                if (binding.PropertyName == "Checked" && (binding.DataSourceUpdateMode == System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
                    binding.WriteValue();
            }
            base.OnCheckedChanged(e);
        }

        protected override void OnEnabledChanged(System.EventArgs e)
        {
            base.OnEnabledChanged(e);
            if (!Enabled)
            {
                if (!originalBackColor.HasValue)
                    originalBackColor = new System.Nullable<System.Drawing.Color>(BackColor);
                BackColor = System.Drawing.SystemColors.Control;
            }
            else
            {
                System.Nullable<System.Drawing.Color> nullable = originalBackColor;
                BackColor = nullable.HasValue ? nullable.GetValueOrDefault() : System.Drawing.Color.White;
            }
            Refresh();
        }

        protected override void OnGotFocus(System.EventArgs e)
        {
            base.OnGotFocus(e);
            if (Enabled)
            {
                if (!originalBackColor.HasValue)
                    originalBackColor = new System.Nullable<System.Drawing.Color>(BackColor);
                BackColor = System.Drawing.SystemColors.Highlight;
            }
            Refresh();
        }

        protected override void OnLostFocus(System.EventArgs e)
        {
            base.OnLostFocus(e);
            if (BackColor == System.Drawing.SystemColors.Highlight)
            {
                System.Nullable<System.Drawing.Color> nullable = originalBackColor;
                BackColor = nullable.HasValue ? nullable.GetValueOrDefault() : System.Drawing.Color.White;
            }
            Refresh();
        }

        protected override void OnSizeChanged(System.EventArgs e)
        {
            base.OnSizeChanged(e);
            System.Drawing.Size size2 = Size;
            if (size2.Width == 12)
            {
                System.Drawing.Size size3 = Size;
                if (size3.Height == 11)
                {
                    System.Drawing.Size size1 = Size;
                    size1.Width = 11;
                    Size = size1;
                }
            }
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message m, System.Windows.Forms.Keys k)
        {
            if ((m.Msg == 256) && (k == System.Windows.Forms.Keys.Enter))
            {
                System.Windows.Forms.Form form = FindForm();
                if ((form == null) || (form.AcceptButton == null))
                {
                    System.Windows.Forms.SendKeys.Send("\t");
                    return true;
                }
            }
            return base.ProcessCmdKey(ref m, k);
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);
            if ((this.FindForm() != null && this.FindForm().Font.Size >= 9) || (this.Parent != null && Parent.Font.Size >= 9))
            {
                if ((Width <= 12) && (Height <= 12))
                {
                    borderDrawer.DrawBorder(ref m, 11, 11);
                    return;
                }
                borderDrawer.DrawBorder(ref m, 0, 3, 11, 11);
            }
            else
            {
                if ((Width <= 11) && (Height <= 11))
                {
                    borderDrawer.DrawBorder(ref m, 0, 6, 11, 11);
                    return;
                }
                borderDrawer.DrawBorder(ref m, 0, 6, 11, 11);
            }
        }

    } 
}
