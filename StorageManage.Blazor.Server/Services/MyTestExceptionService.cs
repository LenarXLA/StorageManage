using DevExpress.ExpressApp.Blazor.Services;
using DevExpress.ExpressApp;
using StorageManage.Module.Controllers;

namespace StorageManage.Blazor.Server.Services
{
    // Для тестовой обработки ошибок
    public class MyTestExceptionService : ExceptionService
    {
        public MyTestExceptionService(ILogger<ExceptionService> logger) : base(logger) { }
        public override void HandleException(Exception exception)
        {
            Exception result = exception is MyTestException ? new UserFriendlyException("Это тестовая обработка ошибок!", exception) : exception;
            base.HandleException(result);
        }
    }
}
