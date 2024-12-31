using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Mudemy.Data;
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=MudemyDb;User ID=sa;Password=YourNewStrong!Passw0rd;TrustServerCertificate=True;");

        return new AppDbContext(optionsBuilder.Options);
    }
}
