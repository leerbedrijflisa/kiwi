$(function () {
    $("#Sub").css("display", "none");
    $("#Type").change(function () {
        
        if ($(this).val() === "Other") {
            $("#Sub").css("display", "block");
        } else {
            $("#Sub").css("display", "none");
        }
    });
})