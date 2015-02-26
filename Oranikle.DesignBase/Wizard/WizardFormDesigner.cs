using System;
using System.Windows.Forms.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Collections;
using System.ComponentModel.Design;

using Oranikle.Studio.Controls.Wizards;


namespace Oranikle.Studio.Controls.Designers
{
  /// <summary>
  /// Designer of Wizard Component.
  /// </summary>
  public class WizardFormDesigner : ParentControlDesigner
  {
    #region Class members
    DesignerVerbCollection m_verbs;
    #endregion

    #region Class properties
    public override System.ComponentModel.Design.DesignerVerbCollection Verbs
    {
      get
      {
        return m_verbs;
      }
    }
    #endregion

    #region Class Constructor
    public WizardFormDesigner()
    {
      m_verbs = new DesignerVerbCollection( 
        new DesignerVerb[] 
        {
          new DesignerVerb("Add Welcome Page", new EventHandler( OnAddWelcomeClick ) ),
          new DesignerVerb("Add Page", new EventHandler( OnAddPageClick ) ),
          new DesignerVerb("Add Final Page", new EventHandler( OnAddFinalClick ) ),
          new DesignerVerb("Remove Page", new EventHandler( OnRemoveClick ) )
        }
        );
    }

    #endregion
    
    #region Class Overrides
    protected override void PreFilterProperties( IDictionary properties )
    {
      base.PreFilterProperties( properties );
    }

    protected override bool GetHitTest( System.Drawing.Point point )
    {
      WizardForm ctrl = ( WizardForm )Control;
      
      if( ctrl != null )
      {
        Point p = ctrl.PointToClient( point );
        return ctrl.OnDesignedWizardButtons( p );
      }
        
      return base.GetHitTest( point );
    }
    #endregion

    #region Verbs events handlers
    private void OnAddWelcomeClick( object sender, EventArgs e )
    {
      WizardForm ctrl = ( WizardForm )Control;
      WizardWelcomePage page = new WizardWelcomePage();
      ctrl.Pages.Add( page );

      IDesignerHost host = (IDesignerHost)GetService( typeof( IDesignerHost ) );
      if( host != null ) host.Container.Add( page );
    }
    
    private void OnAddPageClick( object sender, EventArgs e )
    {
      WizardForm ctrl = ( WizardForm )Control;
      WizardPageBase page = new WizardPageBase();
      ctrl.Pages.Add( page );
      
      IDesignerHost host = (IDesignerHost)GetService( typeof( IDesignerHost ) );
      if( host != null ) host.Container.Add( page );
    }
    
    private void OnAddFinalClick( object sender, EventArgs e )
    {
      WizardForm ctrl = ( WizardForm )Control;
      WizardFinalPage page = new WizardFinalPage();
      ctrl.Pages.Add( page );
      
      IDesignerHost host = (IDesignerHost)GetService( typeof( IDesignerHost ) );
      if( host != null ) host.Container.Add( page );
    }

    private void OnRemoveClick( object sender, EventArgs e )
    {
      WizardForm ctrl = ( WizardForm )Control;
      
      if( ctrl.Pages.Count > 0 && ctrl.PageIndex > 0 )
      {
        ctrl.Pages.RemoveAt( ctrl.PageIndex );
      }
    } 

    #endregion
  }
}
