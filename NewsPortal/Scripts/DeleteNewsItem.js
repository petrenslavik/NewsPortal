$("body").on("click", "section.NewsItem>.buttons>.deleteNews", deleteButtonClick);

function deleteButtonClick() {
    if (confirm($(this).data('message'))) {
        var id = ($(this).attr("newsId"));
        var parent = $(this).parents(".NewsItem")[0];
        appModule.AjaxDeleteNewsItem(($(this).data('url')), id, parent);
    }
};


