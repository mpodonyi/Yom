using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yom.Lib.Data.EF;
using System.Diagnostics;
using System.Configuration;
using System.Data.EntityClient;


namespace Yom.CreateDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            Yom.Lib.Data.Database.Create();
        }
    }
}
