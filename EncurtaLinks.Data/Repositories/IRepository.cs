using EncurtaLinks.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncurtaLinks.Data.Repositories
{
    public interface IRepository<T>
    {
        Task Add(T link);
        Task<T> GetByLink(string link);
    }
}
