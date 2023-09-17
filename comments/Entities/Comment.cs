using Dapper.Contrib.Extensions;

namespace comments.Entities
{
    [Table("tb_commnet")]
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public int PostId { get; set; }

        public string Content { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
