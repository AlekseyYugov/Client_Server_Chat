using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Client_Chat
{
    class SQLConnect
    {
        public bool InsertUser(string _login, string _password, string _name) // добавление пользователя
        {
            if (!ReadingClientForRegistr(_login, _name))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                string sqlExpressionInsert = String.Format("INSERT INTO Users(Login, Password, Name) VALUES('{0}', '{1}', '{2}')", _login, _password, _name);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpressionInsert, connection);
                    int number = command.ExecuteNonQuery();
                }
                return true;
            }
            else
            {
                return false;
            }
            
        }
        
        public bool ReadingClientForOpenChat(string _login, string _password) // проверяем существуют ли данный пользователь в БД
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sqlExpressionSelect = "SELECT * FROM Users";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpressionSelect, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        object Login = reader.GetValue(1);
                        object Password = reader.GetValue(2);

                        string login = Convert.ToString(Login);
                        string password = Convert.ToString(Password);

                        string login_prav = null;
                        string password_prav = null;
                        foreach (var item in login)
                        {
                            if (item != ' ')
                            {
                                login_prav += item;
                            }
                        }
                        foreach (var item in password)
                        {
                            if (item != ' ')
                            {
                                password_prav += item;
                            }
                        }
                        if (login_prav == _login && password_prav == _password)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public string ReceiveName(string _login) // определяем имя пользователя по логину из БД
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sqlExpressionSelect = "SELECT * FROM Users";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpressionSelect, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        object Login = reader.GetValue(1);
                        object Name = reader.GetValue(3);

                        string login = Convert.ToString(Login);
                        string name = Convert.ToString(Name);

                        string name_prav = null;
                        string login_prav = null;
                        foreach (var item in login)
                        {
                            if (item != ' ')
                            {
                                login_prav += item;
                            }
                        }
                        foreach (var item in name)
                        {
                            if (item != ' ')
                            {
                                name_prav += item;
                            }
                        }
                        if (_login == login_prav)
                        {
                            return name_prav;
                        }
                    }
                }
            }
            return null;
        }
        public bool ReadingClientForRegistr(string _login, string _name) // чтение из базы данных при регистрации
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sqlExpressionSelect = "SELECT * FROM Users";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpressionSelect, connection);
                SqlDataReader reader = command.ExecuteReader(); 
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        object Login = reader.GetValue(1);
                        object Name = reader.GetValue(3);

                        string login = Convert.ToString(Login);
                        string name = Convert.ToString(Name);

                        string name_prav = null;
                        string login_prav = null;
                        foreach (var item in login)
                        {
                            if (item != ' ')
                            {
                                login_prav += item;
                            }
                        }
                        foreach (var item in name)
                        {
                            if (item != ' ')
                            {
                                name_prav += item;
                            }
                        }
                        if (login_prav == _login && name_prav == _name)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

    }
}

