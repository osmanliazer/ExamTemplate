using IndigoExam.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IndigoExam.DAL
{
    public class IndigoContext:IdentityDbContext

    {
        public IndigoContext(DbContextOptions<IndigoContext> options) : base(options) { }
   
       public  DbSet<Category> Categories { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

    }
}
