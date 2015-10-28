$(function () {
    $("#Sub").hide();
    $("#Type").change(function () {
        if ($(this).val() === "Anders") {
            $("#Sub").show();
            postResizeMessage();
        } else {
            $("#Sub").hide();
            postResizeMessage();
        }
    });
});