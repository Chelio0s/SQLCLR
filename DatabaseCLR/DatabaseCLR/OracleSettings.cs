using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseCLR
{
    public static class OracleSettings
    {
        public static string Host { get; private set; }
        public static int Port { get; private set; }
        public static string Sid { get; private set; }
        public static string User { get; private set; }
        public static string Password { get; private set; }

        static OracleSettings()
        {
            Host = "prod.vit.belwest.com";
            Port = 1521;
            Sid = "orcl.Belw-MPUDB";
            User = "SergeyTrofimov";
            Password = "jC7EGzQ1pX";
        }

        public static string GetConnectionString()
        {
            string connString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = "
                                + Host + ")(PORT = " + Port + "))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = "
                                + Sid + ")));Password=" + Password + ";User ID=" + User;
            return connString;
        }
    }
}
