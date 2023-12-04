using DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitofWork
{
    public class UnitofWork : IUnitofWork
    {
        private readonly AppDbContext context;

        public UnitofWork(AppDbContext context)
        {
            this.context = context;
        }
        public async Task CommitAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
