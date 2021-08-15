using DrugCatalog.Data;
using DrugCatalog.Features.Drugs;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DrugCatalog
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
            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(typeof(Startup));
            services.AddScoped<IValidator<GetSingleQuery>, GetSingleQueryValidator>();
            services.AddScoped<IValidator<CreateDrugCommand>, CreateDrugCommandValidator>();
            services.AddScoped<IValidator<DeleteDrugCommand>, DeleteDrugCommandValidator>();
            services.AddScoped<IValidator<UpdateDrugSnapshot>, UpdateDrugSnapshotValidator>();

            services.AddControllers(options => options.Filters.Add(new HttpResponseExceptionFilter()));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DrugCatalog", Version = "v1" });
            });

            services.AddDbContext<DrugCatalogContext>(options =>
            {
                options.UseSqlServer(@"Server=.\SQLEXPRESS;Database=DrugCatalog;Trusted_Connection=True");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DrugCatalog v1"));
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<BasicAuthMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
