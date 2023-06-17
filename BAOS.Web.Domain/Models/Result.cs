namespace BAOS.Web.Domain.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int Protocol { get; set; }

        public int RequestId { get; set; }
        public Request Request { get; set; }
    }
}