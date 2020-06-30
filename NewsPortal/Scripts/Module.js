var appModule = (function () {

    return {
        AjaxDeleteNewsItem: function (url, idNews, parent) {
            $.ajax({
                url: url,
                type: "POST",
                data: { id: idNews },
                success: function () {
                    parent.remove();
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert(errorThrown);
                },
            });
        },
        AjaxCommentsPaging: function (url, newsId, currentPageNumber, parentElem) {
            $.ajax({
                url: url,
                type: "POST",
                data: { newsId: newsId, currentPageNumber: currentPageNumber },
                success: function (newComments) {
                    $(parentElem).empty();
                    $(parentElem).html(newComments);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert(errorThrown);
                },
            });
        },
        AjaxPreviewNewsItem: function (url, formData) {
            $.ajax({
                url: url,
                type: 'POST',
                data: formData,
                processData: false,
                contentType: false,
                success: function (result) {
                    var newWindow = window.open('', '_blank');
                    newWindow.document.write(result);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert(errorThrown);
                },
            });
        },
        PreviewPicture: function () {
            var file = $("#uploadPicture").prop("files")[0];
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#previewPicture').attr('src', e.target.result);
            };
            reader.readAsDataURL(file);
        }
    }
})();


