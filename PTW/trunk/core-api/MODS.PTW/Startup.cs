using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MODS.PTW.Helpers;
using MODS.PTW.Services;
using AutoMapper;
using System.Configuration;

namespace MODS.PTW
{
    public class Startup
    {
        
    readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public IConfiguration Configuration { get; }
        public static string ConnectionString { get; private set; }

        public Startup(IHostingEnvironment env)
        {
            Configuration =  new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json").Build();
            
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
        {
            options.AddPolicy(MyAllowSpecificOrigins,
            builder =>
            {
                builder.WithOrigins("http://localhost:4200")/*.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()*/;
            });
                });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            ConnectionString = Configuration["ConnectionStrings:DefaultConnection"];

            services.AddDbContext<DataContext>(options => options.UseSqlServer(ConnectionString));          
            services.AddAutoMapper(typeof(Startup));
                
            var key = Encoding.ASCII.GetBytes(SiteSettings.SecretKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMediaService, MediaService>();
            services.AddScoped<IEquipmentCatagoryService, EquipmentCatagoryService >();
            services.AddScoped<IWorkPermitStatusService, WorkPermitStatusService>();
            services.AddScoped<IWorkPermitTypesService,WorkPermitTypesService > ();
            services.AddScoped<IUserAuthorityService, UserAuthorityService>();
            services.AddScoped<ICompanyCatagoryService, CompanyCatagoryService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IFunctionService, FunctionService>();
            services.AddScoped<ISectionService, SectionService>();
            services.AddScoped<IAreaService, AreaService>();
            services.AddScoped<IEquipmentService, EquipmentService>();
            services.AddScoped<IPTWQuestionService, PTWQuestionService>();
            services.AddScoped<IPTWTypeService, PTWTypeService>();
            services.AddScoped<IPTWQuestiontypeAnsService, PTWQuestionTypeAnsService>();
            services.AddScoped<ICertificateService, CertificateService>();
            services.AddScoped<ICertificateAuthorityService, CertificateAuthorityService>();
            services.AddScoped<ICertificateQAService, CertificateQAService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCors(MyAllowSpecificOrigins);
            //app.UseHttpsRedirection();
            app.UseMvc();

            
        }
    }
}
