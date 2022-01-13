using System.Windows;
using System.Windows.Input;

namespace Client_Chat
{
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
        }

        private void Grid_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                bRegistr_NewWindow_Click(sender, e);
            }
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }
}
