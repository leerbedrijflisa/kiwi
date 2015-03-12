$(function () {
    $("#Sub").hide();
    $("#Type").change(function () {
        if ($(this).val() === "Anders") {
            alert("check");
            $("#Sub").show();
        } else {
            alert("check");
            $("#Sub").hide();
        }
    });
})