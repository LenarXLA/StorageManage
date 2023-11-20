using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;

namespace StorageManage.Module.Controllers
{
    public class MyTestException : Exception
    {
    }

    // Для тестовой обработки ошибок
    public partial class TestExceptionController : ViewController
    {
        public TestExceptionController()
        {
            SimpleAction simpleAction = new SimpleAction(this, "Test Action", PredefinedCategory.Edit);
            simpleAction.Execute += SimpleAction_Execute;
        }

        private void SimpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            // В режиме дебага приложение падает сюда, в режиме прода - нормальная обработка эксепшна
            throw new MyTestException();
        }

    }
}
