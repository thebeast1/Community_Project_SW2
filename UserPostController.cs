using CommuntiyApiDemo.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CommuntiyApiDemo.Controllers
{
    public class UserPostController : ApiController
    {
        private UserPost Upost;

        static string cs = ConfigurationManager.ConnectionStrings["BDCS"].ConnectionString;
        SqlConnection con = new SqlConnection(cs);
        SqlCommand c;
        SqlDataReader reader;
        public UserPostController() { }


        private void getPosts(List<UserPost> list, SqlDataReader reader)
        {
            while (reader.Read())
            {
                int id = Int32.Parse(reader[0].ToString());
                int Uid = Int32.Parse(reader[1].ToString());
                int privacy = Int32.Parse(reader[3].ToString());
                int upVote = Int32.Parse(reader[4].ToString());
                int downVote = Int32.Parse(reader[5].ToString());

                Upost = new UserPost(id, Uid, upVote, downVote,
                    reader[2].ToString(), reader[6].ToString(),
                    reader[7].ToString(), privacy);

                list.Add(Upost);
            }
            reader.Close();
        }

        //RETURN ALL USER POSTS
        [HttpGet, Route("Community/v2/UserPosts")]
        public IHttpActionResult getUserPosts()
        {
            List<UserPost> list = new List<UserPost>();
            String query = "select * from Userposts;";
            c = new SqlCommand(query, con);
            con.Open();
            reader = c.ExecuteReader();

            try
            {
                getPosts(list, reader);
                con.Close();

                return Ok(list);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        //RETURN ALL following USER POSTS
        [HttpGet, Route("Community/v2/FollowingUserPosts")]
        public IHttpActionResult getFollowingUserPosts(int UID)
        {
            List<UserPost> list = new List<UserPost>();
            String query2 = "select * from Userposts where userID IN(" +
                "select FollowingID from FollowUser where UserID = '" + UID+"');";
          
            c = new SqlCommand(query2, con);
            con.Open();
            reader = c.ExecuteReader();
            try
            {
                getPosts(list, reader);
                con.Close();

                return Ok(list);
            }
            catch (Exception e)
            {
                return NotFound();
            }

        }

        //ADDING USER POST TO THE DB
        [HttpPost, Route("Community/v2/AddUserPost")]
        public IHttpActionResult postVacancyPosts([FromBody] UserPost uPost)
        {
            try
            {
                String query = " insert into Userposts(userID,text,privacy,Video,picture,upVote,downVote)" +
                    "VALUES(" + uPost.userID + ",'" + uPost.text + "','" + uPost.privacy +"','" +uPost.Video+"','" + uPost.picture + "','" + uPost.upVote+ "','" + uPost.downVote + "'); ";
                c = new SqlCommand(query, con);
                con.Open();
                c.ExecuteNonQuery();
                con.Close();
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }


        //UPDATE UPVOTE
        [HttpPut, Route("Community/v2/putUpVote")]
        public IHttpActionResult putUpvote([FromUri] int id)
        {
            try
            {
                String query = "UPDATE Userposts SET upVote = upVote+1 where P_ID ='" + id + "';";
                con.Open();
                c = new SqlCommand(query, con);
                c.ExecuteNonQuery();
                con.Close();
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        //UPDATE DOWNVOTE
        [HttpPut, Route("Community/v2/putDownVote")]
        public IHttpActionResult putDownvote([FromUri] int id)
        {
            try
            {
                String query = "UPDATE Userposts SET downVote = downVote+1 where P_ID ='" + id + "';";
                con.Open();
                c = new SqlCommand(query, con);
                c.ExecuteNonQuery();
                con.Close();
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
        //UPDATE PRIVACY
        [HttpPut, Route("Community/v2/putPrivacy")]
        public IHttpActionResult putPrivacy([FromUri] int id)
        {
            try
            {
                String query = "update Userposts set privacy = " +
                    "(case when privacy =1  then 0  when privacy =0 then 1 end)where P_ID = '"+id+"'; ";
                con.Open();
                c = new SqlCommand(query, con);
                c.ExecuteNonQuery();
                con.Close();
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
        //DELETE USER POST
        [HttpDelete, Route("Community/v2/DeleteUserPost")]
        public IHttpActionResult deleteVacancyPosts([FromUri] int id)
        {
            try
            {
                String query = "delete from Userposts where P_ID = " + id + "; ";
                c = new SqlCommand(query, con);
                con.Open();
                c.ExecuteNonQuery();
                con.Close();
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
    }
}
