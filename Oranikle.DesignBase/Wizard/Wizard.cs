using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;

namespace Oranikle.Studio.Controls
{
	/// <summary>
	/// A wizard is the control added to a form to provide a step by step functionality.
	/// It contains <see cref="WizardPage"/>s in the <see cref="Pages"/> collection, which
	/// are containers for other controls. Only one wizard page is shown at a time in the client
	/// are of the wizard.
	/// </summary>
	[Designer(typeof(Oranikle.Studio.Controls.WizardDesigner))]
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(Oranikle.Studio.Controls.Wizard))]
	public class Wizard : System.Windows.Forms.UserControl
    {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Wizard control with designer support
		/// </summary>
		public Wizard()
		{
			//Empty collection of Pages
			vPages = new PageCollection(this);
           

			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}
      

		private void Wizard_Load(object sender, System.EventArgs e)
		{
			//Attempt to activate a page
			ActivatePage(0);

			//Can I add my self as default cancel button
			Form form = this.FindForm();
			if (form != null && DesignMode == false)
				form.CancelButton = btnCancel;
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing ) 
		{
			if( disposing ) 
			{         
				if(components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.pnlButtonDark3d = new System.Windows.Forms.Panel();
            this.pnlButtonBright3d = new System.Windows.Forms.Panel();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.pnlPages = new System.Windows.Forms.TreeView();
            this.btnCancel = new Oranikle.Studio.Controls.StyledButton();
            this.btnNext = new Oranikle.Studio.Controls.StyledButton();
            this.btnBack = new Oranikle.Studio.Controls.StyledButton();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlButtonDark3d
            // 
            this.pnlButtonDark3d.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlButtonDark3d.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlButtonDark3d.Location = new System.Drawing.Point(0, 0);
            this.pnlButtonDark3d.Name = "pnlButtonDark3d";
            this.pnlButtonDark3d.Size = new System.Drawing.Size(621, 1);
            this.pnlButtonDark3d.TabIndex = 2;
            // 
            // pnlButtonBright3d
            // 
            this.pnlButtonBright3d.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pnlButtonBright3d.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlButtonBright3d.Location = new System.Drawing.Point(0, 1);
            this.pnlButtonBright3d.Name = "pnlButtonBright3d";
            this.pnlButtonBright3d.Size = new System.Drawing.Size(621, 1);
            this.pnlButtonBright3d.TabIndex = 1;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnNext);
            this.pnlButtons.Controls.Add(this.btnBack);
            this.pnlButtons.Controls.Add(this.pnlButtonBright3d);
            this.pnlButtons.Controls.Add(this.pnlButtonDark3d);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 377);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(621, 48);
            this.pnlButtons.TabIndex = 0;
            // 
            // pnlPages
            // 
            this.pnlPages.BackColor = System.Drawing.Color.LavenderBlush;
            this.pnlPages.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlPages.Font = new System.Drawing.Font("Tahoma", 9F);
            this.pnlPages.Location = new System.Drawing.Point(0, 0);
            this.pnlPages.Name = "pnlPages";
            this.pnlPages.ShowPlusMinus = false;
            this.pnlPages.ShowRootLines = false;
            this.pnlPages.Size = new System.Drawing.Size(184, 377);
            this.pnlPages.TabIndex = 1;
            this.pnlPages.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.pnlPages_NodeMouseClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnCancel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnCancel.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnCancel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Arial", 9F);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(533, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.OverriddenSize = null;
            this.btnCancel.Size = new System.Drawing.Size(75, 21);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnNext.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnNext.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnNext.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Arial", 9F);
            this.btnNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNext.Location = new System.Drawing.Point(449, 12);
            this.btnNext.Name = "btnNext";
            this.btnNext.OverriddenSize = null;
            this.btnNext.Size = new System.Drawing.Size(75, 21);
            this.btnNext.TabIndex = 0;
            this.btnNext.Text = "&Next >";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            this.btnNext.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnNext_MouseDown);
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnBack.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnBack.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnBack.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Arial", 9F);
            this.btnBack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBack.Location = new System.Drawing.Point(366, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.OverriddenSize = null;
            this.btnBack.Size = new System.Drawing.Size(75, 21);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "< &Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            this.btnBack.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnBack_MouseDown);
            // 
            // Wizard
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pnlPages);
            this.Controls.Add(this.pnlButtons);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Wizard";
            this.Size = new System.Drawing.Size(621, 425);
            this.Load += new System.EventHandler(this.Wizard_Load);
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private PageCollection vPages;
        private Panel pnlButtonDark3d;
        private Panel pnlButtonBright3d;
        protected internal StyledButton btnBack;
        protected internal StyledButton btnNext;
        private StyledButton btnCancel;
        protected internal Panel pnlButtons;
        private TreeView pnlPages;
		/// <summary>
		/// Returns the collection of Pages in the wizard
		/// </summary>
		[Category("Wizard")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public PageCollection Pages
		{
			get
			{
				return vPages;
			}
		}

        public TreeView PageTabs
        {
            get { return pnlPages; }
        }

		private WizardPage vActivePage = null;
		/// <summary>
		/// Gets/Sets the activePage in the wizard
		/// </summary>
		[Category("Wizard")]
		internal int PageIndex
		{
			get
			{
				return vPages.IndexOf(vActivePage);
			}
			set
			{
				//Do I have any pages?
				if(vPages.Count == 0)
				{
					//No then show nothing
					ActivatePage(-1);
					return;
				}
				// Validate the page asked for
				if (value < -1 || value >= vPages.Count)
				{
					throw new ArgumentOutOfRangeException("PageIndex",
						value,
						"The page index must be between 0 and "+Convert.ToString(vPages.Count-1)
						);
				}
				//Select the new page
				ActivatePage(value);
			}
		}

		/// <summary>
		/// Alternative way of getting/Setiing  the current page by using wizardPage objects
		/// </summary>
		public WizardPage Page
		{
			get
			{
				return vActivePage;
			}
			//Dont use this anymore, see Next, Back, NextTo and BackTo
//			set
//			{
//				ActivatePage(value);
//			}
		}


		public void ActivatePage(int index)
		{
			//If the new page is invalid
			if (index <0 || index >= vPages.Count)
			{
				btnNext.Enabled = false;
				btnBack.Enabled = false;

				return;
			}
		

			//Change to the new Page
			WizardPage tWizPage = ((WizardPage) vPages[index]);

			//Really activate the page
			ActivatePage(tWizPage);
		}

		public void ActivatePage(WizardPage page)
		{
			//Deactivate the current
			if (vActivePage != null)
			{
				vActivePage.Visible = false;
			}

			//Activate the new page
			vActivePage = page;

			if (vActivePage != null)
			{
                //Create Links 
              
				//Ensure that this panel displays inside the wizard
				vActivePage.Parent = this;

				if (this.Contains(vActivePage) == false)
				{	
					this.Container.Add(vActivePage);
				}
				//Make it fill the space
				vActivePage.Dock = DockStyle.Fill;
				vActivePage.Visible = true;
				vActivePage.BringToFront();
				vActivePage.FocusFirstTabIndex();
			}
			
			//What should the back button say
			if (this.PageIndex > 0)
			{
				btnBack.Enabled = true;
			}
			else
			{
				btnBack.Enabled = false;
			}

			//What should the Next button say
			if (vPages.IndexOf(vActivePage) < vPages.Count-1
				&& vActivePage.IsFinishPage == false)
			{
				btnNext.Text = "&Next >";
				btnNext.Enabled = true;
				//Don't close the wizard :)
				btnNext.DialogResult = DialogResult.None;
			}
			else
			{
				btnNext.Text = "Fi&nish";
				//Dont allow a finish in designer
				if (DesignMode == true
					&& vPages.IndexOf(vActivePage) == vPages.Count-1)
				{
					btnNext.Enabled = false;
				}
				else
				{
					btnNext.Enabled = true;
					//If Not in design mode then allow a close
					//btnNext.DialogResult = DialogResult.OK;
				}
			}

            CreatePageTabs(this);

           	//Cause a refresh
			if (vActivePage != null)
				vActivePage.Invalidate();
			else
				this.Invalidate();
		}

        private void CreatePageTabs(Wizard wiz)
        {
            wiz.PageTabs.Nodes.Clear();
            foreach (WizardPage page in wiz.Pages)
            {
                //Add Page Tab Links
                TreeNode lblPageLink = page.PageLink;
                if (vActivePage.Equals(page))
                {
                    lblPageLink.BackColor = SystemColors.Highlight;//new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
                    lblPageLink.ForeColor = Color.White;
                }
                else
                {
                    lblPageLink.BackColor = Color.LavenderBlush;
                    lblPageLink.ForeColor = Color.Black;
                    //lblPageLink.NodeFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
                }
                   

                wiz.PageTabs.Nodes.Add(lblPageLink);
            }
        }

        private void pnlPages_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is WizardPage)
            {
                this.ActivatePage(((WizardPage)e.Node.Tag));
            }
        }

      

        private Point GetPageTabLocation(GradientPanel owner, LinkLabel currentLink)
        {
            Point point = new Point(10, 10);
            foreach (Control ctrl in owner.Controls)
            {
                if (ctrl is LinkLabel)
                {
                    point.Y += ctrl.Height;
                    point.Y += 5;
                }
            }
            return point;
        }

		private void btnNext_Click(object sender, System.EventArgs e)
		{
			Next();
		}
	
		private void btnNext_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			//if (DesignMode == true)
				//Next();
		}

		private void btnBack_Click(object sender, System.EventArgs e)
		{
			Back();
		}
		
		private void btnBack_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			//if (DesignMode == true)
				//Back();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			CancelEventArgs arg = new CancelEventArgs();

			//Throw the event out to subscribers
			if (CloseFromCancel != null)
			{
				CloseFromCancel(this, arg);
			}
			//If nobody told me to cancel
			if (arg.Cancel == false)
			{
				//Then we close the form
				this.FindForm().Close();
			}
		}

		/// <summary>
		/// Gets/Sets the enabled state of the Next button. 
		/// </summary>
		[Category("Wizard")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool NextEnabled
		{
			get
			{
				return btnNext.Enabled;
			}
			set
			{
				btnNext.Enabled = value;
			}
		}
		/// <summary>
		/// Gets/Sets the enabled state of the back button. 
		/// </summary>
		[Category("Wizard")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool BackEnabled
		{
			get
			{
				return btnBack.Enabled;
			}
			set
			{
				btnBack.Enabled = value;
			}
		}
		/// <summary>
		/// Gets/Sets the enabled state of the cancel button. 
		/// </summary>
		[Category("Wizard")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CancelEnabled
		{
			get
			{
				return btnCancel.Enabled;
			}
			set
			{
		
				btnCancel.Enabled = value;
			}
		}


		/// <summary>
		/// Called when the cancel button is pressed, before the form is closed. Set e.Cancel to true if 
		/// you do not wish the cancel to close the wizard.
		/// </summary>
		public event CancelEventHandler CloseFromCancel;

        public event EventHandler Finish_Clicked;

        /// <summary>
		/// Closes the current page after a <see cref="WizardPage.CloseFromNext"/>, then moves to 
		/// the Next page and calls <see cref="WizardPage.ShowFromNext"/>
		/// </summary>
		public void Next()
		{
			Debug.Assert(this.PageIndex >=0,"Page Index was below 0");
			//Tell the Application I just closed a Page
			int newPage = vActivePage.OnCloseFromNext(this);

			//Did I just press Finish instead of Next
			if (this.PageIndex <vPages.Count -1
				&& (vActivePage.IsFinishPage == false || DesignMode == true))
			{
				//No still going
				ActivatePage(newPage);
				//Tell the application, I have just shown a page
				vActivePage.OnShowFromNext(this);
			}
			else
			{
				Debug.Assert(this.PageIndex < vPages.Count, "Error I've just gone past the finish",
					"btnNext_Click tried to go to page "+Convert.ToString(this.PageIndex+1)
					+", but I only have "+Convert.ToString(vPages.Count));

				//yep Finish was pressed
                if (DesignMode == false)
                {
                    //this.ParentForm.Close();
                    if (Finish_Clicked != null)
                    {
                        Finish_Clicked.Invoke(this, EventArgs.Empty);
                    }
                }
			}
		}

		/// <summary>
		/// Moves to the page given and calls <see cref="WizardPage.ShowFromNext"/>
		/// </summary>
		/// <remarks>Does NOT call <see cref="WizardPage.CloseFromNext"/> on the current page</remarks>
		/// <param name="page"></param>
		public void NextTo(WizardPage page)
		{
			//Since we have a page to go to, then there is no need to validate most of it
			ActivatePage(page);
			//Tell the application, I have just shown a page
			vActivePage.OnShowFromNext(this);
		}

		/// <summary>
		/// Closes the current page after a <see cref="WizardPage.CloseFromBack"/>, then moves to 
		/// the previous page and calls <see cref="WizardPage.ShowFromBack"/>
		/// </summary>
		public void Back()
		{
			Debug.Assert(this.PageIndex <vPages.Count,"Page Index was beyond Maximum pages");
			//Can I press back
			Debug.Assert(this.PageIndex>0 && this.PageIndex < vPages.Count,"Attempted to go back to a page that doesn't exist");
			//Tell the application that I closed a page
			int newPage = vActivePage.OnCloseFromBack(this);

			ActivatePage(newPage);
			//Tell the application I have shown a page
			vActivePage.OnShowFromBack(this);
		}

		/// <summary>
		/// Moves to the page given and calls <see cref="WizardPage.ShowFromBack"/>
		/// </summary>
		/// <remarks>Does NOT call <see cref="WizardPage.CloseFromBack"/> on the current page</remarks>
		/// <param name="page"></param>
		public void BackTo(WizardPage page)
		{
			//Since we have a page to go to, then there is no need to validate most of it
			ActivatePage(page);
			//Tell the application, I have just shown a page
			vActivePage.OnShowFromNext(this);
		}

#if DEBUG
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint (e);

			if (DesignMode)
			{
				const string noPagesText = "No wizard pages inside the wizard.";

				
				SizeF textSize = e.Graphics.MeasureString(noPagesText, this.Font);
				RectangleF layout = new RectangleF( (this.Width- textSize.Width+pnlPages.Width)/2,
					(this.pnlButtons.Top-textSize.Height)/2,
					textSize.Width, textSize.Height);
		
				Pen dashPen = (Pen) SystemPens.GrayText.Clone();
				dashPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                //e.Graphics.DrawRectangle(dashPen,
                //    this.Left+8, this.Top+8, 
                //    this.Width-17, this.pnlButtons.Top-17);

				e.Graphics.DrawString(noPagesText,this.Font,new SolidBrush(SystemColors.GrayText) ,layout);	
			}
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize (e);

			if (DesignMode)
			{
				this.Invalidate();
			}
		}

      
       

#endif


	}
}