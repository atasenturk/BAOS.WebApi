namespace BAOS.Web.Domain.Models
{
    public class Request
    {
        public int RequestId { get; set; }
        public string Answers { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public Result Result { get; set; }
    }

}