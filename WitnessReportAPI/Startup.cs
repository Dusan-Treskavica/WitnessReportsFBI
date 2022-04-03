using BusinessLogic.Interface.WitnessReports;
using BusinessLogic.Service;
using DataAccess.EF.Context;
using DataAccess.Interface.Repository;
using DataAccess.Interface.UoW;
using DataAccess.Repository;
using DataAccess.UoW;
using ExternalCommunication.Communication;
using ExternalCommunication.Communication.FBI;
using ExternalCommunication.Communication.IP;
using ExternalCommunication.Interface;
using ExternalCommunication.Interface.FBI;
using ExternalCommunication.Interface.IP;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WitnessReportAPI.Middleware;
using WitnessReportAPI.Profiles;

namespace WitnessReportAPI
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
            //Registration of Data Access layer 
            services.AddDbContext<WitnessReportDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IWitnessReportRepository, WitnessReportRepository>();

            //Registration of Business Logic layer 
            services.AddScoped<IWitnessReportService, WitnessReportService>();
            services.AddScoped<IWitnessReportValidationService, WitnessReportValidationService>();

            //Registration of External Communication project 
            services.AddScoped<IFBICaseService, FBICaseService>();
            services.AddScoped<IIPAddressService, IPAddressService>();
            services.AddTransient(typeof(ICommunicator<>), typeof(ServiceCommunicator<>));
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpClient();


            var mapperConfig = new AutoMapper.MapperConfiguration(conf => {
                conf.AddProfile(new WitnessReportVMProfile());
            });
            services.AddSingleton(mapperConfig.CreateMapper());

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WitnessReportAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WitnessReportAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
