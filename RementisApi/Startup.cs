using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RementisApi.Models;

namespace RementisApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //Database in memory
            // services.AddDbContext<SensordataContext>(opt => opt.UseInMemoryDatabase("SensordataList"));

            //Database op local SQL server
            //services.AddDbContext<SensordataContext>(opt => opt.UseSqlServer(@"Data Source=DESKTOP-83H393B\SQLEXPRESS01;Initial Catalog=RementisDb;Integrated Security=True;Pooling=False"));

            //Database op Azure Server
            services.AddDbContext<SensordataContext>(opt => opt.UseSqlServer(@"Data Source=rementisdb.database.windows.net;Initial Catalog=RementisDb;Integrated Security=False;User ID=rementis;Password=alaaf123!;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
