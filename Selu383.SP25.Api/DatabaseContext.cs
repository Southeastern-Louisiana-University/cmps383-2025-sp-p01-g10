using Microsoft.EntityFrameworkCore;

namespace Selu383.SP25.Api.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    }
}
