using alienworlds_web_client;
using alienworlds_web_client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddScoped<IHttpService, HttpService>()
    .AddScoped<ICookieService, CookieService>()
    .AddScoped<ISignalrService,SignalrService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://api.monesv.ml") });//http://localhost:5235

await builder.Build().RunAsync();
