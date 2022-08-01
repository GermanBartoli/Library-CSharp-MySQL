using MySqlConnector;

namespace Library.Models.Client.DBMClient;

public class DBMClient
{
    public List<ClientModel> LoadListClient()
    {
        List<ClientModel> clientList = new List<ClientModel>();

        MySqlConnection connection = new MySqlConnection(DBMConection.ConectionString);


        return clientList;
    }
}