using Learning.Api.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Api.Repositories
{
    public partial class LearningDbContext : DbContext
    {
        public DbSet<LearningUser> Users { get; set; }

        public DbSet<LearningRole> Roles { get; set; }

        public DbSet<LearningClaim> Claims { get; set; }

        public LearningDbContext(DbContextOptions<LearningDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
