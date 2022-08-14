using System.Data;
using Library.Models.Client.MartialStatus;
using Library.Models.DTO;
using MySqlConnector;

namespace Library.Models.Client.DBMClient;
public class DBMClient
{
    public List<ClientModel> LoadClientList()
    {
        List<ClientModel> clientList = new List<ClientModel>();

        MySqlConnection connection = new MySqlConnection(DBMConnection.ConnectionString);

        string consult =
                        @"
                        SELECT
                        clients.client_id AS client_id,
                        martialstatus.martial_status_id AS martial_status_id,
                        martialstatus.description AS martial_status_description,
                        clients.name AS name,
                        clients.email AS email,
                        clients.birthdate AS birthdate,
                        clients.gender AS gender,
                        clients.created_at AS created_at,
                        clients.updated_at AS updated_at,
                        clients.active AS active
                        FROM clients
                        INNER JOIN martialstatus
                          ON clients.martial_status_id = martialstatus.martial_status_id
                        WHERE clients.active = 1";

        MySqlCommand command = new MySqlCommand(consult, connection);
        command.CommandType = CommandType.Text;

        connection.Open();

        MySqlDataReader reader;
        reader = command.ExecuteReader();

        while (reader.Read())
        {
            //Console.WriteLine(reader.GetInt32(0));

            ClientModel client = new();
            client.Client_Id = Convert.IsDBNull(reader.GetValue("client_id")) ? 0 : reader.GetInt32("client_id");

            MartialStatusModel martialStatus = new();
            martialStatus.Martial_Status_Id = Convert.IsDBNull(reader.GetValue("martial_status_id")) ? 0 : reader.GetInt32("martial_status_id");
            martialStatus.Description = Convert.IsDBNull(reader.GetValue("martial_status_description")) ? string.Empty : reader.GetString("martial_status_description");
            client.Martial_Status = martialStatus;

            client.Name = Convert.IsDBNull(reader.GetValue("name")) ? string.Empty : reader.GetString("name");
            client.Email = Convert.IsDBNull(reader.GetValue("email")) ? string.Empty : reader.GetString("email");
            client.Birthdate = Convert.IsDBNull(reader.GetValue("birthdate")) ? DateTime.Now : reader.GetDateTime("birthdate");

            client.Gender = Convert.IsDBNull(reader.GetValue("gender")) ? string.Empty : reader.GetString("gender");

            client.Created_at = Convert.IsDBNull(reader.GetValue("created_at")) ? DateTime.Now : reader.GetDateTime("created_at");
            client.Updated_at = Convert.IsDBNull(reader.GetValue("updated_at")) ? DateTime.Now : reader.GetDateTime("updated_at");

            client.Active = Convert.IsDBNull(reader.GetValue("active")) ? false : reader.GetBoolean("active");

            clientList.Add(client);
        }
        reader.Close();
        connection.Close();

        return clientList;
    }

