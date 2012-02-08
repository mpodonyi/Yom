using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Yom.Web.Models.Share
{
    public class ShareCreateModel: IValidatableObject
    {
        public long BatchId { get; set; }

        public string Description { get; set; }

        public IEnumerable<ShareItemCreateModel> ShareItemCreateModels { get; set; }

        #region IValidatableObject Members

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ShareItemCreateModels == null || ShareItemCreateModels.Count() < 2)
            {
                yield return new ValidationResult("ShareItemCreate is null or less than 2");
            }
            else
            { 
                if(!ShareItemCreateModels.Any(i=>i.Amount>0))
                    yield return new ValidationResult("Value of one must be higher than 0");
            }
        }

        #endregion
    }
}