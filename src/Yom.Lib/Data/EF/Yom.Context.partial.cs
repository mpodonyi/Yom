using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Yom.Lib.Data.EF
{
    public partial class YomContainer 
    {
        public YomContainer(string connectionString)
            : base(connectionString)
        {
        }
    }
}
