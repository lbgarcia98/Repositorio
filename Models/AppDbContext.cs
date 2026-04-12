using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProyectoUniversidad.Models
{
    public class AppDbContext
    {
        public class  AppDbContext :
            IdentityDbContext<IdentityUser>
        {
            public AppDbContext
                (DbContextOptions<AppDbContext> options)
                : base(options)
            {

            }
        }
    }
}
