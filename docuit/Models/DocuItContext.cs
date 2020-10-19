﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DocuItService.Models
{
    public partial class DocuItContext : DbContext
    {
        public DocuItContext()
        {
        }

        public DocuItContext(DbContextOptions<DocuItContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BuildingType> BuildingType { get; set; }
        public virtual DbSet<BuildingTypeProject> BuildingTypeProject { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Dossier> Dossier { get; set; }
        public virtual DbSet<DossierElement> DossierElement { get; set; }
        public virtual DbSet<DossierElementQuestionnaire> DossierElementQuestionnaire { get; set; }
        public virtual DbSet<ElementType> ElementType { get; set; }
        public virtual DbSet<InventoryReport> InventoryReport { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<ProjectSecurity> ProjectSecurity { get; set; }
        public virtual DbSet<Security> Security { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<WorkingCenter> WorkingCenter { get; set; }
        public virtual DbSet<WorkingCenterProject> WorkingCenterProject { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BuildingType>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.Id })
                    .HasName("PRIMARY");

                entity.ToTable("building_type", "DocuIt");

                entity.HasIndex(e => e.CompanyId)
                    .HasName("fk_building_type_company_idx");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.BuildingType)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_building_type_company");
            });

            modelBuilder.Entity<BuildingTypeProject>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.ProjectId, e.CompanyId })
                    .HasName("PRIMARY");

                entity.ToTable("building_type_project", "DocuIt");

                entity.HasIndex(e => new { e.CompanyId, e.Id })
                    .HasName("fk_building_type_project_building_type1_idx");

                entity.HasIndex(e => new { e.CompanyId, e.ProjectId })
                    .HasName("fk_bulding_type_project1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.HasOne(d => d.BuildingType)
                    .WithMany(p => p.BuildingTypeProject)
                    .HasForeignKey(d => new { d.CompanyId, d.Id })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_building_type_project_building_type");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("company", "DocuIt");

                entity.HasIndex(e => e.CompanyId)
                    .HasName("main_index");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FiscalId)
                    .IsRequired()
                    .HasColumnName("fiscal_id")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Town)
                    .HasColumnName("town")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasColumnName("zip_code")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Dossier>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.ProjectId, e.DossierId })
                    .HasName("PRIMARY");

                entity.ToTable("dossier", "DocuIt");

                entity.HasIndex(e => new { e.CompanyId, e.ProjectId, e.UserId })
                    .HasName("fk_dossier_project_security_idx");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.DossierId).HasColumnName("dossier_id");

                entity.Property(e => e.CreationTime)
                    .HasColumnName("creation_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LocationLatitude).HasColumnName("location_latitude");

                entity.Property(e => e.LocationLongitude).HasColumnName("location_longitude");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenceId)
                    .IsRequired()
                    .HasColumnName("reference_id")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.ProjectSecurity)
                    .WithMany(p => p.Dossier)
                    .HasForeignKey(d => new { d.CompanyId, d.ProjectId, d.UserId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_dossier_project_security");
            });

            modelBuilder.Entity<DossierElement>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.ProjectId, e.DossierId, e.ElementId })
                    .HasName("PRIMARY");

                entity.ToTable("dossier_element", "DocuIt");

                entity.HasIndex(e => new { e.CompanyId, e.ElementTypeId })
                    .HasName("fk_dossier_element_element_type_idx");

                entity.HasIndex(e => new { e.FileId, e.CompanyId })
                    .HasName("index");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.DossierId).HasColumnName("dossier_id");

                entity.Property(e => e.ElementId).HasColumnName("element_id");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreationTime)
                    .HasColumnName("creation_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ElementTypeId).HasColumnName("element_type_id");

                entity.Property(e => e.FileId)
                    .HasColumnName("file_id")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.LocationLatitude).HasColumnName("location_latitude");

                entity.Property(e => e.LocationLongitude).HasColumnName("location_longitude");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Dossier)
                    .WithMany(p => p.DossierElement)
                    .HasForeignKey(d => new { d.CompanyId, d.ProjectId, d.DossierId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_dossier_element_dossier");
            });

            modelBuilder.Entity<DossierElementQuestionnaire>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.ProjectId, e.DossierId, e.DossierElementId, e.Id })
                    .HasName("PRIMARY");

                entity.ToTable("dossier_element_questionnaire", "DocuIt");

                entity.HasIndex(e => new { e.CompanyId, e.ProjectId, e.DossierId, e.DossierElementId })
                    .HasName("fk_dossier_element_questionnaire_dossier_element_idx");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.DossierId).HasColumnName("dossier_id");

                entity.Property(e => e.DossierElementId).HasColumnName("dossier_element_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.HasOne(d => d.DossierElement)
                    .WithMany(p => p.DossierElementQuestionnaire)
                    .HasForeignKey(d => new { d.CompanyId, d.ProjectId, d.DossierId, d.DossierElementId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_dossier_element_questionnaire_dossier_element1");
            });

            modelBuilder.Entity<ElementType>(entity =>
            {
                entity.HasKey(e => new { e.ElementTypeId, e.CompanyId })
                    .HasName("PRIMARY");

                entity.ToTable("element_type", "DocuIt");

                entity.HasIndex(e => e.CompanyId)
                    .HasName("fk_element_type_company1_idx");

                entity.Property(e => e.ElementTypeId).HasColumnName("element_type_id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.ElementType)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_element_type_company1");
            });

            modelBuilder.Entity<InventoryReport>(entity =>
            {
                entity.ToTable("inventory_report", "DocuIt");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.CompanyId })
                    .HasName("PRIMARY");

                entity.ToTable("project", "DocuIt");

                entity.HasIndex(e => e.CompanyId)
                    .HasName("fk_project_company_idx");

                entity.HasIndex(e => e.StatusId)
                    .HasName("status_index");

                entity.HasIndex(e => new { e.CompanyId, e.ProjectId })
                    .HasName("project_index");

                entity.Property(e => e.ProjectId)
                    .HasColumnName("project_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.CreationTime)
                    .HasColumnName("creation_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenceId)
                    .HasColumnName("reference_id")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_project_company");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_project_status");
            });

            modelBuilder.Entity<ProjectSecurity>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.ProjectId, e.UserId })
                    .HasName("PRIMARY");

                entity.ToTable("project_security", "DocuIt");

                entity.HasIndex(e => new { e.CompanyId, e.UserId })
                    .HasName("fk_project_security_user_idx");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.CreationTime)
                    .HasColumnName("creation_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsOwner)
                    .HasColumnName("is_owner")
                    .HasComment(@"Boolean meaning the user is the creator of the project.
");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProjectSecurity)
                    .HasForeignKey(d => new { d.CompanyId, d.UserId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_project_security_user");
            });

            modelBuilder.Entity<Security>(entity =>
            {
                entity.ToTable("security", "DocuIt");

                entity.Property(e => e.SecurityId).HasColumnName("security_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("status", "DocuIt");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.UserId })
                    .HasName("PRIMARY");

                entity.ToTable("user", "DocuIt");

                entity.HasIndex(e => e.SecurityId)
                    .HasName("index_security");

                entity.HasIndex(e => new { e.CompanyId, e.Email })
                    .HasName("email_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => new { e.CompanyId, e.UserId })
                    .HasName("user_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => new { e.CompanyId, e.Username })
                    .HasName("username_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.CreationTime)
                    .HasColumnName("creation_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FamilyName)
                    .IsRequired()
                    .HasColumnName("family_name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Locked)
                    .HasColumnName("locked")
                    .HasComment("User can be locked down.");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SecurityId)
                    .HasColumnName("security_id")
                    .HasDefaultValueSql("'3'");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_company");

                entity.HasOne(d => d.Security)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.SecurityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_security1");
            });

            modelBuilder.Entity<WorkingCenter>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.CompanyId })
                    .HasName("PRIMARY");

                entity.ToTable("working_center", "DocuIt");

                entity.HasIndex(e => e.CompanyId)
                    .HasName("fk_working_center_company_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.WorkingCenter)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_working_center_company");
            });

            modelBuilder.Entity<WorkingCenterProject>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.CompanyId, e.ProjectId })
                    .HasName("PRIMARY");

                entity.ToTable("working_center_project", "DocuIt");

                entity.HasIndex(e => new { e.CompanyId, e.ProjectId })
                    .HasName("fk_working_center_project_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.HasOne(d => d.WorkingCenter)
                    .WithMany(p => p.WorkingCenterProject)
                    .HasForeignKey(d => new { d.Id, d.CompanyId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_working_center_project_working_center");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}