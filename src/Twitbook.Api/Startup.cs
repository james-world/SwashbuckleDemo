using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace Twitbook.Api
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
            services.AddMvc()
                .AddXmlDataContractSerializerFormatters();

            services.AddAuthorization(options =>
                options.AddPolicy("TwitbookApi", policy => policy.RequireClaim("scope", "TwitbookApi")));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Audience = "TwitbookApi";
                    options.Authority = "http://localhost:5000/";
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                });


            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(SchemaIdStrategy);
                c.SwaggerDoc("v1", new Info {Title = "Twitbook API", Version = "v1"});

                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Twitbook.Api.xml");
                c.IncludeXmlComments(filePath);

                c.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "application",
                    TokenUrl = "http://localhost:5000/connect/token",
                    Scopes = new Dictionary<string, string>
                    {
                        { "TwitbookApi", "Access the Twitbook API" }
                    }
                });

                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Twitbook API");
                c.ConfigureOAuth2(null, null, null, null);
            });

            app.UseMvc();

            
        }

        private static string SchemaIdStrategy(Type currentClass)
        {
            var name = currentClass.Name;
            return name.EndsWith("Dto") ? name.Substring(0, name.Length - 3) : name;
        }
    }
}
