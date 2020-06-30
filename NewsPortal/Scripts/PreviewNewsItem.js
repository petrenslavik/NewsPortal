$("body").on("click", "#formCreate>.form-horizontal>.form-group>.col-md-1>#previewNews", previewButtonClick);

function previewButtonClick() {
    var formData = new FormData(document.querySelector('#formCreate'));
    formData.set('Text', document.getElementsByTagName("iframe")[0].contentDocument.getElementsByTagName("body")[0].innerHTML);
    appModule.AjaxPreviewNewsItem(($(this).data('url')), formData);
};