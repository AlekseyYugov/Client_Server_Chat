using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client_Chat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
                chatWindow.tNick.Text = tbLogin.Text; // проверка
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
    }
}
