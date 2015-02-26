using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Oranikle.Studio.Controls.Properties;
using log4net;

namespace Oranikle.Studio.Controls
{

    public class CtrlRichTextbox : Oranikle.Studio.Controls.BaseUserControl
    {

        protected static readonly log4net.ILog log;

        private System.Drawing.Color _backColor;
        private bool _readOnly;
        private bool _showToolbar;
        private System.Windows.Forms.ToolStripMenuItem boldToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton bttnBackgroundColour;
        private System.Windows.Forms.ToolStripButton bttnBold;
        private System.Windows.Forms.ToolStripButton bttnCentreAlign;
        private System.Windows.Forms.ToolStripButton bttnColour;
        private System.Windows.Forms.ToolStripButton bttnFont;
        private System.Windows.Forms.ToolStripButton bttnItalic;
        private System.Windows.Forms.ToolStripButton bttnLeftAlign;
        private System.Windows.Forms.ToolStripButton bttnRightAlign;
        private System.Windows.Forms.ToolStripButton bttnUnderline;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private Oranikle.Studio.Controls.RichTextBoxEx ctrlRtf;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.DialogResult dlgResult;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.ToolStripMenuItem italicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private Oranikle.Studio.Controls.RichTextBoxEx rtbTemp;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem underlineToolStripMenuItem;

        private static string RTFLinkEnd;
        private static string RTFLinkStartCloser;
        private static string RTFLinkStartOpener;

        public bool ReadOnly
        {
            get
            {
                return _readOnly;
            }
            set
            {
                _readOnly = value;
                ctrlRtf.ReadOnly = _readOnly;
                if (_readOnly)
                    ctrlRtf.Cursor = System.Windows.Forms.Cursors.Arrow;
                else
                    ctrlRtf.Cursor = System.Windows.Forms.Cursors.Default;
                toolStrip1.Visible = !_readOnly;
            }
        }

        public string RTF
        {
            get
            {
                return ctrlRtf.Rtf;
            }
            set
            {
                if (_readOnly)
                {
                    SetLinksInCtrlRTF(value);
                    return;
                }
                ctrlRtf.Rtf = value;
            }
        }

        public bool ShowToolbar
        {
            get
            {
                return _showToolbar;
            }
            set
            {
                _showToolbar = value;
                toolStrip1.Visible = _showToolbar;
            }
        }

        public Oranikle.Studio.Controls.RichTextBoxEx Textbox
        {
            get
            {
                return ctrlRtf;
            }
        }

        public System.Windows.Forms.ToolStrip Toolbar
        {
            get
            {
                return toolStrip1;
            }
        }

        public override System.Drawing.Color BackColor
        {
            get
            {
                return _backColor;
            }
            set
            {
                _backColor = value;
                if (_backColor == System.Drawing.Color.Transparent)
                    ctrlRtf.BackColor = System.Drawing.Color.White;
                else
                    ctrlRtf.BackColor = _backColor;
                toolStrip1.BackColor = _backColor;
                base.BackColor = _backColor;
            }
        }

        public CtrlRichTextbox()
        {
            _showToolbar = true;
            rtbTemp = new Oranikle.Studio.Controls.RichTextBoxEx();
            InitializeComponent();
        }

        static CtrlRichTextbox()
        {
            Oranikle.Studio.Controls.CtrlRichTextbox.log = log4net.LogManager.GetLogger(typeof(Oranikle.Studio.Controls.CtrlRichTextbox));
            Oranikle.Studio.Controls.CtrlRichTextbox.RTFLinkStartOpener = "<<<a ";
            Oranikle.Studio.Controls.CtrlRichTextbox.RTFLinkStartCloser = ">>>";
            Oranikle.Studio.Controls.CtrlRichTextbox.RTFLinkEnd = "<<</a>>>";
        }

        private void Bold()
        {
            ChangeFontStyle(System.Drawing.FontStyle.Bold);
        }

        private void boldToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Bold();
        }

