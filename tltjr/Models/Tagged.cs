﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tltjr.Models
{
    public class Tagged
    {
        public IEnumerable<Post> Posts { get; set; }
        public string Tag { get; set; }
    }
}