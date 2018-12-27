
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DatabaseCLR;
using System.Linq;
using System;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void ParseEquipment()
    {
        // Получаем оборудование на участках по группам
        var eq = GetEqOnGroup();

        //генерим уник оборудование
        var uniq = GenerateUniqKOB(eq);

        //Получаем необх оборудование по группам взаимозаменяемости
        var needEq = GetNeedEq();

        //Получаем финалочку
        var final = GetFinalList(needEq, uniq);

        InsertInTemp(final);
       
    }

    private static void InsertInTemp(List<FinalEquip> final)
    {
        using (SqlConnection connection = new SqlConnection(conntectStr))
        {
            connection.Open();
           
            SqlCommand command = new SqlCommand("TRUNCATE TABLE [belwestDB].[dbo].[temp_equip]", connection);
            command.ExecuteNonQuery();

            string commandStr =
                string.Format(
                    "INSERT INTO [belwestDB].[dbo].[temp_equip]([ID] ,[KOB],[Group_ID],[Title]) VALUES (@ID, @KOB, @Group_ID, @Title)");
            command = new SqlCommand(commandStr, connection);

            command.Parameters.Add(new SqlParameter("ID", SqlDbType.Int));
            command.Parameters.Add(new SqlParameter("KOB", SqlDbType.Int));
            command.Parameters.Add(new SqlParameter("Group_ID", SqlDbType.Int));
            command.Parameters.Add(new SqlParameter("Title", SqlDbType.VarChar));


            foreach (var f in final)
            {
                command.Parameters["ID"].Value = f.ID;
                command.Parameters["KOB"].Value = f.KOB;
                command.Parameters["Group_ID"].Value = f.Group_ID;
                command.Parameters["Title"].Value = f.Title;

                command.ExecuteNonQuery();
            }

        }
    }

    static readonly string conntectStr = "context connection=true";

    private static List<FinalEquip> GetFinalList(List<NeedEquip> needEq, List<UniqueKOB> uniq)
    {
        List<FinalEquip> finalList = new List<FinalEquip>();
        foreach (var neq in needEq)
        {
            var equipInSite = uniq.Where(x => x.Site_ID == neq.Site_ID).ToList();
            foreach (var item in equipInSite)
            {
                var f = new FinalEquip()
                {
                    KOB = item.KOB,
                    Group_ID = neq.ID_Group,
                    ID = item.ID_uniqKOB,
                    Title = item.Title.Trim() + " KOB:" + item.KOB + " ID:" + item.ID_uniqKOB
                };
                finalList.Add(f);
            }
        }

        return finalList;
    }

    private static List<NeedEquip> GetNeedEq()
    {
        List<NeedEquip> _needEquip = new List<NeedEquip>();
        using (SqlConnection connection = new SqlConnection(conntectStr))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM [belwestDB].[dbo].vw_belwestNeedEquipment", connection);
            SqlDataReader reader = command.ExecuteReader();

            using (reader)
            {
                while (reader.Read())
                {
                    var item = new NeedEquip()
                    {
                        Site_ID = reader.GetInt32(3),
                        KOB = reader.GetInt32(2),
                        ID_Group = reader.GetInt32(0),
                        TitleGroup = reader.GetString(1)
                    };
                    _needEquip.Add(item);
                }
            }
            return _needEquip;
        }
    }

    private static List<UniqueKOB> GenerateUniqKOB(List<EquipmentOnGroup> equpitmentList)
    {
        List<UniqueKOB> kob = new List<UniqueKOB>();

        int z = 0;
        for (int j = 0; j < equpitmentList.Count; j++)
        {
            for (int i = 0; i < equpitmentList[j].KOBCount; i++)
            {
                z++;
                UniqueKOB k = new UniqueKOB()
                {
                    ID_uniqKOB = z,
                    Site_ID = equpitmentList[j].Site_ID,
                    Title = equpitmentList[j].KOBTitle,
                    KOB = equpitmentList[j].KOB
                };
                kob.Add(k);
            }
        }

        return kob;
    }

    private static List<EquipmentOnGroup> GetEqOnGroup()
    {
        List<EquipmentOnGroup> _equipmentOnGroup = new List<EquipmentOnGroup>();
        using (SqlConnection connection = new SqlConnection(conntectStr))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM [belwestDB].[dbo].[vw_belwestEquipmentOnGroup]", connection);
            SqlDataReader reader = command.ExecuteReader();

            using (reader)
            {
                while (reader.Read())
                {
                    var item = new EquipmentOnGroup()
                    {
                        Site_ID = reader.GetInt32(0),

                        KOB = reader.GetInt32(2),
                        KOBTitle = reader.GetString(1),
                        KOBCount = reader.GetInt32(3)
                    };
                    _equipmentOnGroup.Add(item);
                }
            }
            return _equipmentOnGroup;
        }
    }
}

