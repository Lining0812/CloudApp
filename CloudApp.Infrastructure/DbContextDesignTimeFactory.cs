using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CloudApp.Infrastructure
{
    public class DbContextDesignTimeFactory : IDesignTimeDbContextFactory<MyDBContext>
    {
        public MyDBContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<MyDBContext> builder = new DbContextOptionsBuilder<MyDBContext>();
            //builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MyDatabase;Trusted_Connection=True;MultipleActiveResultSets=true");
            //return new MyDBContext(builder.Options);

            // 从环境变量或命令行参数获取数据库类型
            var dbType = Environment.GetEnvironmentVariable("DB_TYPE") ?? "SqlServer";

            if (dbType.Equals("mysql", StringComparison.OrdinalIgnoreCase))
            {
                // MySQL 配置
                var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                    ?? "Server=localhost;Database=neverland;User=root;Password=123456;";
                builder.UseMySQL(connectionString);
            }
            else
            {
                // SQL Server 配置（默认）
                var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                    ?? "Server=(localdb)\\mssqllocaldb;Database=neverland;Trusted_Connection=True;";
                builder.UseSqlServer(connectionString);
            }

            return new MyDBContext(builder.Options);
        }
    }
}
