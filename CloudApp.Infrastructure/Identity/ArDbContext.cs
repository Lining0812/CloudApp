using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CloudApp.Infrastructure.Identity
{
    public class ArDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public ArDbContext(DbContextOptions options)
            :base(options)
        {
        }

    }
}
