//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Yom.Lib.Data.EF
{
    public partial class User
    {
        public User()
        {
            this.ReferenceUsers = new HashSet<ReferenceUser>();
            this.Items = new HashSet<Item>();
        }
    
        public long Id { get; set; }
        public System.Guid ProviderUserKey { get; set; }
    
        public virtual ICollection<ReferenceUser> ReferenceUsers { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
    
}
