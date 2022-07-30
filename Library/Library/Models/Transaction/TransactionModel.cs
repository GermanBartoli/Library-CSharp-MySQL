using Library.Models.Book;
using Library.Models.Client;

namespace Library.Models.Transaction;

private enum Estate { Lend, Sell, Return };

public class TransactionModel
{
    private int transaction_id = 0;
    private BookModel book = new();
    private ClientModel client = new();
    private DateTime date;
    private Estate estate;
    private DateTime created_at;
    private DateTime updated_at;
    private bool finished = 0;
    private bool active = 1;

    public TransactionModel(
        int transaction_id,
        BookModel book,
        ClientModel client,
        DateTime date,
        Estate estate,
        DateTime created_at,
        DateTime updated_at,
        bool finished,
        bool active)
    {
        this.transaction_id = transaction_id;
        this.book = book;
        this.client = client;
        this.date = date;
        this.Estate1 = estate;
        this.created_at = created_at;
        this.updated_at = updated_at;
        this.finished = finished;
        this.active = active;
    }

    public TransactionModel()
    {
    }

    public int Transaction_id { get => transaction_id; set => transaction_id = value; }
    public BookModel Book { get => book; set => book = value; }
    public ClientModel Client { get => client; set => client = value; }
    public DateTime Date { get => date; set => date = value; }
    public DateTime Created_at { get => created_at; set => created_at = value; }
    public DateTime Updated_at { get => updated_at; set => updated_at = value; }
    public bool Finished { get => finished; set => finished = value; }
    public bool Active { get => active; set => active = value; }
    private Estate Estate { get => estate; set => estate = value; }
}