using factory_minimal_edge_models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace factory_minimal_edge_ui.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<SiemensDevice> SiemensDevices { get; set; }

        public DbSet<SiemensTag> SiemensTags { get; set; }

    }
}
