using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Yom.Web.Models.IPayYou
{
    public class IPayYouCreateModel
    {
        [DataType(DataType.MultilineText)]
        [Required]
        public string Description { get; set; }

        [Range(typeof(decimal),"0.01","10000")]
        public decimal Amount { get; set; }

        public long ReferenceUser { get; set; }
    }
}