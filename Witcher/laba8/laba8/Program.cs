using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Laba8
{
    class Program
    {
        // Fields
        private static string connectionString = @"Data Source= MSI;Initial Catalog=WitcherDataBase; Integrated Security=True";

        // Main
        static void Main(string[] args)
        {
            Program program = new Program();

            try
            {
                bool flag = true;
                string input;

                while (flag)
                {
                    Console.WriteLine("0. Выход");
                    Console.WriteLine("Присоединенные:");
                    Console.WriteLine("1. Информация о подключении");
                    Console.WriteLine("2. Обработка данных");
                    Console.WriteLine("3. SqlCommand, Parameters, Select");
                    Console.WriteLine("4. SqlCommand, Parameters, Insert");
                    Console.WriteLine("5. Хранимая процедура без параметров");
                    Console.WriteLine("6. Хранимая процедура с параметрами");
                    Console.WriteLine("Отсоединенные:");
                    Console.WriteLine("7. DataTableCollection");
                    Console.WriteLine("8. DataTableCollection с фильтрами");
                    Console.WriteLine("9. Удаление");
                    Console.WriteLine("10. Вставка");
                    Console.WriteLine("11. XML");
                    Console.Write("Выберите свою судьбу: ");
                    input = Console.ReadLine();
                    switch (input)
                    {
                        case "0":
                            flag = false;
                            break;
                        case "1":
                            program.createConnection();
                            break;
                        case "2":
                            program.FirstQuery();
                            break;
                        case "3":
                            program.SecondQuery();
                            break;
                        case "4":
                            program.ThirdQuery();
                            break;
                        case "5":
                            program.FourthQuery();
                            break;
                        case "6":
                            program.FifthQuery();
                            break;
                        case "7":
                            program.SixthQuery();
                            break;
                        case "8":
                            program.SeventhQuery();
                            break;
                        case "9":
                            program.EighthQuery();
                            break;
                        case "10":
                            program.NinthQuery();
                            break;
                        case "11":
                            program.TenQuery();
                            break;
                        default:
                            Console.WriteLine("Try again..");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error. Message: " + ex.Message);
            }
        }

        // Methods for lab
        // Connected
        // 1.
        // Create connection, shows info, close connection
        void createConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Подключение открыто.");
                Console.WriteLine("Информация о подключении:");
                Console.WriteLine("\tСтрока подключения:   {0}", connection.ConnectionString);
                Console.WriteLine("\tБаза данных:          {0}", connection.Database);
                Console.WriteLine("\tСервер:               {0}", connection.DataSource);
                Console.WriteLine("\tВесия сервера:        {0}", connection.ServerVersion);
                Console.WriteLine("\tСостояние подключения:{0}", connection.State);
                Console.WriteLine("\tId рабочей платформы: {0}", connection.WorkstationId);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Connection error! Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("Connection has been closed.");
            }
            Console.ReadKey();
        }

        // 2.
        public void FirstQuery()
        {
            const string queryString = @"SELECT COUNT(*) FROM Witcher";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(queryString, connection);
            try
            {
                connection.Open();
                Console.WriteLine("Количество ведьмаков " + command.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error! Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.ReadKey();
        }

        // 3.
        public void SecondQuery()
        {
            const string queryString = @"SELECT [Order].Id, Witcher.Name, Monster.Name, CountOfMoney
                                        FROM [Order] JOIN Witcher on [Order].WitcherId = Witcher.Id JOIN Monster ON [Order].MonsterId = Monster.Id
                                         where CountOfMoney BETWEEN (2000) AND (4000)";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(queryString, connection);
            try
            {
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Console.WriteLine("Номер заказа {0};  Имя ведьмака: {1};  Название монстра: {2};  Количество денег: {3}", dataReader.GetValue(0),
                                                                                                         dataReader.GetValue(1),
                                                                                                         dataReader.GetValue(2),
                                                                                                         dataReader.GetValue(3));
                }

                dataReader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error! Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.ReadKey();
        }

        // 4.
        public void ThirdQuery()
        {
            const string insertQuery = @"INSERT INTO Witcher(Name, SkillLevel, NumberOfKills)
                                                     VALUES(@Name, @SkillLevel, @NumberOfKills)";

            SqlConnection connection = new SqlConnection(connectionString);
            
            SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
            insertCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 255);
            insertCommand.Parameters.Add("@SkillLevel", SqlDbType.NVarChar, 255);
            insertCommand.Parameters.Add("@NumberOfKills", SqlDbType.Int);
            try
            {
                connection.Open();

                Console.Write("Ввидите имя: ");
                var Name = Console.ReadLine();

                Console.Write("Ввидите уровень мастерства: ");
                var SkillLevel = Console.ReadLine();

                Console.Write("Введите количество убийств: ");
                int NumberOfKills = Convert.ToInt32(Console.ReadLine()); ;
                
                insertCommand.Parameters["@Name"].Value = Name;
                insertCommand.Parameters["@SkillLevel"].Value = SkillLevel;
                insertCommand.Parameters["@NumberOfKills"].Value = NumberOfKills;

                insertCommand.ExecuteNonQuery();
                Console.WriteLine("Вставка выполнена успешно.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error! Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.ReadKey();
        }

        // 5.
        public void FourthQuery()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetTablesList";

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                    Console.WriteLine(dataReader[0].ToString());

                dataReader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error! Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.ReadKey();
        }

        // 6.
        public void FifthQuery()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetKillByID";

                SqlParameter inParameter = command.Parameters.Add("@id", SqlDbType.Int);
                inParameter.Direction = ParameterDirection.Input;
                Console.Write("Введите номер ведьмака:");
                int id = Convert.ToInt32(Console.ReadLine());
                inParameter.Value = id;

                SqlParameter outParameter = command.Parameters.Add("@Kill", SqlDbType.Int);
                outParameter.Direction = ParameterDirection.Output;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Console.WriteLine(dataReader[0].ToString());
                }

                dataReader.Close();

                Console.WriteLine("Ведьмак под номером " + id + " убил " + command.Parameters["@Kill"].Value);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error! Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.ReadKey();
        }

        // Disconnected
        // 7.
        public void SixthQuery()
        {
            const string queryString = @"SELECT * FROM [Order]
                                        WHERE CountOfMoney > 1000";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(queryString, connection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "[Order]");

                DataTable dataTable = dataSet.Tables["[Order]"];

                foreach (DataRow row in dataTable.Rows)
                {
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        Console.Write(row[column]+ " ");
                    }
                    Console.WriteLine();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error! Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.ReadKey();
        }

        // 8.
        public void SeventhQuery()
        {
            const string queryString = @"SELECT * FROM Witcher";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter();

                dataAdapter.SelectCommand = new SqlCommand(queryString, connection);

                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Witcher");
                DataTableCollection dataTableCollection = dataSet.Tables;

                string filter = "NumberOfKills > 60";
                string sort = "NumberOfKills asc";

                foreach (DataRow row in dataTableCollection["Witcher"].Select(filter, sort))
                {
                    Console.WriteLine(row["Name"] + "   " + (row["NumberOfKills"].ToString()));
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error! Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.ReadKey();
        }

        // 9.
        public void EighthQuery()
        {
            const string dataQuery = @"SELECT * FROM [Order]";
            const string deleteQuery = @"DELETE FROM [Order] WHERE Id = @id";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                Console.Write("Ввидите Id заказа для удаление: ");
                int id = Convert.ToInt32(Console.ReadLine());

                SqlDataAdapter dataAdapter = new SqlDataAdapter(new SqlCommand(dataQuery, connection));
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "[Order]");
                DataTable table = dataSet.Tables["[Order]"];

                string filter = "Id = " + id;

                foreach (DataRow row in table.Select(filter))
                {
                    row.Delete();
                }

                SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.Add("@id", SqlDbType.Int, 4, "Id");
                dataAdapter.DeleteCommand = deleteCommand;
                dataAdapter.Update(dataSet, "[Order]");

                Console.WriteLine("Удаление произведено.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error! Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.ReadKey();
        }

        // 10.
        public void NinthQuery()
        {
            const string dataQuery = @"SELECT * FROM Monster";
            const string insertQuery = @"INSERT INTO Monster(Name, ThreatLevel, Class, SusceptibilityId) VALUES (@Name, @ThreatLevel, @Class, @SusceptibilityId)";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

                Console.Write("Введите название монстра: ");
                var Name = Console.ReadLine();

                Console.Write("Введите уровень угрозы: ");
                var ThreatLevel = Console.ReadLine();

                Console.Write("Введите класс: ");
                var Class = Console.ReadLine();

                Console.Write("Введите id уязвимости: ");
                int Sus = Convert.ToInt32(Console.ReadLine());

                SqlDataAdapter dataAdapter = new SqlDataAdapter(new SqlCommand(dataQuery, connection));
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Monster");
                DataTable table = dataSet.Tables["Monster"];

                DataRow insertingRow = table.NewRow();
                insertingRow["Name"] = Name;
                insertingRow["ThreatLevel"] = ThreatLevel;
                insertingRow["Class"] = Class;
                insertingRow["SusceptibilityId"] = Sus;

                table.Rows.Add(insertingRow);

                SqlCommand insertQueryCommand = new SqlCommand(insertQuery, connection);
                insertQueryCommand.Parameters.Add("@Name", SqlDbType.VarChar, 255, "Name");
                insertQueryCommand.Parameters.Add("@ThreatLevel", SqlDbType.VarChar, 255, "ThreatLevel");
                insertQueryCommand.Parameters.Add("@Class", SqlDbType.VarChar, 255, "Class");
                insertQueryCommand.Parameters.Add("@SusceptibilityId", SqlDbType.Int, 4, "SusceptibilityId");

                dataAdapter.InsertCommand = insertQueryCommand;
                dataAdapter.Update(dataSet, "Monster");

                Console.WriteLine("Вставка успешна.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error! Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.ReadKey();
        }

        // 11.
        public void TenQuery()
        {
            const string query = "SELECT * FROM Monster";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Monster");
                DataTable table = dataSet.Tables["Monster"];

                dataSet.WriteXml("Monster.xml");
                Console.WriteLine("XML успешно создан.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error! Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.ReadKey();
        }
    }
}
