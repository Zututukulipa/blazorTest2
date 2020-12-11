using ElsaWebApp.Models.Database;
using ElsaWebApp.Models.Views;
using Microsoft.EntityFrameworkCore;

namespace ElsaWebApp.Controllers.DataAccess
{
    public class SchooldContext : DbContext
    {
            public DbSet<DbUser> Students { get; set; }
            public DbSet<DbRole> Roles { get; set; }
            public DbSet<Address> Addresses { get; set; }
            public DbSet<Classroom> Classrooms { get; set; }
            public DbSet<ClassroomSubjects> ClassroomSubjects { get; set; }
            public DbSet<Comment> Comments { get; set; }
            public DbSet<Exam> Exams { get; set; }
            public DbSet<ExamResult> ExamResults { get; set; }
            public DbSet<ExamType> ExamTypes { get; set; }
            public DbSet<ExamQuestion> ExamQuestions { get; set; }
            public DbSet<ExamAnswer> ExamAnswers { get; set; }
            public DbSet<MessageRecipient> Recipients { get; set; }
            public DbSet<UserAnswers> UserAnswers { get; set; }
            public DbSet<AnswerType> AnswerTypes { get; set; }
            public DbSet<PrivateMessage> PrivateMessages { get; set; }
            public DbSet<Subject> Subjects { get; set; }
            public DbSet<SupportMaterial> SupportMaterials { get; set; }
            public DbSet<UserGroup> UserGroupSet { get; set; }
            public DbSet<UserGroups> UserGroups { get; set; }
            public DbSet<UserSubjects> UserSubjects { get; set; }
            public DbSet<UserExamAnswersView> UserExamAnswersView { get; set; }
            

            public SchooldContext(DbContextOptions<SchooldContext> options) : base(options)
            {
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
            }
            
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                
                modelBuilder.Entity<UserGroups>()
                    .HasKey(bc => new { bc.UserId, bc.GroupId});  

                modelBuilder.Entity<UserGroups>()
                    .HasOne(bc => bc.User)
                    .WithMany(b => b.UserGroups)
                    .HasForeignKey(bc => bc.GroupId);  
                
                modelBuilder.Entity<UserGroups>()
                    .HasOne(bc => bc.Group)
                    .WithMany(c => c.UserGroups)
                    .HasForeignKey(bc => bc.UserId);
                
                modelBuilder.Entity<UserSubjects>()
                    .HasKey(bc => new { bc.StudentId, bc.SubjectId });  
                
                modelBuilder.Entity<UserSubjects>()
                    .HasOne(bc => bc.Student)
                    .WithMany(b => b.UserSubjects)
                    .HasForeignKey(bc => bc.SubjectId);  
                
                modelBuilder.Entity<UserSubjects>()
                    .HasOne(bc => bc.Subject)
                    .WithMany(c => c.Students)
                    .HasForeignKey(bc => bc.StudentId);

                modelBuilder.Entity<ClassroomSubjects>()
                    .HasKey(bc => new { bc.ClassroomId, bc.SubjectId });  
                
                modelBuilder.Entity<ClassroomSubjects>()
                    .HasOne(cs => cs.Classroom)
                    .WithMany(c => c.Subjects)
                    .HasForeignKey(cs => cs.SubjectId);
                
                modelBuilder.Entity<ClassroomSubjects>()
                    .HasOne(cs => cs.Subject)
                    .WithMany(s => s.Classrooms)
                    .HasForeignKey(cs => cs.ClassroomId);
                
                modelBuilder.Entity<UserAnswers>()
                    .HasKey(bc => new { bc.UserId, bc.AnswerId });  
                
                modelBuilder.Entity<UserAnswers>()
                    .HasOne(cs => cs.User)
                    .WithMany(c => c.Answers)
                    .HasForeignKey(cs => cs.AnswerId);
                
                modelBuilder.Entity<UserAnswers>()
                    .HasOne(cs => cs.Answer)
                    .WithMany(s => s.Users)
                    .HasForeignKey(cs => cs.UserId);

                modelBuilder.Entity<ExamQuestion>()
                    .HasMany(a => a.Answers)
                    .WithOne(q => q.Question);

                modelBuilder.Entity<UserExamAnswersView>().HasNoKey();
            }
    }
}