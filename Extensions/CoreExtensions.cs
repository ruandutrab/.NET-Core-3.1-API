using api_teste_dotnet.Context;
using api_teste_dotnet.Models;
using api_teste_dotnet.Service;
using api_teste_dotnet.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    // Classe de extensão utilizada pela classe Startup
    public static class CoreExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            // Esse ponto é utilizado na injeção de dependência da configuração
            // As controllers podem receber a interface IConfiguration por parâmetro no construtor
            services.AddSingleton(configuration);

            // Esse ponto serve para configuração da interface do swagger
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "No need to put the `bearer` keyword in front of token",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Name = "Authorization"
                });
                options.OperationFilter<AddRequiredHeaderParameter>();

                // Esse ponto serve para documentação e descrições da api
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API para entrevista",
                    Description = "Uma API que atende aos requisitos solicitados no documento de teste",
                    Contact = new OpenApiContact
                    {
                        Name = "Ruan Dutra Braga",
                        Url = new Uri("https://github.com/ruandutrab")
                    }
                });

                // Esse ponto está injetando a operação de filtro para inclusão de cabeçalho de autenticação obrigatório nos métodos
                options.OperationFilter<AddRequiredHeaderParameter>();
            });

            // Esse ponto serve para configuração do JWT
            services.AddAuthentication
                (JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        

                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey
                      (Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });

            // Esse ponto faz a injeção do contexto do banco de dados para utilização nos controllers
            services.AddDbContextPool<UsuariosDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));

            // Esse ponto faz a injeção do contexto do banco de dados para utilização nos controllers
            services.AddDbContextPool<TarefasDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));

            // Esse ponto faz a injeção da interface para utilização nos controllers
            services.AddScoped<IUsuariosService, UsuariosService>();

            // Esse ponto faz a injeção da interface para utilização nos controllers
            services.AddScoped<IUsuarioLoginService, UsuarioLoginService>();

            // Esse ponto faz a injeção da interface para utilização nos controllers
            services.AddScoped<ITarefasService, TarefasService>();

        }

        public static void ConfigureApp(this IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            // Esse ponto realiza a configuração do swagger para utilização do V2
            app.UseSwagger(options =>
            {
                options.SerializeAsV2 = true;
            });

            // Esse ponto remove a necessidade de utilizar o prefixo do swagger na URL
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            // Esse ponto faz parte da configuração do JWT
            app.UseAuthentication();

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
