using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.IO;

namespace ConsoleBackup
{
    class Program
    {
        static void Main(string[] args)
        {
            string outputDirectory = ConfigurationManager.AppSettings["OutputDirectory"];
            string filePath = Path.Combine(outputDirectory, $"{DateTime.Now:yyyyMMdd_HHmmss}.sql");
            Directory.CreateDirectory(outputDirectory);

            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                conn.Close();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ExportToFile(filePath);
                        conn.Close();
                    }
                }
            }
        }
    }
}
