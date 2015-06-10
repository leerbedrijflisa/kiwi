$( document ).ready(function () {
    $("#bug-report").click(function () {
        var hidden = $(".bug-report-form").is(" :hidden")
        $(".bug-report-form").slideToggle("slow");
        $("#bug-report").animate({
            "top": hidden ? "+=205px" : "-=205px"
        }, "slow");
    });
});