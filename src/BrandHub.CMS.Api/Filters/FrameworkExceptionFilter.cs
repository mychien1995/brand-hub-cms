using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrandHub.CMS.Api.Filters
{
    public class FrameworkExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<FrameworkExceptionFilter> _logger;
        public FrameworkExceptionFilter(ILogger<FrameworkExceptionFilter> logger)
        {
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "");
        }
    }
}
