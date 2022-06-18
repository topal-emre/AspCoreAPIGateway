using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using System;
using System.Text;

namespace Tulpar.Gateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            SymmetricSecurityKey signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Security"]));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signInKey,
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = Configuration["JWT:Audience"],
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        RequireExpirationTime = true
                    };
                });
            services.AddOcelot().AddConsul();
            services.AddControllers();

            services.AddSwaggerForOcelot(Configuration, (o) => { o.GenerateDocsForGatewayItSelf = true; });
        }
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwaggerForOcelotUI(opt =>
            {
                opt.PathToSwaggerGenerator = "/swagger/docs";
            });
            await app.UseOcelot();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
