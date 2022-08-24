using System.Data;
using Library.Models.Author;
using Library.Models.Book;
using Library.Models.DTO;
using MySqlConnector;

namespace Library.Models.Book.DBMBook;
public class DBMBook
{
    public List<BookModel> LoadBookList()
    {
        List<BookModel> bookList = new List<BookModel>();

        MySqlConnection connection = new MySqlConnection(DBMConnection.ConnectionString);

        string consult =
        @"
	        SELECT
                books.book_id AS book_id,
                books.author_id AS author_id,
                authors.name AS author_name,
                authors.nationality AS author_nationality,
                authors.created_at AS author_created_at,
                authors.updated_at AS author_updated_at,
                authors.active AS author_active,
                books.title AS title,
                books.year AS year,
                books.language AS language,
                books.cover_url AS cover_url,
                books.price AS price,
                books.sellable AS sellable,
                books.copies AS copies,
                books.description AS description,
                books.created_at AS created_at,
                books.updated_at AS updated_at,
                books.active AS active
            FROM 
                books
            LEFT JOIN 
                authors
            ON books.book_id = authors.author_id
            WHERE 
                books.active = 1;
        ";

        MySqlCommand command = new MySqlCommand(consult, connection);
        command.CommandType = CommandType.Text;

        connection.Open();

        MySqlDataReader reader;
        reader = command.ExecuteReader();

        while (reader.Read())
        {
            //Console.WriteLine(reader.GetInt32(0));

            BookModel book = new();
            book.Book_Id = Convert.IsDBNull(reader.GetValue("book_id")) ? 0 : reader.GetInt32("book_id");

            // Author
            AuthorModel author = new();
            author.Author_Id = Convert.IsDBNull(reader.GetValue("author_id")) ? 0 : reader.GetInt32("author_id");

            author.Name = Convert.IsDBNull(reader.GetValue("author_name")) ? string.Empty : reader.GetString("author_name");
            author.Nationality = Convert.IsDBNull(reader.GetValue("author_nationality")) ? string.Empty : reader.GetString("author_nationality");

            author.Created_at = Convert.IsDBNull(reader.GetValue("author_created_at")) ? DateTime.Now : reader.GetDateTime("author_created_at");
            author.Updated_at = Convert.IsDBNull(reader.GetValue("author_updated_at")) ? DateTime.Now : reader.GetDateTime("author_updated_at");

            book.Title = Convert.IsDBNull(reader.GetValue("title")) ? string.Empty : reader.GetString("title");
            book.Year = Convert.IsDBNull(reader.GetValue("year")) ? 0 : reader.GetInt32("year");
            book.Language = Convert.IsDBNull(reader.GetValue("language")) ? string.Empty : reader.GetString("language");
            book.Cover_url = Convert.IsDBNull(reader.GetValue("cover_url")) ? string.Empty : reader.GetString("cover_url");
            book.Price = Convert.IsDBNull(reader.GetValue("price")) ? 0 : reader.GetDouble("price");
            book.Sellable = Convert.IsDBNull(reader.GetValue("sellable")) ? false : reader.GetBoolean("sellable");
            book.Copies = Convert.IsDBNull(reader.GetValue("copies")) ? 0 : reader.GetInt32("copies");
            book.Description = Convert.IsDBNull(reader.GetValue("description")) ? string.Empty : reader.GetString("description");

            author.Active = Convert.IsDBNull(reader.GetValue("active")) ? false : reader.GetBoolean("active");

            book.Author = author;
            // Author

            book.Created_at = Convert.IsDBNull(reader.GetValue("created_at")) ? DateTime.Now : reader.GetDateTime("created_at");
            book.Updated_at = Convert.IsDBNull(reader.GetValue("updated_at")) ? DateTime.Now : reader.GetDateTime("updated_at");

            book.Active = Convert.IsDBNull(reader.GetValue("active")) ? false : reader.GetBoolean("active");

            bookList.Add(book);
        }
        reader.Close();
        connection.Close();

        return bookList;
    }

