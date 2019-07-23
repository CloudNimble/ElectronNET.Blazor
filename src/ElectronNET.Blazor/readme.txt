===================================================
ElectronNET.Blazor
Written By:		Robert McLaws
Version:		1.0.0
Last Updated:	6 July 2019
===================================================

Installation Instructions:

1) Add this package to your project (you're doing great so far!)

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
3) Modify the Startup.cs file of your Server app to look like this:
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

7) Next to the green arrow that starts debugging, make sure the "Electron.NET App" profile is selected, and 
   then hit the green button.

8) PROFIT!!!