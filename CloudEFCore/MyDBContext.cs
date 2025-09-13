using CloudEFCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudEFCore
{
    public class MyDBContext: DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options)
            :base(options)
        { 
        }
        public DbSet<User> Users { get; set; }
        public DbSet<SingleSong> SingleSongs { get; set; }
    }
}
