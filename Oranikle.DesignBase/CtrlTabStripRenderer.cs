using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Oranikle.Studio.Controls.Properties;

namespace Oranikle.Studio.Controls
{

    public class CtrlTabStripRenderer : System.Windows.Forms.ToolStripRenderer
    {

        private const int selOffset = 2;

        private System.Windows.Forms.ToolStripRenderer currentRenderer;
        private System.Drawing.Color m_BorderColor;
        private bool mirrored;
        private System.Windows.Forms.ToolStripRenderMode renderMode;
        private bool useVS;

        public System.Drawing.Color BorderColor
        {
            get
            {
                return m_BorderColor;
            }
            set
            {
                m_BorderColor = value;
            }
        }

        public bool Mirrored
        {
            get
            {
                return mirrored;
            }
            set
            {
                mirrored = value;
            }
        }

        public System.Windows.Forms.ToolStripRenderMode RenderMode
        {
            get
            {
                return renderMode;
            }
            set
            {
                renderMode = value;
                switch (renderMode)
                {
                    case System.Windows.Forms.ToolStripRenderMode.Professional:
                        currentRenderer = new System.Windows.Forms.ToolStripProfessionalRenderer();
                        return;

                    case System.Windows.Forms.ToolStripRenderMode.System:
                        currentRenderer = new System.Windows.Forms.ToolStripSystemRenderer();
                        return;
                }
                currentRenderer = null;
            }
        }

        public bool UseVS
        {
            get
            {
                return useVS;
            }
            set
            {
                if (value && !System.Windows.Forms.Application.RenderWithVisualStyles)
                    return;
                useVS = value;
            }
        }

        public CtrlTabStripRenderer()
        {
            useVS = System.Windows.Forms.Application.RenderWithVisualStyles;
            m_BorderColor = System.Drawing.Color.Gray;
        }

        protected override void Initialize(System.Windows.Forms.ToolStrip ts)
        {
            base.Initialize(ts);
        }

        protected override void OnRenderArrow(System.Windows.Forms.ToolStripArrowRenderEventArgs e)
        {
            if (currentRenderer != null)
            {
                currentRenderer.DrawArrow(e);
                return;
            }
            base.OnRenderArrow(e);
        }

