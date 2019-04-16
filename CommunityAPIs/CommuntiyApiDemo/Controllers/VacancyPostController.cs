using CommuntiyApiDemo.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;

public class VacancyPostController : ApiController
{

    private Vacancypost Vpost ;
   /* SqlConnection con = new SqlConnection(@"Server=DESKTOP-G8USPBK\SQLEXPRESS; DataBase=Community; Integrated Security=true;");
    SqlConnection con2 = new SqlConnection(@"Server=DESKTOP-G8USPBK\SQLEXPRESS; DataBase=Community; Integrated Security=true;");
    SqlConnection con3 = new SqlConnection(@"Server=DESKTOP-G8USPBK\SQLEXPRESS; DataBase=Community; Integrated Security=true;");*/
    SqlCommand c,c2,c3;
    SqlDataReader reader , reader2 , reader3;
    public VacancyPostController(){}
    static string cs = ConfigurationManager.ConnectionStrings["BDCS"].ConnectionString;
    SqlConnection con = new SqlConnection(cs);
    SqlConnection con2 = new SqlConnection(cs);
    SqlConnection con3 = new SqlConnection(cs);


    //RETURN ALL VACANCY POSTS
    [HttpGet, Route("Community/v1/VacancyPost")]
    public IHttpActionResult getVacancyPosts()
     {
        List<Vacancypost> list = new List<Vacancypost>();
        String query = "select * from Vacancyposts;";
        c = new SqlCommand(query, con);
        con.Open();
        reader = c.ExecuteReader();
    
        try
        {
             while (reader.Read())
             {
                int id = Int32.Parse(reader[0].ToString());
                int Uid = Int32.Parse(reader[1].ToString());
                int salary = Int32.Parse(reader[4].ToString());
                int upVote = Int32.Parse(reader[8].ToString());
                int downVote = Int32.Parse(reader[9].ToString());

                Vpost = new Vacancypost(id, Uid, upVote, downVote,
                    reader[2].ToString(), reader[3].ToString(),
                    reader[5].ToString(), reader[6].ToString(),
                    reader[7].ToString(), salary);

                list.Add(Vpost);
             }
             reader.Close();
             con.Close();
             return Ok(list);
         }
         catch (Exception e)
         {
            return NotFound();
         }

     }

   
    [HttpGet, Route("Community/v1/gVacancyPost")]
    public IHttpActionResult getVacancyPostsOfFollowing(int UserID )
    {
        List<Vacancypost> list2 = new List<Vacancypost>();
        con = new SqlConnection(@"Server=DESKTOP-G8USPBK\SQLEXPRESS; DataBase=Community; Integrated Security=true;");
       List<String> interests = new List<string>(); //from user amangement give him the id and get the interests
        //List<int> FollowingIDs = new List<int>(); //to get posts of the followign users elly ana 3amelohm follow 2a4an a4oof el posts beta3ethom
        interests.Add("hhhhhh"); interests.Add("dedede"); //will be deleted
        String interest = "";
        for(int i=0; i<interests.Count; i++)
        {
            if (i == interests.Count - 1) interest += "'" + interests[i] + "'";
            else interest += "'"+interests[i]+"',";
        }
        String query = "select * from Vacancyposts where jobtype IN("+interest+");";
        c = new SqlCommand(query, con);
        
        con.Open();
        reader = c.ExecuteReader();

       
            while (reader.Read())
            {

            int id = Int32.Parse(reader[0].ToString());
            int Uid = Int32.Parse(reader[1].ToString());
            int salary = Int32.Parse(reader[4].ToString());
            int upVote = Int32.Parse(reader[8].ToString());
            int downVote = Int32.Parse(reader[9].ToString());

            Vpost = new Vacancypost(id, Uid, upVote, downVote,
                reader[2].ToString(), reader[3].ToString(),
                reader[5].ToString(), reader[6].ToString(),
                reader[7].ToString(), salary);
            
            list2.Add(Vpost);
            }
            reader.Close();
            con.Close();
            /*************************************************************************************************************************/
            String query2 = "select FollowingID from FollowUser where UserID = 10;";


       String followingIDS="";
            c3 = new SqlCommand(query2, con3);
            con3.Open();
            reader3 = c3.ExecuteReader();
        try { 
        while (reader3.Read())
        {
            followingIDS += "'"+reader3[0].ToString()+"',";
        }
        reader3.Close();con3.Close();
        followingIDS =  followingIDS.Remove(followingIDS.Length-1);
        
        String Q = "select * from Vacancyposts where userID IN("+followingIDS+ ") and jobType NOT IN (" + interest + ");";
        c2 = new SqlCommand(Q, con2);
        con2.Open();
        reader2 = c2.ExecuteReader();

            while (reader2.Read())
            {

                int id = Int32.Parse(reader[0].ToString());
                int Uid = Int32.Parse(reader[1].ToString());
                int salary = Int32.Parse(reader[4].ToString());
                int upVote = Int32.Parse(reader[8].ToString());
                int downVote = Int32.Parse(reader[9].ToString());

                Vpost = new Vacancypost(id, Uid, upVote, downVote,
                    reader[2].ToString(), reader[3].ToString(),
                    reader[5].ToString(), reader[6].ToString(),
                    reader[7].ToString(), salary);
                
                list2.Add(Vpost);
            }
            reader2.Close();
            con2.Close();
            /*************************************************************************************************************************/
            
            return Ok(list2);
        }
        catch (Exception e)
        {
            return NotFound();
        }
    }

