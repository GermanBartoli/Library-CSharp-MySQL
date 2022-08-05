namespace Library.Models.Client.MartialStatus;

public class MartialStatusModel
{
    private int martial_status_id = 0;
    private string description = "";

    public MartialStatusModel(int martial_status_id, string description)
    {
        this.Martial_Status_Id = martial_status_id;
        this.Description = description;
    }

    public MartialStatusModel()
    {
    }

    public int Martial_Status_Id { get => martial_status_id; set => martial_status_id = value; }
    public string Description { get => description; set => description = value; }
}