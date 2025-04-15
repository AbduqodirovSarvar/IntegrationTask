using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Web.Extentions
{
    public static class WebAppExtentions
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddDepencies(builder.Configuration);
            builder.AddServiceDefaults();
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }

        public static void ConfigurePipeline(this WebApplication app)
        {
            app.MapDefaultEndpoints();

            // Configure the HTTP request pipeline.
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
        }

        public static async Task ApplyMigration(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<AppDbContext>();
            await context.Database.MigrateAsync();
        }
    }
}
