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
using DevExpress.ExpressApp.Web.Templates;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using XafCookiesWarning.Module.BusinessObjects;

namespace XafCookiesWarning.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppWindowControllertopic.aspx.
    public partial class CheckCookiesController : ViewController, IXafCallbackHandler//WindowController
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
        protected XafCallbackManager CallbackManager
        {
            get { return ((ICallbackManagerHolder)WebWindow.CurrentRequestPage).CallbackManager; }
        }
        void IXafCallbackHandler.ProcessAction(string parameter)
        {
          
            CallbackManager.NeedEndResponse = true;
            //objectSpace = Application.CreateObjectSpace(typeof(CookiesMessage));
            //Instance = objectSpace.CreateObject<CookiesMessage>();
            //objectSpace = Application.CreateObjectSpace(typeof(DomainObject1));
            //Instance = objectSpace.CreateObject<DomainObject1>();
            //Instance.Message = "Please enable the cookies";


            //DetailView dialogView = Application.CreateDetailView(objectSpace, Instance);
            //Application.ShowViewStrategy.ShowViewInPopupWindow(dialogView,
            //    () => Application.ShowViewStrategy.ShowMessage("Done."),
            //    () => Application.ShowViewStrategy.ShowMessage("Cancelled."),
            //    null, null, this.Frame
            //);


            //this.Frame.SetView(this.Application.CreateDetailView(objectSpace, Instance));

            //string Script = $"alert('the script was executed and the parameter value is {parameter}')";
            //((WebWindow)this.Frame).RegisterStartupScript("CallBack", Script, true);
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            //HACK you need to register the callbacks in this method "OnViewControlsCreated" otherwise the callback won't trigger
            //CallbackManager.RegisterHandler("cookies", this);
        }
        IObjectSpace objectSpace;
        //CookiesMessage Instance;
        DomainObject1 Instance;
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
