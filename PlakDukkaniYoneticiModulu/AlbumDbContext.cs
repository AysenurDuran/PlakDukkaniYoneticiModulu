using System;
using System.Data.Entity;
using System.Linq;

namespace PlakDukkaniYoneticiModulu
{
    public class AlbumDbContext : DbContext
    {
        // Your context has been configured to use a 'AlbumDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'PlakDukkaniYoneticiModulu.AlbumDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'AlbumDbContext' 
        // connection string in the application configuration file.
        public AlbumDbContext()
            : base("name=AlbumDbContext")
        {
        }
        public DbSet<Album> Albums { get; set; }    
        public DbSet<User> Users { get; set; }  
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}