using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Samples;

namespace Samples.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20160823174620_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CetoTree.RelationalTree", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("Trees");
                });

            modelBuilder.Entity("CetoTree.RelationalTreeNode<CetoTree.UnitTests.TestDataClasses.TestContent>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DataId");

                    b.Property<int>("PostOrderId");

                    b.Property<int>("PreOrderId");

                    b.Property<int>("TreeId");

                    b.HasKey("Id");

                    b.HasIndex("DataId");

                    b.HasIndex("TreeId");

                    b.ToTable("Nodes");
                });

            modelBuilder.Entity("CetoTree.UnitTests.TestDataClasses.TestContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.HasKey("Id");

                    b.ToTable("Data");
                });

            modelBuilder.Entity("CetoTree.RelationalTreeNode<CetoTree.UnitTests.TestDataClasses.TestContent>", b =>
                {
                    b.HasOne("CetoTree.UnitTests.TestDataClasses.TestContent", "Data")
                        .WithMany()
                        .HasForeignKey("DataId");

                    b.HasOne("CetoTree.RelationalTree", "Tree")
                        .WithMany()
                        .HasForeignKey("TreeId");
                });
        }
    }
}
