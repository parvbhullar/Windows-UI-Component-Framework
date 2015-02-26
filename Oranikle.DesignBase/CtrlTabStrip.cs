using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;

namespace Oranikle.Studio.Controls
{

    public class CtrlTabStrip : System.Windows.Forms.ToolStrip
    {
        private System.ComponentModel.Design.DesignerVerb insPage;

        private int minNumOfTabButtons;
        private Oranikle.Studio.Controls.CtrlTabStripRenderer myRenderer;
        protected Oranikle.Studio.Controls.CtrlTabStripButton mySelTab;
        public delegate void ItemBeforeRemoveEventHandler(object sender, Oranikle.Studio.Controls.ItemBeforeRemoveEventArgs e);
        public delegate void SelectedTabChangedEventHandler(object sender, Oranikle.Studio.Controls.SelectedTabChangedEventArgs e);
        public event ItemBeforeRemoveEventHandler ItemBeforeRemove;
        public event SelectedTabChangedEventHandler SelectedTabChanged;

        [System.ComponentModel.Description("Border color of the TabStrip")]
        [System.ComponentModel.Category("Appearance")]
        public System.Drawing.Color BorderColor
        {
            get
            {
                return myRenderer.BorderColor;
            }
            set
            {
                myRenderer.BorderColor = value;
                Invalidate();
            }
        }

        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.Description("Specifies if TabButtons should be drawn flipped (for right- and bottom-aligned CtrlTabStrips)")]
        public bool FlipButtons
        {
            get
            {
                return myRenderer.Mirrored;
            }
            set
            {
                myRenderer.Mirrored = value;
                Invalidate();
            }
        }

