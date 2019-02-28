using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http;
using System.Reflection;
using D1.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using WebApi;


namespace D1.IntegrationTests.WebApi
{
    public class ApiSut
    {
        public HttpClient Client { get; set; }
        public SqlConnectionStringBuilder Master => new SqlConnectionStringBuilder { DataSource = @"(LocalDB)\MSSQLLocalDB", InitialCatalog = "master", IntegratedSecurity = true };
        public SqlConnectionStringBuilder ArchySoftD1 => new SqlConnectionStringBuilder { DataSource = @"(LocalDB)\MSSQLLocalDB", InitialCatalog = "D1.IntegrationTest", IntegratedSecurity = true };
        public string DbFilePath => Path.Combine(Path.GetDirectoryName(GetType().GetTypeInfo().Assembly.Location), "D1.IntegrationTest.mdf");
        public DataContext Context => new DataContext(GetConfiguration());

        public ApiSut()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "IntegrationTest");
            Environment.SetEnvironmentVariable("D1_DATACONTEXT", "Server=(localdb)\\mssqllocaldb;Database=D1.IntegrationTest;Trusted_Connection=True;MultipleActiveResultSets=true");

            DestroyDatabase();
            CreateDatabase();


            var server = new TestServer(WebHost.CreateDefaultBuilder().UseEnvironment("IntegrationTest").UseUrls("https://localhost:5555").UseStartup<Startup>())
            {
                BaseAddress = new Uri("https://localhost:5555")
            };
            Client = server.CreateClient();
        }

        public void DestroyDatabase()
        {
            var fileNames = ExecuteSqlQuery(Master, @"
                SELECT [physical_name] FROM [sys].[master_files]
                WHERE [database_id] = DB_ID('D1.IntegrationTest')",
                row => (string)row["physical_name"]);

            if (fileNames.Any())
            {
                ExecuteSqlCommand(Master, @"
                    ALTER DATABASE [D1.IntegrationTest] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    EXEC sp_detach_db 'D1.IntegrationTest'");

                fileNames.ForEach(File.Delete);
            }
        }


        public void CreateDatabase()
        {
            ExecuteSqlCommand(Master, $@"CREATE DATABASE [D1.IntegrationTest] ON (NAME = 'D1.IntegrationTest', FILENAME = '{DbFilePath}')");

            Context.Database.Migrate();
        }

        public IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.IntegrationTest.json")))
            {
                builder.AddJsonFile("appsettings.IntegrationTest.json");
            }

            return builder.Build();
        }

        public List<T> ExecuteSqlQuery<T>(SqlConnectionStringBuilder connectionStringBuilder, string queryText, Func<SqlDataReader, T> read)
        {
            var result = new List<T>();
            using (var connection = new SqlConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = queryText;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(read(reader));
                        }
                    }
                }
            }

            return result;
        }

        public void ExecuteSqlCommand(SqlConnectionStringBuilder connectionStringBuilder, string commandText)
        {
            using (var connection = new SqlConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = commandText;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
