using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsPortal.Attributes
{
    public class ImageSizeValidationAttribute : ValidationAttribute , IClientValidatable
    {
        public int imgSize;
        public ImageSizeValidationAttribute(int size)
        {
            imgSize = size;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "maxsize",
            };
            rule.ValidationParameters.Add("size", imgSize);
            yield return rule;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;
            HttpPostedFileBase file = value as HttpPostedFileBase;
            int fileSizeInBytes = file.ContentLength;
            if (fileSizeInBytes < imgSize)
                return true;
            return false;
        }
    }
}