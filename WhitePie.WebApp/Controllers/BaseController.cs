using Microsoft.AspNetCore.Mvc;

namespace WhitePie.WebApp.Controllers
{
    public class BaseController<T> : Controller where T : BaseController<T>
    {
        private ILogger<T> _logger;

        protected ILogger<T> Logger
            => _logger ??= HttpContext
            .RequestServices
            .GetRequiredService<ILogger<T>>();
    }
}
