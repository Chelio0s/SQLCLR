using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DatabaseCLR;
using Microsoft.SqlServer.Server;
using Oracle.ManagedDataAccess.Client;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void sp_clr_select_oracle (SqlString selectCommandText)
    {
        using (OracleConnection con = new OracleConnection(OracleSettings.GetConnectionString()))
        {
            con.Open();
           
            OracleCommand command = new OracleCommand(selectCommandText.Value, con);
            var reader = command.ExecuteReader();
            SqlMetaData[] metaDatas = new SqlMetaData[reader.FieldCount];
            for (int i = 0; i < reader.FieldCount; i++)
            {
               metaDatas[i] = new SqlMetaData(reader.GetName(i), SqlDbType.NVarChar, 150 );
            }

            bool first = true;
            while (reader.Read()) 
            {
                SqlDataRecord record = new SqlDataRecord(metaDatas);
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    record.SetSqlString(i, reader[i].ToString());
                }

                if (first)
                {
                    SqlContext.Pipe.SendResultsStart(record);
                    first = false;
                }
                else
                {
                    SqlContext.Pipe.SendResultsRow(record);
                }
                
            }
            SqlContext.Pipe.SendResultsEnd();
            
        }
    }
}
