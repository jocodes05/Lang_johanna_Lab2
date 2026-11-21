using System.ComponentModel.DataAnnotations;

namespace Lang_johanna_Lab2.Models
{
    public class Publisher
    {

        public int ID { get; set; }
        public string PublisherName { get; set; }
        [ScaffoldColumn(false)]
        public ICollection<Book>? Books { get; set; }
    }
}
