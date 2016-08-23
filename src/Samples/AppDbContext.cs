using CetoTree;
using CetoTree.UnitTests.TestDataClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples
{
    public class AppDbContext:DbContext
    {
        public DbSet<RelationalTreeNode<TestContent>> Nodes { get; set; }

        public DbSet<RelationalTree> Trees { get; set; }

        public DbSet<TestContent> Data { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TreeStorage;Trusted_Connection=True;MultipleActiveResultSets=true;Connection Timeout=60;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RelationalTreeNode<TestContent>>().HasOne(p => p.Tree).WithMany().OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);

        }
    }
}
