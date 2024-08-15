using Granite.Server.Config.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Granite.Server.Startup.Services;

public class SwaggerServiceRegistrar : IServiceRegistrar
{
    public void Register(IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "CPT API", Version = "v1" });

            var openApiOptions = new OpenApiOptions();
            configuration.GetSection("OpenApi").Bind(openApiOptions);

            foreach (var server in openApiOptions.Servers)
            {
                c.AddServer(server);
            }

            c.ExampleFilters();
            c.EnableAnnotations();

            // Include 'SecurityScheme' to use JWT Authentication
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { jwtSecurityScheme, Array.Empty<string>() }
            });
        });

        services.AddSwaggerExamplesFromAssemblies(typeof(AssemblyReference).Assembly);
    }
}