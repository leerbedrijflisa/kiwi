$("#Sub").hide();
$("#subject").change(function () {
    if ($(this).val() === "Other") {
        $("#Sub").show();
        $("#subject").hide();
    } else {
        $("#Sub").hide();
    }
});