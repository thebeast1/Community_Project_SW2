using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Http;
using System.Configuration;
using CommuntiyApiDemo.Entities;

namespace CommuntiyApiDemo.Controllers
{
    public class CommentsController : ApiController
    {
        static string cs = ConfigurationManager.ConnectionStrings["BDCS"].ConnectionString;
        SqlConnection connection = new SqlConnection(cs);

        List<Comment> fieldofinterests = new List<Comment>();

        [HttpGet, Route("Community/v1/getComments")]
        public IHttpActionResult Get()
        {
            try
            {
                Comment comment;
                SqlDataReader dataReader;
                SqlCommand command = new SqlCommand("select * from Comment", connection);
                connection.Open();
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    comment = new Comment()
                    {
                        commentID = Int32.Parse(dataReader[0].ToString()),
                        userID = Int32.Parse(dataReader[1].ToString()),
                        postID = Int32.Parse(dataReader[2].ToString()),
                        text = dataReader[3].ToString(),
                        Video = dataReader[4].ToString(),
                        picture = dataReader[5].ToString()
                    };
                    fieldofinterests.Add(comment);
                }
                connection.Close();
                dataReader.Close();
                return Ok(fieldofinterests);
            }
            catch
            {
                connection.Close();
            }
            return NotFound();
        }



        [HttpGet, Route("Community/v1/getPostComments")]
        public IHttpActionResult GetComments(int postID)
        {
            try
            {
                Comment comment;
                SqlDataReader dataReader;
                SqlCommand command = new SqlCommand("select * from Comment where postID = "+postID+";", connection);
                connection.Open();
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    comment = new Comment()
                    {
                        commentID = Int32.Parse(dataReader[0].ToString()),
                        userID = Int32.Parse(dataReader[1].ToString()),
                        postID = Int32.Parse(dataReader[2].ToString()),
                        text = dataReader[3].ToString(),
                        Video = dataReader[4].ToString(),
                        picture = dataReader[5].ToString()
                    };
                    fieldofinterests.Add(comment);
                }
                connection.Close();
                dataReader.Close();
                return Ok(fieldofinterests);
            }
            catch
            {
                connection.Close();
            }
            return NotFound();
        }



        [HttpPost, Route("Community/v1/addComment")]
        // POST api/values
        public IHttpActionResult Post([FromBody]Comment value)
        {
            try
            {
                Console.WriteLine("hi: " + value.text);

                SqlDataReader dataReader;

                SqlCommand command = new SqlCommand("insert into Comment values (" + value.userID + "," + value.postID + ",'" + value.text + "','" + value.Video + "','" + value.picture + "')", connection);
                connection.Open();
                dataReader = command.ExecuteReader();

                dataReader.Close();
                connection.Close();
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }


        [HttpDelete, Route("Community/v1/deleteComment")]
        public IHttpActionResult Delete(int commentID, int userID, int postID)
        {
            try
            {
                SqlDataReader dataReader;
                //"insert into Comment values (" + value.userID + "," + value.postID + ",'" + value.text + "','" + value.Video + "','" + value.picture + "')"
                SqlCommand command = new SqlCommand("delete from Comment where commentID =" + commentID + "and Comment.userID =" + userID + " and Comment.postID =" + postID, connection);
                connection.Open();
                dataReader = command.ExecuteReader();

                dataReader.Close();
                connection.Close();
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
