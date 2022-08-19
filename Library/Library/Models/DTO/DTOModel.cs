using Library.Models.Author;
using Library.Models.Client;

namespace Library.Models.DTO;

public class DTOModel
{
    private List<ClientModel> clientList;
    private ClientModel client;

    private List<AuthorModel> authorList;
    private AuthorModel author;

    public DTOModel(
        List<ClientModel> clientList,
        ClientModel client,
        List<AuthorModel> authorList,
        AuthorModel author)
    {
        this.clientList = clientList;
        this.client = client;
        this.authorList = authorList;
        this.author = author;
    }

    public DTOModel()
    {
        clientList = new();
        client = new();
        authorList = new();
        author = new();
    }

    public List<ClientModel> ClientList { get => clientList; set => clientList = value; }
    public ClientModel Client { get => client; set => client = value; }
    public List<AuthorModel> AuthorList { get => authorList; set => authorList = value; }
    public AuthorModel Author { get => author; set => author = value; }
}
