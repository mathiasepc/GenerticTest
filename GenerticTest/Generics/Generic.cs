using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Reflection;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;




namespace GenerticTest.Generics
{
    //Man kan også erklærer klassen som generisk
    public class Generic/*<T> where T : class, new()*/
    {
        #region FørsteMetode
        public List<T> ShowData<T>(List<T> list) where T : class
        {
            List<T> _output = new List<T>();
            foreach (var item in list)
            {
                _output.Add(item);
            }

            return _output;
        }
        #endregion

        #region Generisk LoadFromCSVFile
        public List<T> LoadFromCSVFile<T>(string filePath) where T : class, new()
        {
            List<T> _output = new List<T>();

            //Henter propperties fra T og indsætter i propertyInfos.
            //Gør klar til ConvertListToObject<T>().
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();

            //henter værdier fra CSV fil.
            var lines = File.ReadAllLines(filePath).ToList();

            if (lines.Count < 1)
                throw new IndexOutOfRangeException("The file was either empty or missing");

            //fjerner header rækken.
            lines.RemoveAt(0);

            for (int i = 0; i < lines.Count(); i++)
            {
                //Find match
                /*
                \"([^\"]*)\"  # Matcher tekst inde i anførselstege. Gruppe[1]
                |             # ELLER
                ([^,]*)       # Matcher tekst uden for anførselstegn og komma. Gruppe[2]
                */
                var values = Regex.Matches(lines[i], "\"([^\"]+)\"|([^,]+)")
                    //cast data vi får i return fra .Matches(). Match er return fra Regex.Matches().
                    .Cast<Match>()
                    //Vælger den gruppe som matcher regex udtrykket.
                    .Select(match => match.Groups[1].Success ? match.Groups[1].Value : match.Groups[2].Value)
                    //Gemmer i liste.
                    .ToList();

                //indsætter tom værdi. Det gør at propertyInfos og stringValues samme længde i ConvertListToObject.
                values.Insert(0, "");

                // Opret et objekt af typen T og tilføj det til output
                T obj = ConvertListToObject<T>(values, propertyInfos);
                _output.Add(obj);
            }

            return _output;
        }

        private T ConvertListToObject<T>(List<string> stringValues, PropertyInfo[] propertyInfos) where T : new()
        {
            //Opretter et T objekt
            T _obj = new T();

            //Kontrollere om stringValues ikke er 0
            if (stringValues.Count() > 0)
            {
                //gennemgår værdierne.
                for (int i = 0; i < Math.Min(stringValues.Count, propertyInfos.Length); i++)
                {
                    //opretter et objekt
                    object convertedValues;

                    if (i == 0)
                    {
                        //Sætter guid værdi i objektet
                        propertyInfos[i].SetValue(_obj, Guid.NewGuid());
                    }
                    else
                    {
                        //Conventerer til objekt
                        convertedValues = Convert.ChangeType(stringValues[i], propertyInfos[i].PropertyType);

                        //indsætter i T _obj.
                        propertyInfos[i].SetValue(_obj, convertedValues);
                    }
                }
            }
            return _obj;
        }
        #endregion

        #region Generisk ADO.NET insert
        public bool InsertIntoDatabase<T>(List<T> values) where T : class, new()
        {
            //Gør klar til at indsætte.
            T entry = new T();
            var type = entry.GetType();
            var model = type.Name;
            var properties = type.GetProperties();

            try
            {
                var tableName = model.Remove(model.Length - 5);

                var columnNames = string.Join(", ", properties.Select(p => p.Name));
                var valuePlaceholders = string.Join(", ", properties.Select(p => "@" + p.Name));

                string sqlString = $"INSERT INTO {tableName} ({columnNames}) VALUES ({valuePlaceholders})";

                ADOConnection.Open();

                using (MySqlCommand cmd = new MySqlCommand(sqlString, ADOConnection.conn))
                {
                    foreach (var item in values)
                    {
                        foreach (var property in properties)
                        {
                            cmd.Parameters.AddWithValue($"@{property.Name}", property.GetValue(item));
                        }

                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }

                return true;
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message);
            }
        }
        #endregion
    }
}
