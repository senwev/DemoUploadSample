using System;
using System.Text;
using System.Data.SqlClient;

namespace ConsoleApp1
{
    class Program
    {
        //选定图书管理数据库
        static void ConnectDataBase(SqlConnection conn)
        {
            String sql = "USE BookManagement;";
            using (SqlCommand command = new SqlCommand(sql, conn))
            {
                command.ExecuteNonQuery();
                Console.WriteLine("图书管理系统加载完成！");
                Console.WriteLine("请输入大写字母选择下一步操作：");
                Console.WriteLine("0.查书名 1.查类型 2.增加书目 3.删除书目 4.修改书目");
            }
        }

        //显示选定的图书信息
        static void ReadData(SqlCommand command)
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine("{0} {1} {2} {3}", reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                }
            }
        }

        //通过书名查询图书信息
        static void SearchName(SqlConnection conn)
        {
            Console.WriteLine("请输入查询的书名：");
            String read = Console.ReadLine();  //输入图书名
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM BookList WHERE Name ='");
            sb.Append(read);
            sb.Append("';");
            String sql = sb.ToString();
            using (SqlCommand command = new SqlCommand(sql, conn))
            {
                command.ExecuteNonQuery();
                ReadData(command);
            }
        }

        //通过图书类型查询图书信息
        static void SearchType(SqlConnection conn)
        {
            Console.WriteLine("请输入查询的类型：");
            String read = Console.ReadLine();  //输入图书类型
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM BookList WHERE Type ='");
            sb.Append(read);
            sb.Append("';");
            String sql = sb.ToString();
            using (SqlCommand command = new SqlCommand(sql, conn))
            {
                command.ExecuteNonQuery();
                ReadData(command);
            }
        }

        //增添书目
        static void AddBook(SqlConnection conn)
        {
            //录入书目信息
            Console.WriteLine("输入图书ID：");
            string addID = Console.ReadLine();
            Console.WriteLine("输入图书名：");
            string addName = Console.ReadLine();
            Console.WriteLine("输入图书类型：");
            string addType = Console.ReadLine();
            string addSituation = "在架上";
            //将录入的内容插入数据表
            StringBuilder sb = new StringBuilder();
            sb.Append("insert BookList (ID, Name, Type, Situation)");
            sb.Append("values (@ID, @Name, @Type, @Situation);");
            String sql = sb.ToString();
            using (SqlCommand command = new SqlCommand(sql, conn))
            {
                command.Parameters.AddWithValue("@ID", addID);
                command.Parameters.AddWithValue("@Name", addName);
                command.Parameters.AddWithValue("@Type", addType);
                command.Parameters.AddWithValue("@Situation", addSituation);
                command.ExecuteNonQuery();
                Console.WriteLine("图书录入成功！");
            }
        }

        //删除书目
        static void DeleteBook(SqlConnection conn)
        {
            //查询图书ID，显示相关书目信息
            Console.WriteLine("输入要删除的书目ID：");
            String read = Console.ReadLine();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM BookList WHERE ID ='");
            sb.Append(read);
            sb.Append("';");
            String sql = sb.ToString();
            using (SqlCommand command = new SqlCommand(sql, conn))
            {
                command.ExecuteNonQuery();
                ReadData(command);
            }
            //将相关书目信息从数据表中移除（含确认操作）
            Console.WriteLine("确认删除？（0.确认 1.取消）");
            int readchar = int.Parse(Console.ReadLine());
            switch (readchar)
            {
                case 0:
                    sb.Clear();
                    sb.Append("delete from BookList where ID='");
                    sb.Append(read);
                    sb.Append("';");
                    sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("图书删除成功！");
                    }
                    break;
                case 1:
                    Console.WriteLine("操作取消！");
                    break;
            }
        }

        //修改书目信息
        static void UpdateBook(SqlConnection conn)
        {
            //查询图书ID，显示相关书目信息
            Console.WriteLine("输入要修改的书目ID：");
            String read = Console.ReadLine();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM BookList WHERE ID ='");
            sb.Append(read);
            sb.Append("';");
            String sql = sb.ToString();
            using (SqlCommand command = new SqlCommand(sql, conn))
            {
                command.ExecuteNonQuery();
                ReadData(command);
            }
            //录入修改后的书目信息
            Console.WriteLine("输入修改后的图书名：");
            string addName = Console.ReadLine();
            Console.WriteLine("输入修改后的图书类型：");
            string addType = Console.ReadLine();
            Console.WriteLine("确认修改？（0.确认 1.取消）");
            int readchar = int.Parse(Console.ReadLine());
            //确认修改操作
            switch (readchar)
            {
                case 0:
                    sb.Clear();
                    sb.Append("UPDATE BookList SET Name = N'"); 
                    sb.Append(addName);
                    sb.Append("', Type = N'");
                    sb.Append(addType);
                    sb.Append("' WHERE ID = '");
                    sb.Append(read);
                    sb.Append("';");
                    sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("图书修改成功！");
                    }
                    break;
                case 1:
                    Console.WriteLine("操作取消！");
                    break;
            }

        }

        static void Main(string[] args)
        {
            //连接sqlserver
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "127.0.0.1";  //请修改为自己的服务器名
            builder.UserID = "sa";             //请修改为自己的账号
            builder.Password = "12345678";     //请修改为自己的密码
            builder.InitialCatalog = "master";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();             
                ConnectDataBase(connection);   //连接图书管理数据库           
                //通过输入数字选择需要运行的功能
                int readchar = int.Parse(Console.ReadLine());
                switch (readchar)
                {
                    case 0:
                        SearchName(connection);
                        break;
                    case 1:
                        SearchType(connection);
                        break;
                    case 2:
                        AddBook(connection);
                        break;
                    case 3:
                        DeleteBook(connection);
                        break;
                    case 4:
                        UpdateBook(connection);
                        break;
                }
                Console.ReadKey(true);
            }
        }
    }
}
