using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
