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
        public IEnumerable<Post> RecentPosts { get; private set; }

        public SidebarModel(IEnumerable<Post> recentPosts)
        {
            RecentPosts = recentPosts;
        }

        public IEnumerable<ArchiveModel> Archives
        {
            get
            {
                yield return new ArchiveModel(DateTime.Now);
                yield return new ArchiveModel(DateTime.Now.AddMonths(-1));
                yield return new ArchiveModel(DateTime.Now.AddMonths(-2));
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