        protected override void OnRenderButtonBackground(System.Windows.Forms.ToolStripItemRenderEventArgs e)
        {
            System.Drawing.Rectangle rectangle1;

            System.Drawing.Graphics graphics1 = e.Graphics;
            Oranikle.Studio.Controls.CtrlTabStrip ctrlTabStrip = e.ToolStrip as Oranikle.Studio.Controls.CtrlTabStrip;
            Oranikle.Studio.Controls.CtrlTabStripButton ctrlTabStripButton = e.Item as Oranikle.Studio.Controls.CtrlTabStripButton;
            if ((ctrlTabStrip == null) || (ctrlTabStripButton == null))
            {
                if (currentRenderer != null)
                {
                    currentRenderer.DrawButtonBackground(e);
                    return;
                }
                base.OnRenderButtonBackground(e);
                return;
            }
            bool flag1 = ctrlTabStripButton.Checked;
            bool flag2 = ctrlTabStripButton.Selected;
            int i1 = 0, i2 = 0;
            System.Drawing.Rectangle rectangle3 = ctrlTabStripButton.Bounds;
            int i3 = rectangle3.Width - 1;
            System.Drawing.Rectangle rectangle4 = ctrlTabStripButton.Bounds;
            int i4 = rectangle4.Height - 1;
            if (UseVS)
            {
                if (ctrlTabStrip.Orientation == System.Windows.Forms.Orientation.Horizontal)
                {
                    if (!flag1)
                    {
                        i1 = 2;
                        i4--;
                    }
                    else
                    {
                        i1 = 1;
                    }
                    rectangle1 = new System.Drawing.Rectangle(0, 0, i3, i4);
                }
                else
                {
                    if (!flag1)
                    {
                        i2 = 2;
                        i3--;
                    }
                    else
                    {
                        i2 = 1;
                    }
                    rectangle1 = new System.Drawing.Rectangle(0, 0, i4, i3);
                }
                using (System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(rectangle1.Width, rectangle1.Height))
                {
                    System.Windows.Forms.VisualStyles.VisualStyleElement visualStyleElement = System.Windows.Forms.VisualStyles.VisualStyleElement.Tab.TabItem.Normal;
                    if (flag1)
                        visualStyleElement = System.Windows.Forms.VisualStyles.VisualStyleElement.Tab.TabItem.Pressed;
                    if (flag2)
                        visualStyleElement = System.Windows.Forms.VisualStyles.VisualStyleElement.Tab.TabItem.Hot;
                    if (!ctrlTabStripButton.Enabled)
                        visualStyleElement = System.Windows.Forms.VisualStyles.VisualStyleElement.Tab.TabItem.Disabled;
                    if (!flag1 || flag2)
                        rectangle1.Width++;
                    else
                        rectangle1.Height++;
                    using (System.Drawing.Graphics graphics2 = System.Drawing.Graphics.FromImage(bitmap))
                    {
                        System.Windows.Forms.VisualStyles.VisualStyleRenderer visualStyleRenderer = new System.Windows.Forms.VisualStyles.VisualStyleRenderer(visualStyleElement);
                        visualStyleRenderer.DrawBackground(graphics2, rectangle1);
                        if (ctrlTabStrip.Orientation == System.Windows.Forms.Orientation.Vertical)
                        {
                            if (Mirrored)
                                bitmap.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);
                            else
                                bitmap.RotateFlip(System.Drawing.RotateFlipType.Rotate270FlipNone);
                        }
                        else if (Mirrored)
                        {
                            bitmap.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipX);
                        }
                        if (Mirrored)
                        {
                            System.Drawing.Rectangle rectangle5 = ctrlTabStripButton.Bounds;
                            i2 = rectangle5.Width - bitmap.Width - i2;
                            System.Drawing.Rectangle rectangle6 = ctrlTabStripButton.Bounds;
                            i1 = rectangle6.Height - bitmap.Height - i1;
                        }
                        graphics1.DrawImage(bitmap, i2, i1);
                    }
                    return;
                }
            }
            if (ctrlTabStrip.Orientation == System.Windows.Forms.Orientation.Horizontal)
            {
                if (!flag1)
                {
                    i1 = ctrlTabStripButton.VerticalOffsetInactive;
                    i4 -= ctrlTabStripButton.VerticalOffsetInactive - 1;
                }
                else
                {
                    i1 = ctrlTabStripButton.VerticalOffset;
                }
                if (Mirrored)
                {
                    i2 = 1;
                    i1 = 0;
                }
                else
                {
                    i1++;
                }
                i3--;
            }
            else
            {
                if (!flag1)
                {
                    i2 = 2;
                    i3--;
                }
                else
                {
                    i2 = 1;
                }
                if (Mirrored)
                {
                    i2 = 0;
                    i1 = 1;
                }
            }
            i4--;
            rectangle1 = new System.Drawing.Rectangle(i2, i1, i3, i4);
            using (System.Drawing.Drawing2D.GraphicsPath graphicsPath = new System.Drawing.Drawing2D.GraphicsPath())
            {
                if (Mirrored && (ctrlTabStrip.Orientation == System.Windows.Forms.Orientation.Horizontal))
                {
                    graphicsPath.AddLine(rectangle1.Left, rectangle1.Top, rectangle1.Left, rectangle1.Bottom - 2);
                    graphicsPath.AddArc(rectangle1.Left, rectangle1.Bottom - 3, 2, 2, 90.0F, 90.0F);
                    graphicsPath.AddLine(rectangle1.Left + 2, rectangle1.Bottom, rectangle1.Right - 2, rectangle1.Bottom);
                    graphicsPath.AddArc(rectangle1.Right - 2, rectangle1.Bottom - 3, 2, 2, 0.0F, 90.0F);
                    graphicsPath.AddLine(rectangle1.Right, rectangle1.Bottom - 2, rectangle1.Right, rectangle1.Top);
                }
                else if (!Mirrored && (ctrlTabStrip.Orientation == System.Windows.Forms.Orientation.Horizontal))
                {
                    int i5 = 1, i6 = 3;
                    graphicsPath.AddLine(rectangle1.Left, rectangle1.Bottom, rectangle1.Left, rectangle1.Top + i6);
                    graphicsPath.AddArc(rectangle1.Left, rectangle1.Top + i6 - 1, i6, i6, 180.0F, 90.0F);
                    graphicsPath.AddLine(rectangle1.Left + i6, rectangle1.Top, rectangle1.Right - i6 - i5, rectangle1.Top);
                    graphicsPath.AddArc(rectangle1.Right - i6 - i5, rectangle1.Top + i6 - 1, i6, i6, 270.0F, 90.0F);
                    graphicsPath.AddLine(rectangle1.Right - i5, rectangle1.Top + i6, rectangle1.Right - i5, rectangle1.Bottom);
                }
                else if (Mirrored && (ctrlTabStrip.Orientation == System.Windows.Forms.Orientation.Vertical))
                {
                    graphicsPath.AddLine(rectangle1.Left, rectangle1.Top, rectangle1.Right - 2, rectangle1.Top);
                    graphicsPath.AddArc(rectangle1.Right - 2, rectangle1.Top + 1, 2, 2, 270.0F, 90.0F);
                    graphicsPath.AddLine(rectangle1.Right, rectangle1.Top + 2, rectangle1.Right, rectangle1.Bottom - 2);
                    graphicsPath.AddArc(rectangle1.Right - 2, rectangle1.Bottom - 3, 2, 2, 0.0F, 90.0F);
                    graphicsPath.AddLine(rectangle1.Right - 2, rectangle1.Bottom, rectangle1.Left, rectangle1.Bottom);
                }
                else
                {
                    graphicsPath.AddLine(rectangle1.Right, rectangle1.Top, rectangle1.Left + 2, rectangle1.Top);
                    graphicsPath.AddArc(rectangle1.Left, rectangle1.Top + 1, 2, 2, 180.0F, 90.0F);
                    graphicsPath.AddLine(rectangle1.Left, rectangle1.Top + 2, rectangle1.Left, rectangle1.Bottom - 2);
                    graphicsPath.AddArc(rectangle1.Left, rectangle1.Bottom - 3, 2, 2, 90.0F, 90.0F);
                    graphicsPath.AddLine(rectangle1.Left + 2, rectangle1.Bottom, rectangle1.Right, rectangle1.Bottom);
                }
                System.Drawing.Color color1 = ctrlTabStripButton.BackColorInactive;
                if (flag1)
                    color1 = ctrlTabStripButton.BackColor;
                else if (flag2)
                    color1 = ctrlTabStripButton.BackColorHot;
                System.Drawing.Color color2 = ctrlTabStripButton.BackColor2Inactive;
                if (flag1)
                    color2 = ctrlTabStripButton.BackColor2;
                else if (flag2)
                    color2 = ctrlTabStripButton.BackColor2Hot;
                if (renderMode == System.Windows.Forms.ToolStripRenderMode.Professional)
                {
                    color1 = flag2 ? System.Windows.Forms.ProfessionalColors.ButtonCheckedGradientBegin : System.Windows.Forms.ProfessionalColors.ButtonCheckedGradientEnd;
                    using (System.Drawing.Drawing2D.LinearGradientBrush linearGradientBrush1 = new System.Drawing.Drawing2D.LinearGradientBrush(ctrlTabStripButton.ContentRectangle, color1, System.Windows.Forms.ProfessionalColors.ButtonCheckedGradientMiddle, System.Drawing.Drawing2D.LinearGradientMode.Vertical))
                    {
                        graphics1.FillPath(linearGradientBrush1, graphicsPath);
                        goto label_1;
                    }
                }
                using (System.Drawing.Drawing2D.LinearGradientBrush linearGradientBrush2 = new System.Drawing.Drawing2D.LinearGradientBrush(ctrlTabStripButton.ContentRectangle, color1, color2, System.Drawing.Drawing2D.LinearGradientMode.Vertical))
                {
                    graphics1.FillPath(linearGradientBrush2, graphicsPath);
                }
            label_1:
                if (flag1)
                {
                    using (System.Drawing.Pen pen1 = new System.Drawing.Pen(ctrlTabStripButton.BorderColor))
                    {
                        graphics1.DrawPath(pen1, graphicsPath);
                        goto label_2;
                    }
                }
                if (flag2)
                {
                    using (System.Drawing.Pen pen2 = new System.Drawing.Pen(ctrlTabStripButton.BorderColorHot))
                    {
                        graphics1.DrawPath(pen2, graphicsPath);
                        goto label_2;
                    }
                }
                using (System.Drawing.Pen pen3 = new System.Drawing.Pen(ctrlTabStripButton.BorderColorInactive))
                {
                    graphics1.DrawPath(pen3, graphicsPath);
                }
            label_2:
                if (ctrlTabStripButton.ShowCloseButton)
                {
                    System.Drawing.Image image = Oranikle.Studio.Controls.Properties.Resources.Icon_Close_Disabled_16;
                    if (flag2)
                        image = Oranikle.Studio.Controls.Properties.Resources.Icon_Close_16;
                    System.Drawing.Rectangle rectangle2 = new System.Drawing.Rectangle(i2 + i3 - ctrlTabStripButton.CloseButtonHorizontalOffset, ctrlTabStripButton.CloseButtonVerticalOffset, 8, 8);
                    graphics1.DrawImage(image, rectangle2);
                }
            }
        }

        protected override void OnRenderDropDownButtonBackground(System.Windows.Forms.ToolStripItemRenderEventArgs e)
        {
            if (currentRenderer != null)
            {
                currentRenderer.DrawDropDownButtonBackground(e);
                return;
            }
            base.OnRenderDropDownButtonBackground(e);
        }

        protected override void OnRenderGrip(System.Windows.Forms.ToolStripGripRenderEventArgs e)
        {
            if (currentRenderer != null)
            {
                currentRenderer.DrawGrip(e);
                return;
            }
            base.OnRenderGrip(e);
        }

        protected override void OnRenderImageMargin(System.Windows.Forms.ToolStripRenderEventArgs e)
        {
            if (currentRenderer != null)
            {
                currentRenderer.DrawImageMargin(e);
                return;
            }
            base.OnRenderImageMargin(e);
        }

        protected override void OnRenderItemBackground(System.Windows.Forms.ToolStripItemRenderEventArgs e)
        {
            if (currentRenderer != null)
            {
                currentRenderer.DrawItemBackground(e);
                return;
            }
            base.OnRenderItemBackground(e);
        }

        protected override void OnRenderItemCheck(System.Windows.Forms.ToolStripItemImageRenderEventArgs e)
        {
            if (currentRenderer != null)
            {
                currentRenderer.DrawItemCheck(e);
                return;
            }
            base.OnRenderItemCheck(e);
        }

        protected override void OnRenderItemImage(System.Windows.Forms.ToolStripItemImageRenderEventArgs e)
        {
            System.Drawing.Rectangle rectangle = e.ImageRectangle;
            Oranikle.Studio.Controls.CtrlTabStripButton ctrlTabStripButton = e.Item as Oranikle.Studio.Controls.CtrlTabStripButton;
            if (ctrlTabStripButton != null)
            {
                if (ctrlTabStripButton.DisplayStyle == System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText)
                {
                    int i1 = (Mirrored ? -1 : 1) * (ctrlTabStripButton.Checked ? ctrlTabStripButton.VerticalOffset : ctrlTabStripButton.VerticalOffsetInactive);
                    rectangle = new System.Drawing.Rectangle(10, 6 + i1, 22, 22);
                }
                else
                {
                    int i2 = (Mirrored ? -1 : 1) * (ctrlTabStripButton.Checked ? ctrlTabStripButton.VerticalOffset - 4 : ctrlTabStripButton.VerticalOffsetInactive - 4);
                    if (e.ToolStrip.Orientation == System.Windows.Forms.Orientation.Horizontal)
                        rectangle.Offset(-1, i2 + (Mirrored ? 1 : 0));
                    else
                        rectangle.Offset(-1, 0);
                }
            }
            System.Windows.Forms.ToolStripItemImageRenderEventArgs toolStripItemImageRenderEventArgs = new System.Windows.Forms.ToolStripItemImageRenderEventArgs(e.Graphics, e.Item, e.Image, rectangle);
            if (currentRenderer != null)
            {
                currentRenderer.DrawItemImage(toolStripItemImageRenderEventArgs);
                return;
            }
            base.OnRenderItemImage(toolStripItemImageRenderEventArgs);
        }

        protected override void OnRenderItemText(System.Windows.Forms.ToolStripItemTextRenderEventArgs e)
        {
            System.Drawing.Rectangle rectangle = e.TextRectangle;
            Oranikle.Studio.Controls.CtrlTabStripButton ctrlTabStripButton = e.Item as Oranikle.Studio.Controls.CtrlTabStripButton;
            System.Drawing.Color color = e.TextColor;
            System.Drawing.Font font = e.TextFont;
            if (ctrlTabStripButton != null)
            {
                int i1 = (Mirrored ? -1 : 1) * (ctrlTabStripButton.Checked ? ctrlTabStripButton.VerticalOffset : ctrlTabStripButton.VerticalOffsetInactive);
                int i2 = ctrlTabStripButton.Width - 37 - ctrlTabStripButton.CloseButtonHorizontalOffset - 7;
                if (i2 < 25)
                    return;
                rectangle = new System.Drawing.Rectangle(37, rectangle.Y + i1, i2, rectangle.Height);
                if (ctrlTabStripButton.Selected)
                    color = ctrlTabStripButton.TextColorHot;
                else if (ctrlTabStripButton.Checked)
                    color = ctrlTabStripButton.TextColor;
                else
                    color = ctrlTabStripButton.TextColorInactive;
            }
            System.Windows.Forms.ToolStripItemTextRenderEventArgs toolStripItemTextRenderEventArgs = new System.Windows.Forms.ToolStripItemTextRenderEventArgs(e.Graphics, e.Item, e.Text, rectangle, color, font, System.Drawing.ContentAlignment.MiddleLeft);
            if (currentRenderer != null)
            {
                currentRenderer.DrawItemText(toolStripItemTextRenderEventArgs);
                return;
            }
            base.OnRenderItemText(toolStripItemTextRenderEventArgs);
        }

        protected override void OnRenderLabelBackground(System.Windows.Forms.ToolStripItemRenderEventArgs e)
        {
            if (currentRenderer != null)
            {
                currentRenderer.DrawLabelBackground(e);
                return;
            }
            base.OnRenderLabelBackground(e);
        }

        protected override void OnRenderMenuItemBackground(System.Windows.Forms.ToolStripItemRenderEventArgs e)
        {
            if (currentRenderer != null)
            {
                currentRenderer.DrawMenuItemBackground(e);
                return;
            }
            base.OnRenderMenuItemBackground(e);
        }

        protected override void OnRenderOverflowButtonBackground(System.Windows.Forms.ToolStripItemRenderEventArgs e)
        {
            if (currentRenderer != null)
            {
                currentRenderer.DrawOverflowButtonBackground(e);
                return;
            }
            base.OnRenderOverflowButtonBackground(e);
        }

        protected override void OnRenderSeparator(System.Windows.Forms.ToolStripSeparatorRenderEventArgs e)
        {
            if (currentRenderer != null)
            {
                currentRenderer.DrawSeparator(e);
                return;
            }
            base.OnRenderSeparator(e);
        }

        protected override void OnRenderSplitButtonBackground(System.Windows.Forms.ToolStripItemRenderEventArgs e)
        {
            if (currentRenderer != null)
            {
                currentRenderer.DrawSplitButton(e);
                return;
            }
            base.OnRenderSplitButtonBackground(e);
        }

        protected override void OnRenderStatusStripSizingGrip(System.Windows.Forms.ToolStripRenderEventArgs e)
        {
            if (currentRenderer != null)
            {
                currentRenderer.DrawStatusStripSizingGrip(e);
                return;
            }
            base.OnRenderStatusStripSizingGrip(e);
        }

        protected override void OnRenderToolStripBackground(System.Windows.Forms.ToolStripRenderEventArgs e)
        {
            if (currentRenderer != null)
            {
                currentRenderer.DrawToolStripBackground(e);
                return;
            }
            base.OnRenderToolStripBackground(e);
        }

        protected override void OnRenderToolStripBorder(System.Windows.Forms.ToolStripRenderEventArgs e)
        {
            // decompiler error
        }

        protected override void OnRenderToolStripContentPanelBackground(System.Windows.Forms.ToolStripContentPanelRenderEventArgs e)
        {
            if (currentRenderer != null)
            {
                currentRenderer.DrawToolStripContentPanelBackground(e);
                return;
            }
            base.OnRenderToolStripContentPanelBackground(e);
        }

        protected override void OnRenderToolStripPanelBackground(System.Windows.Forms.ToolStripPanelRenderEventArgs e)
        {
            if (currentRenderer != null)
            {
                currentRenderer.DrawToolStripPanelBackground(e);
                return;
            }
            base.OnRenderToolStripPanelBackground(e);
        }

        protected override void OnRenderToolStripStatusLabelBackground(System.Windows.Forms.ToolStripItemRenderEventArgs e)
        {
            if (currentRenderer != null)
            {
                currentRenderer.DrawToolStripStatusLabelBackground(e);
                return;
            }
            base.OnRenderToolStripStatusLabelBackground(e);
        }

    } // class CtrlTabStripRenderer

}

