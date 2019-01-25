using DatabaseCLR;
using System;
using System.Collections;
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;


public partial class UserDefinedFunctions
{
    [Microsoft.SqlServer.Server.SqlFunction(
        FillRowMethodName = "FillRow", SystemDataAccess = SystemDataAccessKind.Read,
        DataAccess = DataAccessKind.Read,
        TableDefinition =
        "NOMER int, " +
        "WP int," +
        "KOB int")]
    public static IEnumerable fn_get_equipment_oracle_db()
    {
        string host = "prod.vit.belwest.com";
        int port = 1521;
        string sid = "orcl.Belw-MPUDB";
        string user = "SergeyTrofimov";
        string password = "jC7EGzQ1pX";

        using (OracleConnection con = new OracleConnection(OracleSettings.GetConnectionString()))
        {
            con.Open();
            
            string commandStr = @"SELECT
            nomer,
            wp,
            kob,
            time_s,
            text,
            date_s,
            pc_s
                FROM
            belwpr.ri_conve3";

            OracleCommand comm = new OracleCommand(commandStr, con);
            var reader = comm.ExecuteReader();
            ArrayList list = new ArrayList();
            while (reader.Read())
            {
                var r = new RI_Conve3()
                {
                    NOMER = Convert.ToInt32(reader[0]),
                    WP = Convert.ToInt32(reader[1]),
                    KOB = Convert.ToInt32(reader[2]),
                    TEXT = reader[4].ToString()
                };
                list.Add(r);
            }
            return list;
        }
    }

    /// <summary>
    /// Метод описывающий как заполнять строку SQL данными из IEnumara
    /// </summary>
    /// <param name="RI_Conve3">Объект, который необходимо перевести в строку SQL</param>
    /// <param name="NOMER">Выходной параметр</param>
    /// <param name="WP">Выходной параметр</param>
    /// <param name="KOB">Выходной параметр</param>
    public static void FillRow(object RI_Conve3, out SqlInt32 NOMER,
        out SqlInt32 WP, out SqlInt32 KOB)
    {
        var ri = RI_Conve3 as RI_Conve3;
        NOMER = ri.NOMER;
        WP = ri.WP;
        KOB = ri.KOB;
    }
}
