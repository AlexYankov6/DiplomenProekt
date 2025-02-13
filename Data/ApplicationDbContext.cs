using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProektAleks.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ReservationApartment> ReservationsApartments { get; set; }
        public DbSet<ReservationHouse> ReservationsHouses { get; set; }
        

    }
}
