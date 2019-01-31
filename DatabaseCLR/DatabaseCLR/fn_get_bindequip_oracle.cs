using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DatabaseCLR;
using Microsoft.SqlServer.Server;
using Oracle.ManagedDataAccess.Client;

public partial class UserDefinedFunctions
{
    [Microsoft.SqlServer.Server.SqlFunction(FillRowMethodName = "FillRow",
        SystemDataAccess = SystemDataAccessKind.Read,
        TableDefinition =
            @"KPLOT NVARCHAR(max) ,	--Код участка
            WP  INT , --Номер места
            KOB INT , --Код оборудования
            TIME_S  DATETIME , --Время запуска
            [TEXT]  NVARCHAR(max), --Примечание
            DATE_S  DATETIME , --Время ввода записи
            PC_S    NVARCHAR(max)")]

    public static IEnumerable fn_get_bindequip_oracle()
    {
        ArrayList list = new ArrayList();
        string conntectStr = "context connection=true";
        using (SqlConnection con = new SqlConnection(conntectStr))
        {
            con.Open();
            string commandString =
              @"EXEC [dbo].[sp_clr_select_oracle] @selectCommandText = 
	            'SELECT
		            kplot,
		            wp,
		            kob,
		            time_s,
		            text,
		            date_s,
		            pc_s
	            FROM belwpr.ri_bind_ob'";
            SqlCommand command = new SqlCommand(commandString, con);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var obj = new BindEquipment()
                {
                    kplot = reader[0].ToString(),
                    wp = Convert.ToInt32(reader[1]),
                    kob = Convert.ToInt32(reader[2]),
                    time_s = Convert.ToDateTime(reader[3]),
                    text = reader[4].ToString(),
                    date_s = Convert.ToDateTime(reader[5]),
                    pc_s = reader[6].ToString()
                };
                list.Add(obj);
            }
        }
        return list;
    }

    public static void FillRow(object resultObject, out SqlString kplot, out SqlInt32 wp, out SqlInt32 kob,
        out SqlDateTime time_s, out SqlString text, out SqlDateTime date_s, out SqlString pc_s)
    {
        var obj = resultObject as BindEquipment;
        kplot = obj.kplot;
        wp = obj.wp;
        kob = obj.kob;
        time_s = obj.time_s;
        text = obj.text;
        date_s = obj.date_s;
        pc_s = obj.pc_s;
    }
}
