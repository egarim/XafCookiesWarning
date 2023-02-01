using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Web;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace XafCookiesWarning.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppWindowControllertopic.aspx.
    public partial class CheckCookiesController : ViewController//WindowController
    {
        SimpleAction simpleAction;
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public CheckCookiesController()
        {
            InitializeComponent();
            this.TargetViewType = ViewType.DetailView;
            this.TargetObjectType = typeof(AuthenticationStandardLogonParameters);
            //this.TargetWindowType = WindowType.Main;
            simpleAction = new SimpleAction(this, "test cookies", "No Visible actions");
            simpleAction.Execute += SimpleAction_Execute;
            // Target required Windows (via the TargetXXX properties) and create their Actions.
        }

        private void SimpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var script = ReadResourceFile("XafCookiesWarning.Module.Web.Scripts.Modernizer.txt");
            var CurrentType=this.View.CurrentObject.GetType();
            var webWindow=(this.Application as WebApplication).LogonWindow;
            //WebWindow.CurrentRequestWindow.RegisterClientScript("cookies", script, true);
            webWindow.RegisterClientScript("cookies", script, true);
        }

        private string ReadResourceFile(string filename)
        {
            var thisAssembly = Assembly.GetExecutingAssembly();
            using (var stream = thisAssembly.GetManifestResourceStream(filename))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            this.simpleAction.DoExecute();
            // Perform various tasks depending on the target Window.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
