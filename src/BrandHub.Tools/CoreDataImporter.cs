using CsvHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandHub.Tools
{
    public class CountryCsvData
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
    public class DistrictCsvData
    {
        public string No { get; set; }
        public string Province { get; set; }
        public string Capital { get; set; }
        public string Area { get; set; }
        public string Population { get; set; }
        public string Density { get; set; }
        public string Urban { get; set; }
        public string Region { get; set; }
    }
    public class CoreDataImporter
    {
        private string ConnString = ConfigurationManager.AppSettings["ConnString"];
        public void ImportCountries()
        {
            var countryCsvPath = ConfigurationManager.AppSettings["countryCsvPath"];
            using (var reader = new StreamReader(countryCsvPath))
            using (var csv = new CsvReader(reader))
            {
                var records = csv.GetRecords<CountryCsvData>();
                var query = new StringBuilder();
                query.AppendLine("BEGIN TRANSACTION;");
                foreach (var item in records)
                {
                    query.AppendLine($"INSERT INTO Countries(Name, CountryCode) VALUES ('{item.Name.Replace("'", " ")}','{item.Code}');");
                }
                query.AppendLine("COMMIT;");
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    var queryString = query.ToString();
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void ImportProvince(string countryCode)
        {
            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                var queryString = $"SELECT ID FROM Countries WHERE CountryCode='{countryCode}'";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                int countryId = 0;
                while (reader.Read())
                {
                    countryId = reader.GetInt32(0);
                }
                var csvPath = ConfigurationManager.AppSettings["provinceCsvPath"];
                using (var strReader = new StreamReader(csvPath))
                using (var csv = new CsvReader(strReader))
                {
                    var records = csv.GetRecords<DistrictCsvData>();
                    var query = new StringBuilder();
                    query.AppendLine("BEGIN TRANSACTION;");
                    foreach (var item in records)
                    {
                        query.AppendLine($"INSERT INTO Provinces(Name, CountryId) VALUES ('{item.Province.Replace("'", " ").Replace(" Province", "").Replace(" (city)", "")}','{countryId}');");
                    }
                    query.AppendLine("COMMIT;");
                    queryString = query.ToString();
                    command = new SqlCommand(queryString, connection);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
