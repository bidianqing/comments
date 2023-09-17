using comments.Entities;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;
using OneAspNet.Repository.Dapper;

namespace comments.Controllers
{
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly ILogger<PostController> _logger;

        public PostController(ILogger<PostController> logger, ConnectionFactory connectionFactory)
        {
            _logger = logger;
            _connectionFactory = connectionFactory;
        }

        [HttpGet]
        [Route("posts")]
        public async Task<IEnumerable<Post>> GetPosts()
        {
            var connection = _connectionFactory.CreateConnection();
            return await connection.GetAllAsync<Post>();
        }

        [HttpPost]
        [Route("posts")]
        public async Task<int> AddPost([FromBody] Post post)
        {
            post.CreateTime = DateTime.Now;
            var connection = _connectionFactory.CreateConnection();
            return await connection.InsertAsync(post);
        }

        [HttpPost]
        [Route("post/comments")]
        public async Task<int> AddComment([FromBody] Comment comment)
        {
            comment.CreateTime = DateTime.Now;
            var connection = _connectionFactory.CreateConnection();
            var post = await connection.GetAsync<Post>(comment.PostId);
            if (post == null)
            {
                return -1;
            }

            return await connection.InsertAsync(comment);
        }

        [HttpGet]
        [Route("post/comments")]
        public async Task<IEnumerable<Comment>> AddComment([FromQuery] int postId)
        {
            var connection = _connectionFactory.CreateConnection();
            var post = await connection.GetAsync<Post>(postId);
            if (post == null)
            {
                return Enumerable.Empty<Comment>();
            }

            string sql = "select * from tb_commnet where PostId = @PostId";

            return await connection.QueryAsync<Comment>(sql, new { postId });
        }
    }
}