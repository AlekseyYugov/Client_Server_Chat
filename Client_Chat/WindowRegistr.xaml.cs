using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client_Chat
{
    /// <summary>
    /// Логика взаимодействия для WindowRegistr.xaml
    /// </summary>
    public partial class WindowRegistr : Window
    {
        public WindowRegistr()
        {
            InitializeComponent();
        }
        private void bRegistr_NewWindow_Click(object sender, RoutedEventArgs e)
        {
            ShowEx_RegistrWindow.Text = null;
            SuccessfulRegistration.Text = null;
            if (tbLogin.Text.Length < 5)
            {
                tbLogin.Text = null;
                ShowEx_RegistrWindow.Text = "Логин должен быть от 5 до 10 символов";
                return;
            }
            if (pbPassword.Password != pbPassword_2.Password)
            {
                pbPassword.Password = null;
                pbPassword_2.Password = null;
                ShowEx_RegistrWindow.Text = "Ваши пароли не совпадают";
                return;
            }
            if (pbPassword.Password.Length < 5)
            {
                pbPassword.Password = null;
                pbPassword_2.Password = null;
                ShowEx_RegistrWindow.Text = "Пароль должен быть от 5 до 10 символов";
                return;
            }
            if (tbName.Text.Length < 2)
            {
                ShowEx_RegistrWindow.Text = "Имя должно быть от 3 до 10 символов";
            }

            SQLConnect connect = new SQLConnect();
            if (connect.InsertUser(tbLogin.Text, pbPassword.Password, tbName.Text))
            {
                SuccessfulRegistration.Text = "Регистрация прошла успешно";
            }


            //string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //string sqlExpressionSelect = "SELECT * FROM Users";

            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    bool allclear = true;
            //    connection.Open();
            //    SqlCommand command = new SqlCommand(sqlExpressionSelect, connection);
            //    SqlDataReader reader = command.ExecuteReader(); // чтение из базы данных
            //    if (reader.HasRows)
            //    {
            //        while (reader.Read())
            //        {
            //            object Id = reader.GetValue(0);
            //            object Login = reader.GetValue(1);
            //            object Password = reader.GetValue(2);
            //            object Name = reader.GetValue(3);

            //            string login = Convert.ToString(Login);
            //            string name = Convert.ToString(Name);

            //            string name_prav = null;
            //            string login_prav = null;
            //            foreach (var item in login)
            //            {
            //                if (item != ' ')
            //                {
            //                    login_prav += item;
            //                }
            //            }
            //            foreach (var item in name)
            //            {
            //                if (item != ' ')
            //                {
            //                    name_prav += item;
            //                }
            //            }


            //            if (login_prav == tbLogin.Text)
            //            {
            //                ShowEx_RegistrWindow.Text = "Ошибка: Пользователь с таким логином уже существует\n";
            //                tbLogin.Text = null;
            //                allclear = false;
            //            }
            //            if (name_prav == tbName.Text)
            //            {
            //                ShowEx_RegistrWindow.Text = "Ошибка: Пользователь с таким именем уже существует\n";
            //                tbName.Text = null;
            //                allclear = false;
            //            }
            //            if (tbPassword.Password != tbPassword_2.Password)
            //            {
            //                ShowEx_RegistrWindow.Text = "Ошибка: Пароли не совпадают";
            //                allclear = false;
            //            }
            //            if (tbPassword.Password.Length < 5)
            //            {
            //                ShowEx_RegistrWindow.Text = "Ошибка: Пароль должен быть от 5 до 10 цифр";
            //                allclear = false;
            //            }
            //        }
            //        reader.Close();
            //        if (allclear == true)
            //        {
            //            InsertUser();
            //            SuccessfulRegistration.Text = "Регистрация прошла успешно";
            //        }
            //    }
            //    else
            //    {
            //        reader.Close();
            //        InsertUser();
            //        SuccessfulRegistration.Text = "Регистрация прошла успешно";
            //    }

            //}
        }
        //public void InsertUser() // добавление пользователя
        //{
        //    string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //    string sqlExpressionInsert = String.Format("INSERT INTO Users(Login, Password, Name) VALUES('{0}', {1}, '{2}')", tbLogin.Text, tbPassword.Password, tbName.Text);
            
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        SqlCommand command = new SqlCommand(sqlExpressionInsert, connection);
        //        int number = command.ExecuteNonQuery();
        //    }
        //}
    }
}
