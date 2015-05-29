$(function () {
    $("#Sub").hide();
    $("#Type").change(function () {
        if ($(this).val() === "Anders") {
            $("#Sub").show();
        } else {
            $("#Sub").hide();
        }
    });
})

$( document ).ready(function(){
    alert($("#Type").val());
});