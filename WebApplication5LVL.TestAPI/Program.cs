using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using WebApplication5LVL.Contracts.User;
using WebApplication5LVL.DataAccess.Db;
using WebApplication5LVL.Infrastructure.MapProfiles;
using WebApplication5LVL.Infrastructure.Repositories;
using WebApplication5LVL.Register;

namespace WebApplication5LVL.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Добавляем DbContext
            builder.Services.AddSingleton<IDbContextOptionsConfigurator<DbAppContext>, DbContextConfiguration>();

            builder.Services.AddDbContext<DbAppContext>((sp, dbOptions) => sp
            .GetRequiredService<IDbContextOptionsConfigurator<DbAppContext>>()
            .Configure((DbContextOptionsBuilder<DbAppContext>)dbOptions));

            builder.Services.AddScoped((Func<IServiceProvider, DbContext>)(sp => sp.GetRequiredService<DbAppContext>()));

            builder.Services.ConfigureServices();

            // Add repositories to the container.
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


            builder.Services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));

            builder.Services.AddControllers();

            #region Authentication & Authorization

            builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddAuthentication();

            builder.Services.AddAuthorization();

            #endregion

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle :D here is my advice 
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Apterra Adverts Api", Version = "V1" });

                options.IncludeXmlComments(Path.Combine(Path.Combine(AppContext.BaseDirectory,
                $"{typeof(CreateUserRequest).Assembly.GetName().Name}.xml")));
                options.IncludeXmlComments(Path.Combine(Path.Combine(AppContext.BaseDirectory,
                $"{typeof(InfoUserResponse).Assembly.GetName().Name}.xml")));
                options.IncludeXmlComments(Path.Combine(Path.Combine(AppContext.BaseDirectory,
                $"{typeof(UpdateUserRequest).Assembly.GetName().Name}.xml")));

                options.IncludeXmlComments(Path.Combine(Path.Combine(AppContext.BaseDirectory, "documentation.xml")));
            });

            var app = builder.Build();

            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHsts();

            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();

            static MapperConfiguration GetMapperConfiguration()
            {
                var configuration = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<UserProfile>();
                });
                return configuration;
            }
        }
    }
}