using LinkoChat.Data.Properties;
using LinkoChat.Domain.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;
using System.Text.Json;

#nullable disable

namespace LinkoChat.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var text = Encoding.UTF8.GetString(Properties.Resources.locations);
            var root = JsonSerializer.Deserialize<Root>(RemoveBOM(text));

            foreach (var stateData in root.States)
            {
                migrationBuilder.InsertData(
                    table: "Statements",
                    columns: new[] { "Id", "Name" },
                    values: new object[] { stateData.Id, stateData.Name }
                );

                var cityDataList = new List<object[]>();
                foreach (var city in stateData.Cities)
                {
                    cityDataList.Add([city.Id, city.Name, city.StateId]);
                }

                var cityDataArray = ConvertToMultidimensionalArray(cityDataList);

                migrationBuilder.InsertData(
                    table: "Cities",
                    columns: new[] { "Id", "Name", "StateId" },
                    values: cityDataArray
                );
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }

        public class Root
        {
            public List<State> States { get; set; }
        }

        private string RemoveBOM(string text)
        {
            // Check for BOM (Byte Order Mark)
            if (text.Length > 0 && text[0] == 0xFEFF)
            {
                return text.Substring(1);
            }
            return text;
        }

        private object[,] ConvertToMultidimensionalArray(List<object[]> list)
        {
            int rows = list.Count;
            int columns = list[0].Length;
            var array = new object[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    array[i, j] = list[i][j];
                }
            }
            return array;
        }
    }
}
