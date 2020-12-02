using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElsaWebApp.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElsaWebApp.Controllers.DataAccess
{
    [ApiController]
    [Route("[controller]")]
    public class UserController
    {
        private OracleService OracleService { get; }
        private SchooldContext Context { get; }

        public UserController(SchooldContext context)
        {
            Context = context;
        }

        // public User Login(string email, string password)
        // {
        //     User user = null;
        //     string sql = "PKG_USER.LOGIN";
        //     var cmd = new OracleCommand(sql, OracleService.Connection) {CommandType = CommandType.StoredProcedure};
        //     
        //     var parameters = new[]
        //     {
        //         new OracleParameter {Direction = ParameterDirection.Input, ParameterName = "p_email", OracleDbType = OracleDbType.NVarchar2, Value = email},
        //         new OracleParameter {Direction = ParameterDirection.Input, ParameterName = "p_password", OracleDbType = OracleDbType.NVarchar2, Value = password}
        //     };
        //     var rval = cmd.Parameters.Add("user_record", OracleDbType.RefCursor);
        //     rval.Direction = ParameterDirection.ReturnValue;
        //     cmd.Parameters.AddRange(parameters);
        //     try
        //     {
        //         OracleService.Connection.Open();
        //         cmd.ExecuteNonQuery();
        //         OracleDataReader reader = ((OracleRefCursor)rval.Value).GetDataReader();
        //
        //         while (reader.Read())
        //         {
        //             user = new User
        //             {
        //                 Id = reader.GetInt32("USER_ID"), UserUid = reader.GetString("USER_UID"),
        //                 Firstname = reader.GetString("FIRSTNAME"), Surname = reader.GetString("SURNAME"),
        //                 BirthDate = reader.GetDateTime("BIRTH_DATE"), Phone = reader.GetString("PHONE"),
        //                 Email = reader.GetString("EMAIL"), UserPassword = reader.GetString("USER_PASSWORD"),
        //                 StudyYear = reader.GetInt32("STUDY_YEAR"), ProfilePicture = reader.GetString("PROFILE_PICTURE"),
        //                 RoleRoleId = reader.GetInt32("ROLE_ROLE_ID"),
        //                 AddressAddressId = reader.GetInt32("ADDRESS_ADDRESS_ID")
        //             };
        //         }
        //     }
        //     catch (Exception e)
        //     {
        //         Console.WriteLine(e.Message);
        //         OracleService.Connection.Close();
        //     }
        //
        //     OracleService.Connection.Close();
        //     return user;
        // }
        //
        //
        // public Models.Database.Address GetAddress(User user)
        // {
        //     Models.Database.Address address = null;
        //     string sql = "PKG_USER.GETUSERADDRESS";
        //     var cmd = new OracleCommand(sql, OracleService.Connection) {CommandType = CommandType.StoredProcedure};
        //     cmd.Parameters.Add("address_record", OracleDbType.RefCursor, ParameterDirection.ReturnValue);
        //     cmd.Parameters.Add("p_email",new OracleParameter {OracleDbType = OracleDbType.NVarchar2, Value = user.Email});
        //     try
        //     {
        //         OracleService.Connection.Open();
        //         var reader = cmd.ExecuteReader();
        //         while (reader.Read())
        //         {
        //             address = new Models.Database.Address()
        //             {
        //                 AddressId = reader.GetInt32("ADDRESS_ID"), Street = reader.GetString("STREET"),
        //                 HouseNr = reader.GetString("HOUSE_NR"), City = reader.GetString("CITY"),
        //                 Zip = reader.GetString("ZIP")
        //             };
        //         }
        //     }
        //     catch (Exception)
        //     {
        //         OracleService.Connection.Close();
        //     }
        //     OracleService.Connection.Close();
        //     return address;
        // }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<DbUser>>> GetUsers()
        {
            var users = await Context.Students.ToListAsync();
            if (users.Count == 0)
                return new NotFoundResult();
            return new OkObjectResult(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DbUser>> GetUserById(int id)
        {
            var user = await Context.Students.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
                return new NotFoundResult();
            return new OkObjectResult(user);
        }

        [HttpGet("year/{groupId}")]
        public Task<List<DbUser>> GetUsers(int groupId)
        {
            return Context.Students.Where(u => int.Parse(u.StudyYear) == groupId).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> InsertUser(DbUser user)
        {
            try
            {
                await Context.Students.AddAsync(user);
                await Context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception _)
            {
                return new BadRequestResult();
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(DbUser user)
        {
            try
            {
                await Task.Run(() => { Context.Update(user); });
                await Context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception e)
            {
                return new BadRequestResult();
            }
        }
    }
}