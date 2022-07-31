using System.ComponentModel.DataAnnotations;

namespace Library.Models.Client;

public class ClientModel
{
    private int client_id = 0;
    private string name = "";

    [Required]
    private string email = "";

    private DateTime birthdate;
    public enum GenderClient { M, F };
    private GenderClient gender;
    private DateTime create_at;
    private DateTime update_at;
    private Boolean active = false;

}