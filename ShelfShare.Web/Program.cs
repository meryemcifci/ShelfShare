using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using ShelfShare.Business.Abstract;
using ShelfShare.Business.Concrete;
using ShelfShare.Business.Mapping;
using ShelfShare.DataAccess.Abstract;
using ShelfShare.DataAccess.Concrete;
using ShelfShare.DataAccess.Repository;

namespace ShelfShare.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:5173") // Vite dev server portu
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = "/Auth/Login";
                opt.AccessDeniedPath = "/Auth/AccessDenied";
            });

            // Repository'ler
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // Service'ler
            builder.Services.AddScoped<IBookService, BookService>();

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowFrontend"); // Cors'u buraya ekliyoruz

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllers();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
