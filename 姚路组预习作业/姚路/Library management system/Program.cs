using System;
using System.Text;
using System.Data.SqlClient;
namespace SqlServerSample
{
    class Program
    {
        private static SqlConnectionStringBuilder builder { get; set; }
        static void Main()
        {
            builder = new SqlConnectionStringBuilder();
            builder.DataSource = "127.0.0.1";  // "服务器名称"
            builder.UserID = "sa";             // "登录名 "
            builder.Password = "Y13027155802zl";  // "登录密码"
            try
            {
                Console.Write("Connecting to SQL Server ... ");
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString)) 
                {
                    connection.Open();        // "进行连接"
                    Console.WriteLine("Done.");
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("Please choose one state");
            Console.WriteLine("0--------basic");
            Console.WriteLine("1--------advanced");
            int s = int.Parse(Console.ReadLine());
            switch (s)
            {
                case 0:
                    Basic();
                    break;
                case 1:
                    Advanced();
                    break;
                default:
                    Console.WriteLine("It's not correct!");
                    break;
            }
        }
        static public void DataBase(string Name, int Operator)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Done.");

                    string name = Name;
                    StringBuilder sqltemp = new StringBuilder();
                    switch (Operator)
                    {
                        case 0:
                            sqltemp.Append("DROP DATABASE [");
                            sqltemp.Append(Name);    
                            sqltemp.Append("]");
                            break;
                        case 1:
                            sqltemp.Append("DROP DATABASE IF EXISTS [");
                            sqltemp.Append(name);
                            sqltemp.Append("]; CREATE DATABASE [");
                            sqltemp.Append(name);          
                            sqltemp.Append("]");
                            break;
                        default:
                            Console.WriteLine("It's not correct!");
                            break;
                    }
                    string sql = sqltemp.ToString();
                    Console.WriteLine(sql);
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Done.");
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        static void Table(string DataBaseName, string TableName, int Operator)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Done.");
                    StringBuilder sqltemp = new StringBuilder();
                    sqltemp.Append("USE ");
                    sqltemp.Append(DataBaseName);
                    sqltemp.Append("; ");
                    switch (Operator)
                    {
                        case 0:
                            sqltemp.Append("DROP TABLE ");
                            sqltemp.Append(TableName);
                            break;
                        case 1:
                            sqltemp.Append("CREATE TABLE ");
                            sqltemp.Append(TableName + " ( ");
                            Console.WriteLine("Please input how many fields you need");  
                            int n = int.Parse(Console.ReadLine());         //  "输入需要的属性名个数"
                            for (int i = 0; i < n; i++)
                            {
                                Console.WriteLine("Please input field and its type");
                                Console.WriteLine("Be care! It should be like this:");
                                Console.WriteLine("ID INT");
                                sqltemp.Append(Console.ReadLine());     //  "输入需要的属性名及数据格式"
                                if (i != n - 1)
                                {
                                    sqltemp.Append(", ");
                                }
                            }
                            sqltemp.Append(" );");
                            break;
                        case 2:
                            sqltemp.Append("Alter Table ");
                            sqltemp.Append(TableName);
                            Console.WriteLine("0-------ADD");
                            Console.WriteLine("1-------MODIFY");
                            Console.WriteLine("2-------DROP COLUMN");
                            int m = int.Parse(Console.ReadLine());
                            switch (m)
                            {
                                case 0:
                                    Console.WriteLine("Please input field and its type");
                                    Console.WriteLine("Be care! It should be like this:");
                                    Console.WriteLine("ID INT");
                                    sqltemp.Append(" ADD ");
                                    break;
                                case 1:
                                    Console.WriteLine("Please input field and its type");
                                    Console.WriteLine("Be care! It should be like this:");
                                    Console.WriteLine("ID INT");
                                    sqltemp.Append(" ALTER COLUMN ");
                                    break;
                                case 2:
                                    Console.WriteLine("Please input field");
                                    sqltemp.Append(" DROP COLUMN ");
                                    break;
                                default:
                                    Console.WriteLine("It's not correct!");
                                    break;
                            }
                            sqltemp.Append(Console.ReadLine());   //  "输入需要的属性名或数据格式"
                            sqltemp.Append(" ;");
                            break;
                        default:
                            Console.WriteLine("It's not correct!");
                            break;
                    }
                    string sql = sqltemp.ToString();
                    Console.WriteLine(sql);
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Done.");
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        static void Data(string DataBaseName, string TableName, int Operator)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Done.");
                    StringBuilder sqltemp = new StringBuilder();
                    sqltemp.Append("USE ");
                    sqltemp.Append(DataBaseName);
                    sqltemp.Append("; ");
                    string ConditionFiled;
                    string ConditionValue;
                    switch (Operator)
                    {
                        case 0:
                            sqltemp.Append("INSERT INTO ");
                            sqltemp.Append(TableName);
                            sqltemp.Append(" (");
                            Console.WriteLine("Please tell me how many fields");
                            StringBuilder Valuetemp = new StringBuilder();    
                            Valuetemp.Append(" VALUES (");
                            int m = int.Parse(Console.ReadLine());            //  "输入想要插入的属性个数"
                            for (int i = 0; i < m; i++)
                            {
                                Valuetemp.Append("@");
                                Console.WriteLine("Please input field's name");  
                                string temp = Console.ReadLine();              //  "输入属性名称"
                                sqltemp.Append(temp);
                                Valuetemp.Append(temp.ToLower());
                                if (i != m - 1)
                                {
                                    sqltemp.Append(",");
                                    Valuetemp.Append(",");
                                }
                            }
                            sqltemp.Append(") ");
                            Valuetemp.Append(");");
                            string Value = Valuetemp.ToString();
                            sqltemp.Append(Value);
                            string insertsql = sqltemp.ToString();
                            using (SqlCommand command = new SqlCommand(insertsql, connection))
                            {
                                bool add = true;
                                while (add)
                                {
                                    for (int i = 0; i < m; i++)
                                    {
                                        Console.WriteLine("Please input fild");    //  "输入元组的属性名"
                                        string insertstr1 = Console.ReadLine();
                                        Console.WriteLine("Please input value");
                                        string insertstr2 = Console.ReadLine();    //  "输入元组的数据"
                                        command.Parameters.AddWithValue("@" + insertstr1.ToLower(), insertstr2);
                                    }
                                    command.ExecuteNonQuery();
                                    Console.WriteLine("Done");
                                    Console.WriteLine("Press 0 to break,else to continue");
                                    string str3 = Console.ReadLine();    
                                    if (str3 == "0")
                                    {
                                        add = false;
                                    }
                                }
                            }
                            sqltemp.Clear();
                            break;
                        case 1:
                            sqltemp.Append(" DELETE FROM ");
                            sqltemp.Append(TableName);
                            Console.WriteLine("Please input Conditionfild");
                            ConditionFiled = Console.ReadLine();    //  "输入进行判断的属性名"
                            Console.WriteLine("Please input Conditionvalue");
                            ConditionValue = Console.ReadLine();   //  "输入进行判断的元组该属性名的值"
                            sqltemp.Append(" WHERE ");
                            sqltemp.Append(ConditionFiled);
                            sqltemp.Append("= @" + ConditionFiled.ToLower());
                            string deletesql = sqltemp.ToString();
                            using (SqlCommand command = new SqlCommand(deletesql, connection))
                            {
                                command.Parameters.AddWithValue("@" + ConditionFiled.ToLower(), ConditionValue);
                                command.ExecuteNonQuery();
                            }
                            sqltemp.Clear();
                            break;
                        case 2:
                            sqltemp.Append(" UPDATE ");
                            sqltemp.Append(TableName);
                            sqltemp.Append(" SET ");
                            Console.WriteLine("Please input fild");
                            string updatestr1 = Console.ReadLine();    //  "输入进行修改的属性名"
                            Console.WriteLine("Please input value");
                            string updatestr2 = Console.ReadLine();    //  "输入修改后的元组该属性名的值"
                            Console.WriteLine("Please input Conditionfild");
                            ConditionFiled = Console.ReadLine();    //  "输入进行判断的属性名"
                            Console.WriteLine("Please input Conditionvalue");
                            ConditionValue = Console.ReadLine();    //  "输入进行判断的元组该属性名的值"
                            sqltemp.Append(updatestr1);
                            sqltemp.Append("= N'");
                            sqltemp.Append(updatestr2);
                            sqltemp.Append("' WHERE ");
                            sqltemp.Append(ConditionFiled);
                            sqltemp.Append("= @" + ConditionFiled.ToLower());
                            string updatesql = sqltemp.ToString();
                            Console.WriteLine(updatesql);
                            using (SqlCommand command = new SqlCommand(updatesql, connection))
                            {
                                command.Parameters.AddWithValue("@" + ConditionFiled.ToLower(), ConditionValue);
                                command.ExecuteNonQuery();
                            }
                            sqltemp.Clear();
                            break;
                        case 3:
                            sqltemp.Append(" SELECT ");
                            Console.WriteLine("Please tell me how any fields");
                            int n = int.Parse(Console.ReadLine());      //  "输入选择出属性个数"
                            Console.WriteLine("Please input all fields");
                            Console.WriteLine("Be care! There should be a ',' between fields");
                            sqltemp.Append(Console.ReadLine());    //  "输入各个属性名称"
                            sqltemp.Append(" FROM ");
                            sqltemp.Append(TableName);
                            string selectsql = sqltemp.ToString();
                            using (SqlCommand command = new SqlCommand(selectsql, connection))
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        for (int i = 0; i < n; i++)
                                        {
                                            Console.Write("{0}  ", reader.GetValue(i));
                                        }
                                        Console.WriteLine();
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        static void Advanced()
        {
            bool Keep = true;
            while (Keep)
            {
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("0-------end");
                Console.WriteLine("1-------DataBase");
                Console.WriteLine("2-------Table");
                Console.WriteLine("3-------Data");
                string command1 = Console.ReadLine();
                switch (int.Parse(command1))
                {
                    case 0:
                        Keep = false;
                        break;
                    case 1:
                        Console.WriteLine("0----------DROP");
                        Console.WriteLine("1----------CREATE");
                        string command2 = Console.ReadLine();
                        Console.WriteLine("Please input Name");
                        string DataBaseName1 = Console.ReadLine();   //  "输入数据库的名称"
                        DataBase(DataBaseName1, int.Parse(command2));
                        break;
                    case 2:
                        Console.WriteLine("Please input DaTaBaseName");
                        string DataBaseName2 = Console.ReadLine();   //  "输入数据库的名称"
                        Console.WriteLine("0----------DROP");
                        Console.WriteLine("1----------CREATE");
                        Console.WriteLine("2----------ALTER");
                        string command3 = Console.ReadLine();        //  "输入数据表的名称"
                        Console.WriteLine("Please input TableName");
                        string TableName1 = Console.ReadLine();
                        Table(DataBaseName2, TableName1, int.Parse(command3));
                        break;
                    case 3:
                        Console.WriteLine("Please input DaTaBaseName");
                        string DataBaseName3 = Console.ReadLine();   //  "输入数据库的名称"
                        Console.WriteLine("Please input TableName");
                        string TableName2 = Console.ReadLine();      //  "输入数据表的名称"
                        Console.WriteLine("0----------INSERT");
                        Console.WriteLine("1----------DELETE");
                        Console.WriteLine("2----------UPDATE");
                        Console.WriteLine("3----------SELECT");
                        string command4 = Console.ReadLine();
                        Data(DataBaseName3, TableName2, int.Parse(command4));
                        break;
                    default:
                        Console.WriteLine("It's not correct!");
                        break;
                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            Console.ReadKey(true);
        }
        static void Basic()
        {
            bool Keep = true;
            while (Keep)
            {
                Console.WriteLine("0----------INSERT");
                Console.WriteLine("1----------DELETE");
                Console.WriteLine("2----------UPDATE");
                Console.WriteLine("3----------SELECT");
                string command = Console.ReadLine();
                Data("Library", "Book", int.Parse(command));   //"输入想要进行的操作符"
                Console.WriteLine("Press '0' to break,else to continue!");
                if (int.Parse(Console.ReadLine()) == 0)
                {
                    Keep = false;
                }
            }
        }
    }
}