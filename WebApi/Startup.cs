using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AutoMapper;
using D1.Data.Repositories.Abstract;
using D1.Model.Services.Abstract;
using D1.Model.Services.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using D1.Data.Repositories.Concrete;
using D1.Model;
using FluentValidation.AspNetCore;
using Serilog;
using WebApi.Utilites.Filters;
using WebApi.Utilites.Middleware;
using D1.Data;
using D1.Data.Entities;
using D1.Model.Authentification;
using D1.Model.Mappings;
using D1.Model.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;


namespace WebApi
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        private readonly ISettingsService _settingsService;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var jwtSettings = new JwtSettings();
            Configuration.Bind(nameof(JwtSettings), jwtSettings);

            var emailSettings = new EmailSettings();
            Configuration.Bind(nameof(EmailSettings), emailSettings);

            var uiUrlsettings= new UIUrlSettings();
            Configuration.Bind(nameof(UIUrlSettings), uiUrlsettings);

            _settingsService = new SettingsService(jwtSettings,emailSettings,uiUrlsettings);
                    
        }




        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>();

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.SignIn.RequireConfirmedEmail = true;

            })
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _settingsService.JwtSettings.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settingsService.JwtSettings.Key)),
                    ClockSkew = TimeSpan.Zero,
                };
            });

            services.AddCors();
            services.AddResponseCaching();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom
                .Configuration(Configuration)
                .CreateLogger();
            AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();
            services.AddSingleton(Log.Logger);


            services.AddSingleton<IMapper>(new Mapper(new MapperConfiguration(options =>
            {
                options.CreateMissingTypeMaps = false;
                options.AddProfile<UserMapping>();
            })));         


            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));

                options.Filters.Add(new ValidationModelStateFilter());

            }).AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<LoginModel>()
                    .RegisterValidatorsFromAssemblyContaining<ForgotPasswordModel>()
                    .RegisterValidatorsFromAssemblyContaining<RecoverPasswordModel>()
            );

            //services
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient(typeof(IUserService), typeof(UserService));
            services.AddTransient(typeof(IEmailService), typeof(EmailService));
          

            //repositories          
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();

            // Settings
            services.AddSingleton(_settingsService);
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDatabaseInitializer databaseInitializer)
        {
            app.Use(async (ctx, next) =>
            {
                await next();
                if (ctx.Response.StatusCode == 204)
                {
                    ctx.Response.ContentLength = 0;
                }
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                databaseInitializer.Initialize();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(options =>
                options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("X-File-Name"));

            app.UseAuthentication();

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            // app.UseMiddleware<ErrorHandlingMiddleware>();

            // app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
