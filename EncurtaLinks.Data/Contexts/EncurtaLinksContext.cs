using EncurtaLinks.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncurtaLinks.Data.Contexts
{
    public class EncurtaLinksContext : DbContext
    {
        public EncurtaLinksContext(DbContextOptions<EncurtaLinksContext> options) : base(options) { }

        public DbSet<LinkEncurtado> LinksEncurtados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LinkEncurtado>(builder =>
            {
                builder
                    .Property(linkEncurtado => linkEncurtado.UltimaParteUrl)
                    .HasMaxLength(LinkEncurtadoConfigs.MaxCaracteres);

                builder
                    .HasIndex(linkEncurtado => linkEncurtado.UltimaParteUrl)
                    .IsUnique();
            });
        }
    }
}
