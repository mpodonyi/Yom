

$(document).delegate('div[data-role="page"]', "pageinit", function () {
    jQuery.validator.unobtrusive.parse("form");
});

$(document).delegate('#youOweMeCreate, #iOweYouCreate, ', "pageinit", function () {

    $("#ReferenceUser", this).change(function (event) {
        if ($(this).val() == 'createnewreferenceuser') {
            $.mobile.changePage('/ReferenceUser/Create', {
                data: { ret: document.location.href }
            });
        }

        return false;
    });

});


