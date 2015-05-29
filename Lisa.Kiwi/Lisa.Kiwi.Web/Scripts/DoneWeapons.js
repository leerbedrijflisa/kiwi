$(window).load(function () {
    if($("#OtherType").val() != "")
    {
        $("#Type").val("Anders");
    }
    else
    {
        $("#Sub").hide();
    }
});

$(function () {
    $("#Type").change(function () {
        if ($(this).val() === "Anders") {
            $("#Sub").show();
        } else {
            $("#Sub").hide();
        }
    });
})