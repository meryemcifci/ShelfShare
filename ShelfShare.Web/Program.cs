using Microsoft.EntityFrameworkCore;
using ShelfShare.Business.Interfaces;
using ShelfShare.Business.Mapping;
using ShelfShare.Business.Services;
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

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            // Repository'ler
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserBookRepository, UserBookRepository>();

            // Service'ler
            builder.Services.AddScoped<IBookService, BookService>();
            //builder.Services.AddScoped<IFamilyService, FamilyService>();
            //builder.Services.AddScoped<IReviewService, ReviewService>();

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
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
