using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using System.Drawing.Design;

using System.Windows.Forms;


namespace Oranikle.Studio.Controls.Wizards
{
  [ToolboxItem(false)]
  [Designer( "Oranikle.Studio.Controls.Designers.WizardWelcomePageDesigner, Oranikle.Studio.Controls.Designers, Version=1.0.4.0, Culture=neutral, PublicKeyToken=fe06e967d3cf723d" )]
  public class WizardWelcomePage : Oranikle.Studio.Controls.Wizards.WizardPageBase
  {
    #region Form controls
    private System.Windows.Forms.CheckBox chkDontShow;
    private System.Windows.Forms.Label lblDescription2;
    private System.Windows.Forms.Label lblDescription;
    private System.Windows.Forms.Label lblHint;
    private System.Windows.Forms.Label lblWizardName;
    private System.Windows.Forms.PictureBox imgWatermark;
    private System.Windows.Forms.PictureBox imgWelcome;
    private System.ComponentModel.IContainer components = null;
    #endregion

    #region Class Properties
    protected override System.Drawing.Size DefaultSize
    {
      get
      {
        return new Size( 498, 328 );
      }
    }
    
    [Browsable(true)]
    [Category("Wizard Page")]
    [Description("Is checkbox \"Don't show...\" checked.")]
    [DefaultValue(false)]
    public bool DontShow
    {
      get
      {
        return chkDontShow.Checked;
      }
      set
      {
        if( value != chkDontShow.Checked )
        {
          chkDontShow.Checked = value;
          OnDontShowChanged();
          OnChanged();
        }
      }
    }
    
    [Browsable(true)]
    [Category("Wizard Page")]
    [Description("Gets/Sets wizard page second description Text. This description used only by welocme and final pages")]
    public string Description2
    {
      get
      {
        // if control not intialized yet
        if( lblDescription2 == null ) return string.Empty;

        return lblDescription2.Text;
      }
      set
      {
        // skip text set if control not created yet
        if( lblDescription2 == null ) return;

        if( value != lblDescription2.Text )
        {
          lblDescription2.Text = value;
          OnDescription2Changed();
        }
      }
    }
    #endregion

    #region Class Initialize/Finilize methods
    public WizardWelcomePage()
    {
      InitializeComponent();
      
      this.Size = new Size( 498, 328 );
      this.Name = "wizWelcomePage";
      base.WelcomePage = true;

      base.Title = lblWizardName.Text;
      base.Description = lblDescription.Text;
      base.HeaderImage = imgWelcome.Image;

      lblDescription2.SizeChanged += new EventHandler( OnLabelSizeChanged );
    }

    protected override void Dispose( bool disposing )
    {
      if( disposing && components != null )
      {
        components.Dispose();
      }

      base.Dispose( disposing );
    }
    #endregion

