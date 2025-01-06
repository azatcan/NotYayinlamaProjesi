
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using ÜNY.Domain.Abstract;
using ÜNY.Domain.Concrete;
using ÜNY.Domain.Data;
using ÜNY.Domain.Entities;
using ÜNY.Infrastructure.Abstract;
using ÜNY.Infrastructure.Concrete;
using ÜNY.WebAPI.HandlersAPI;

namespace ÜNY.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddDbContext<DataContext>(conf => conf.UseSqlServer(builder.Configuration.GetConnectionString("DefalutConnection")));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            ConfigureAuth(builder);
            RegisterServices(builder);
            RegisterRepositories(builder);


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUnitİnformationService, UnitİnformationManager>();
            builder.Services.AddScoped<IGenderService, GenderManager>();
            builder.Services.AddScoped<IFeeİnformationService, FeeİnformationManager>();
            builder.Services.AddScoped<IEnrollmentService, EnrollmentManager>();
            builder.Services.AddScoped<ICoursesService, CoursesManager>();
            builder.Services.AddScoped<IContactİnformationService, ContactİnformationManager>();
        }

        private static void RegisterRepositories(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUnitİnformationRepository, UnitİnformationRepository>();
            builder.Services.AddScoped<IGenderRepository, GenderRepository>();
            builder.Services.AddScoped<IFeeİnformationRepository, FeeİnformationRepository>();
            builder.Services.AddScoped<IEnrollmentRepository, EnrolmentRepository>();
            builder.Services.AddScoped<ICoursesRepository, CoursesRepository>();
            builder.Services.AddScoped<IContactİnformationRepository, ContactİnformationRepository>();
        }

        private static void ConfigureAuth(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie();

            builder.Services.AddIdentity<Users, Roles>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;

                options.User.RequireUniqueEmail = false;

            })
                .AddEntityFrameworkStores<DataContext>();
        }
    }
}
