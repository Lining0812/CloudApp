using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudApp.Infrastructure
{
    public class DbContextDesignTimeFactory : IDesignTimeDbContextFactory<MyDBContext>
    {
        public MyDBContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<MyDBContext> builder = new DbContextOptionsBuilder<MyDBContext>();
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MyDatabase;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new MyDBContext(builder.Options);
        }
    }
}
