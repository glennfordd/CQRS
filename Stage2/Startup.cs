using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stage2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Stage2
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
            services.AddControllersWithViews(options =>
            {
                options.Conventions.Add(new FeatureConvention());
            });

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationFormats.Clear();
                options.ViewLocationFormats.Add("/Features/{2}/{1}/{0}.cshtml");
                options.ViewLocationFormats.Add("/Features/{2}/{0}.cshtml");
                options.ViewLocationFormats.Add("/Features/Shared/{0}.cshtml");
                options.ViewLocationExpanders.Add(new FeatureFoldersRazorViewEngine());
            });

            services.AddDbContext<ReservationContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:Sql"]));
            services.AddMediatR(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

public class FeatureConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        controller.Properties.Add("feature", GetFeatureName(controller.ControllerType));
    }

    private static string GetFeatureName(TypeInfo controllerType)
    {
        var tokens = controllerType.FullName.Split('.');
        if (tokens.All(t => t != "Features"))
            return "";
        var featureName = tokens
        .SkipWhile(t => !t.Equals("features", StringComparison.CurrentCultureIgnoreCase))
        .Skip(1)
        .Take(1)
        .FirstOrDefault();

        return featureName;
    }
}

public class FeatureFoldersRazorViewEngine : IViewLocationExpander
{
    public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context,
          IEnumerable<string> viewLocations)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (viewLocations == null)
        {
            throw new ArgumentNullException(nameof(viewLocations));
        }

        var controllerActionDescriptor = context.ActionContext.ActionDescriptor as ControllerActionDescriptor;
        if (controllerActionDescriptor == null)
        {
            throw new NullReferenceException("ControllerActionDescriptor cannot be null.");
        }

        string featureName = controllerActionDescriptor.Properties["feature"] as string;
        foreach (var location in viewLocations)
        {
            yield return location.Replace("{3}", featureName);
        }
    }

    public void PopulateValues(ViewLocationExpanderContext context) { }
}