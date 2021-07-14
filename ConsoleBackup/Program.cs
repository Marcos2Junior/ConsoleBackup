using System;
using System.IO;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace ConsoleBackup
{
    class Program
    {
        static void Main(string[] args)
        {
            string outputDirectory = ConfigurationManager.AppSettings["OutputDirectory"];
            string filePath = Path.Combine(outputDirectory, $"{DateTime.Now:yyyyMMdd_HHmmss}.sql");
            Directory.CreateDirectory(outputDirectory);

            using (MySqlConnection conn = new MySqlConnection(Encoding.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])))
            {
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

        static void EncriptConnectionString()
        {
            //Salvar o retorno no arquivo app.config
            string connectionStringEncript = Encoding.Encrypt("server=localhost;userid=root;password=root;database=autoescolalider");
        }
    }
}
