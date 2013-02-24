using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tltjr.Models
{
    public class IndexModel 
    {
        public IEnumerable<Post> Posts { get; set; }
        public SidebarModel SidebarModel { get; set; }
    }

    public class PostModel
    {
        public Post Post { get; set; }
        public SidebarModel SidebarModel { get; set; }
    }

    public class SidebarModel
    {
        private IEnumerable<Post> _sortedPosts;

        public SidebarModel(IEnumerable<Post> sortedPosts)
        {
            _sortedPosts = sortedPosts;
        }

        public IEnumerable<Post> RecentPosts { get { return _sortedPosts.Take(3); } }
        public IEnumerable<ArchiveModel> Archives
        {
            get
            {
                List<ArchiveModel> result = new List<ArchiveModel>();
                for (int i = 0; i > -24; i--)
                {
                    var dateTime = DateTime.Now.AddMonths(i);
                    if (_sortedPosts.Any(post => post.CreatedAt.Month == dateTime.Month && post.CreatedAt.Year == dateTime.Year))
                        result.Add(new ArchiveModel(dateTime));
                    if (result.Count == 3)
                        continue;
                }
                return result;
            }
        }
    }

    public class ArchiveModel
    {
        public string Text 
        {
            get { return DateTime.ToString("MMMM") + " " + DateTime.Year; }
        }

        public DateTime DateTime { get; set; }
        public ArchiveModel(DateTime dateTime)
        {
            DateTime = dateTime;
        }
    }
}