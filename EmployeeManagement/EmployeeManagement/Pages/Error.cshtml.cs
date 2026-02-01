using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeManagement.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        public string? RequestId { get; set; }

        public int? ResponseStatusCode { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger;

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionFeature?.Error != null)
            {
                _logger.LogError(
                    exceptionFeature.Error,
                    "Unhandled exception for {Method} {Path}{QueryString}. TraceId={TraceId}",
                    HttpContext.Request.Method,
                    exceptionFeature.Path,
                    HttpContext.Request.QueryString,
                    HttpContext.TraceIdentifier
                );
            }

            var statusCodeFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            if (statusCodeFeature != null)
            {
                ResponseStatusCode = HttpContext.Response.StatusCode;
                _logger.LogWarning(
                    "HTTP {StatusCode} for {OriginalPath}{OriginalQueryString}. TraceId={TraceId}",
                    ResponseStatusCode,
                    statusCodeFeature.OriginalPath,
                    statusCodeFeature.OriginalQueryString,
                    HttpContext.TraceIdentifier
                );
            }
        }
    }
}
