using System;
using System.Data;
using HumanLab;
using Oracle.ManagedDataAccess.Client;

namespace Playground
{
    public class UserAdd
    {
        public OracleConnection Connection { get;} = new OracleConnection("DATA SOURCE=localhost/XE;USER ID=elsa; password=letitgo;");
        private HumanLab.PersonLib PersonLib { get; } = new PersonLib();
        public void AddRandomUsers(int count)
        {
            var users = PersonLib.GetPeople(count).Result;

            for (int i = 0; i < count; i++)
            {
                    InsertUser(users[i], i+1);
            }
        }

        public void InsertUser(Person person, int addId)
        {
            var sql = "INSERT INTO DBUser(FIRSTNAME, SURNAME, BIRTH_DATE, PHONE, EMAIL, USER_PASSWORD, TIME_CREATED, ROLE_ROLE_ID, ADDRESS_ADDRESS_ID, SALT) VALUES (:fName, :sName, :bd, :phone, :email, :pw, :created, :role, :aID, :salt)";
            var cmd = new OracleCommand(sql, Connection) {CommandType = CommandType.Text};
            var salt = RandomUtils.RandomSalt(30);
            var pass = HashUtils.ComputeSha256Hash(HashUtils.SaltPassword("password", salt));
            var parameters2 = new []
            {
                new OracleParameter {OracleDbType = OracleDbType.NVarchar2, Value = person.FirstName},
                new OracleParameter {OracleDbType = OracleDbType.NVarchar2, Value = person.LastName},
                new OracleParameter {OracleDbType = OracleDbType.Date, Value = person.BirthDate},
                new OracleParameter {OracleDbType = OracleDbType.NVarchar2, Value = person.PhoneNumber},
                new OracleParameter {OracleDbType = OracleDbType.NVarchar2, Value = person.Email},
                new OracleParameter {OracleDbType = OracleDbType.NVarchar2, Value = pass},
                new OracleParameter {OracleDbType = OracleDbType.Date, Value = DateTime.Now},
                new OracleParameter {OracleDbType = OracleDbType.Int32, Value = "3"},
                new OracleParameter {OracleDbType = OracleDbType.Int32, Value = addId.ToString()},
                new OracleParameter {OracleDbType = OracleDbType.NVarchar2, Value = salt}
            };
            
            cmd.Parameters.AddRange(parameters2);
            Connection.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Connection.Close();
            }

            Connection.Close();
        }

        private void InsertAddress(Person person)
        {
            string sql = "INSERT INTO ADDRESS(STREET, HOUSE_NR, CITY, ZIP) VALUES (:street, :houseNr, :city, :zip)";
            OracleCommand cmd = new OracleCommand(sql, Connection) {CommandType = CommandType.Text};
            var parameters1 = new[]
            {
                new OracleParameter {OracleDbType = OracleDbType.NVarchar2, Value = person.Address.Street},
                new OracleParameter {OracleDbType = OracleDbType.NVarchar2, Value = person.Address.HouseNr},
                new OracleParameter {OracleDbType = OracleDbType.NVarchar2, Value = person.Address.City},
                new OracleParameter {OracleDbType = OracleDbType.NVarchar2, Value = person.Address.Zip}
            };
            cmd.Parameters.AddRange(parameters1);                     
            Connection.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Connection.Close();
            }
            Connection.Close();
        }
    }
}