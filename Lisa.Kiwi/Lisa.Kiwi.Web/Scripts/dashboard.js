$(function () {
    var reports = $.connection.reportsHub;

    reports.client.UpdateReport = function (report) {
        console.log(report);
    };
    $.connection.hub.url = window.ApiUrl + "signalr";

    
    $.connection.hub.start().done(function () {
        
    });
});