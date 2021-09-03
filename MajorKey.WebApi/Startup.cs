using FluentValidation;
using FluentValidation.AspNetCore;
using MajorKey.Core.Contracts.Repositories;
using MajorKey.Core.Contracts.Services;
using MajorKey.Core.Models;
using MajorKey.Core.Models.DataTransfer;
using MajorKey.Core.Services;
using MajorKey.Insfrastructure.DAL;
using MajorKey.Insfrastructure.Repositories;
using MajorKey.Validation.Request;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace MajorKey.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddDbContext<ApplicationContext>(opt => opt.UseInMemoryDatabase("RequestsDB"));
            services.AddScoped<IRequestRepository, RequestRepository>();
            services.AddScoped<IRequestService, RequestService>();
            services.AddTransient<IMailService, MailService>();
            services.AddFluentValidation();
            services.AddTransient<IValidator<CreateRequestDto>, CreateRequestValidator>();
            services.AddTransient<IValidator<UpdateRequestDto>, UpdateRequestValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
