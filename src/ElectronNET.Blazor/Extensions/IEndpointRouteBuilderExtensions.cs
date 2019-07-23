using ElectronNET.API;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System;

namespace Microsoft.Extensions.FileProviders
{

    /// <summary>
    /// Extensions for the <see cref="IEndpointRouteBuilder"/> for working with Electron.NET.
    /// </summary>
    public static class IEndpointRouteBuilderExtensions
    {

        /// <summary>
        /// Registers the fallback to the index.html file where Electron.NET's build process expects to find it.
        /// </summary>
        /// <typeparam name="TClientApp">The Startup class in the Blazor.Client app to wire up.</typeparam>
        /// <param name="builder">The <see cref="IEndpointRouteBuilder"/> instance to extend.</param>
        /// <returns>The <see cref="IEndpointRouteBuilder"/> instance to continue fluent chaining.</returns>
        public static IEndpointRouteBuilder MapFallbackToClientSideElectronNET<TClientApp>(this IEndpointRouteBuilder builder, string path)
        {
            if (HybridSupport.IsElectronActive)
            {
                var assemblyName = typeof(TClientApp).Assembly.GetName().Name.ToLower().Replace(".", "");
                builder.MapFallbackToClientSideBlazor<TClientApp>($"_content/{assemblyName}/{path}");
            }
            else
            {
                builder.MapFallbackToClientSideBlazor<TClientApp>("index.html");
            }

            return builder;
        }

    }

}