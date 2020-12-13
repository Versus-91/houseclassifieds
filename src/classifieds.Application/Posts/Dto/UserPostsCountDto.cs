using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Posts.Dto
{
    public class UserPostsCountDto
    {
        public string  UserName { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }

        public long Id { get; set; }
        public int PostsCount { get; set; }

    }
}
