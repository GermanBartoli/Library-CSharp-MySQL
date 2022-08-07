
using System;
using System.Collections.Generic;
using MySqlConnector;
using System.Data;

namespace Library.Models.Client.MartialStatus;

public class DBMMartialStatus
{
    public List<MartialStatusModel> LoadComboBoxMartialStatus()
    {
        List<MartialStatusModel> comboBoxMartialStatus = new();

        MySqlConnection connection = new MySqlConnection(DBMConnection.ConnectionString);

        String consult =
                                        @"SELECT
                                          martialstatus.martial_status_id,
	                                      martialstatus.despcription
                                        FROM martialstatus
                                        order by 2; ";

        MySqlCommand command = new MySqlCommand(consult, connection);
        command.CommandType = CommandType.Text;

        connection.Open();

        MySqlDataReader reader;
        reader = command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine(reader.GetInt32(0));

            MartialStatusModel martialStatus = new();
            martialStatus.Martial_Status_Id = reader.GetInt32(0);
            martialStatus.Description = reader.GetString(1);

            comboBoxMartialStatus.Add(martialStatus);
        }
        reader.Close();
        connection.Close();

        return comboBoxMartialStatus;
    }
}
