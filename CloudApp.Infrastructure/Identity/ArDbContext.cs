using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CloudApp.Infrastructure.Identity
{
    public class ArDbContext : IdentityDbContext
    {
        public ArDbContext(DbContextOptions options)
            :base(options)
        {
            
        }
    }
}
