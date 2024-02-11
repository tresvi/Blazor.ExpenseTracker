//using BlazorExpenseTracker.UI.Data;
using BlazorExpenseTracker.UI.Interfaces;
using BlazorExpenseTracker.UI.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlazorExpenseTracker.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });
            builder.Services.AddHttpClient<ICategoryService, CategoryService>(
                client => { client.BaseAddress = new Uri("https://localhost:7255"); });
            builder.Services.AddHttpClient<IExpenseService, ExpenseService>(
                client => { client.BaseAddress = new Uri("https://localhost:7255"); });

            //builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.Configure<RazorPagesOptions>(options => options.RootDirectory = "/Pages");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}