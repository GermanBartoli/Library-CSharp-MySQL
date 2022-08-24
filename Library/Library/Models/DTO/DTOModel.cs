using Library.Models.Author;
using Library.Models.Book;
using Library.Models.Book.DBMBook;
using Library.Models.Client;
using Library.Models.Transaction;

namespace Library.Models.DTO;

public class DTOModel
{
    private ClientModel client;
    private List<ClientModel> clientList;

    private AuthorModel author;
    private List<AuthorModel> authorList;

    private BookModel book;
    private List<BookModel> bookList;
    private DBMBook dBMBook;

    private TransactionModel transaction;
    private List<TransactionModel> transactionList;

    public DTOModel(
    ClientModel client,
    List<ClientModel> clientList,
    AuthorModel author,
    List<AuthorModel> authorList,
    BookModel book,
    List<BookModel> bookList,
    DBMBook dBMBook,
    TransactionModel transaction,
    List<TransactionModel> transactionList)
    {
        this.client = client;
        this.clientList = clientList;
        this.author = author;
        this.authorList = authorList;
        this.book = book;
        this.bookList = bookList;
        this.dBMBook = dBMBook;
        this.transaction = transaction;
        this.transactionList = transactionList;
    }

    public DTOModel()
    {
        client = new();
        clientList = new();

        author = new();
        authorList = new();

        book = new();
        bookList = new();
        dBMBook = new();

        transactionList = new();
        transaction = new();
    }

    public ClientModel Client { get => client; set => client = value; }
    public List<ClientModel> ClientList { get => clientList; set => clientList = value; }
    public AuthorModel Author { get => author; set => author = value; }
    public List<AuthorModel> AuthorList { get => authorList; set => authorList = value; }
    public BookModel Book { get => book; set => book = value; }
    public List<BookModel> BookList { get => bookList; set => bookList = value; }
    public DBMBook DBMBook { get => dBMBook; set => dBMBook = value; }
    public TransactionModel Transaction { get => transaction; set => transaction = value; }
    public List<TransactionModel> TransactionList { get => transactionList; set => transactionList = value; }
}
