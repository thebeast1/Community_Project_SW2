using System;


namespace CommuntiyApiDemo.Entities
{
    public class Vacancypost
    {
      //  public Vacancypost() { }
        public Vacancypost(int id, int userid, int upvote, int downvote, String title, String benefit, String description, String requirements, String type, int salary)
        {
            this.ID = id;
            this.userID = userid;
            this.upVote = upvote;
            this.downVote = downvote;
            this.title = title;
            this.benefit = benefit;
            this.jobDescription = description;
            this.requirement = requirements;
            this.jobType = type;
            this.salary = salary;
        }
        private int ID;
        public int userID;
        public int upVote;
        public int downVote;

        public String title;
        public String benefit;
        public String jobDescription;
        public String requirement;
        public String jobType;
        public int salary;

        public override string ToString()
        {
            return "VID: " + ID + " -userID:  " + userID + " -Title  " + title + " -Benefits:  " + benefit + " -salary: " + salary + " -Jobdecription:  " + jobDescription + " -Requirements:  " + requirement + " -Type:  " + jobType + " -UpVote:  " + upVote + " -DownVote: " + downVote;
        }

    }
}