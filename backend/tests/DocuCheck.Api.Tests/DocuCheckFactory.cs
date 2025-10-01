using DocuCheck.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DocuCheck.Api.Tests
{
    internal class DocuCheckFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)  
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureTestServices(services =>
            { });
            
            base.ConfigureWebHost(builder);
        }
    }
}