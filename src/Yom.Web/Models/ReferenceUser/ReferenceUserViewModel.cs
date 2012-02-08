using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Yom.Web.Models.ReferenceUser
{
    public class ReferenceUserViewModel
    {
        public ReferenceUserViewModel(Yom.Lib.Data.EF.ReferenceUser model)
        {
            Id = model.Id;
            Mail = model.Mail;
            Firstname = model.Firstname;
            Lastname = model.Lastname;
        }

        public long Id { get; private set; }
        public string Mail { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }

        public override string ToString()
        {
            return string.Format("{0} {1} <{2}>",Firstname,Lastname,Mail);
        }
    }
}