using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using System.Drawing.Design;


namespace Oranikle.Studio.Controls.Wizards
{
  [ToolboxItem(false)]
  [Designer( "Oranikle.Studio.Controls.Designers.WizardPageDesigner, Oranikle.Studio.Controls.Designers, Version=1.0.4.0, Culture=neutral, PublicKeyToken=fe06e967d3cf723d" )]
  public class WizardPageBase : System.Windows.Forms.UserControl
  {
    #region Class Members
    private System.ComponentModel.Container components = null;  // container
    private WizardForm m_parent = null;                         // page parent

    private int     m_iIndex = -1;                              // Order identifier of page
    private string  m_strTitle;                                 // Page Title
    private string  m_strDescription;                           // Page Desription
    private Image   m_imgHeader;                                // Page Image
    private bool    m_bFireEvents = true;                       // Identifier of QUIET mode
    private bool    m_bFinishPage;                              // Is Wizard Page a Finish Page
    private bool    m_bHelpExists;                              // Does page has Help or Not
    private bool    m_bIsWelcome;                               // Is Page Use Full Wizard Panel or not
    
    private ImageList m_imgList;
    private int       m_iImageIndex = -1;
    #endregion

    #region Class Properties
    [Browsable(true)]
    [DefaultValue(-1)]
    [Description("Order position of wizard page in Wizard Control")]
    [Category("Wizard Page")]
    public int Index
    {
      get
      {
        return m_iIndex;
      }
      set
      {
        if( value != m_iIndex )
        {
          m_iIndex = value;
          OnIndexChanged();
          OnChanged();
        }
      }
    }

    [Browsable(false)]
    [Description("Gets/Sets Page Parent control")]
    public WizardForm WizardPageParent
    {
      get
      {
        return m_parent;
      }
      set
      {
        if( value != m_parent )
        {
          m_parent = value;
          OnWizardPageParentChanged();
        }
      }
    }

    [Browsable(true)]
    [Category("Wizard Page")]
    [Description("Gets/Sets wizard page Title Text")]
    public string Title
    {
      get
      {
        return m_strTitle;
      }
      set
      {
        if( value != m_strTitle )
        {
          m_strTitle = value;
          OnTitleChanged();
          OnChanged();
        }
      }
    }

    [Browsable(true)]
    [Category("Wizard Page")]
    [Description("Gets/Sets wizard page description Text")]
    public string Description
    {
      get
      {
        return m_strDescription;
      }
      set
      {
        if( value != m_strDescription )
        {
          m_strDescription = value;
          OnDescriptionChanged();
          OnChanged();
        }
      }
    }

    [Browsable(true)]
    [Category("Wizard Page")]
    [Description("Gets/Sets wizard page special Image")]
    [DefaultValue(null)]
    public Image HeaderImage
    {
      get
      {
        return m_imgHeader;
      }
      set
      {
        if( value != m_imgHeader )
        {
          m_imgHeader = value;
          OnHeaderImageChanged();
          OnChanged();
        }
      }
    }

    [Browsable(true)]
    [Category("Wizard Page")]
    [Description("Gets/Sets wizard page special ImageList")]
    [DefaultValue(null)]
    public ImageList ImageList
    {
      get
      {
        return m_imgList;
      }
      set
      {
        if( value != m_imgList )
        {
          m_imgList = value;
          OnImageListChanged();
          OnChanged();
        }
      }
    }

    [Browsable(true)]
    [Category("Wizard Page")]
    [Description("Gets/Sets Image from ImageList which will be used as a HeaderImage")]
    [DefaultValue(-1)]
    [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
    [TypeConverter("System.Windows.Forms.ImageIndexConverter")]
    public int ImageIndex
    {
      get
      {
        return m_iImageIndex;
      }
      set
      {
        if( value != m_iImageIndex )
        {
          m_iImageIndex = value;
          OnImageIndexChanged();
          OnChanged();
        }
      }
    }

    [Browsable(false)]
    [DefaultValue(false)]
    public bool QuietMode
    {
      get
      {
        return !m_bFireEvents;
      }
      set
      {
        if( value != m_bFireEvents )
        {
          m_bFireEvents = !value;
          OnQuietModeChanged();
        }
      }
    }
    
    [Browsable(true)]
    [Category("Wizard Page")]
    [Description("Gets/Sets Is wizard page last in Wizard Control")]
    [DefaultValue(false)]
    public bool FinishPage
    {
      get
      {
        return m_bFinishPage;
      }
      set
      {
        if( value != m_bFinishPage )
        {
          m_bFinishPage = value;
          OnFinishPageChanged();
          OnChanged();
        }
      }
    }

    [Browsable(true)]
    [Category("Wizard Page")]
    [Description("Gets/Sets Is wizard page Has help Page")]
    [DefaultValue(false)]
    public bool HelpAvailable
    {
      get
      {
        return m_bHelpExists;
      }
      set
      {
        if( value != m_bHelpExists )
        {
          m_bHelpExists = value;
          OnHelpAvailableChangd();
          OnChanged();
        }
      }
    }

    [Browsable(true)]
    [Category("Wizard Page")]
    [Description("Gets/Sets Is wizard page is a Welcome Page")]
    [DefaultValue(false)]
    public bool WelcomePage
    {
      get
      {
        return m_bIsWelcome;
      }
      set
      {
        if( value != m_bIsWelcome )
        {
          m_bIsWelcome = value;
          OnWelcomePageChanged();
          OnChanged();
        }
      }
    }

    protected override System.Drawing.Size DefaultSize
    {
      get
      {
        return new Size( 498, 328 );
      }
    }
    #endregion

    #region Class Events
    [Category("Wizard Page")]
    public event EventHandler Changed;
    #endregion

    #region Class Initialze/Finilize methods
    /// <summary>
    /// Default constractor
    /// </summary>
    public WizardPageBase()
    {
      InitializeComponent();
     
      base.Dock = DockStyle.Fill;
    }

    /// <summary>
    /// Free custom resources
    /// </summary>
    /// <param name="disposing">if TRUE then free resources</param>
    protected override void Dispose( bool disposing )
    {
      if( disposing && components != null )
      {
        components.Dispose();
      }

      base.Dispose( disposing );
    }
    #endregion

    #region Component Designer generated code
    /// <summary>
    /// Initialize Control
    /// </summary>
    private void InitializeComponent()
    {
      // 
      // WizardPageBase
      // 
      this.Name = "WizardPage";
      this.Size = new System.Drawing.Size(424, 248);

    }
    #endregion

    #region Class overrides for inheritors
    protected virtual void OnIndexChanged(){}
    protected virtual void OnWizardPageParentChanged(){}
    protected virtual void OnTitleChanged(){}
    protected virtual void OnDescriptionChanged(){}
    protected virtual void OnHeaderImageChanged(){}
    protected virtual void OnImageListChanged(){}
    protected virtual void OnImageIndexChanged(){}
    protected virtual void OnQuietModeChanged(){}
    protected virtual void OnFinishPageChanged(){}
    protected virtual void OnHelpAvailableChangd(){}
    protected virtual void OnWelcomePageChanged(){}
    #endregion

    #region Class Overrides
    /// <summary>
    /// Function Raise "Changed" event when property of class changed
    /// </summary>
    protected virtual void OnChanged()
    {
      if( Changed != null && m_bFireEvents == true )
      {
        Changed( this, EventArgs.Empty );
      }
    }
    #endregion
  }
}