    public bool InsertBook(BookModel book)
    {
        int rowsAffected = 0;

        MySqlConnection connection = new MySqlConnection(DBMConnection.ConnectionString);

        string consult =
        @"
            INSERT INTO books
            (
                author_id
                ,title
                ,year
                ,language
                ,cover_url
                ,price
                ,sellable
                ,copies
                ,description
                ,created_at
                ,updated_at
                ,active
            )
            VALUES
            (
                @author_id -- author_id - INT NOT NULL
                ,@title -- title - VARCHAR(100) NOT NULL
                ,@year -- year - INT NOT NULL
                ,@language -- language - VARCHAR(2) NOT NULL
                ,@cover_url -- cover_url - VARCHAR(500)
                ,@price -- price - DOUBLE(6, 2)
                ,@sellable -- sellable - TINYINT NOT NULL
                ,@copies -- copies - INT NOT NULL
                ,@description -- description - TEXT
                ,@active -- active - TINYINT NOT NULL
            );
        ";

        MySqlCommand command = new MySqlCommand(consult, connection);
        command.CommandType = CommandType.Text;

        command.Parameters.AddWithValue("@author_id", book.Author.Author_Id);
        command.Parameters.AddWithValue("@title", book.Title);
        command.Parameters.AddWithValue("@year", book.Year);
        command.Parameters.AddWithValue("@language", book.Language);
        command.Parameters.AddWithValue("@cover_url", book.Cover_url);
        command.Parameters.AddWithValue("@price", book.Price);
        command.Parameters.AddWithValue("@sellable", book.Sellable);
        command.Parameters.AddWithValue("@copies", book.Copies);
        command.Parameters.AddWithValue("@description", book.Description);
        command.Parameters.AddWithValue("@active", 1);

        connection.Open();

        rowsAffected = command.ExecuteNonQuery();

        connection.Close();

        if (rowsAffected > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool UpdateBook(BookModel book)
    {
        int rowsAffected = 0;

        MySqlConnection connection = new MySqlConnection(DBMConnection.ConnectionString);

        string consult =
        @"
            UPDATE 
                books 
            SET
                author_id = @author_id -- author_id - INT NOT NULL
                ,title = @title -- title - VARCHAR(100) NOT NULL
                ,year = @year -- year - INT NOT NULL
                ,language = @language -- language - VARCHAR(2) NOT NULL
                ,cover_url = @cover_url -- cover_url - VARCHAR(500)
                ,price = @price -- price - DOUBLE(6, 2)
                ,sellable = @sellable -- sellable - TINYINT NOT NULL
                ,copies = @copies -- copies - INT NOT NULL
                ,description = @description -- description - TEXT
            WHERE
                book_id = @book_id -- book_id - INT NOT NULL;
        ";

        MySqlCommand command = new MySqlCommand(consult, connection);

        command.Parameters.AddWithValue("@author_id", book.Author.Author_Id);
        command.Parameters.AddWithValue("@title", book.Title);
        command.Parameters.AddWithValue("@year", book.Year);
        command.Parameters.AddWithValue("@language", book.Language);
        command.Parameters.AddWithValue("@cover_url", book.Cover_url);
        command.Parameters.AddWithValue("@price", book.Price);
        command.Parameters.AddWithValue("@sellable", book.Sellable);
        command.Parameters.AddWithValue("@copies", book.Copies);
        command.Parameters.AddWithValue("@description", book.Description);

        command.Parameters.AddWithValue("@book_id", book.Book_Id);

        connection.Open();

        rowsAffected = command.ExecuteNonQuery();

        connection.Close();

        if (rowsAffected > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool DisableBook(int BookID)
    {
        int rowsAffected = 0;

        MySqlConnection connection = new MySqlConnection(DBMConnection.ConnectionString);

        string consult =
        @"
            Update 
                books
            set 
                active = false
            where 
                book_id = @book_id
        ";

        MySqlCommand command = new();

        command.Connection = connection;
        command.CommandText = consult;
        command.CommandType = CommandType.Text;

        command.Parameters.AddWithValue("@book_id", BookID);

        connection.Open();

        rowsAffected = command.ExecuteNonQuery();

        connection.Close();

        if (rowsAffected > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public DTOModel GetBookByBookID(int? Book_Id)
    {
        DTOModel dto = new();

        String consult =
        @"
            SELECT
                books.book_id AS book_id,
                books.author_id AS author_id,
                authors.name AS author_name,
                authors.nationality AS author_nationality,
                authors.created_at AS author_created_at,
                authors.updated_at AS author_updated_at,
                authors.active AS author_active,
                books.title AS title,
                books.year AS year,
                books.language AS language,
                books.cover_url AS cover_url,
                books.price AS price,
                books.sellable AS sellable,
                books.copies AS copies,
                books.description AS description,
                books.created_at AS created_at,
                books.updated_at AS updated_at,
                books.active AS active
            FROM 
                books
            INNER JOIN 
                authors
            ON books.book_id = authors.author_id
            WHERE 
                books.active = 1
                    and
                books.book_id = @book_id;
        ";

        MySqlConnection connection = new MySqlConnection(DBMConnection.ConnectionString);

        MySqlCommand command = new MySqlCommand(consult, connection);
        command.CommandType = CommandType.Text;

        command.Parameters.AddWithValue("@book_id", Book_Id);

        connection.Open();

        MySqlDataReader reader;
        reader = command.ExecuteReader();

        while (reader.Read())
        {
//Console.WriteLine(reader.GetInt32(0));

            BookModel book = new();
            book.Book_Id = Convert.IsDBNull(reader.GetValue("book_id")) ? 0 : reader.GetInt32("book_id");

            // Author
            AuthorModel author = new();
            author.Author_Id = Convert.IsDBNull(reader.GetValue("author_id")) ? 0 : reader.GetInt32("author_id");

            author.Name = Convert.IsDBNull(reader.GetValue("author_name")) ? string.Empty : reader.GetString("author_name");
            author.Nationality = Convert.IsDBNull(reader.GetValue("author_nationality")) ? string.Empty : reader.GetString("author_nationality");

            author.Created_at = Convert.IsDBNull(reader.GetValue("author_created_at")) ? DateTime.Now : reader.GetDateTime("author_created_at");
            author.Updated_at = Convert.IsDBNull(reader.GetValue("author_updated_at")) ? DateTime.Now : reader.GetDateTime("author_updated_at");

            book.Title = Convert.IsDBNull(reader.GetValue("title")) ? string.Empty : reader.GetString("title");
            book.Year = Convert.IsDBNull(reader.GetValue("year")) ? 0 : reader.GetInt32("year");
            book.Language = Convert.IsDBNull(reader.GetValue("language")) ? string.Empty : reader.GetString("language");
            book.Cover_url = Convert.IsDBNull(reader.GetValue("cover_url")) ? string.Empty : reader.GetString("cover_url");
            book.Price = Convert.IsDBNull(reader.GetValue("price")) ? 0 : reader.GetDouble("price");
            book.Sellable = Convert.IsDBNull(reader.GetValue("sellable")) ? false : reader.GetBoolean("sellable");
            book.Copies = Convert.IsDBNull(reader.GetValue("copies")) ? 0 : reader.GetInt32("copies");
            book.Description = Convert.IsDBNull(reader.GetValue("description")) ? string.Empty : reader.GetString("description");

            author.Active = Convert.IsDBNull(reader.GetValue("active")) ? false : reader.GetBoolean("active");

            book.Author = author;
            // Author

            book.Created_at = Convert.IsDBNull(reader.GetValue("created_at")) ? DateTime.Now : reader.GetDateTime("created_at");
            book.Updated_at = Convert.IsDBNull(reader.GetValue("updated_at")) ? DateTime.Now : reader.GetDateTime("updated_at");

            book.Active = Convert.IsDBNull(reader.GetValue("active")) ? false : reader.GetBoolean("active");

            dto.Book = book;
        }
        reader.Close();
        connection.Close();

        return dto;
    }

    public int GetLastBookID()
    {
        int lastBookID = 0;

        try
        {
            MySqlConnection connection = new MySqlConnection(DBMConnection.ConnectionString);

            string consult =
            "SELECT " +
                "MAX(book_id) " +
            "FROM Books";

            MySqlCommand command = new MySqlCommand(consult, connection);
            command.CommandType = CommandType.Text;

            connection.Open();

            MySqlDataReader reader;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                //Console.WriteLine(reader.GetInt32(0));

                lastBookID = reader.GetInt32(0);
            }

            reader.Close();
            connection.Close();
        }
        catch (global::System.Exception)
        {
            throw;
        }
        return lastBookID;
    }
}
