using Library_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;

namespace Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class librarycontroller : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public librarycontroller(IConfiguration configuration)
        {
            _configuration = configuration;
        }  
        
        [HttpGet]
        public JsonResult Get()
        {
            string query =@"
             select authors.name as ""Author_name"", authors.DOB as ""Author_DOB"", 
                books.title as ""Book_Title"", books.DOP as ""Book_DOP""
             from authors
            join relation on relation.author = authors.name 
            join books on relation.book = books.title
            order by authors.name;
             ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("LibraryCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(authors_books authors_books)
        {
            string query = @"
            insert into books(title, DOP) 
                select @title, @DOP
                where not exists (
                    select 1 from books where title=@title and DOP=@DOP
                );
            insert into authors(name, DOB) 
                select @name, @DOB
                where not exists (
                    select 1 from authors where name=@name and DOB=@DOB
                );
            insert into relation(author,book)
                select @name, @title
                where not exists (
                    select 1 from relation where book=@title and author=@name
                );
            
        ";

            string sqlDataSource = _configuration.GetConnectionString("LibraryCon");
            NpgsqlDataReader myReader;

            DataTable table = new DataTable();
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DOP", authors_books.DOP);
                    myCommand.Parameters.AddWithValue("@DOB", authors_books.DOB);
                    myCommand.Parameters.AddWithValue("@name", authors_books.name);
                    myCommand.Parameters.AddWithValue("@title", authors_books.title);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("Added Sucessfully");
        }

        [HttpPut("Updating DOB")]
        public JsonResult Put1(string Author_Name, string DOB)
        {
            string query = @"

            update authors 
            set DOB=@DOB 
            where name = (@name);
            
        ";

            string sqlDataSource = _configuration.GetConnectionString("LibraryCon");
            NpgsqlDataReader myReader;

            DataTable table = new DataTable();
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@name", Author_Name);
                    myCommand.Parameters.AddWithValue("@DOB", DOB);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("Updated Sucessfully");
        }

        [HttpPut("Updating DOP")]
        public JsonResult Put2(string Book_Title, string DOP)
        {
            string query = @"

            update books 
            set DOP=@DOP 
            where title = (@title);
            
        ";

            string sqlDataSource = _configuration.GetConnectionString("LibraryCon");
            NpgsqlDataReader myReader;

            DataTable table = new DataTable();
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@title", Book_Title);
                    myCommand.Parameters.AddWithValue("@DOP", DOP);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("Updated Sucessfully");
        }

        [HttpDelete("Using Book Title")]
        public JsonResult Delete1(string booktitle)
        {
            string query = @"
            delete from books
            where title=(@title) ;

            delete from relation 
            where book = (@title);
            
        ";

            string sqlDataSource = _configuration.GetConnectionString("LibraryCon");
            NpgsqlDataReader myReader;

            DataTable table = new DataTable();
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@title", booktitle);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("Deleted Sucessfully");
        }

        [HttpDelete("Using Author Name")]
        public JsonResult Delete2(string authorname)
        {
            string query = @"
            delete from authors
            where name=(@name) ;

            delete from relation 
            where author = (@name);
            
        ";

            string sqlDataSource = _configuration.GetConnectionString("LibraryCon");
            NpgsqlDataReader myReader;

            DataTable table = new DataTable();
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@name", authorname);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("Deleted Sucessfully");
        }
    }

}
