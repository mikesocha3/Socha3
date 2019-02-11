using Microsoft.EntityFrameworkCore;

namespace Socha3.Common.DataAccess.EF.Domo.Models
{
    public class DomoEntities : DbContext
    {
        public DomoEntities() : base() { }

        public DomoEntities(DbContextOptions options) : base(options) { }

        public virtual DbSet<Error> Errors { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Meme> Memes { get; set; }

        public virtual DbSet<Email> Emails { get; set; }
    }
}