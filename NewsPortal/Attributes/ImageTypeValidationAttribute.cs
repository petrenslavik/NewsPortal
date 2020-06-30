using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsPortal.Attributes
{
    public class ImageTypeValidationAttribute : ValidationAttribute, IClientValidatable
    {
        public string[] imageTypes;

        public ImageTypeValidationAttribute(params string[] types)
        {
            imageTypes = types;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "imagetype",
            };
            string types = ""; ;
            for(int i=0; i<imageTypes.Length ;i++)
            {
                if (types != "")
                    types += "+" + imageTypes[i];
                else
                    types = imageTypes[i];
            }
            rule.ValidationParameters.Add("types", types);
            yield return rule;
        }

        public override bool IsValid(object value)
        {
            if(value==null)
                return true;
            HttpPostedFileBase file = value as HttpPostedFileBase;
            foreach (string type in imageTypes)
            {
                if (file.ContentType == "image/" + type)
                    return true;
            }
            return false;
        }
    }
}