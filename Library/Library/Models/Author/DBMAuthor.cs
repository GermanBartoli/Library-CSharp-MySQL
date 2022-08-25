using System.Data;
using Library.Models.DTO;
using MySqlConnector;

namespace Library.Models.Author.DBMAuthor;
public class DBMAuthor
{
    public List<AuthorModel> LoadAuthorList()
    {
        List<AuthorModel> authorList = new List<AuthorModel>();

        MySqlConnection connection = new MySqlConnection(DBMConnection.ConnectionString);

        string consult =
        @"
            SELECT
                authors.author_id AS author_id,
                authors.name AS name,
                authors.nationality AS nationality,
                authors.created_at AS created_at,
                authors.updated_at AS updated_at,
                authors.active AS active
            FROM 
                authors
            WHERE 
                authors.active = 1
        ";

        MySqlCommand command = new MySqlCommand(consult, connection);
        command.CommandType = CommandType.Text;

        connection.Open();

        MySqlDataReader reader;
        reader = command.ExecuteReader();

        while (reader.Read())
        {
            //Console.WriteLine(reader.GetInt32(0));

            AuthorModel author = new();
            author.Author_Id = Convert.IsDBNull(reader.GetValue("author_id")) ? 0 : reader.GetInt32("author_id");

            author.Name = Convert.IsDBNull(reader.GetValue("name")) ? string.Empty : reader.GetString("name");
            author.Nationality = Convert.IsDBNull(reader.GetValue("nationality")) ? string.Empty : reader.GetString("nationality");

            author.Created_at = Convert.IsDBNull(reader.GetValue("created_at")) ? DateTime.Now : reader.GetDateTime("created_at");
            author.Updated_at = Convert.IsDBNull(reader.GetValue("updated_at")) ? DateTime.Now : reader.GetDateTime("updated_at");

            author.Active = Convert.IsDBNull(reader.GetValue("active")) ? false : reader.GetBoolean("active");

            authorList.Add(author);
        }
        reader.Close();
        connection.Close();

        return authorList;
    }

    public bool InsertAuthor(AuthorModel author)
    {
        int rowsAffected = 0;

        MySqlConnection connection = new MySqlConnection(DBMConnection.ConnectionString);

        string consult =
        @"
            INSERT INTO authors
            (
                name
                ,nationality
                ,active
            )
            VALUES
            (
                @name -- name - VARCHAR(100) NOT NULL
                ,@nationality -- nationality - VARCHAR(100)
                ,@active -- active - TINYINT NOT NULL
        );";

        MySqlCommand command = new MySqlCommand(consult, connection);
        command.CommandType = CommandType.Text;

        command.Parameters.AddWithValue("@name", author.Name);
        command.Parameters.AddWithValue("@nationality", author.Nationality);
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

    public bool UpdateAuthor(AuthorModel author)
    {
        int rowsAffected = 0;

        MySqlConnection connection = new MySqlConnection(DBMConnection.ConnectionString);

        string consult =
        @"
            UPDATE authors 
            SET
                name = @name -- name - VARCHAR(100) NOT NULL
                ,nationality = @nationality -- nationality - VARCHAR(100)
                ,active = @active -- active - TINYINT NOT NULL
            WHERE
                author_id = @author_id  -- author_id - INT NOT NULL;
        ";

        MySqlCommand command = new MySqlCommand(consult, connection);

        command.Parameters.AddWithValue("@name", author.Name);
        command.Parameters.AddWithValue("@nationality", author.Nationality);
        command.Parameters.AddWithValue("@active", true);

        command.Parameters.AddWithValue("@author_id", author.Author_Id);

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

    public bool DisableAuthor(int AuthorID)
    {
        int rowsAffected = 0;

        MySqlConnection connection = new MySqlConnection(DBMConnection.ConnectionString);

        string consult =
        @"
            Update 
                authors
            set 
                active = false
            where 
                author_id = @author_id
        ";

        MySqlCommand command = new();

        command.Connection = connection;
        command.CommandText = consult;
        command.CommandType = CommandType.Text;

        command.Parameters.AddWithValue("@author_id", AuthorID);

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

    public DTOModel GetAuthorByAuthorID(int? Author_Id)
    {
        DTOModel dto = new();

        String consult =
        @"
            SELECT
                authors.author_id AS author_id,
                authors.name AS name,
                authors.nationality AS nationality,
                authors.created_at AS created_at,
                authors.updated_at AS updated_at,
                authors.active AS active
            FROM 
                authors
            WHERE 
                authors.active = 1
                    and
                authors.author_id = @author_id;
        ";

        MySqlConnection connection = new MySqlConnection(DBMConnection.ConnectionString);

        MySqlCommand command = new MySqlCommand(consult, connection);
        command.CommandType = CommandType.Text;

        command.Parameters.AddWithValue("@author_id", Author_Id);

        connection.Open();

        MySqlDataReader reader;
        reader = command.ExecuteReader();

        while (reader.Read())
        {
            //Console.WriteLine(Convert.IsDBNull(reader.GetValue("author_id")) ? 0 : reader.GetInt32("author_id"));

            AuthorModel author = new();
            author.Author_Id = Convert.IsDBNull(reader.GetValue("author_id")) ? 0 : reader.GetInt32("author_id");

            author.Name = Convert.IsDBNull(reader.GetValue("name")) ? string.Empty : reader.GetString("name");
            author.Nationality = Convert.IsDBNull(reader.GetValue("nationality")) ? string.Empty : reader.GetString("nationality");

            author.Created_at = Convert.IsDBNull(reader.GetValue("created_at")) ? DateTime.Now : reader.GetDateTime("created_at");
            author.Updated_at = Convert.IsDBNull(reader.GetValue("updated_at")) ? DateTime.Now : reader.GetDateTime("updated_at");

            author.Active = Convert.IsDBNull(reader.GetValue("active")) ? false : reader.GetBoolean("active");

            dto.Author = author;
        }
        reader.Close();
        connection.Close();

        return dto;
    }

    public int GetLastAuthorID()
    {
        int lastAuthorID = 0;

        try
        {
            MySqlConnection connection = new MySqlConnection(DBMConnection.ConnectionString);

            string consult =
            "SELECT " +
                "MAX(author_id) " +
            "FROM Authors";

            MySqlCommand command = new MySqlCommand(consult, connection);
            command.CommandType = CommandType.Text;

            connection.Open();

            MySqlDataReader reader;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                //Console.WriteLine(reader.GetInt32(0));

                lastAuthorID = reader.GetInt32(0);
            }

            reader.Close();
            connection.Close();
        }
        catch (global::System.Exception)
        {
            throw;
        }
        return lastAuthorID;
    }
}
