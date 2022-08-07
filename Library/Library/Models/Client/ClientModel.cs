using System.ComponentModel.DataAnnotations;
using Library.Models.Client.MartialStatus;

namespace Library.Models.Client;

public partial class ClientModel
{
    private int client_id = 0;
    private MartialStatusModel martial_status = new();
    private string name = "";
    [Required]
    private string email = "";
    private DateTime birthdate;
    private string gender = "";

    private DateTime created_at;
    private DateTime updated_at;
    private Boolean active = false;

    public ClientModel(
        int client_id,
        MartialStatusModel martial_status,
        string name,
        string email,
        DateTime birthdate,
        string gender,
        DateTime created_at,
        DateTime updated_at,
        bool active)
    {
        this.client_id = client_id;
        this.martial_status = martial_status;
        this.name = name;
        this.email = email;
        this.birthdate = birthdate;
        this.gender = gender;
        this.created_at = created_at;
        this.updated_at = updated_at;
        this.active = active;
    }

    public ClientModel()
    {
    }

    public int Client_Id { get => client_id; set => client_id = value; }
    public MartialStatusModel Martial_Status { get => martial_status; set => martial_status = value; }
    public string Name { get => name; set => name = value; }
    public string Email { get => email; set => email = value; }
    public DateTime Birthdate { get => birthdate; set => birthdate = value; }
    public string Gender { get => gender; set => gender = value; }
    public DateTime Created_at { get => created_at; set => created_at = value; }
    public DateTime Updated_at { get => updated_at; set => updated_at = value; }
    public bool Active { get => active; set => active = value; }
}
