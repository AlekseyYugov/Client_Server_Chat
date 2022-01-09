using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Chat
{
    class MessageHistory
    {
        public void InsertMessage(string _name, string _message, string _time)
        {
            if (_message != null && _message != "")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
                string sqlExpressionInsert = String.Format("INSERT INTO History(Name, Message, Time) VALUES('{0}', '{1}', '{2}')", _name, _message, _time);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpressionInsert, connection);
                    int number = command.ExecuteNonQuery();
                }
            }
            
        }
    }
}