        private void bttnBackgroundColour_Click(object sender, System.EventArgs e)
        {
            //ctrlRtf.SelectionBackColor;
            colorDialog1.Color = ctrlRtf.SelectionBackColor;
            dlgResult = colorDialog1.ShowDialog();
            if (dlgResult == System.Windows.Forms.DialogResult.Cancel)
                return;
            ctrlRtf.SelectionBackColor = colorDialog1.Color;
        }

        private void bttnBold_Click(object sender, System.EventArgs e)
        {
            Bold();
        }

        private void bttnBullet_Click(object sender, System.EventArgs e)
        {
            ctrlRtf.SelectionBullet = !ctrlRtf.SelectionBullet;
        }

        private void bttnCentreAlign_Click(object sender, System.EventArgs e)
        {
            ctrlRtf.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
        }

        private void bttnColour_Click(object sender, System.EventArgs e)
        {
            //ctrlRtf.SelectionColor;
            colorDialog1.Color = ctrlRtf.SelectionColor;
            dlgResult = colorDialog1.ShowDialog();
            if (dlgResult == System.Windows.Forms.DialogResult.Cancel)
                return;
            ctrlRtf.SelectionColor = colorDialog1.Color;
        }

        private void bttnCopy_Click(object sender, System.EventArgs e)
        {
            Copy();
        }

        private void bttnCut_Click(object sender, System.EventArgs e)
        {
            Cut();
        }

        private void bttnFont_Click(object sender, System.EventArgs e)
        {
            if (ctrlRtf.SelectionFont != null)
                fontDialog1.Font = ctrlRtf.SelectionFont;
            dlgResult = fontDialog1.ShowDialog();
            if (dlgResult == System.Windows.Forms.DialogResult.Cancel)
                return;
            ctrlRtf.SelectionFont = fontDialog1.Font;
        }

        private void bttnIndent_Click(object sender, System.EventArgs e)
        {
            ctrlRtf.SelectionIndent += Oranikle.Studio.Controls.BaseUserControl.RichTextBoxIndentPixel;
        }

        private void bttnItalic_Click(object sender, System.EventArgs e)
        {
            Italic();
        }

        private void bttnLeftAlign_Click(object sender, System.EventArgs e)
        {
            ctrlRtf.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
        }

        private void bttnOutdent_Click(object sender, System.EventArgs e)
        {
            if (ctrlRtf.SelectionIndent <= Oranikle.Studio.Controls.BaseUserControl.RichTextBoxIndentPixel)
            {
                ctrlRtf.SelectionIndent = 0;
                return;
            }
            ctrlRtf.SelectionIndent -= Oranikle.Studio.Controls.BaseUserControl.RichTextBoxIndentPixel;
        }

        private void bttnPaste_Click(object sender, System.EventArgs e)
        {
            Paste();
        }

        private void bttnRedo_Click(object sender, System.EventArgs e)
        {
            Redo();
        }

        private void bttnRightAlign_Click(object sender, System.EventArgs e)
        {
            ctrlRtf.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Right;
        }

        private void bttnUnderline_Click(object sender, System.EventArgs e)
        {
            Underline();
        }

        private void bttnUndo_Click(object sender, System.EventArgs e)
        {
            Undo();
        }

        private void ChangeFontStyle(System.Drawing.FontStyle style, System.Windows.Forms.RichTextBox rtb)
        {
            if (rtb.SelectionFont == null)
                Oranikle.Studio.Controls.ApplicationFlowException.ThrowNewApplicationFlowException("Selection Font is null.");
            bool flag = false;
            switch (style)
            {
                case System.Drawing.FontStyle.Bold:
                    if (rtb.SelectionFont.Bold)
                    {
                        flag = false;
                        goto label_0;
                    }
                    flag = true;
                    break;

                case System.Drawing.FontStyle.Italic:
                    if (rtb.SelectionFont.Italic)
                    {
                        flag = false;
                        goto label_0;
                    }
                    flag = true;
                    break;

                case System.Drawing.FontStyle.Strikeout:
                    if (rtb.SelectionFont.Strikeout)
                    {
                        flag = false;
                        goto label_0;
                    }
                    flag = true;
                    break;

                case System.Drawing.FontStyle.Underline:
                    if (rtb.SelectionFont.Underline)
                    {
                        flag = false;
                        goto label_0;
                    }
                    flag = true;
                    break;

                case System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic:
                    goto label_0;
                    break;
            }
        label_0: if (flag)
            {
                rtb.SelectionFont = new System.Drawing.Font(rtb.SelectionFont, (System.Drawing.FontStyle)(rtb.SelectionFont.Style | style));
                return;
            }
            rtb.SelectionFont = new System.Drawing.Font(rtb.SelectionFont, (System.Drawing.FontStyle)(rtb.SelectionFont.Style & ~style));

        }

