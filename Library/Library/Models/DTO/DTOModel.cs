using Library.Models.Client;

namespace Library.Models.DTO;

public class DTOModel
{
    private List<ClientModel> clientList;
    private ClientModel client;

    public DTOModel(List<ClientModel> clientList, ClientModel client)
    {
        this.clientList = clientList;
        this.client = client;
    }

    public DTOModel()
    {
        clientList = new();
        client = new();
    }

    public List<ClientModel> ClientList { get => clientList; set => clientList = value; }
    public ClientModel Client { get => client; set => client = value; }
}
