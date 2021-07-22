using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

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

        public virtual DbSet<BuildingType> BuildingTypes { get; set; }
        public virtual DbSet<BuildingTypeProject> BuildingTypeProjects { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Dossier> Dossiers { get; set; }
        public virtual DbSet<DossierElement> DossierElements { get; set; }
        public virtual DbSet<DossierElementQuestionnaire> DossierElementQuestionnaires { get; set; }
        public virtual DbSet<ElementType> ElementTypes { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectSecurity> ProjectSecurities { get; set; }
        public virtual DbSet<ProjectUserSecurity> ProjectUserSecurities { get; set; }
        public virtual DbSet<Questionnaire> Questionnaires { get; set; }
        public virtual DbSet<QuestionnaireParagraph> QuestionnaireParagraphs { get; set; }
        public virtual DbSet<QuestionnaireQuestion> QuestionnaireQuestions { get; set; }
        public virtual DbSet<QuestionnaireReport> QuestionnaireReports { get; set; }
        public virtual DbSet<QuestionnaireReportAnswer> QuestionnaireReportAnswers { get; set; }
        public virtual DbSet<QuestionnaireType> QuestionnaireTypes { get; set; }
        public virtual DbSet<Security> Securities { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<WorkingCenter> WorkingCenters { get; set; }
        public virtual DbSet<WorkingCenterProject> WorkingCenterProjects { get; set; }

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

                entity.ToTable("building_type");

                entity.HasIndex(e => e.CompanyId, "fk_building_type_company_idx");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.Id)
                    .HasMaxLength(5)
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(25)
                    .HasColumnName("name");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.BuildingTypes)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_building_type_company");
            });

            modelBuilder.Entity<BuildingTypeProject>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.ProjectId, e.CompanyId })
                    .HasName("PRIMARY");

                entity.ToTable("building_type_project");

                entity.HasIndex(e => new { e.CompanyId, e.Id }, "fk_building_type_project_building_type1_idx");

                entity.HasIndex(e => new { e.CompanyId, e.ProjectId }, "fk_bulding_type_project1_idx");

                entity.Property(e => e.Id)
                    .HasMaxLength(5)
                    .HasColumnName("id");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.HasOne(d => d.BuildingType)
                    .WithMany(p => p.BuildingTypeProjects)
                    .HasForeignKey(d => new { d.CompanyId, d.Id })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_building_type_project_building_type");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("company");

                entity.HasIndex(e => e.CompanyId, "main_index");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.FiscalId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("fiscal_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("name");

                entity.Property(e => e.Town)
                    .HasMaxLength(30)
                    .HasColumnName("town");

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(10)
                    .HasColumnName("zip_code");
            });

            modelBuilder.Entity<Dossier>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.ProjectId, e.DossierId })
                    .HasName("PRIMARY");

                entity.ToTable("dossier");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.DossierId).HasColumnName("dossier_id");

                entity.Property(e => e.ChildId).HasColumnName("child_id");

                entity.Property(e => e.LocationLatitude).HasColumnName("location_latitude");

                entity.Property(e => e.LocationLongitude).HasColumnName("location_longitude");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.Property(e => e.ReferenceId)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("reference_id");
            });

            modelBuilder.Entity<DossierElement>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.ProjectId, e.DossierId, e.ElementId })
                    .HasName("PRIMARY");

                entity.ToTable("dossier_element");

                entity.HasIndex(e => new { e.CompanyId, e.ElementTypeId }, "fk_dossier_element_element_type_idx");

                entity.HasIndex(e => new { e.FileId, e.CompanyId }, "index");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.DossierId).HasColumnName("dossier_id");

                entity.Property(e => e.ElementId).HasColumnName("element_id");

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .HasColumnName("comment");

                entity.Property(e => e.ElementTypeId).HasColumnName("element_type_id");

                entity.Property(e => e.FileId)
                    .HasMaxLength(25)
                    .HasColumnName("file_id");

                entity.Property(e => e.LocationLatitude).HasColumnName("location_latitude");

                entity.Property(e => e.LocationLongitude).HasColumnName("location_longitude");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.HasOne(d => d.Dossier)
                    .WithMany(p => p.DossierElements)
                    .HasForeignKey(d => new { d.CompanyId, d.ProjectId, d.DossierId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_dossier_element_dossier");
            });

            modelBuilder.Entity<DossierElementQuestionnaire>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.ProjectId, e.DossierId, e.DossierElementId, e.Id })
                    .HasName("PRIMARY");

                entity.ToTable("dossier_element_questionnaire");

                entity.HasIndex(e => new { e.CompanyId, e.ProjectId, e.DossierId, e.DossierElementId }, "fk_dossier_element_questionnaire_dossier_element_idx");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.DossierId).HasColumnName("dossier_id");

                entity.Property(e => e.DossierElementId).HasColumnName("dossier_element_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.HasOne(d => d.DossierElement)
                    .WithMany(p => p.DossierElementQuestionnaires)
                    .HasForeignKey(d => new { d.CompanyId, d.ProjectId, d.DossierId, d.DossierElementId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_dossier_element_questionnaire_dossier_element1");
            });

            modelBuilder.Entity<ElementType>(entity =>
            {
                entity.HasKey(e => new { e.ElementTypeId, e.CompanyId })
                    .HasName("PRIMARY");

                entity.ToTable("element_type");

                entity.HasIndex(e => e.CompanyId, "fk_element_type_company1_idx");

                entity.Property(e => e.ElementTypeId).HasColumnName("element_type_id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("name");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.ElementTypes)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_element_type_company1");
            });

            modelBuilder.Entity<Picture>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.ProjectId, e.DossierId, e.ReportId, e.PictureId })
                    .HasName("PRIMARY");

                entity.ToTable("pictures");

                entity.HasIndex(e => new { e.CompanyId, e.ProjectId, e.DossierId, e.ReportId }, "fk_company_1_inventory_report1_idx");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.DossierId).HasColumnName("dossier_id");

                entity.Property(e => e.ReportId)
                    .HasMaxLength(5)
                    .HasColumnName("report_id");

                entity.Property(e => e.PictureId)
                    .HasMaxLength(45)
                    .HasColumnName("picture_id");

                entity.Property(e => e.Angle)
                    .HasColumnType("decimal(3,1)")
                    .HasColumnName("angle");

                entity.Property(e => e.Comment)
                    .HasMaxLength(45)
                    .HasColumnName("comment");

                entity.Property(e => e.Description)
                    .HasMaxLength(45)
                    .HasColumnName("description");

                entity.Property(e => e.Height)
                    .HasColumnName("height")
                    .HasComment("In Centimeters");

                entity.Property(e => e.Image)
                    .HasColumnType("blob")
                    .HasColumnName("image");

                entity.Property(e => e.Latitude)
                    .HasColumnType("decimal(10,7)")
                    .HasColumnName("latitude");

                entity.Property(e => e.Longitude)
                    .HasColumnType("decimal(11,8)")
                    .HasColumnName("longitude");

                entity.Property(e => e.Orientation)
                    .HasColumnType("decimal(4,1)")
                    .HasColumnName("orientation");

                entity.HasOne(d => d.QuestionnaireReport)
                    .WithMany(p => p.Pictures)
                    .HasForeignKey(d => new { d.CompanyId, d.ProjectId, d.DossierId, d.ReportId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_company_1_inventory_report1");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.CompanyId })
                    .HasName("PRIMARY");

                entity.ToTable("project");

                entity.HasIndex(e => e.CompanyId, "fk_project_company_idx");

                entity.HasIndex(e => new { e.CompanyId, e.OwnerUserId }, "fk_project_user1_idx");

                entity.HasIndex(e => new { e.CompanyId, e.ProjectId }, "project_index");

                entity.HasIndex(e => e.StatusId, "status_index");

                entity.Property(e => e.ProjectId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("project_id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");

                entity.Property(e => e.OwnerUserId).HasColumnName("owner_user_id");

                entity.Property(e => e.ReferenceId)
                    .HasMaxLength(45)
                    .HasColumnName("reference_id");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_project_company");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_project_status");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => new { d.CompanyId, d.OwnerUserId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_project_user");
            });

            modelBuilder.Entity<ProjectSecurity>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.ProjectId, e.UserId })
                    .HasName("PRIMARY");

                entity.ToTable("project_security");

                entity.HasIndex(e => new { e.CompanyId, e.UserId }, "fk_project_security_user_idx");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Rights).HasColumnName("rights");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProjectSecurities)
                    .HasForeignKey(d => new { d.CompanyId, d.UserId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_project_security_user");
            });

            modelBuilder.Entity<ProjectUserSecurity>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("project_user_security");

                entity.Property(e => e.FamilyName)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Rights)
                    .HasColumnName("rights")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Questionnaire>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("questionnaire");

                entity.Property(e => e.Answer).HasMaxLength(45);

                entity.Property(e => e.ParagraphName).HasMaxLength(45);

                entity.Property(e => e.QuestionId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.QuestionText).HasMaxLength(45);

                entity.Property(e => e.QuestionnaireReportId)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.QuestionnaireTypeId)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.QuestionnaireTypeName)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<QuestionnaireParagraph>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.QuestionnaireTypeId, e.Id })
                    .HasName("PRIMARY");

                entity.ToTable("questionnaire_paragraph");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.QuestionnaireTypeId)
                    .HasMaxLength(10)
                    .HasColumnName("questionnaire_type_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");

                entity.Property(e => e.SortIndex).HasColumnName("sort_index");

                entity.HasOne(d => d.QuestionnaireType)
                    .WithMany(p => p.QuestionnaireParagraphs)
                    .HasForeignKey(d => new { d.CompanyId, d.QuestionnaireTypeId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_inventory_paragraph_inventory_type");
            });

            modelBuilder.Entity<QuestionnaireQuestion>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.QuestionnaireTypeId, e.Id, e.QuestionId })
                    .HasName("PRIMARY");

                entity.ToTable("questionnaire_questions");

                entity.HasIndex(e => new { e.CompanyId, e.QuestionnaireTypeId }, "fk_inventory_questions_inventory_type_idx");

                entity.HasIndex(e => new { e.CompanyId, e.Id, e.QuestionnaireTypeId }, "index_paragraph_id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.QuestionnaireTypeId)
                    .HasMaxLength(10)
                    .HasColumnName("questionnaire_type_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.QuestionId)
                    .HasMaxLength(25)
                    .HasColumnName("question_id");

                entity.Property(e => e.ParagraphId).HasColumnName("paragraph_id");

                entity.Property(e => e.QuestionText)
                    .HasMaxLength(45)
                    .HasColumnName("question_text");

                entity.Property(e => e.SortIndex).HasColumnName("sort_index");

                entity.HasOne(d => d.QuestionnaireType)
                    .WithMany(p => p.QuestionnaireQuestions)
                    .HasForeignKey(d => new { d.CompanyId, d.QuestionnaireTypeId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_inventory_questions_inventory_type");
            });

            modelBuilder.Entity<QuestionnaireReport>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.ProjectId, e.DossierId, e.Id })
                    .HasName("PRIMARY");

                entity.ToTable("questionnaire_report");

                entity.HasIndex(e => new { e.CompanyId, e.ProjectId, e.BuildingTypeId }, "fk_inventory_report_building_type_project1_idx");

                entity.HasIndex(e => new { e.CompanyId, e.ProjectId, e.DossierId }, "fk_inventory_report_dossier1_idx");

                entity.HasIndex(e => new { e.WorkingCenterId, e.CompanyId, e.ProjectId }, "fk_inventory_report_working_center_project1_idx");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.DossierId).HasColumnName("dossier_id");

                entity.Property(e => e.Id)
                    .HasMaxLength(45)
                    .HasColumnName("id");

                entity.Property(e => e.BuildingTypeId)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("building_type_id");

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .HasColumnName("comment");

                entity.Property(e => e.QuestionnaireTypeId)
                    .HasMaxLength(25)
                    .HasColumnName("questionnaire_type_id");

                entity.Property(e => e.WorkingCenterId)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("working_center_id");

                entity.HasOne(d => d.Dossier)
                    .WithMany(p => p.QuestionnaireReports)
                    .HasForeignKey(d => new { d.CompanyId, d.ProjectId, d.DossierId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_inventory_report_dossier");

                entity.HasOne(d => d.WorkingCenterProject)
                    .WithMany(p => p.QuestionnaireReports)
                    .HasForeignKey(d => new { d.WorkingCenterId, d.CompanyId, d.ProjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_inventory_report_working_center_project");
            });

            modelBuilder.Entity<QuestionnaireReportAnswer>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.ProjectId, e.DossierId, e.QuestionnaireReportId, e.QuestionId })
                    .HasName("PRIMARY");

                entity.ToTable("questionnaire_report_answers");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.DossierId).HasColumnName("dossier_id");

                entity.Property(e => e.QuestionnaireReportId)
                    .HasMaxLength(45)
                    .HasColumnName("questionnaire_report_id");

                entity.Property(e => e.QuestionId)
                    .HasMaxLength(45)
                    .HasColumnName("question_id");

                entity.Property(e => e.Answer)
                    .HasMaxLength(45)
                    .HasColumnName("answer");

                entity.HasOne(d => d.QuestionnaireReport)
                    .WithMany(p => p.QuestionnaireReportAnswers)
                    .HasForeignKey(d => new { d.CompanyId, d.ProjectId, d.DossierId, d.QuestionnaireReportId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_questionnaire_report_answer_questionnaire_report");
            });

            modelBuilder.Entity<QuestionnaireType>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.Id })
                    .HasName("PRIMARY");

                entity.ToTable("questionnaire_type");

                entity.HasIndex(e => e.CompanyId, "fk_inventory_type_company1_idx");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("name");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.QuestionnaireTypes)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_inventory_type_company");
            });

            modelBuilder.Entity<Security>(entity =>
            {
                entity.ToTable("security");

                entity.Property(e => e.SecurityId).HasColumnName("security_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("status");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.UserId })
                    .HasName("PRIMARY");

                entity.ToTable("user");

                entity.HasIndex(e => new { e.CompanyId, e.Email }, "email_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.SecurityId, "index_security");

                entity.HasIndex(e => new { e.CompanyId, e.UserId }, "unique_company_user");

                entity.HasIndex(e => new { e.CompanyId, e.UserId }, "user_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => new { e.CompanyId, e.Username }, "username_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.FamilyName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("family_name");

                entity.Property(e => e.Image)
                    .HasColumnType("longblob")
                    .HasColumnName("image");

                entity.Property(e => e.Locked)
                    .HasColumnName("locked")
                    .HasComment("User can be locked down.");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("password");

                entity.Property(e => e.SecurityId)
                    .HasColumnName("security_id")
                    .HasDefaultValueSql("'3'");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("username");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_company");

                entity.HasOne(d => d.Security)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.SecurityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_security1");
            });

            modelBuilder.Entity<WorkingCenter>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.CompanyId })
                    .HasName("PRIMARY");

                entity.ToTable("working_center");

                entity.HasIndex(e => e.CompanyId, "fk_working_center_company_idx");

                entity.Property(e => e.Id)
                    .HasMaxLength(5)
                    .HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(25)
                    .HasColumnName("name");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.WorkingCenters)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_working_center_company");
            });

            modelBuilder.Entity<WorkingCenterProject>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.CompanyId, e.ProjectId })
                    .HasName("PRIMARY");

                entity.ToTable("working_center_project");

                entity.HasIndex(e => new { e.CompanyId, e.ProjectId }, "fk_working_center_project_idx");

                entity.Property(e => e.Id)
                    .HasMaxLength(5)
                    .HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("name");

                entity.HasOne(d => d.WorkingCenter)
                    .WithMany(p => p.WorkingCenterProjects)
                    .HasForeignKey(d => new { d.Id, d.CompanyId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_working_center_project_working_center");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
