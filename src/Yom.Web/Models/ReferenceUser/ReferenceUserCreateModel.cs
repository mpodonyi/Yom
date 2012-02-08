using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace Yom.Web.Models.ReferenceUser
{
    public class ReferenceUserCreateModel
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }

        [Required]
        [Email]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }

    }
}