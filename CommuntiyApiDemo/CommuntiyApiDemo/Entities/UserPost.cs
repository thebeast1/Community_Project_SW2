using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommuntiyApiDemo.Entities
{
    public class UserPost
    {
        public UserPost(int id, int userid, int upvote, int downvote, String text, String video, String picture, int privacy)
        {
            this.P_ID = id;
            this.userID = userid;
            this.upVote = upvote;
            this.downVote = downvote;
            this.text = text;
            this.Video = video;
            this.picture = picture;
            this.privacy = privacy;
        }


        private int P_ID;
        public int userID;
        public int upVote;
        public int downVote;

        public int privacy;
        public String text;
        public String Video;
        public String picture;
    }
}