        public new System.Windows.Forms.ToolStripLayoutStyle LayoutStyle
        {
            get
            {
                return base.LayoutStyle;
            }
            set
            {
                switch (value)
                {
                    case System.Windows.Forms.ToolStripLayoutStyle.StackWithOverflow:
                    case System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow:
                    case System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow:
                        base.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.StackWithOverflow;
                        return;

                    case System.Windows.Forms.ToolStripLayoutStyle.Table:
                        base.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
                        return;

                    case System.Windows.Forms.ToolStripLayoutStyle.Flow:
                        base.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
                        return;
                }
                base.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.StackWithOverflow;
            }
        }

        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.Description("Minimum number of tab buttons.")]
        public int MinNumOfTabButtons
        {
            get
            {
                return minNumOfTabButtons;
            }
            set
            {
                minNumOfTabButtons = value;
            }
        }

        [System.ComponentModel.Browsable(false)]
        public new System.Windows.Forms.Padding Padding
        {
            get
            {
                return base.DefaultPadding;
            }
            set
            {
            }
        }

        public new System.Windows.Forms.ToolStripRenderer Renderer
        {
            get
            {
                return myRenderer;
            }
            set
            {
                base.Renderer = myRenderer;
            }
        }

        [System.Obsolete("Use RenderStyle instead")]
        [System.ComponentModel.Browsable(false)]
        public new System.Windows.Forms.ToolStripRenderMode RenderMode
        {
            get
            {
                return base.RenderMode;
            }
            set
            {
                RenderStyle = value;
            }
        }

        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.Description("Gets or sets render style for CtrlTabStrip. You should use this property instead of RenderMode.")]
        public System.Windows.Forms.ToolStripRenderMode RenderStyle
        {
            get
            {
                return myRenderer.RenderMode;
            }
            set
            {
                myRenderer.RenderMode = value;
                Invalidate();
            }
        }

        public Oranikle.Studio.Controls.CtrlTabStripButton SelectedTab
        {
            get
            {
                return mySelTab;
            }
            set
            {
                if (value == null)
                    return;
                if (mySelTab == value)
                    return;
                if (value.Owner != this)
                    throw new System.ArgumentException("Cannot select TabButtons that do not belong to this CtrlTabStrip");
                base.OnItemClicked(new System.Windows.Forms.ToolStripItemClickedEventArgs(value));
                Invalidate();
            }
        }

        public int TabCount
        {
            get
            {
                int i = 0;
                foreach (System.Windows.Forms.ToolStripItem toolStripItem in Items)
                {
                    if ((toolStripItem is Oranikle.Studio.Controls.CtrlTabStripButton))
                        i++;
                }
                return i;
            }
        }

        [System.ComponentModel.Description("Specifies if CtrlTabStrip should use system visual styles for painting items")]
        [System.ComponentModel.Category("Appearance")]
        public bool UseVisualStyles
        {
            get
            {
                return myRenderer.UseVS;
            }
            set
            {
                myRenderer.UseVS = value;
                Invalidate();
            }
        }

        protected override System.Windows.Forms.Padding DefaultPadding
        {
            get
            {
                return System.Windows.Forms.Padding.Empty;
            }
        }

        public override System.ComponentModel.ISite Site
        {
            get
            {
                System.ComponentModel.ISite isite = base.Site;
                if ((isite != null) && isite.DesignMode)
                {
                    System.ComponentModel.IContainer icontainer = isite.Container;
                    if (icontainer != null)
                    {
                        System.ComponentModel.Design.IDesignerHost idesignerHost = icontainer as System.ComponentModel.Design.IDesignerHost;
                        if (idesignerHost != null)
                        {
                            System.ComponentModel.Design.IDesigner idesigner = idesignerHost.GetDesigner(isite.Component);
                            if ((idesigner != null) && !idesigner.Verbs.Contains(insPage))
                                idesigner.Verbs.Add(insPage);
                        }
                    }
                }
                return isite;
            }
            set
            {
                base.Site = value;
            }
        }

        public CtrlTabStrip()
        {
            myRenderer = new Oranikle.Studio.Controls.CtrlTabStripRenderer();
            minNumOfTabButtons = 2;
            InitControl();
        }

        public CtrlTabStrip(params Oranikle.Studio.Controls.CtrlTabStripButton[] buttons) : base(buttons)
        {
            myRenderer = new Oranikle.Studio.Controls.CtrlTabStripRenderer();
            minNumOfTabButtons = 2;
            InitControl();
        }

        public bool FireItemBeforeRemoveEvent(Oranikle.Studio.Controls.ItemBeforeRemoveEventArgs e)
        {
            ItemBeforeRemove.Invoke(this, e);
            return e.Cancel;
        }

        protected void InitControl()
        {
            base.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            base.Renderer = myRenderer;
            myRenderer.RenderMode = RenderStyle;
            insPage = new System.ComponentModel.Design.DesignerVerb("Insert tab page", new System.EventHandler(OnInsertPageClicked));
        }

        protected void OnInsertPageClicked(object sender, System.EventArgs e)
        {
            System.ComponentModel.ISite isite = base.Site;
            if ((isite != null) && isite.DesignMode)
            {
                System.ComponentModel.IContainer icontainer = isite.Container;
                if (icontainer != null)
                {
                    Oranikle.Studio.Controls.CtrlTabStripButton ctrlTabStripButton = new Oranikle.Studio.Controls.CtrlTabStripButton();
                    icontainer.Add(ctrlTabStripButton);
                    ctrlTabStripButton.Text = ctrlTabStripButton.Name;
                }
            }
        }

        protected bool OnTabSelected(Oranikle.Studio.Controls.CtrlTabStripButton beforeTab, Oranikle.Studio.Controls.CtrlTabStripButton afterTab)
        {
            if (SelectedTabChanged != null)
            {
                Oranikle.Studio.Controls.SelectedTabChangedEventArgs selectedTabChangedEventArgs = new Oranikle.Studio.Controls.SelectedTabChangedEventArgs(beforeTab, afterTab);
                SelectedTabChanged.Invoke(this, selectedTabChangedEventArgs);
                return selectedTabChangedEventArgs.Cancel;
            }
            return false;
        }

        protected override void OnItemAdded(System.Windows.Forms.ToolStripItemEventArgs e)
        {
            base.OnItemAdded(e);
        }

        protected override void OnItemClicked(System.Windows.Forms.ToolStripItemClickedEventArgs e)
        {
            Oranikle.Studio.Controls.CtrlTabStripButton ctrlTabStripButton = e.ClickedItem as Oranikle.Studio.Controls.CtrlTabStripButton;
            if ((ctrlTabStripButton != null) && (ctrlTabStripButton != mySelTab))
            {
                bool flag = OnTabSelected(mySelTab, ctrlTabStripButton);
                if (!flag)
                {
                    SuspendLayout();
                    mySelTab = ctrlTabStripButton;
                    ResumeLayout();
                    base.OnItemClicked(e);
                    Invalidate();
                }
            }
        }

    } // class CtrlTabStrip

}

