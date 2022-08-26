using System.Data;
using Library.Models.Book;
using Library.Models.Client;
using Library.Models.DTO;
using MySqlConnector;

namespace Library.Models.Transaction.DBMTransaction;

public class DBMTransaction
{
    public List<TransactionModel> LoadTransactionList()
    {
        List<TransactionModel> transactionList = new List<TransactionModel>();

        MySqlConnection connection = new MySqlConnection(DBMConnection.ConnectionString);

        string consult =
        @"
            SELECT
                transactions.transaction_id AS transaction_id,
                transactions.date AS date,
                transactions.estate AS estate,
                transactions.created_at AS created_at,
                transactions.updated_at AS updated_at,
                transactions.finished AS finished,
                transactions.active AS active,
                books.book_id AS book_id,
                books.title AS title,
                clients.client_id AS client_id,
                clients.name AS name
            FROM 
                transactions
            INNER JOIN clients
                ON transactions.client_id = clients.client_id
            INNER JOIN books
                ON transactions.book_id = books.book_id
            WHERE 
                transactions.active = 1
        ";

        MySqlCommand command = new MySqlCommand(consult, connection);
        command.CommandType = CommandType.Text;

        connection.Open();

        MySqlDataReader reader;
        reader = command.ExecuteReader();

        while (reader.Read())
        {
            //Console.WriteLine(reader.GetInt32(0));

            TransactionModel transaction = new();
            transaction.Transaction_Id = Convert.IsDBNull(reader.GetValue("transaction_id")) ? 0 : reader.GetInt32("transaction_id");
            transaction.Date = Convert.IsDBNull(reader.GetValue("date")) ? DateTime.Now : reader.GetDateTime("date");
            transaction.Estate = Convert.IsDBNull(reader.GetValue("estate")) ? string.Empty : reader.GetString("estate");
            transaction.Created_at = Convert.IsDBNull(reader.GetValue("created_at")) ? DateTime.Now : reader.GetDateTime("created_at");
            transaction.Updated_at = Convert.IsDBNull(reader.GetValue("updated_at")) ? DateTime.Now : reader.GetDateTime("updated_at");
            transaction.Finished = Convert.IsDBNull(reader.GetValue("finished")) ? false : reader.GetBoolean("finished");
            transaction.Active = Convert.IsDBNull(reader.GetValue("active")) ? false : reader.GetBoolean("active");
            
            BookModel book = new();
            book.Book_Id = Convert.IsDBNull(reader.GetValue("book_id")) ? 0 : reader.GetInt32("book_id");
            book.Title = Convert.IsDBNull(reader.GetValue("title")) ? string.Empty : reader.GetString("title");
            transaction.Book = book;

            ClientModel client = new();
            client.Client_Id = Convert.IsDBNull(reader.GetValue("client_id")) ? 0 : reader.GetInt32("client_id");
            client.Name = Convert.IsDBNull(reader.GetValue("name")) ? string.Empty : reader.GetString("name");
            transaction.Client = client;

            transactionList.Add(transaction);
        }
        reader.Close();
        connection.Close();

        return transactionList;
    }
}
