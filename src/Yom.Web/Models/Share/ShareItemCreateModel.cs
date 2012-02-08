using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yom.Web.Models.Share
{
    public class ShareItemCreateModel
    {
        public long UserId { get; set; }

        public decimal Amount { get; set; }

        public bool Enabled { get; set; }

    }
}