using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;



public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void AvgStuffNum(string Witcher_name)
    {
        using (SqlConnection contextConnection = new SqlConnection("context connection = true"))
        {

            SqlCommand contextCommand =

               new SqlCommand(

               "Select AVG(NumberOfKills) from Witcher " +

               "where Name = @name", contextConnection);



            contextCommand.Parameters.AddWithValue("@name", Witcher_name);

            contextConnection.Open();



            SqlContext.Pipe.ExecuteAndSend(contextCommand);

        }



    }

}