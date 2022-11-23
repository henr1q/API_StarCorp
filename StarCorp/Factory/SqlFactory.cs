using System.Data;
using System.Data.SqlClient;

namespace StarCorp.Factory;

public class SqlFactory
{
    public IDbConnection SqlConnection()
    {   
        return new SqlConnection("Server=localhost; Database=StarCorp; User=sa; Password=1472; Trusted_Connection=False; ");
    }
}