using Microsoft.AspNetCore.Builder;
using System;
using System.IO;

namespace Microsoft.Extensions.FileProviders
{

    /// <summary>
    /// Extensions for the <see cref="IApplicationBuilder"/> for working with Electron.NET.
    /// </summary>
    public static class IApplicationBuilderExtensions
    {

        /// <summary>
        /// Registers the static file options where Electron.NET's build process expects to find them.
        /// </summary>
        /// <typeparam name="TClientApp">The Startup class in the Blazor.Client app to wire up.</typeparam>
        /// <param name="builder">The <see cref="IApplicationBuilder"/> instance to extend.</param>
        /// <returns>The <see cref="IApplicationBuilder"/> instance to continue fluent chaining.</returns>
        public static IApplicationBuilder UseElectronNETStaticFiles<TClientApp>(this IApplicationBuilder builder)
        {
            if (Environment.GetEnvironmentVariable("IS_ELECTRON") == "true")
            {
                var assemblyName = typeof(TClientApp).Assembly.GetName().Name.ToLower().Replace(".", "");
                builder.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\_content\\{assemblyName}")),
                    RequestPath = ""
                });
            }
            else
            {
                builder.UseStaticFiles();
            }

            return builder;
        }

    }
}
