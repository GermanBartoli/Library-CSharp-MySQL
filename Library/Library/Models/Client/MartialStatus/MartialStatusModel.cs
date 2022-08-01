namespace Library.Models.Client;

public class MartialStatusModel
{
    private int martial_status_id = 0;
    private string description = "";

    public MartialStatusModel(int martial_status_id, string description)
    {
        this.Martial_status_id = martial_status_id;
        this.Description = description;
    }

    public MartialStatusModel()
    {
    }

    public int Martial_status_id { get => martial_status_id; set => martial_status_id = value; }
    public string Description { get => description; set => description = value; }
}