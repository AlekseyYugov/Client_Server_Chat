using System.Windows;
using System.Windows.Input;

namespace Client_Chat
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void bOpen_Click(object sender, RoutedEventArgs e)
        {
            ShowEx_MainWindow.Text = null;
            SQLConnect connect = new SQLConnect();
            if (connect.ReadingClientForOpenChat(tbLogin.Text, pbPassword.Password))
            {
                ChatWindow chatWindow = new ChatWindow();
                chatWindow.Show();
                chatWindow.getName = tbLogin.Text;
                Close();
            }
            else
            {
                ShowEx_MainWindow.Text = "Неверно указан логин или пароль";
            }
        }

        private void bRegistr_Click(object sender, RoutedEventArgs e)
        {
            WindowRegistr windowRegistr = new WindowRegistr();
            windowRegistr.Owner = this;
            windowRegistr.Show();
        }

        private void Grid_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                bOpen_Click(sender, e);
            }
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }
}
