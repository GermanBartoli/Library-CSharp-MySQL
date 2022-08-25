using Library.Models.Author;
using Library.Models.Author.DBMAuthor;

using Library.Models.Book;
using Library.Models.Book.DBMBook;

using Library.Models.Client;
using Library.Models.Client.DBMClient;

using Library.Models.Transaction;
using Library.Models.Transaction.DBMTransaction;

namespace Library.Models.DTO;

public class DTOModel
{
    private ClientModel client;
    private List<ClientModel> clientList;
    private DBMClient dBMClient;

    private AuthorModel author;
    private List<AuthorModel> authorList;
    private DBMAuthor dBMAuthor;

    private BookModel book;
    private List<BookModel> bookList;
    private DBMBook dBMBook;

    private TransactionModel transaction;
    private List<TransactionModel> transactionList;
    private DBMTransaction dBMTransaction;

    public DTOModel(

    ClientModel client,
    List<ClientModel> clientList,
    DBMClient dBMClient,

    AuthorModel author,
    List<AuthorModel> authorList,
    DBMAuthor dBMAuthor,

    BookModel book,
    List<BookModel> bookList,
    DBMBook dBMBook,

    TransactionModel transaction,
    List<TransactionModel> transactionList,
    DBMTransaction dBMTransaction)
    {
        this.client = client;
        this.clientList = clientList;
        this.dBMClient = dBMClient;

        this.author = author;
        this.authorList = authorList;
        this.dBMAuthor = dBMAuthor;

        this.book = book;
        this.bookList = bookList;
        this.dBMBook = dBMBook;

        this.transaction = transaction;
        this.transactionList = transactionList;
        this.dBMTransaction = dBMTransaction;
    }

    public DTOModel()
    {
        client = new();
        clientList = new();
        dBMClient = new();

        author = new();
        authorList = new();
        dBMAuthor = new();

        book = new();
        bookList = new();
        dBMBook = new();

        transaction = new();
        transactionList = new();
        dBMTransaction = new();
    }

    public ClientModel Client { get => client; set => client = value; }
    public List<ClientModel> ClientList { get => clientList; set => clientList = value; }
    public DBMClient DBMClient { get => dBMClient; set => dBMClient = value; }

    public AuthorModel Author { get => author; set => author = value; }
    public List<AuthorModel> AuthorList { get => authorList; set => authorList = value; }
    public DBMAuthor DBMAuthor { get => dBMAuthor; set => dBMAuthor = value; }

    public BookModel Book { get => book; set => book = value; }
    public List<BookModel> BookList { get => bookList; set => bookList = value; }
    public DBMBook DBMBook { get => dBMBook; set => dBMBook = value; }

    public TransactionModel Transaction { get => transaction; set => transaction = value; }
    public List<TransactionModel> TransactionList { get => transactionList; set => transactionList = value; }
    public DBMTransaction DBMTransaction { get => dBMTransaction; set => dBMTransaction = value; }
}