    public bool AddClient(ClientModel client)
    {
        int rowsAffected = 0;

        MySqlConnection connection = new MySqlConnection(DBMConnection.ConnectionString);

        string consult =
                        @"
                        INSERT INTO clients
                        (
                        martial_status_id
                        ,name
                        ,email
                        ,birthdate
                        ,gender
                        ,created_at
                        ,updated_at
                        ,active
                        )
                        VALUES
                        (
                        @martial_status_id -- martial_status_id - INT NOT NULL
                        ,@name -- name - VARCHAR(50)
                        ,@email -- email - VARCHAR(100) NOT NULL
                        ,@birthdate -- birthdate - DATE
                        ,@gender -- gender - ENUM('M','F')
                        ,@active -- active - TINYINT NOT NULL
                        );";

        MySqlCommand command = new MySqlCommand(consult, connection);
        command.CommandType = CommandType.Text;

        command.Parameters.AddWithValue("@martial_status_id", client.Martial_Status.Martial_Status_Id);
        command.Parameters.AddWithValue("@name", client.Name);
        command.Parameters.AddWithValue("@email", client.Email);
        command.Parameters.AddWithValue("@birthdate", client.Birthdate);
        command.Parameters.AddWithValue("@gender", client.Gender);

        command.Parameters.AddWithValue("@active", 1);

        connection.Open();

        rowsAffected = command.ExecuteNonQuery();

        connection.Close();

        if (rowsAffected > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool EditClient(ClientModel client)
    {
        int rowsAffected = 0;

        MySqlConnection connection = new MySqlConnection(DBMConnection.ConnectionString);

        string consult =
                        @"
                        UPDATE clients 
                        SET
                          martial_status_id = @martial_status_id -- martial_status_id - INT NOT NULL
                          ,name = @name -- name - VARCHAR(50)
                          ,email = @email -- email - VARCHAR(100) NOT NULL
                          ,birthdate = @birthdate -- birthdate - DATE
                          ,gender = @gender -- gender - ENUM('M','F')
                          ,active = @active -- active - TINYINT NOT NULL
                        WHERE
                          client_id = @client_id  -- client_id - INT NOT NULL;";

        MySqlCommand command = new MySqlCommand(consult, connection);

        command.Parameters.AddWithValue("@martial_status_id", client.Martial_Status.Martial_Status_Id);
        command.Parameters.AddWithValue("@name", client.Name);
        command.Parameters.AddWithValue("@email", client.Email);
        command.Parameters.AddWithValue("@birthdate", client.Birthdate);
        command.Parameters.AddWithValue("@gender", client.Gender);
        command.Parameters.AddWithValue("@active", true);

        command.Parameters.AddWithValue("@client_id", client.Client_Id);

        connection.Open();

        rowsAffected = command.ExecuteNonQuery();

        connection.Close();

        if (rowsAffected > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool DisableClient(int ClientID)
    {
        int rowsAffected = 0;

        MySqlConnection connection = new MySqlConnection(DBMConnection.ConnectionString);

        string consult = @"
                                Update clients
                                set active = false
                                where client_id = @client_id
                                ";

        MySqlCommand command = new();

        command.Connection = connection;
        command.CommandText = consult;
        command.CommandType = CommandType.Text;

        command.Parameters.AddWithValue("@client_id", ClientID);

        connection.Open();

        rowsAffected = command.ExecuteNonQuery();

        connection.Close();

        if (rowsAffected > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public DTOModel GetClientByClientID(int? Client_Id)
    {
        DTOModel dto = new();

        String consult =
                        @"
                        SELECT
                        clients.client_id AS client_id,
                        martialstatus.martial_status_id AS martial_status_id,
                        martialstatus.description AS martial_status_description,
                        clients.name AS name,
                        clients.email AS email,
                        clients.birthdate AS birthdate,
                        clients.gender AS gender,
                        clients.created_at AS created_at,
                        clients.updated_at AS updated_at,
                        clients.active AS active
                        FROM clients
                        INNER JOIN martialstatus
                          ON clients.martial_status_id = martialstatus.martial_status_id
                        WHERE clients.active = 1 and
                        clients.client_id = @client_ID;";

        MySqlConnection connection = new MySqlConnection(DBMConnection.ConnectionString);

        MySqlCommand command = new MySqlCommand(consult, connection);
        command.CommandType = CommandType.Text;

        command.Parameters.AddWithValue("@client_ID", Client_Id);

        connection.Open();

        MySqlDataReader reader;
        reader = command.ExecuteReader();

        while (reader.Read())
        {
            //Console.WriteLine(Convert.IsDBNull(reader.GetValue("client_id")) ? 0 : reader.GetInt32("client_id"));

            ClientModel client = new();
            client.Client_Id = Convert.IsDBNull(reader.GetValue("client_id")) ? 0 : reader.GetInt32("client_id");

            MartialStatusModel martialStatus = new();
            martialStatus.Martial_Status_Id = Convert.IsDBNull(reader.GetValue("martial_status_id")) ? 0 : reader.GetInt32("martial_status_id");
            martialStatus.Description = Convert.IsDBNull(reader.GetValue("martial_status_description")) ? string.Empty : reader.GetString("martial_status_description");
            client.Martial_Status = martialStatus;

            client.Name = Convert.IsDBNull(reader.GetValue("name")) ? string.Empty : reader.GetString("name");
            client.Email = Convert.IsDBNull(reader.GetValue("email")) ? string.Empty : reader.GetString("email");
            client.Birthdate = Convert.IsDBNull(reader.GetValue("birthdate")) ? DateTime.Now : reader.GetDateTime("birthdate");

            client.Gender = Convert.IsDBNull(reader.GetValue("gender")) ? string.Empty : reader.GetString("gender");

            client.Created_at = Convert.IsDBNull(reader.GetValue("created_at")) ? DateTime.Now : reader.GetDateTime("created_at");
            client.Updated_at = Convert.IsDBNull(reader.GetValue("updated_at")) ? DateTime.Now : reader.GetDateTime("updated_at");

            client.Active = Convert.IsDBNull(reader.GetValue("active")) ? false : reader.GetBoolean("active");

            dto.Client = client;
        }
        reader.Close();
        connection.Close();

        return dto;
    }

    public int GetLastClientID()
    {
        int lastClientID = 0;

        try
        {
            MySqlConnection connection = new MySqlConnection(DBMConnection.ConnectionString);

            string consult =
                "SELECT " +
                "MAX(client_id) " +
                "FROM Client";

            MySqlCommand command = new MySqlCommand(consult, connection);
            command.CommandType = CommandType.Text;

            connection.Open();

            MySqlDataReader reader;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                //Console.WriteLine(reader.GetInt32(0));

                lastClientID = reader.GetInt32(0);
            }

            reader.Close();
            connection.Close();
        }
        catch (global::System.Exception)
        {
            throw;
        }
        return lastClientID;
    }
}
