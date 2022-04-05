﻿using Crafter.RecipeComposition.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Crafter.RecipeComposition
{
    public sealed class RecipeCompositionBoundedContextConfigurator
    {
        private readonly IHostEnvironment environment;

        public RecipeCompositionBoundedContextConfigurator(IHostEnvironment environment, IServiceCollection services)
        {
            this.environment = environment;
            services.AddControllersWithViews()
                .ConfigureApplicationPartManager(setup => setup.ApplicationParts.Add(new AssemblyPart(this.GetType().Assembly)));

            services.AddScoped<RecipeService>();
        }

        public void Configure(WebApplication app)
        {
            app.MapAreaControllerRoute(
                name: AreaRoute.CustomerFacing.Name,
                areaName: AreaRoute.CustomerFacing.AreaName,
                pattern: $"{AreaRoute.CustomerFacing.UrlPrefix.Trim('/')}/{{controller=Home}}/{{action=Index}}/{{id?}}");

            app.MapAreaControllerRoute(
                name: AreaRoute.BackOffice.Name,
                areaName: AreaRoute.BackOffice.AreaName,
                pattern: $"{AreaRoute.BackOffice.UrlPrefix.Trim('/')}/{{controller=Home}}/{{action=Index}}/{{id?}}");
        }
    }
}