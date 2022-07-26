using System.ComponentModel.DataAnnotations;

namespace Library.Models.Client
{
    public class ClientModel
    {
        private int client_id = 0;
        private string name = "";

        [Required]
        private string email = "";

        private DateTime birthdate;
        private Gender gender = 0;
        private DateTime create_at;
        private Boolean active = false;

        private enum Gender
        {
            Female = 0,
            Male = 1
        }
    }
}