# ElectronNET.Blazor
A library to help Blazor and Electron.NET work better together.

## Installation Instructions:

1) Add the [https://www.nuget.org/packages/ElectronNET.Blazor](https://www.nuget.org/packages/ElectronNET.Blazor) NuGet package to your project.

2) Modify the Program.cs of your Server app to look like this:
```
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseElectron(args)
                        .UseStartup<Startup>();
                });

    }
```
3) Modify the Startup.cs file of your Server app to look like this (pay attention, the registration order is important):
```
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();
            }

            app.UseElectronNETStaticFiles<Client.Startup>();

            app.UseClientSideBlazorFiles<Client.Startup>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapFallbackToClientSideElectronNET<Client.Startup>("index.html");
            });

            Task.Run(async () => await Electron.WindowManager.CreateWindowAsync());
        }
```

4) Open Windows Explorer, navigate to the root folder of your solution, then hold down the Shift key
   while right-clicking that folder, and select "Open Powershell window here".

5) Run the following command:
```
dotnet tool install ElectronNET.CLI -g
```

6) Run the following command:
```
electronize init
```

7) Open the Project Properties of your Server project, open the Debug tab, and under "Environment Variables",
   add an `IS_ELECTRON` variable with the value of `true`.
   
   ![image](https://user-images.githubusercontent.com/1657085/60770021-894b3980-a0a4-11e9-9812-2d76d2f555c8.png)

8) Next to the green arrow that starts debugging, make sure the "Electron.NET App" profile is selected, and 
   then hit the green button.
   
   ![image](https://user-images.githubusercontent.com/1657085/60770039-b992d800-a0a4-11e9-9cbd-3cf6a011f21b.png)

9) PROFIT!!!
