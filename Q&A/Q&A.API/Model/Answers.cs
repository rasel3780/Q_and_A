namespace Q_A.API.Model
{
    public class Answers
    {
        public int AnswerID { get; set; }
        public int QuestionID { get; set; }
        public string Answer { get; set; }
        public string MakeBy { get; set; }
        public DateTime MakeDate { get; set; }
        public string AnswerAcceptedBy { get; set; }
        public DateTime AnswerAcceptedDate { get; set; }
        public int UserID { get; set; }


    }
}