    #region Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.chkDontShow = new System.Windows.Forms.CheckBox();
        this.lblDescription2 = new System.Windows.Forms.Label();
        this.lblDescription = new System.Windows.Forms.Label();
        this.lblHint = new System.Windows.Forms.Label();
        this.lblWizardName = new System.Windows.Forms.Label();
        this.imgWatermark = new System.Windows.Forms.PictureBox();
        this.imgWelcome = new System.Windows.Forms.PictureBox();
        ((System.ComponentModel.ISupportInitialize)(this.imgWatermark)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.imgWelcome)).BeginInit();
        this.SuspendLayout();
        // 
        // chkDontShow
        // 
        this.chkDontShow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.chkDontShow.BackColor = System.Drawing.Color.Transparent;
        this.chkDontShow.Location = new System.Drawing.Point(200, 528);
        this.chkDontShow.Name = "chkDontShow";
        this.chkDontShow.Size = new System.Drawing.Size(1092, 16);
        this.chkDontShow.TabIndex = 9;
        this.chkDontShow.Text = "&Do not show this Welcome page again";
        this.chkDontShow.UseVisualStyleBackColor = false;
        // 
        // lblDescription2
        // 
        this.lblDescription2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.lblDescription2.BackColor = System.Drawing.Color.Transparent;
        this.lblDescription2.Location = new System.Drawing.Point(200, 160);
        this.lblDescription2.Name = "lblDescription2";
        this.lblDescription2.Size = new System.Drawing.Size(1092, 127);
        this.lblDescription2.TabIndex = 7;
        this.lblDescription2.Text = "some more description...";
        // 
        // lblDescription
        // 
        this.lblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.lblDescription.BackColor = System.Drawing.Color.Transparent;
        this.lblDescription.Location = new System.Drawing.Point(200, 88);
        this.lblDescription.Name = "lblDescription";
        this.lblDescription.Size = new System.Drawing.Size(1092, 64);
        this.lblDescription.TabIndex = 6;
        this.lblDescription.Text = "This wizard will guide your throw process of ...";
        // 
        // lblHint
        // 
        this.lblHint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.lblHint.BackColor = System.Drawing.Color.Transparent;
        this.lblHint.Location = new System.Drawing.Point(200, 504);
        this.lblHint.Name = "lblHint";
        this.lblHint.Size = new System.Drawing.Size(1092, 16);
        this.lblHint.TabIndex = 8;
        this.lblHint.Text = "To continue, click Next";
        this.lblHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // lblWizardName
        // 
        this.lblWizardName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.lblWizardName.BackColor = System.Drawing.Color.Transparent;
        this.lblWizardName.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.lblWizardName.Location = new System.Drawing.Point(200, 16);
        this.lblWizardName.Name = "lblWizardName";
        this.lblWizardName.Size = new System.Drawing.Size(1092, 64);
        this.lblWizardName.TabIndex = 5;
        this.lblWizardName.Text = "Welcome to ...";
        // 
        // imgWatermark
        // 
        this.imgWatermark.BackColor = System.Drawing.SystemColors.Control;
        this.imgWatermark.Location = new System.Drawing.Point(104, 16);
        this.imgWatermark.Name = "imgWatermark";
        this.imgWatermark.Size = new System.Drawing.Size(61, 61);
        this.imgWatermark.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        this.imgWatermark.TabIndex = 15;
        this.imgWatermark.TabStop = false;
        // 
        // imgWelcome
        // 
        this.imgWelcome.Dock = System.Windows.Forms.DockStyle.Left;
        this.imgWelcome.Location = new System.Drawing.Point(0, 0);
        this.imgWelcome.Name = "imgWelcome";
        this.imgWelcome.Size = new System.Drawing.Size(184, 552);
        this.imgWelcome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        this.imgWelcome.TabIndex = 14;
        this.imgWelcome.TabStop = false;
        // 
        // WizardWelcomePage
        // 
        this.BackColor = System.Drawing.Color.White;
        this.Controls.Add(this.imgWatermark);
        this.Controls.Add(this.imgWelcome);
        this.Controls.Add(this.chkDontShow);
        this.Controls.Add(this.lblDescription2);
        this.Controls.Add(this.lblDescription);
        this.Controls.Add(this.lblHint);
        this.Controls.Add(this.lblWizardName);
        this.Name = "WizardWelcomePage";
        this.Size = new System.Drawing.Size(1310, 552);
        ((System.ComponentModel.ISupportInitialize)(this.imgWatermark)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.imgWelcome)).EndInit();
        this.ResumeLayout(false);

    }
    #endregion

    #region Class helper methods
    private void OnLabelSizeChanged( object sender, EventArgs e )
    {
      OnResize( e );
    }
    #endregion

    #region Class Overrides
    protected override void OnResize(System.EventArgs e)
    {
      base.OnResize( e );

      if( Description2 != null && Description2.Length > 0 )
      {
        Bitmap bmp = new Bitmap( lblDescription2.Width, lblDescription2.Height );
        Graphics g = Graphics.FromImage( bmp );
        //this.CreateGraphics();

        StringFormat format = new StringFormat();
        format.Trimming = StringTrimming.Word;

        SizeF size = g.MeasureString( Description2, lblDescription2.Font, lblDescription2.Width, format );

        lblDescription2.Height = (int)size.Height + 16;
        lblHint.Top = lblDescription2.Bottom + 8;

        g.Dispose();
        bmp.Dispose();
      }
    }
    protected override void OnImageListChanged()
    {
      if( base.ImageList != null )
      {
        if( base.ImageIndex >= 0 && ImageIndex < base.ImageList.Images.Count )
        {
          imgWatermark.Image = base.ImageList.Images[ base.ImageIndex ];
        }
      }
    }

    protected override void OnImageIndexChanged()
    {
      if( base.ImageList != null )
      {
        if( base.ImageIndex >= 0 && ImageIndex < base.ImageList.Images.Count )
        {
          imgWatermark.Image = base.ImageList.Images[ base.ImageIndex ];
        }
      }
    }

    protected override void OnHeaderImageChanged()
    {
      imgWelcome.Image = base.HeaderImage;
    }

    protected override void OnTitleChanged()
    {
      lblWizardName.Text = base.Title;
    }

    protected override void OnDescriptionChanged()
    {
      lblDescription.Text = base.Description;
    }

    protected override void OnLoad(System.EventArgs e)
    {
      OnImageListChanged();
      OnHeaderImageChanged();
      base.OnLoad(e);
    }
    protected virtual  void OnDescription2Changed()
    {
      OnResize( EventArgs.Empty );
    }

    protected virtual  void OnDontShowChanged()
    {
      
    }
    #endregion
  }
}

