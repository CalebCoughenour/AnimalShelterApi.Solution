using Microsoft.EntityFrameworkCore;
using AnimalShelterApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AnimalShelterApi.Models
{
  public class AnimalShelterApiContext : DbContext
  {
      public AnimalShelterApiContext(DbContextOptions<AnimalShelterApiContext> options)
            : base(options)
        {
        }

        public DbSet<Animal> Animals { get; set; }
  }
}