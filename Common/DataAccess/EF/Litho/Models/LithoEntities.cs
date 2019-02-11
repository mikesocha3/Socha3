using Microsoft.EntityFrameworkCore;
namespace Socha3.Common.DataAccess.EF.Litho.Models
{
    public class LithoEntities : DbContext
    {
        public LithoEntities() : base() { }
        public LithoEntities(DbContextOptions options) : base(options) { }
        public virtual DbSet<admin> Admins { get; set; }
        public virtual DbSet<fragrance_product> FragranceProducts { get; set; }
        public virtual DbSet<nutrition_nutrient> NutritionNutrients { get; set; }
        public virtual DbSet<nutrition_product> NutritionProducts { get; set; }
        public virtual DbSet<order> Orders { get; set; }
        public virtual DbSet<referral> Referrals { get; set; }
        public virtual DbSet<zip> Zips { get; set; }
        public virtual DbSet<zip_combo> ZipCombos { get; set; }
    }
}