using Microsoft.EntityFrameworkCore;

namespace Project1.Models
{
    public class TripContext : DbContext 
    {

        public TripContext(DbContextOptions<TripContext> options) : base(options) { }

        public DbSet<Trip> Trips { get; set; }
    }
}
