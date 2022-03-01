using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Community.Data
{
    public class CommunityMessageContext : DbContext
    {
        public CommunityMessageContext(DbContextOptions<CommunityMessageContext> options) : base(options)
        {
        }
        public DbSet<CommunityMessage> Messages { get; set; }
    }
}
