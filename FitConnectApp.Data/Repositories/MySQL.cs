// using Microsoft.EntityFrameworkCore.Design;
// using Microsoft.EntityFrameworkCore;

// namespace FitConnectApp.Data.Repositories
// {
//     public class FitConnectContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
//     {
//         public DatabaseContext CreateDbContext(string[] args)
//         {
//             var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
//             optionsBuilder.UseMySql("Server=localhost;Port=3306;Database=mydb;User=root;Password=Paramore101!;");

//             return new DatabaseContext(optionsBuilder.Options);
//         }
//     }
// }