using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yom.Lib.Data.EF;
using System.Data.EntityClient;

namespace Yom.Web.Services
{
    public abstract class BaseService
    {
        protected YomContainer GetYomContainer()
        {
            var connectionstring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["YomContainer"].ConnectionString;

            EntityConnectionStringBuilder builder = new EntityConnectionStringBuilder();
            builder.Metadata="res://*/Data.EF.Yom.csdl|res://*/Data.EF.Yom.ssdl|res://*/Data.EF.Yom.msl";
            builder.Provider="System.Data.SqlClient";
            builder.ProviderConnectionString=connectionstring;

            return new YomContainer(builder.ConnectionString);
        }

    }
}