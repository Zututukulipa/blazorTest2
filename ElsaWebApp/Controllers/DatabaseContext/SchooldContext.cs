using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ElsaWebApp.Models.Database;
using ElsaWebApp.Models.Views;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

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
                    .HasOne<DbUser>(bc => bc.User)
                    .WithMany(b => b.UserGroups)
                    .HasForeignKey(bc => bc.UserId);  
                
                modelBuilder.Entity<UserGroups>()
                    .HasOne<UserGroup>(bc => bc.Group)
                    .WithMany(c => c.UserGroups)
                    .HasForeignKey(bc => bc.GroupId);
                
                modelBuilder.Entity<UserSubjects>()
                    .HasKey(bc => new { bc.StudentId, bc.SubjectId });  
                
                modelBuilder.Entity<UserSubjects>()
                    .HasOne(bc => bc.Student)
                    .WithMany(b => b.UserSubjects)
                    .HasForeignKey(bc => bc.StudentId);  
                
                modelBuilder.Entity<UserSubjects>()
                    .HasOne(bc => bc.Subject)
                    .WithMany(c => c.UserSubjects)
                    .HasForeignKey(bc => bc.SubjectId);

                modelBuilder.Entity<ClassroomSubjects>()
                    .HasKey(bc => new { bc.ClassroomId, bc.SubjectId });  
                
                modelBuilder.Entity<ClassroomSubjects>()
                    .HasOne(cs => cs.Classroom)
                    .WithMany(c => c.Subjects)
                    .HasForeignKey(cs => cs.ClassroomId);
                
                modelBuilder.Entity<ClassroomSubjects>()
                    .HasOne(cs => cs.Subject)
                    .WithMany(s => s.Classrooms)
                    .HasForeignKey(cs => cs.SubjectId);
                
                modelBuilder.Entity<UserAnswers>()
                    .HasKey(bc => new { bc.UserId, bc.AnswerId });  
                
                modelBuilder.Entity<UserAnswers>()
                    .HasOne(cs => cs.User)
                    .WithMany(c => c.Answers)
                    .HasForeignKey(cs => cs.UserId);
                
                modelBuilder.Entity<UserAnswers>()
                    .HasOne(cs => cs.Answer)
                    .WithMany(s => s.Users)
                    .HasForeignKey(cs => cs.AnswerId);

                modelBuilder.Entity<ExamQuestion>()
                    .HasMany(a => a.Answers)
                    .WithOne(q => q.Question);

                modelBuilder.Entity<UserExamAnswersView>().HasNoKey();
            }


            public async Task<DbUser> Login(string email, string password)
            {
                await using var conn = new OracleConnection("DATA SOURCE=localhost/XE;USER ID=elsa; password=letitgo;");
                conn.Open();

                await using var command = conn.CreateCommand();
                command.CommandText =
                    @"begin :prm_Result := PKG_USER.LOGIN(:email, :pass); end;";

                command.Parameters.Add(":prm_Result", OracleDbType.RefCursor, ParameterDirection.Output);
                command.Parameters.Add(":email", OracleDbType.Varchar2).Value = email;
                command.Parameters.Add(":pass", OracleDbType.Varchar2).Value = password;


                OracleDataReader rdr = command.ExecuteReader();
                DbUser user = null;
                while (rdr.Read())
                {
                    user = new DbUser
                    {
                        UserId = rdr.GetInt32(0),
                        Firstname = rdr.GetString(1),
                        Surname = rdr.GetString(2),
                        BirthDate = rdr.GetDateTime(3),
                        Phone = rdr.GetString(4),
                        Email = rdr.GetString(5),
                        UserPassword = rdr.GetString(6),
                        Salt = rdr.GetString(7)
                    };
                    if (!rdr.IsDBNull(8))
                        user.StudyYear = rdr.GetInt32(8);
                    if (!rdr.IsDBNull(9))
                        user.ProfilePicture = rdr.GetOracleBlob(9).Value;
                    user.TimeCreated = rdr.GetDateTime(10);
                    user.RoleId = rdr.GetInt32(11);
                    user.AddressId = rdr.GetInt32(12);
                }
                return user;
            }
            
            
            
            public async Task RegisterUser(DbUser user)
            {
                await using var conn = new OracleConnection("DATA SOURCE=localhost/XE;USER ID=elsa; password=letitgo;");

                // Procedure RegisterUser(p_firstname IN DBUSER.FIRSTNAME%type, p_surname IN DBUSER.SURNAME%TYPE,
                //      p_birthdate IN DBUSER.BIRTH_DATE%type, p_phone IN DBUSER.PHONE%TYPE,
                //      p_studyYear IN DBUSER.STUDY_YEAR%TYPE, p_role IN DBUSER.ROLE_ID%TYPE, p_password IN DBUSER.USER_PASSWORD%type,
                //      p_address_street IN ADDRESS.STREET%type, p_address_houseNr IN ADDRESS.HOUSE_NR%TYPE,
                //      p_address_zip IN ADDRESS.ZIP%type, p_address_city IN ADDRESS.CITY%TYPE)
                
                await using var command = conn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"PKG_USER.RegisterUser";

                command.Parameters.Add("p_firstname", OracleDbType.Varchar2).Value = user.Firstname;
                command.Parameters.Add("p_surname", OracleDbType.Varchar2).Value = user.Surname;
                command.Parameters.Add("p_birthdate", OracleDbType.Date).Value = user.BirthDate;
                command.Parameters.Add("p_phone", OracleDbType.Varchar2).Value = user.Phone;
                command.Parameters.Add("p_studyYear", OracleDbType.Decimal).Value = user.StudyYear;
                command.Parameters.Add("p_role", OracleDbType.Decimal).Value = user.RoleId;
                command.Parameters.Add("p_password", OracleDbType.Varchar2).Value = user.UserPassword;
                command.Parameters.Add("p_address_street", OracleDbType.Varchar2).Value = user.Address.Street;
                command.Parameters.Add("p_address_houseNr", OracleDbType.Varchar2).Value = user.Address.HouseNr;
                command.Parameters.Add("p_address_zip", OracleDbType.Varchar2).Value = user.Address.Zip;
                command.Parameters.Add("p_address_city", OracleDbType.Varchar2).Value = user.Address.City;

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
    }
}