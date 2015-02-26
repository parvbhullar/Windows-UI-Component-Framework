using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace Oranikle.Studio.Controls.Wizards
{
  [ToolboxItem(false)]
  [Designer( "Oranikle.Studio.Controls.Designers.WizardPageDesigner, Oranikle.Studio.Controls.Designers, Version=1.0.4.0, Culture=neutral, PublicKeyToken=fe06e967d3cf723d" )]
  public class WizardFinalPage : Oranikle.Studio.Controls.Wizards.WizardPageBase
  {
    #region Page controls

    private System.Windows.Forms.Label lblFinishHint;
    private System.Windows.Forms.PictureBox imgWatermark;
    private System.Windows.Forms.Label lblDescription;
    private System.Windows.Forms.Label lblDescription2;
    private System.Windows.Forms.PictureBox imgWelcome;
    private System.Windows.Forms.Label lblWizardName;
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
    [Description("Gets/Sets wizard page second description Text. This description used only by welocme and final pages")]
    public string Description2
    {
      get
      {
        return lblDescription2.Text;
      }
      set
      {
        if( value != lblDescription2.Text )
        {
          lblDescription2.Text = value;
          OnDescription2Changed();
        }
      }
    }
    #endregion    

    #region Class Constructor/Finilize methods
    public WizardFinalPage()
    {
      InitializeComponent();
      
      this.Size = new Size( 498, 328 );
      this.Name = "wizFinalPage";
      base.WelcomePage = true;

      base.Title       = lblWizardName.Text;
      base.Description = lblDescription.Text;
      base.HeaderImage = imgWelcome.Image;
      base.FinishPage  = true;
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
        this.imgWatermark = new System.Windows.Forms.PictureBox();
        this.lblDescription = new System.Windows.Forms.Label();
        this.lblWizardName = new System.Windows.Forms.Label();
        this.imgWelcome = new System.Windows.Forms.PictureBox();
        this.lblFinishHint = new System.Windows.Forms.Label();
        this.lblDescription2 = new System.Windows.Forms.Label();
        ((System.ComponentModel.ISupportInitialize)(this.imgWatermark)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.imgWelcome)).BeginInit();
        this.SuspendLayout();
        // 
        // imgWatermark
        // 
        this.imgWatermark.BackColor = System.Drawing.Color.Transparent;
        this.imgWatermark.Location = new System.Drawing.Point(88, 16);
        this.imgWatermark.Name = "imgWatermark";
        this.imgWatermark.Size = new System.Drawing.Size(61, 61);
        this.imgWatermark.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
        this.imgWatermark.TabIndex = 12;
        this.imgWatermark.TabStop = false;
        // 
        // lblDescription
        // 
        this.lblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.lblDescription.BackColor = System.Drawing.Color.Transparent;
        this.lblDescription.Location = new System.Drawing.Point(184, 64);
        this.lblDescription.Name = "lblDescription";
        this.lblDescription.Size = new System.Drawing.Size(1108, 64);
        this.lblDescription.TabIndex = 10;
        // 
        // lblWizardName
        // 
        this.lblWizardName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.lblWizardName.BackColor = System.Drawing.Color.Transparent;
        this.lblWizardName.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.lblWizardName.Location = new System.Drawing.Point(184, 8);
        this.lblWizardName.Name = "lblWizardName";
        this.lblWizardName.Size = new System.Drawing.Size(1108, 48);
        this.lblWizardName.TabIndex = 9;
        // 
        // imgWelcome
        // 
        this.imgWelcome.Dock = System.Windows.Forms.DockStyle.Left;
        this.imgWelcome.Location = new System.Drawing.Point(0, 0);
        this.imgWelcome.Name = "imgWelcome";
        this.imgWelcome.Size = new System.Drawing.Size(168, 552);
        this.imgWelcome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        this.imgWelcome.TabIndex = 8;
        this.imgWelcome.TabStop = false;
        // 
        // lblFinishHint
        // 
        this.lblFinishHint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.lblFinishHint.BackColor = System.Drawing.Color.Transparent;
        this.lblFinishHint.Location = new System.Drawing.Point(184, 528);
        this.lblFinishHint.Name = "lblFinishHint";
        this.lblFinishHint.Size = new System.Drawing.Size(1108, 16);
        this.lblFinishHint.TabIndex = 11;
        this.lblFinishHint.Text = "To close this Wizard, click Finish.";
        // 
        // lblDescription2
        // 
        this.lblDescription2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.lblDescription2.Location = new System.Drawing.Point(184, 136);
        this.lblDescription2.Name = "lblDescription2";
        this.lblDescription2.Size = new System.Drawing.Size(1108, 369);
        this.lblDescription2.TabIndex = 13;
        // 
        // WizardFinalPage
        // 
        this.BackColor = System.Drawing.Color.White;
        this.Controls.Add(this.lblDescription2);
        this.Controls.Add(this.imgWatermark);
        this.Controls.Add(this.lblDescription);
        this.Controls.Add(this.lblWizardName);
        this.Controls.Add(this.imgWelcome);
        this.Controls.Add(this.lblFinishHint);
        this.Name = "WizardFinalPage";
        this.Size = new System.Drawing.Size(1310, 552);
        ((System.ComponentModel.ISupportInitialize)(this.imgWatermark)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.imgWelcome)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }
    #endregion

    #region Class overrides
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
    
    protected virtual  void OnDescription2Changed()
    {
    }
    protected override void OnLoad(System.EventArgs e)
    {
      OnImageListChanged();
      OnHeaderImageChanged();
      base.OnLoad(e);
    }
    #endregion
  }
}

