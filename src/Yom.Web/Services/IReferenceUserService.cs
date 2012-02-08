using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Yom.Web.Models.User;
using Yom.Lib.Data.EF;

namespace Yom.Web.Services
{
    public interface IReferenceUserService
    {
        OperationResult Create(string mail, string lastName, string firstName);

        IEnumerable<ReferenceUser> Get();
    }
}
