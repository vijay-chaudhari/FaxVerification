using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace FaxVerification;

public class FaxVerificationWebTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<FaxVerificationWebTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
