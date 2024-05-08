using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Task4.Models;

namespace Task4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ProductsContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("ProductsContext")));

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

            app.UseAuthorization();
            app.Map("/", () => Results.Redirect("/Products/Index"));
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Products}/{action=Index}");

            app.Run();
        }
    }
}