    //RETURN ALL VACANCY POSTS WITH CERTAIN TYPE (INTEREST)
    [HttpGet, Route("Community/v1/searchByInterset")]
    public IHttpActionResult getVacancyPosts(String interest) //for search
    {
        List<Vacancypost> list = new List<Vacancypost>();
        String query = "select * from Vacancyposts where jobtype = "+interest+";";
        c = new SqlCommand(query, con);
        con.Open();
        reader = c.ExecuteReader();

        try
        {
            while (reader.Read())
            {
                int id = Int32.Parse(reader[0].ToString());
                int Uid = Int32.Parse(reader[1].ToString());
                int salary = Int32.Parse(reader[4].ToString());
                int upVote = Int32.Parse(reader[8].ToString());
                int downVote = Int32.Parse(reader[9].ToString());

                Vpost = new Vacancypost(id,Uid,upVote,downVote, 
                    reader[2].ToString(), reader[3].ToString(), 
                    reader[5].ToString(), reader[6].ToString(), 
                    reader[7].ToString(),salary);
                
                list.Add(Vpost);
            }
            reader.Close();
            con.Close();
            return Ok(list);
        }
        catch (Exception e)
        {
            return NotFound();
        }
    }

    //ADDING VACANCY POST TO THE DB
    [HttpPost, Route("Community/v1/AddvacancyPost")]
    public IHttpActionResult postVacancyPosts([FromBody] Vacancypost vPost)
    {
        try
        {
        String query = " INSERT INTO Vacancyposts(userID, title, benefit, salary, jobDescription, requirement, jobType, upVote, downVote)" +
                "VALUES("+vPost.userID+",'"+vPost.title+"','"+ vPost.benefit + "','"+ vPost.salary + "','"+ vPost.jobDescription+ "','"+vPost.requirement+"', '"+vPost.jobType+"','"+vPost.upVote+"', '"+ vPost.downVote + "'); ";
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
    [HttpPut, Route("Community/v1/putUpVote")]
    public IHttpActionResult putUpvote([FromUri] int id)
    {
        try
        {
            String query = "UPDATE Vacancyposts SET upVote = upVote+1 where V_ID ='" + id + "';";
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
    [HttpPut, Route("Community/v1/putDownVote")]
    public IHttpActionResult putDownvote([FromUri] int id)
    {
        try
        {
            String query = "UPDATE Vacancyposts SET downVote = downVote+1 where V_ID ='" +id+"';";
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

    //DELETE VACANCY POST
    [HttpDelete, Route("Community/v1/DeleteVacanyPost")]
    public IHttpActionResult deleteVacancyPosts([FromUri] int id)
    {
        try
        {
            con = new SqlConnection(@"Server=DESKTOP-G8USPBK\SQLEXPRESS; DataBase=Community; Integrated Security=true;");

            String query = "delete from Vacancyposts where V_ID = "+id+"; ";
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
