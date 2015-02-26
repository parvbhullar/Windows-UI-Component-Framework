using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Oranikle.Studio.Controls
{
	/// <summary>
	/// Summary description for WizardHeader.
	/// </summary>
	[Designer(typeof(HeaderDesigner))]
	public class Header : UserControl
	{
        private System.Windows.Forms.Panel pnlDockPadding;
		private System.Windows.Forms.Panel pnl3dDark;
		private System.Windows.Forms.Panel pnl3dBright;
        private Label lblTitle;
        private Label lblDescription;
        private Label label1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Constructor for Header
		/// </summary>
		public Header()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
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
            this.pnlDockPadding = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnl3dDark = new System.Windows.Forms.Panel();
            this.pnl3dBright = new System.Windows.Forms.Panel();
            this.pnlDockPadding.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDockPadding
            // 
            this.pnlDockPadding.BackColor = System.Drawing.SystemColors.Window;
            this.pnlDockPadding.BackgroundImage = global::Oranikle.Studio.Controls.Properties.Resources.software_wizard_header1;
            this.pnlDockPadding.Controls.Add(this.label1);
            this.pnlDockPadding.Controls.Add(this.lblDescription);
            this.pnlDockPadding.Controls.Add(this.lblTitle);
            this.pnlDockPadding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDockPadding.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlDockPadding.Location = new System.Drawing.Point(0, 0);
            this.pnlDockPadding.Name = "pnlDockPadding";
            this.pnlDockPadding.Padding = new System.Windows.Forms.Padding(8, 6, 4, 4);
            this.pnlDockPadding.Size = new System.Drawing.Size(622, 73);
            this.pnlDockPadding.TabIndex = 6;
            this.pnlDockPadding.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlDockPadding_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(542, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 35);
            this.label1.TabIndex = 2;
            this.label1.Text = "Accy";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblDescription.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblDescription.ForeColor = System.Drawing.Color.White;
            this.lblDescription.Location = new System.Drawing.Point(24, 32);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(71, 13);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Description";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(23, 11);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(188, 18);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Welcome to accy wizard";
            // 
            // pnl3dDark
            // 
            this.pnl3dDark.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnl3dDark.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl3dDark.Location = new System.Drawing.Point(0, 71);
            this.pnl3dDark.Name = "pnl3dDark";
            this.pnl3dDark.Size = new System.Drawing.Size(622, 1);
            this.pnl3dDark.TabIndex = 7;
            // 
            // pnl3dBright
            // 
            this.pnl3dBright.BackColor = System.Drawing.Color.White;
            this.pnl3dBright.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl3dBright.Location = new System.Drawing.Point(0, 72);
            this.pnl3dBright.Name = "pnl3dBright";
            this.pnl3dBright.Size = new System.Drawing.Size(622, 1);
            this.pnl3dBright.TabIndex = 8;
            // 
            // Header
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CausesValidation = false;
            this.Controls.Add(this.pnl3dDark);
            this.Controls.Add(this.pnl3dBright);
            this.Controls.Add(this.pnlDockPadding);
            this.Name = "Header";
            this.Size = new System.Drawing.Size(622, 73);
            this.SizeChanged += new System.EventHandler(this.Header_SizeChanged);
            this.pnlDockPadding.ResumeLayout(false);
            this.pnlDockPadding.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void ResizeImageAndText()
		{
			
		}

		private void Header_SizeChanged(object sender, System.EventArgs e)
		{
			ResizeImageAndText();
		}

		/// <summary>
		/// Get/Set the title for the wizard page
		/// </summary>
		[Category("Appearance")]
		public string Title
		{
			get
			{
				return lblTitle.Text;
			}
			set
			{
                lblTitle.Text = value;
			}
		}

		/// <summary>
		/// Gets/Sets the
		/// </summary>
		[Category("Appearance")]
		public string Description
		{
			get
			{
				return lblDescription.Text;
			}
			set
			{
				lblDescription.Text = value;
			}
		}

		/// <summary>
		/// Gets/Sets the Icon
		/// </summary>
		[Category("Appearance")]
		public Image Image
		{
			get
			{
				return this.BackgroundImage;
			}
			set
			{
                this.BackgroundImage = value;
				ResizeImageAndText();
			}
		}

        private void pnlDockPadding_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
	}
}
