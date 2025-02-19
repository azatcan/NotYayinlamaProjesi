using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Configuration;
using ÜNY.Application.Mappings;
using ÜNY.Application.Middlewares;
using ÜNY.Application.Services.Abstract;
using ÜNY.Application.Services.Concrete;
using ÜNY.Domain.Entities;
using ÜNY.Domain.Repositories;
using ÜNY.Infrastructure.Persistence.Data;
using ÜNY.Infrastructure.Repositories;
using URL.Application.Services.Concrete;
using URL.Domain.Repositories;
using URL.Infrastructure.Repositories;

namespace ÜNY.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
            builder.Services.AddControllers();
            builder.Services.AddDbContext<DataContext>(conf => conf.UseSqlServer(builder.Configuration.GetConnectionString("DefalutConnection")).UseLazyLoadingProxies());
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            ConfigureAuth(builder);
            RegisterServices(builder);
            RegisterRepositories(builder);
            builder.Services.AddAutoMapper(typeof(ExamProfile));
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("AllowAllOrigins");
            app.UseMiddleware<ExceptionMiddleware>();

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
            builder.Services.AddScoped<IExamService, ExamManager>();
            builder.Services.AddScoped<IExamGradeService, ExamGradeManager>();
            builder.Services.AddScoped<ICourseUnitInformationService, CourseUnitInformationManager>();
            builder.Services.AddScoped<IUserService,UserManager>();
        }

        private static void RegisterRepositories(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUnitİnformationRepository, UnitİnformationRepository>();
            builder.Services.AddScoped<IGenderRepository, GenderRepository>();
            builder.Services.AddScoped<IFeeİnformationRepository, FeeİnformationRepository>();
            builder.Services.AddScoped<IEnrollmentRepository, EnrolmentRepository>();
            builder.Services.AddScoped<ICoursesRepository, CoursesRepository>();
            builder.Services.AddScoped<IContactİnformationRepository, ContactİnformationRepository>();
            builder.Services.AddScoped<IExamRepository, ExamRepository>();
            builder.Services.AddScoped<IExamGradeRepository, ExamGradeRepository>();
            builder.Services.AddScoped<ICourseUnitInformationRepository, CourseUnitInformationRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
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
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;

                options.User.RequireUniqueEmail = false;
                

            })
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();
        }
    }
}
