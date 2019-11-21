using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class SqlFunc

{

    [Microsoft.SqlServer.Server.SqlFunction]

    public static SqlInt32 GetFact(SqlInt32 s)

    {
        int b = 1;
        int k = s.Value;
        while (k > 0)
        {
            b *= k;
            k--;
        }
        return b;

    }

}