using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Web.Data;
using Web.Data.Identities;
using Web.Options;
using Web.Repositories.Interfaces;
using Web.Repositories.Locals;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var swaggerOptions = Configuration.GetSection(nameof(SwaggerOptions)).Get<SwaggerOptions>();
            var jwtOptions = Configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

            services.AddSingleton(swaggerOptions);
            services.AddSingleton(jwtOptions);

            services.AddSingleton<ITestRepository, LocalTestRepository>();
            services.AddSingleton<IBookRepository, LocalBookRepository>();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AppDbContext>();

            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(swaggerOptions.Version, new OpenApiInfo { Title = swaggerOptions.Title, Version = swaggerOptions.Version });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var swaggerOptions = Configuration.GetSection(nameof(SwaggerOptions)).Get<SwaggerOptions>();
            app.UseSwagger(c => c.RouteTemplate = swaggerOptions.JsonRoute);
            app.UseSwaggerUI(c => c.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description));

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
