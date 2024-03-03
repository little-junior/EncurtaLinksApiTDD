using EncurtaLinks.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EncurtaLinks.Data.Contexts;

namespace EncurtaLinks.Data.Repositories
{
    public class EncurtaLinksRepository : IRepository<LinkEncurtado>
    {
        private readonly EncurtaLinksContext _context;

        public EncurtaLinksRepository(EncurtaLinksContext context)
        {
            _context = context;
        }

        public async Task Add(LinkEncurtado link)
        {
            await _context.LinksEncurtados.AddAsync(link);
            await _context.SaveChangesAsync();
        }

        public async Task<LinkEncurtado> GetByLink(string link)
        {
            var result = await _context.LinksEncurtados.SingleOrDefaultAsync(l => l.UrlGerado == link);

            return result;
        }
    }
}
