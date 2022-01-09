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

            if (stream != null)
            {
                try
                {
                    bytes = stream.Read(data, 0, data.Length);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                catch (System.ObjectDisposedException)
                {

                    bytes = 0;
                }
                

            }
            message = builder.ToString();
            Message(message);
        }
        public void Message(string message_nov)
        {
            try
            {
                Dispatcher.Invoke(() => tMessage.Text += message_nov + "\n");
                Function();
            }
            catch (System.Threading.Tasks.TaskCanceledException)
            {

                client.Close();
                stream.Close();
            }
            

        }



        private void tbMessageOutput_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tbMessageOutput.Text = null;
        }



        private void bSend_Click(object sender, RoutedEventArgs e)
        {
            if (tbMessageOutput.Text != null && tbMessageOutput.Text != "Введите сообщение" && tbMessageOutput.Text != "" && stream != null)
            {
                message = String.Format("{0}:{1}", name, tbMessageOutput.Text);
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);
                tbMessageOutput.Text = null;
            }
            
        }

        private void bConnect_Click(object sender, RoutedEventArgs e)
        {
            Task task = null;
            try
            {
                client = new TcpClient(tbIPAddress.Text, Convert.ToInt32(tbPort.Text));
                stream = client.GetStream();
            }
            catch (System.NullReferenceException)
            {

                tMessage.Text += "Подключение не удалось!\n";
            }
            catch (SocketException)
            {
                tMessage.Text += "Подключение не удалось!\n";
            }

            if (stream != null)
            {
                task = Task.Run(Function);
                tMessage.Text += "Вы успешно подключились к серверу\n";
                bConnect.IsEnabled = false;
                SQLConnect connect = new SQLConnect();
                message = string.Format(connect.ReceiveName(tNick.Text));
                byte[] data = Encoding.Unicode.GetBytes(message);
                if (stream != null)
                {
                    stream.Write(data, 0, data.Length);
                }
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (stream != null)
            {
                message = String.Format("{0}:{1}", name, "|||exit");
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);
                client.Close();
                stream.Close();
            }
            
        }
    }
}
