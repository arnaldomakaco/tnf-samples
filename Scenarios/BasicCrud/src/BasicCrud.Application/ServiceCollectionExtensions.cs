﻿using BasicCrud.Application.AppServices;
using BasicCrud.Application.AppServices.Interfaces;
using BasicCrud.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace BasicCrud.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServiceDependency(this IServiceCollection services)
        {
            // Dependencia do projeto BasicCrud.Domain
            services.AddDomainDependency();

            // Registro dos serviços
            services.AddTransient<ICustomerAppService, CustomerAppService>();

            return services;
        }
    }
}