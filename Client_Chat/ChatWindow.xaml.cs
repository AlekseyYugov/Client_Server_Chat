using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
    /// Логика взаимодействия для ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        TcpClient client = null;
        NetworkStream stream = null;
        public string name = null;
        public ChatWindow()
        {
            InitializeComponent();
        }
        string message = null;
        public void Function()
        {
            byte[] data = new byte[64];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            } while (stream.DataAvailable);
            message = builder.ToString();
            Message(message);
        }
        public void Message(string message_nov)
        {
            Dispatcher.Invoke(() => tMessage.Text += message_nov + "\n");
            Function();
        }



        private void tbMessageOutput_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tbMessageOutput.Text = null;
        }



        private void bSend_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            message = String.Format("{0}:{1}", name, tbMessageOutput.Text);
            byte[] data = Encoding.Unicode.GetBytes(message);
            stream.Write(data, 0, data.Length);
            tbMessageOutput.Text = null;
        }

        private void bConnectDisconnect_Click(object sender, RoutedEventArgs e)
        {
            client = new TcpClient(tbIPAddress.Text, Convert.ToInt32(tbPort.Text));
            stream = client.GetStream();

            Task task = Task.Run(Function);
            tMessage.Text += "Вы успешно подключились к серверу\n";
            bConnectDisconnect.Content = "Отключиться";
        }
    }
}
