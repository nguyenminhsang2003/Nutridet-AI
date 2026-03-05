using Microsoft.EntityFrameworkCore;
using System;

namespace nutridet_ai_api.Models
{
    public class NutridetAiDbContext : DbContext
    {
        public NutridetAiDbContext(DbContextOptions<NutridetAiDbContext> options)
        : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<ScanImage> ScanImages { get; set; }
        public DbSet<OutputNutrition> OutputNutritions { get; set; }
        public DbSet<NutritionVisualRule> NutritionVisualRules { get; set; }
        public DbSet<OutputNutritionVisual> OutputNutritionVisuals { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ScanImage>()
                .HasOne(x => x.User)
                .WithMany(u => u.ScanImages)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<OutputNutrition>()
                .HasOne(x => x.ScanImage)
                .WithOne(s => s.OutputNutrition)
                .HasForeignKey<OutputNutrition>(o => o.ScanImageId);

            modelBuilder.Entity<OutputNutritionVisual>()
                .HasOne(x => x.OutputNutrition)
                .WithMany(u => u.OutputNutritionVisuals)
                .HasForeignKey(x => x.OutputNutritionId);
        }
    }
}
