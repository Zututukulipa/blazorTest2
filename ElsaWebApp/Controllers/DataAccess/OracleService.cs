using Oracle.ManagedDataAccess.Client;

namespace ElsaWebApp.Controllers.DataAccess
{
    public class OracleService
    {
        public OracleConnection Connection { get;}
        
        public OracleService(string connectionString)
        {
            Connection = new OracleConnection(connectionString);
        }
    }
}