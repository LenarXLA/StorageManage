using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using StorageManage.Module.BusinessObjects.StorageManageDataModelCode;

namespace StorageManage.Module.Controllers
{
    // Поиск с последующим редактированием пикета напрямую
    public partial class FindBySubjectController : ViewController
    {
        public FindBySubjectController()
        {
            InitializeComponent();

            // Activate the controller only in the List View.
            TargetViewType = ViewType.ListView;
            // Activate the controller only for root Views.
            TargetViewNesting = Nesting.Root;
            // Specify the type of objects that can use the controller.
            TargetObjectType = typeof(Picket);

            ParametrizedAction findBySubjectAction =
                new ParametrizedAction(this, "FindBySubjectAction", PredefinedCategory.View, typeof(string))
                {
                    ImageName = "Action_Search",
                    NullValuePrompt = "Find picket by name..."
                };
            findBySubjectAction.Execute += FindBySubjectAction_Execute;

        }

        private void FindBySubjectAction_Execute(object sender, ParametrizedActionExecuteEventArgs e)
        {
            var objectType = ((ListView)View).ObjectTypeInfo.Type;
            IObjectSpace objectSpace = Application.CreateObjectSpace(objectType);
            string paramValue = e.ParameterCurrentValue as string;
            try
            {
                object obj = objectSpace.FirstOrDefault<Picket>(picket => picket.PicketName.Contains(paramValue));
                if (obj != null)
                {
                    DetailView detailView = Application.CreateDetailView(objectSpace, obj);
                    detailView.ViewEditMode = ViewEditMode.Edit;
                    e.ShowViewParameters.CreatedView = detailView;
                }
            }
            catch (Exception ex)
            {
                // To-Do Обработать в логах в будущем
                Console.WriteLine(ex.Message);
            }

        }

        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
