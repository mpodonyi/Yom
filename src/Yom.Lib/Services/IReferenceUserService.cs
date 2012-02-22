using System.Collections.Generic;
using Yom.Lib.Data.EF;

namespace Yom.Lib.Services
{
    public interface IReferenceUserService
    {
        OperationResult Create(string mail, string lastName, string firstName);

        IEnumerable<ReferenceUser> Get();
    }
}
