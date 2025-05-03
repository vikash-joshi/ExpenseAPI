using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitectureDemo.Infrastructure.Persistence;
using CleanArchitectureDemo.Domain.Repositories;
using Microsoft.EntityFrameworkCore;


namespace CleanArchitectureDemo.Infrastructure
{
    /*public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string configuration)
        {
            //services.AddDbContext<AppDbContext>(options =>
             //   options.UseSqlServer(configuration));

             services.AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(configuration));


            services.AddScoped<IExpenseRepository, ExpenseRepository>();

            return services;
        }
    }*/

    public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration));
            services.AddScoped<IExpenseRepository, ExpenseRepository>();

        return services;
    }
}

}