using MenorPreco.DAL.Estabelecimentos;
using MenorPreco.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenorPreco.Api
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
            services.AddApiVersioning();

            //services.AddDbContext<MenorPrecoContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnectionString")));
            services.AddDbContext<MenorPrecoContext>(options =>
               options.UseSqlite(Configuration.GetConnectionString("SqLiteConnectionString")));

            services.AddTransient<IRepository<Produto>, RepositorioBaseEF<Produto>>();
            services.AddTransient<IRepository<Estabelecimento>, RepositorioBaseEF<Estabelecimento>>();
            services.AddTransient<IRepository<Venda>, RepositorioBaseEF<Venda>>();
            services.AddTransient<IRepository<VendaProduto>, RepositorioBaseEF<VendaProduto>>();

            //services.AddControllersWithViews();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            serviceProvider
                .GetService<MenorPrecoContext>().Database.EnsureCreated();

            serviceProvider
                .GetService<MenorPrecoContext>().Database.Migrate();
        }
    }
}
