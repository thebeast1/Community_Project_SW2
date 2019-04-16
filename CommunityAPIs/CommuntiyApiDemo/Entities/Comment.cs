using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommuntiyApiDemo.Entities
{
    public class Comment
    {

        public int commentID { get; set; }
        public int userID { get; set; }
        public int postID { get; set; }

        public string text { get; set; }
        public string Video { get; set; }
        public string picture { get; set; }

    }
}