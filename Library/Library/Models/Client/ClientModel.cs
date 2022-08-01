using System.ComponentModel.DataAnnotations;

namespace Library.Models.Client;

public class ClientModel
{
    private int client_id = 0;
    private MartialStatusModel martial_status = new();
    private string name = "";

    [Required]
    private string email = "";

    private DateTime birthdate;

    public enum GenderClient
    { M, F };

    private GenderClient gender;
    private DateTime create_at;
    private DateTime update_at;
    private Boolean active = false;

    public ClientModel(int client_id, MartialStatusModel martial_status, string name, string email, DateTime birthdate, GenderClient gender, DateTime create_at, DateTime update_at, bool active)
    {
        this.client_id = client_id;
        this.martial_status = martial_status;
        this.name = name;
        this.email = email;
        this.birthdate = birthdate;
        this.gender = gender;
        this.create_at = create_at;
        this.update_at = update_at;
        this.active = active;
    }

    public ClientModel()
    {
    }

    public int Client_id { get => client_id; set => client_id = value; }
    public MartialStatusModel Martial_Status { get => martial_status; set => martial_status = value; }
    public string Name { get => name; set => name = value; }
    public string Email { get => email; set => email = value; }
    public DateTime Birthdate { get => birthdate; set => birthdate = value; }
    public GenderClient Gender { get => gender; set => gender = value; }
    public DateTime Create_at { get => create_at; set => create_at = value; }
    public DateTime Update_at { get => update_at; set => update_at = value; }
    public bool Active { get => active; set => active = value; }
}