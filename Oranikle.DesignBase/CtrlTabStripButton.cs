using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Oranikle.Studio.Controls
{

    [System.Windows.Forms.Design.ToolStripItemDesignerAvailability(System.Windows.Forms.Design.ToolStripItemDesignerAvailability.ToolStrip)]
    public class CtrlTabStripButton : System.Windows.Forms.ToolStripButton
    {

        private System.Drawing.Color m_BackColor2;
        private System.Drawing.Color m_BackColor2Hot;
        private System.Drawing.Color m_BackColor2Inactive;
        private System.Drawing.Color m_BackColorHot;
        private System.Drawing.Color m_BackColorInactive;
        private System.Drawing.Color m_BorderColor;
        private System.Drawing.Color m_BorderColorHot;
        private System.Drawing.Color m_BorderColorInactive;
        private int m_CloseButtonHorizontalOffset;
        private int m_CloseButtonVerticalOffset;
        private System.Drawing.Color m_TextColor;
        private System.Drawing.Color m_TextColorHot;
        private System.Drawing.Color m_TextColorInactive;
        private int m_VerticalOffset;
        private int m_VerticalOffsetHot;
        private int m_VerticalOffsetInactive;
        private bool showCloseButton;

        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.Description("Active Border color of the TabButton")]
        public System.Drawing.Color BackColor2
        {
            get
            {
                return m_BackColor2;
            }
            set
            {
                m_BackColor2 = value;
                Invalidate();
            }
        }

        [System.ComponentModel.Description("Border color when TabButton is highlighted")]
        [System.ComponentModel.Category("Appearance")]
        public System.Drawing.Color BackColor2Hot
        {
            get
            {
                return m_BackColor2Hot;
            }
            set
            {
                m_BackColor2Hot = value;
                Invalidate();
            }
        }

        [System.ComponentModel.Description("Inactive Border color of the TabButton")]
        [System.ComponentModel.Category("Appearance")]
        public System.Drawing.Color BackColor2Inactive
        {
            get
            {
                return m_BackColor2Inactive;
            }
            set
            {
                m_BackColor2Inactive = value;
                Invalidate();
            }
        }

        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.Description("Back color when TabButton is highlighted")]
        public System.Drawing.Color BackColorHot
        {
            get
            {
                return m_BackColorHot;
            }
            set
            {
                m_BackColorHot = value;
                Invalidate();
            }
        }

        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.Description("Inactive Back color of the TabButton")]
        public System.Drawing.Color BackColorInactive
        {
            get
            {
                return m_BackColorInactive;
            }
            set
            {
                m_BackColorInactive = value;
                Invalidate();
            }
        }

        [System.ComponentModel.Description("Active Border color of the TabButton")]
        [System.ComponentModel.Category("Appearance")]
        public System.Drawing.Color BorderColor
        {
            get
            {
                return m_BorderColor;
            }
            set
            {
                m_BorderColor = value;
                Invalidate();
            }
        }

        [System.ComponentModel.Description("Border color when TabButton is highlighted")]
        [System.ComponentModel.Category("Appearance")]
        public System.Drawing.Color BorderColorHot
        {
            get
            {
                return m_BorderColorHot;
            }
            set
            {
                m_BorderColorHot = value;
                Invalidate();
            }
        }

        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.Description("Inactive Border color of the TabButton")]
        public System.Drawing.Color BorderColorInactive
        {
            get
            {
                return m_BorderColorInactive;
            }
            set
            {
                m_BorderColorInactive = value;
                Invalidate();
            }
        }

        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Browsable(false)]
        public new bool Checked
        {
            get
            {
                return IsSelected;
            }
            set
            {
            }
        }

        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.Description("Text color when TabButton is highlighted")]
        public int CloseButtonHorizontalOffset
        {
            get
            {
                return m_CloseButtonHorizontalOffset;
            }
            set
            {
                m_CloseButtonHorizontalOffset = value;
                Invalidate();
            }
        }

        [System.ComponentModel.Description("Text color when TabButton is highlighted")]
        [System.ComponentModel.Category("Appearance")]
        public int CloseButtonVerticalOffset
        {
            get
            {
                return m_CloseButtonVerticalOffset;
            }
            set
            {
                m_CloseButtonVerticalOffset = value;
                Invalidate();
            }
        }

        [System.ComponentModel.Browsable(false)]
        public bool IsSelected
        {
            get
            {
                Oranikle.Studio.Controls.CtrlTabStrip ctrlTabStrip = Owner as Oranikle.Studio.Controls.CtrlTabStrip;
                if (ctrlTabStrip != null)
                    return this == ctrlTabStrip.SelectedTab;
                return false;
            }
            set
            {
                if (!value)
                    return;
                Oranikle.Studio.Controls.CtrlTabStrip ctrlTabStrip = Owner as Oranikle.Studio.Controls.CtrlTabStrip;
                if (ctrlTabStrip == null)
                    return;
                ctrlTabStrip.SelectedTab = this;
            }
        }

        [System.ComponentModel.Browsable(false)]
        public new System.Windows.Forms.Padding Margin
        {
            get
            {
                return base.Margin;
            }
            set
            {
            }
        }

        [System.ComponentModel.Browsable(false)]
        public new System.Windows.Forms.Padding Padding
        {
            get
            {
                return base.Padding;
            }
            set
            {
            }
        }

        [System.ComponentModel.Description("Show button or not to close the tab.")]
        [System.ComponentModel.Category("Appearance")]
        public bool ShowCloseButton
        {
            get
            {
                return showCloseButton;
            }
            set
            {
                showCloseButton = value;
                Invalidate();
            }
        }

        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.Description("Active Text color of the TabButton")]
        public System.Drawing.Color TextColor
        {
            get
            {
                return m_TextColor;
            }
            set
            {
                m_TextColor = value;
                Invalidate();
            }
        }

        [System.ComponentModel.Description("Text color when TabButton is highlighted")]
        [System.ComponentModel.Category("Appearance")]
        public System.Drawing.Color TextColorHot
        {
            get
            {
                return m_TextColorHot;
            }
            set
            {
                m_TextColorHot = value;
                Invalidate();
            }
        }

        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.Description("Inactive Text color of the TabButton")]
        public System.Drawing.Color TextColorInactive
        {
            get
            {
                return m_TextColorInactive;
            }
            set
            {
                m_TextColorInactive = value;
                Invalidate();
            }
        }

        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.Description("Active Text color of the TabButton")]
        public int VerticalOffset
        {
            get
            {
                return m_VerticalOffset;
            }
            set
            {
                m_VerticalOffset = value;
                Invalidate();
            }
        }

        [System.ComponentModel.Description("Text color when TabButton is highlighted")]
        [System.ComponentModel.Category("Appearance")]
        public int VerticalOffsetHot
        {
            get
            {
                return m_VerticalOffsetHot;
            }
            set
            {
                m_VerticalOffsetHot = value;
                Invalidate();
            }
        }

        [System.ComponentModel.Description("Inactive Text color of the TabButton")]
        [System.ComponentModel.Category("Appearance")]
        public int VerticalOffsetInactive
        {
            get
            {
                return m_VerticalOffsetInactive;
            }
            set
            {
                m_VerticalOffsetInactive = value;
                Invalidate();
            }
        }

        protected override System.Windows.Forms.Padding DefaultMargin
        {
            get
            {
                return new System.Windows.Forms.Padding(0);
            }
        }

        public CtrlTabStripButton()
        {
            m_BorderColor = System.Drawing.Color.Gray;
            m_BorderColorInactive = System.Drawing.Color.Gray;
            m_BorderColorHot = System.Drawing.Color.Orange;
            m_BackColor2 = System.Drawing.Color.Gray;
            m_BackColor2Inactive = System.Drawing.Color.Gray;
            m_BackColor2Hot = System.Drawing.Color.Orange;
            m_BackColorInactive = System.Drawing.Color.Gray;
            m_BackColorHot = System.Drawing.Color.Orange;
            m_TextColor = System.Drawing.Color.Gray;
            m_TextColorInactive = System.Drawing.Color.Gray;
            m_TextColorHot = System.Drawing.Color.Orange;
            m_VerticalOffset = 1;
            m_VerticalOffsetInactive = 3;
            m_VerticalOffsetHot = 1;
            showCloseButton = true;
            m_CloseButtonVerticalOffset = 12;
            m_CloseButtonHorizontalOffset = 16;
            InitButton();
        }

        public CtrlTabStripButton(System.Drawing.Image image) : base(image)
        {
            m_BorderColor = System.Drawing.Color.Gray;
            m_BorderColorInactive = System.Drawing.Color.Gray;
            m_BorderColorHot = System.Drawing.Color.Orange;
            m_BackColor2 = System.Drawing.Color.Gray;
            m_BackColor2Inactive = System.Drawing.Color.Gray;
            m_BackColor2Hot = System.Drawing.Color.Orange;
            m_BackColorInactive = System.Drawing.Color.Gray;
            m_BackColorHot = System.Drawing.Color.Orange;
            m_TextColor = System.Drawing.Color.Gray;
            m_TextColorInactive = System.Drawing.Color.Gray;
            m_TextColorHot = System.Drawing.Color.Orange;
            m_VerticalOffset = 1;
            m_VerticalOffsetInactive = 3;
            m_VerticalOffsetHot = 1;
            showCloseButton = true;
            m_CloseButtonVerticalOffset = 12;
            m_CloseButtonHorizontalOffset = 16;
            InitButton();
        }

        public CtrlTabStripButton(string text) : base(text)
        {
            m_BorderColor = System.Drawing.Color.Gray;
            m_BorderColorInactive = System.Drawing.Color.Gray;
            m_BorderColorHot = System.Drawing.Color.Orange;
            m_BackColor2 = System.Drawing.Color.Gray;
            m_BackColor2Inactive = System.Drawing.Color.Gray;
            m_BackColor2Hot = System.Drawing.Color.Orange;
            m_BackColorInactive = System.Drawing.Color.Gray;
            m_BackColorHot = System.Drawing.Color.Orange;
            m_TextColor = System.Drawing.Color.Gray;
            m_TextColorInactive = System.Drawing.Color.Gray;
            m_TextColorHot = System.Drawing.Color.Orange;
            m_VerticalOffset = 1;
            m_VerticalOffsetInactive = 3;
            m_VerticalOffsetHot = 1;
            showCloseButton = true;
            m_CloseButtonVerticalOffset = 12;
            m_CloseButtonHorizontalOffset = 16;
            InitButton();
        }

        public CtrlTabStripButton(string text, System.Drawing.Image image) : base(text, image)
        {
            m_BorderColor = System.Drawing.Color.Gray;
            m_BorderColorInactive = System.Drawing.Color.Gray;
            m_BorderColorHot = System.Drawing.Color.Orange;
            m_BackColor2 = System.Drawing.Color.Gray;
            m_BackColor2Inactive = System.Drawing.Color.Gray;
            m_BackColor2Hot = System.Drawing.Color.Orange;
            m_BackColorInactive = System.Drawing.Color.Gray;
            m_BackColorHot = System.Drawing.Color.Orange;
            m_TextColor = System.Drawing.Color.Gray;
            m_TextColorInactive = System.Drawing.Color.Gray;
            m_TextColorHot = System.Drawing.Color.Orange;
            m_VerticalOffset = 1;
            m_VerticalOffsetInactive = 3;
            m_VerticalOffsetHot = 1;
            showCloseButton = true;
            m_CloseButtonVerticalOffset = 12;
            m_CloseButtonHorizontalOffset = 16;
            InitButton();
        }

        public CtrlTabStripButton(string Text, System.Drawing.Image Image, System.EventHandler Handler) : base(Text, Image, Handler)
        {
            m_BorderColor = System.Drawing.Color.Gray;
            m_BorderColorInactive = System.Drawing.Color.Gray;
            m_BorderColorHot = System.Drawing.Color.Orange;
            m_BackColor2 = System.Drawing.Color.Gray;
            m_BackColor2Inactive = System.Drawing.Color.Gray;
            m_BackColor2Hot = System.Drawing.Color.Orange;
            m_BackColorInactive = System.Drawing.Color.Gray;
            m_BackColorHot = System.Drawing.Color.Orange;
            m_TextColor = System.Drawing.Color.Gray;
            m_TextColorInactive = System.Drawing.Color.Gray;
            m_TextColorHot = System.Drawing.Color.Orange;
            m_VerticalOffset = 1;
            m_VerticalOffsetInactive = 3;
            m_VerticalOffsetHot = 1;
            showCloseButton = true;
            m_CloseButtonVerticalOffset = 12;
            m_CloseButtonHorizontalOffset = 16;
            InitButton();
        }

        public CtrlTabStripButton(string Text, System.Drawing.Image Image, System.EventHandler Handler, string name) : base(Text, Image, Handler, name)
        {
            m_BorderColor = System.Drawing.Color.Gray;
            m_BorderColorInactive = System.Drawing.Color.Gray;
            m_BorderColorHot = System.Drawing.Color.Orange;
            m_BackColor2 = System.Drawing.Color.Gray;
            m_BackColor2Inactive = System.Drawing.Color.Gray;
            m_BackColor2Hot = System.Drawing.Color.Orange;
            m_BackColorInactive = System.Drawing.Color.Gray;
            m_BackColorHot = System.Drawing.Color.Orange;
            m_TextColor = System.Drawing.Color.Gray;
            m_TextColorInactive = System.Drawing.Color.Gray;
            m_TextColorHot = System.Drawing.Color.Orange;
            m_VerticalOffset = 1;
            m_VerticalOffsetInactive = 3;
            m_VerticalOffsetHot = 1;
            showCloseButton = true;
            m_CloseButtonVerticalOffset = 12;
            m_CloseButtonHorizontalOffset = 16;
            InitButton();
        }

        private void InitButton()
        {
        }

        private void RemoveTab()
        {
            Oranikle.Studio.Controls.ItemBeforeRemoveEventArgs itemBeforeRemoveEventArgs = new Oranikle.Studio.Controls.ItemBeforeRemoveEventArgs(this);
            if (((Oranikle.Studio.Controls.CtrlTabStrip)Parent).FireItemBeforeRemoveEvent(itemBeforeRemoveEventArgs))
                return;
            if ((Tag is System.IDisposable))
                ((System.IDisposable)Tag).Dispose();
            Tag = null;
            Parent.Items.Remove(this);
            Dispose();
        }

        public override System.Drawing.Size GetPreferredSize(System.Drawing.Size constrainingSize)
        {
            System.Drawing.Size size = base.GetPreferredSize(constrainingSize);
            if ((Owner != null) && (Owner.Orientation == System.Windows.Forms.Orientation.Vertical))
            {
                size.Width += 3;
                size.Height += 10;
            }
            return size;
        }

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if ((e.Button == System.Windows.Forms.MouseButtons.Middle) && (((Oranikle.Studio.Controls.CtrlTabStrip)Parent).TabCount > ((Oranikle.Studio.Controls.CtrlTabStrip)Parent).MinNumOfTabButtons))
            {
                RemoveTab();
                return;
            }
            if (showCloseButton)
            {
                System.Drawing.Point point = e.Location;
                System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(Width - m_CloseButtonHorizontalOffset, m_CloseButtonVerticalOffset, 8, 8);
                if (rectangle.Contains(point) && (((Oranikle.Studio.Controls.CtrlTabStrip)Parent).TabCount > ((Oranikle.Studio.Controls.CtrlTabStrip)Parent).MinNumOfTabButtons))
                    RemoveTab();
            }
        }

        protected override void OnOwnerChanged(System.EventArgs e)
        {
            if ((Owner != null) && !(Owner is Oranikle.Studio.Controls.CtrlTabStrip))
                throw new System.Exception("Cannot add CtrlTabStripButton to " + Owner.GetType().Name);
            base.OnOwnerChanged(e);
        }

    } // class CtrlTabStripButton

}

