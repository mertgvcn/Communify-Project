using Microsoft.EntityFrameworkCore;


namespace CommunifyLibrary
{
    public class CommunifyContext : DbContext
    {
        public CommunifyContext(DbContextOptions<CommunifyContext> options) : base(options)
        {
            
        }


    }
}
