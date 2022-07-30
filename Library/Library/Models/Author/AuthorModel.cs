namespace Library.Models.Author;
public class AuthorModel
{
    private int author_id = 0;
    private string name = "";
    private string nationality = "";
    private DateTime create_at;
    private DateTime updated_at;
    private bool active = false;
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
        this.create_at = create_at;
        this.updated_at = updated_at;
        this.active = active;
    }
    public AuthorModel()
    {
    }
    public int Author_id { get => author_id; set => author_id = value; }
    public string Name { get => name; set => name = value; }
    public string Nationality { get => nationality; set => nationality = value; }
    public DateTime Create_at { get => create_at; set => create_at = value; }
    public DateTime Updated_at { get => updated_at; set => updated_at = value; }
    public bool Active { get => active; set => active = value; }
}