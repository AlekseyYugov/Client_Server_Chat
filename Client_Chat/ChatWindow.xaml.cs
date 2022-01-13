using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Client_Chat
{
    public partial class ChatWindow : Window
    {
        TcpClient client = null;
        NetworkStream stream = null;
        public string name = null;
        public string getName = null;
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
                catch (System.IO.IOException)
                {
                    return;
                }
            }
            message = builder.ToString();
            
            Message(message);
        }

        public void Message(string message_nov)
        {
            try
            {
                Dispatcher.Invoke(() => tMessage.Text += message_nov);
                Function();
            }
            catch (System.Threading.Tasks.TaskCanceledException)
            {
                client.Close();
                stream.Close();
            }
            

        }



        //private void tbMessageOutput_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (tbMessageOutput.Text == "Введите сообщение ")
        //    {
        //        tbMessageOutput.Text = null;
        //    }
            
        //}



        private void bSend_Click(object sender, RoutedEventArgs e) // отправка сообщения клиентом
        {
            if (tbMessageOutput.Text != null &&  tbMessageOutput.Text != "" && stream != null)
            {
                message = String.Format("{0}: {1}", name, tbMessageOutput.Text);
                byte[] data = Encoding.Unicode.GetBytes(message);
                try
                {
                    stream.Write(data, 0, data.Length);
                }
                catch (System.IO.IOException)
                {

                    tMessage.Text += "Нет связи с сервером\n";
                    bConnect.IsEnabled = true;
                    stream = null;
                }
                
                tbMessageOutput.Text = null;
                Scroll.ScrollToEnd();
            }
            
        }

        private void bConnect_Click(object sender, RoutedEventArgs e) // подключение клиента
        {
            if (tbIPAddress.Text != "" && tbPort.Text != "")
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
                    tbIPAddress.IsEnabled = false;
                    tbPort.IsEnabled = false;
                    SQLConnect connect = new SQLConnect();
                    message = string.Format(connect.ReceiveName(getName));
                    getName = message;
                    byte[] data = Encoding.Unicode.GetBytes(message);
                    if (stream != null)
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }
            }
            else
            {
                tMessage.Text += "IP и порт не могут быть пустыми\n";
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) // обработка закрытия чата пользователем
        {
            Disconnect();
            
        }

        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (bConnect.IsEnabled == true)
                {
                    bConnect_Click(sender, e);
                }
                else
                {
                    bSend_Click(sender, e);
                }
                
            }
            if (e.Key == Key.Escape)
            {
                Close();
                Disconnect();
            }
        }
        private void Disconnect()
        {
            if (stream != null)
            {
                message = String.Format("{0}{1}", name, "|||exit");
                byte[] data = Encoding.Unicode.GetBytes(message);
                try
                {
                    stream.Write(data, 0, data.Length);
                }
                catch (System.ObjectDisposedException)
                {
                    client.Close();
                    stream.Close();
                }
                
            }
        }
    }
}
