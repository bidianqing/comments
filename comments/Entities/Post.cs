using Dapper.Contrib.Extensions;

namespace comments.Entities
{
    [Table("tb_post")]
    public class Post
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
