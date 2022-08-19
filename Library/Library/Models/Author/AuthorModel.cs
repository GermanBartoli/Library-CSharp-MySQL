namespace Library.Models.Author;

public class AuthorModel
{
    private int author_id;
    private string name;
    private string nationality;
    private DateTime created_at;
    private DateTime updated_at;
    private bool active;

    public AuthorModel(
        int author_id,
        string name,
        string nationality,
        DateTime create_at,
        DateTime updated_at,
        bool active)
    {
        this.author_id = author_id;
        this.name = name;
        this.nationality = nationality;
        this.created_at = create_at;
        this.updated_at = updated_at;
        this.active = active;
    }

    public AuthorModel()
    {
        author_id = 0;
        name = "";
        nationality = "";
        created_at = DateTime.Now;
        updated_at = DateTime.Now;
        active = false;
    }

    public int Author_Id { get => author_id; set => author_id = value; }
    public string Name { get => name; set => name = value; }
    public string Nationality { get => nationality; set => nationality = value; }
    public DateTime Created_at { get => created_at; set => created_at = value; }
    public DateTime Updated_at { get => updated_at; set => updated_at = value; }
    public bool Active { get => active; set => active = value; }
}
