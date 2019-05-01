using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Http;
using System.Configuration;
using CommuntiyApiDemo.Entities;
using System.Net.Http;
using System.Net;

namespace CommuntiyApiDemo.Controllers
{
    public class FollowersController : ApiController
    {
        static string cs = ConfigurationManager.ConnectionStrings["BDCS"].ConnectionString;
        SqlConnection connection = new SqlConnection(cs);

        List<Follower> fieldofinterests = new List<Follower>();

        [HttpGet, Route("Community/v1/getFollowers")]
        public HttpResponseMessage Get(int UserID)
        {
            try
            {
                Follower follower;
                SqlDataReader dataReader;
                SqlCommand command = new SqlCommand("select * from FollowUser where FollowUser.UserID =" + UserID, connection);
                connection.Open();
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    follower = new Follower()
                    {
                        UserID = Int32.Parse(dataReader[0].ToString()),
                        FollowerID = Int32.Parse(dataReader[1].ToString()),

                    };
                    fieldofinterests.Add(follower);
                }
                connection.Close();
                dataReader.Close();

                if (fieldofinterests[0] == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Can't find user with that id:" + UserID);

                return Request.CreateResponse(HttpStatusCode.OK, fieldofinterests);
            }
            catch
            {
                connection.Close();
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Can't find user with that id:" + UserID);
        }

        [HttpPost, Route("Community/v1/addFollower")]
        // POST api/values
        public IHttpActionResult Post(int UserID, int FollowingID)
        {
            try
            {

                SqlDataReader dataReader;

                SqlCommand command = new SqlCommand("insert into FollowUser values (" + UserID + "," + FollowingID + ")", connection);
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


        [HttpDelete, Route("Community/v1/deleteFollower")]
        public IHttpActionResult Delete(int userID, int followerID)
        {
            try
            {
                SqlDataReader dataReader;
                SqlCommand command = new SqlCommand("delete from FollowUser where FollowUser.UserID=" + userID + "and FollowUser.FollowingID =" + followerID, connection);
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
