namespace Library.Models.Author
{
    public class AuthorModel
    {
        private int author_id;
        private string name = "";
        private string nationality = "";
        private bool active = false;

        public AuthorModel(int author_id,
                           string name,
                           string nationality,
                           bool active)
        {
            this.Author_id = author_id;
            this.Name = name;
            this.Nationality = nationality;
            this.Active = active;
        }

        public int Author_id { get => author_id; set => author_id = value; }
        public string Name { get => name; set => name = value; }
        public string Nationality { get => nationality; set => nationality = value; }
        public bool Active { get => active; set => active = value; }
    }
}