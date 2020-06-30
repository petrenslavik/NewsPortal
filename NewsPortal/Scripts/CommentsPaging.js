$("body").on("click", "div.comments>.pages>a.commentPage", commentsPagingButtonClick);

function commentsPagingButtonClick() {
    var currentPageNumber = $(this).text() - 1;
    var newsId = $("div.comments").attr("newsId");
    var parent = $(this).parents('.comments')[0];
    appModule.AjaxCommentsPaging(($(this).data('url')), newsId, currentPageNumber, parent);
};