        private void ChangeFontStyle(System.Drawing.FontStyle style)
        {
            if ((style != System.Drawing.FontStyle.Bold) && (style != System.Drawing.FontStyle.Italic) && (style != System.Drawing.FontStyle.Strikeout) && (style != System.Drawing.FontStyle.Underline))
                Oranikle.Studio.Controls.ApplicationFlowException.ThrowNewApplicationFlowException("Invalid style parameter to ChangeFontStyle");
            int i1 = ctrlRtf.SelectionStart;
            int i2 = ctrlRtf.SelectionLength;
            int i3 = 0;
            if ((i2 <= 1) && (ctrlRtf.SelectionFont != null))
            {
                ChangeFontStyle(style, ctrlRtf);
                return;
            }
            rtbTemp.Rtf = ctrlRtf.SelectedRtf;
            for (int i4 = 0; i4 < i2; i4++)
            {
                rtbTemp.Select(i3 + i4, 1);
                ChangeFontStyle(style, rtbTemp);
            }
            rtbTemp.Select(i3, i2);
            ctrlRtf.SelectedRtf = rtbTemp.SelectedRtf;
            ctrlRtf.Select(i1, i2);
        }

        private void Copy()
        {
            ctrlRtf.Copy();
        }

        private void copyToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Copy();
        }

        private void Cut()
        {
            ctrlRtf.Cut();
        }

        private void cutToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Cut();
        }

        private string ExtractId(string p)
        {
            string s = "";
            for (int i = 0; i < p.Length; i++)
            {
                if (System.Char.IsNumber(p[i]))
                    s = System.String.Concat(s, p[i]);
            }
            return s;
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager componentResourceManager = new System.ComponentModel.ComponentResourceManager(typeof(Oranikle.Studio.Controls.CtrlRichTextbox));
            toolStrip1 = new System.Windows.Forms.ToolStrip();
            bttnFont = new System.Windows.Forms.ToolStripButton();
            bttnColour = new System.Windows.Forms.ToolStripButton();
            bttnBackgroundColour = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            bttnBold = new System.Windows.Forms.ToolStripButton();
            bttnItalic = new System.Windows.Forms.ToolStripButton();
            bttnUnderline = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            bttnLeftAlign = new System.Windows.Forms.ToolStripButton();
            bttnCentreAlign = new System.Windows.Forms.ToolStripButton();
            bttnRightAlign = new System.Windows.Forms.ToolStripButton();
            ctrlRtf = new Oranikle.Studio.Controls.RichTextBoxEx();
            contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
            boldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            italicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            underlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            fontDialog1 = new System.Windows.Forms.FontDialog();
            colorDialog1 = new System.Windows.Forms.ColorDialog();
            toolStrip1.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            toolStrip1.BackColor = System.Drawing.Color.Transparent;
            toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            System.Windows.Forms.ToolStripItem[] toolStripItemArr1 = new System.Windows.Forms.ToolStripItem[] {
                                                                                                                bttnBold, 
                                                                                                                bttnItalic, 
                                                                                                                bttnUnderline, 
                                                                                                                toolStripSeparator1, 
                                                                                                                bttnLeftAlign, 
                                                                                                                bttnCentreAlign, 
                                                                                                                bttnRightAlign, 
                                                                                                                toolStripSeparator2, 
                                                                                                                bttnFont, 
                                                                                                                bttnColour, 
                                                                                                                bttnBackgroundColour };
            toolStrip1.Items.AddRange(toolStripItemArr1);
            toolStrip1.Location = new System.Drawing.Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            toolStrip1.Size = new System.Drawing.Size(460, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            bttnFont.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            bttnFont.Image = Oranikle.Studio.Controls.Properties.Resources.Icon_Font_16;
            bttnFont.ImageTransparentColor = System.Drawing.Color.White;
            bttnFont.Name = "bttnFont";
            bttnFont.Size = new System.Drawing.Size(23, 22);
            bttnFont.Text = "toolStripButton4";
            bttnFont.ToolTipText = "Font";
            bttnFont.Click += new System.EventHandler(bttnFont_Click);
            bttnColour.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            bttnColour.Image = Oranikle.Studio.Controls.Properties.Resources.Icon_Color_16;
            bttnColour.ImageTransparentColor = System.Drawing.Color.White;
            bttnColour.Name = "bttnColour";
            bttnColour.Size = new System.Drawing.Size(23, 22);
            bttnColour.Text = "toolStripButton5";
            bttnColour.ToolTipText = "Colour";
            bttnColour.Click += new System.EventHandler(bttnColour_Click);
            bttnBackgroundColour.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            bttnBackgroundColour.Image = Oranikle.Studio.Controls.Properties.Resources.Icon_FontColor_16;
            bttnBackgroundColour.ImageTransparentColor = System.Drawing.Color.Magenta;
            bttnBackgroundColour.Name = "bttnBackgroundColour";
            bttnBackgroundColour.Size = new System.Drawing.Size(23, 22);
            bttnBackgroundColour.Text = "toolStripButton1";
            bttnBackgroundColour.ToolTipText = "Background Colour";
            bttnBackgroundColour.Click += new System.EventHandler(bttnBackgroundColour_Click);
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            bttnBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            bttnBold.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            bttnBold.Image = (System.Drawing.Image)componentResourceManager.GetObject("bttnBold.Image");
            bttnBold.ImageTransparentColor = System.Drawing.Color.Magenta;
            bttnBold.Name = "bttnBold";
            bttnBold.Size = new System.Drawing.Size(23, 22);
            bttnBold.Text = "B";
            bttnBold.ToolTipText = "Bold";
            bttnBold.Click += new System.EventHandler(bttnBold_Click);
            bttnItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            bttnItalic.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
            bttnItalic.Image = (System.Drawing.Image)componentResourceManager.GetObject("bttnItalic.Image");
            bttnItalic.ImageTransparentColor = System.Drawing.Color.Magenta;
            bttnItalic.Name = "bttnItalic";
            bttnItalic.Size = new System.Drawing.Size(23, 22);
            bttnItalic.Text = "I";
            bttnItalic.ToolTipText = "Italic";
            bttnItalic.Click += new System.EventHandler(bttnItalic_Click);
            bttnUnderline.BackColor = System.Drawing.Color.Transparent;
            bttnUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            bttnUnderline.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
            bttnUnderline.Image = (System.Drawing.Image)componentResourceManager.GetObject("bttnUnderline.Image");
            bttnUnderline.ImageTransparentColor = System.Drawing.Color.Magenta;
            bttnUnderline.Name = "bttnUnderline";
            bttnUnderline.Size = new System.Drawing.Size(23, 22);
            bttnUnderline.Text = "U";
            bttnUnderline.ToolTipText = "Underline";
            bttnUnderline.Click += new System.EventHandler(bttnUnderline_Click);
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            bttnLeftAlign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            bttnLeftAlign.Image = Oranikle.Studio.Controls.Properties.Resources.Icon_AlignMiddleLeft_16;
            bttnLeftAlign.ImageTransparentColor = System.Drawing.Color.Black;
            bttnLeftAlign.Name = "bttnLeftAlign";
            bttnLeftAlign.Size = new System.Drawing.Size(23, 22);
            bttnLeftAlign.Text = "toolStripButton1";
            bttnLeftAlign.ToolTipText = "Align Left";
            bttnLeftAlign.Click += new System.EventHandler(bttnLeftAlign_Click);
            bttnCentreAlign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            bttnCentreAlign.Image = Oranikle.Studio.Controls.Properties.Resources.Icon_AlignMiddleCenter_16;
            bttnCentreAlign.ImageTransparentColor = System.Drawing.Color.Black;
            bttnCentreAlign.Name = "bttnCentreAlign";
            bttnCentreAlign.Size = new System.Drawing.Size(23, 22);
            bttnCentreAlign.Text = "toolStripButton2";
            bttnCentreAlign.ToolTipText = "Align Center";
            bttnCentreAlign.Click += new System.EventHandler(bttnCentreAlign_Click);
            bttnRightAlign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            bttnRightAlign.Image = Oranikle.Studio.Controls.Properties.Resources.Icon_AlignMiddleRight_16;
            bttnRightAlign.ImageTransparentColor = System.Drawing.Color.Black;
            bttnRightAlign.Name = "bttnRightAlign";
            bttnRightAlign.Size = new System.Drawing.Size(23, 22);
            bttnRightAlign.Text = "toolStripButton3";
            bttnRightAlign.ToolTipText = "Align Right";
            bttnRightAlign.Click += new System.EventHandler(bttnRightAlign_Click);
            ctrlRtf.ContextMenuStrip = contextMenuStrip1;
            ctrlRtf.Dock = System.Windows.Forms.DockStyle.Fill;
            ctrlRtf.Location = new System.Drawing.Point(0, 25);
            ctrlRtf.Name = "ctrlRtf";
            ctrlRtf.Size = new System.Drawing.Size(460, 227);
            ctrlRtf.TabIndex = 1;
            ctrlRtf.Text = "";
            System.Windows.Forms.ToolStripItem[] toolStripItemArr2 = new System.Windows.Forms.ToolStripItem[] {
                                                                                                                boldToolStripMenuItem, 
                                                                                                                italicToolStripMenuItem, 
                                                                                                                underlineToolStripMenuItem, 
                                                                                                                toolStripSeparator5, 
                                                                                                                cutToolStripMenuItem, 
                                                                                                                copyToolStripMenuItem, 
                                                                                                                pasteToolStripMenuItem };
            contextMenuStrip1.Items.AddRange(toolStripItemArr2);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new System.Drawing.Size(159, 142);
            boldToolStripMenuItem.Name = "boldToolStripMenuItem";
            boldToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.RButton | System.Windows.Forms.Keys.Control;
            boldToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            boldToolStripMenuItem.Text = "Bold";
            boldToolStripMenuItem.Click += new System.EventHandler(boldToolStripMenuItem_Click);
            italicToolStripMenuItem.Name = "italicToolStripMenuItem";
            italicToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.LButton | System.Windows.Forms.Keys.Back | System.Windows.Forms.Keys.Control;
            italicToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            italicToolStripMenuItem.Text = "Italic";
            italicToolStripMenuItem.Click += new System.EventHandler(italicToolStripMenuItem_Click);
            underlineToolStripMenuItem.Name = "underlineToolStripMenuItem";
            underlineToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.LButton | System.Windows.Forms.Keys.MButton | System.Windows.Forms.Keys.ShiftKey | System.Windows.Forms.Keys.Control;
            underlineToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            underlineToolStripMenuItem.Text = "Underline";
            underlineToolStripMenuItem.Click += new System.EventHandler(underlineToolStripMenuItem_Click);
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new System.Drawing.Size(155, 6);
            cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            cutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Back | System.Windows.Forms.Keys.ShiftKey | System.Windows.Forms.Keys.Control;
            cutToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            cutToolStripMenuItem.Text = "Cut";
            cutToolStripMenuItem.Click += new System.EventHandler(cutToolStripMenuItem_Click);
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.LButton | System.Windows.Forms.Keys.RButton | System.Windows.Forms.Keys.Control;
            copyToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            copyToolStripMenuItem.Text = "Copy";
            copyToolStripMenuItem.Click += new System.EventHandler(copyToolStripMenuItem_Click);
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.RButton | System.Windows.Forms.Keys.MButton | System.Windows.Forms.Keys.ShiftKey | System.Windows.Forms.Keys.Control;
            pasteToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            pasteToolStripMenuItem.Text = "Paste";
            pasteToolStripMenuItem.Click += new System.EventHandler(pasteToolStripMenuItem_Click);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            base.BackColor = System.Drawing.Color.Transparent;
            Controls.Add(ctrlRtf);
            Controls.Add(toolStrip1);
            Name = "CtrlRichTextbox";
            Size = new System.Drawing.Size(460, 252);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private void Italic()
        {
            ChangeFontStyle(System.Drawing.FontStyle.Italic);
        }

        private void italicToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Italic();
        }

        private string ParseOutRTFCode(string s)
        {
            string s1 = "";
            bool flag = false;
            for (int i = 0; i < s.Length; i++)
            {
                if (flag && (s[i] == 32))
                {
                    flag = false;
                }
                else
                {
                    if (!flag)
                    {
                        if (s[i] == 92)
                        {
                            flag = true;
                            continue;
                        }
                        if ((s[i] != 125) && (s[i] != 123))
                            s1 = System.String.Concat(s1, s[i]);
                    }
                }
            }
            return s1;
        }

        private void Paste()
        {
            ctrlRtf.Paste();
        }

        private void pasteToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Paste();
        }

        private void Redo()
        {
            ctrlRtf.Redo();
        }

        private void SetLinksInCtrlRTF(string str)
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder("");
            System.Collections.Generic.List<string> list1 = new System.Collections.Generic.List<string>();
            System.Collections.Generic.List<string> list2 = new System.Collections.Generic.List<string>();
            System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>();
            while (true)
            {
                System.GC.Collect();
                int i1 = str.IndexOf(Oranikle.Studio.Controls.CtrlRichTextbox.RTFLinkStartOpener);
                if (i1 <= 0)
                {
                    stringBuilder.Append(str);
                    break;
                }
                stringBuilder.Append(str.Substring(0, i1 - 1));
                string s1 = ExtractId(ParseOutRTFCode(str.Substring(i1 + Oranikle.Studio.Controls.CtrlRichTextbox.RTFLinkStartOpener.Length, str.IndexOf(Oranikle.Studio.Controls.CtrlRichTextbox.RTFLinkStartCloser) - i1 - Oranikle.Studio.Controls.CtrlRichTextbox.RTFLinkStartOpener.Length))).Trim();
                Oranikle.Studio.Controls.CtrlRichTextbox.log.Debug("Id = '" + s1 + "'");
                string s2 = ParseOutRTFCode(str.Substring(str.IndexOf(Oranikle.Studio.Controls.CtrlRichTextbox.RTFLinkStartCloser) + Oranikle.Studio.Controls.CtrlRichTextbox.RTFLinkStartCloser.Length, str.IndexOf(Oranikle.Studio.Controls.CtrlRichTextbox.RTFLinkEnd) - str.IndexOf(Oranikle.Studio.Controls.CtrlRichTextbox.RTFLinkStartCloser) - Oranikle.Studio.Controls.CtrlRichTextbox.RTFLinkStartCloser.Length)).Replace("\r", "").Replace("\n", "").Trim();
                Oranikle.Studio.Controls.CtrlRichTextbox.log.Debug("linkStr = '" + s2 + "'");
                list1.Add(s1);
                list2.Add(s2);
                using (Oranikle.Studio.Controls.RichTextBoxEx richTextBoxEx = new Oranikle.Studio.Controls.RichTextBoxEx())
                {
                    richTextBoxEx.Rtf = stringBuilder.ToString();
                    list.Add(richTextBoxEx.Text.Length);
                }
                str = str.Substring(str.IndexOf(Oranikle.Studio.Controls.CtrlRichTextbox.RTFLinkEnd) + Oranikle.Studio.Controls.CtrlRichTextbox.RTFLinkEnd.Length);
            }
            ctrlRtf.Rtf = "";
            ctrlRtf.Rtf = stringBuilder.ToString();
            for (int i2 = list1.Count - 1; i2 >= 0; i2--)
            {
                ctrlRtf.InsertLink(list2[i2], list1[i2], list[i2]);
            }
        }

        private void Underline()
        {
            ChangeFontStyle(System.Drawing.FontStyle.Underline);
        }

        private void underlineToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Underline();
        }

        private void Undo()
        {
            ctrlRtf.Undo();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

    } // class CtrlRichTextbox

}

