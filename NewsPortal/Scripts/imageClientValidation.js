/// <reference path="jquery-3.2.1.js" />
/// <reference path="jquery.validate.js" />
/// <reference path="jquery.validate.unobtrusive.js" />

if ($.validator && $.validator.unobtrusive) {

    $.validator.unobtrusive.adapters.addSingleVal("imagetype", "types");

    $.validator.unobtrusive.adapters.addSingleVal("maxsize", "size");

    $.validator.addMethod("imagetype", function (value, element, types) {
        if (value)
        {
            var arrOfType = types.split('+');
            for (var i = 0; i < arrOfType.length; i++)
                if (element.files[0].type == "image/" + arrOfType[i])
                    return true;
            return false;
        }
        return true;
    });

    $.validator.addMethod("maxsize", function (value, element, maxSize)
    {
        if (value && element.files[0].size > maxSize)
                return false;
        return true;
    });

}