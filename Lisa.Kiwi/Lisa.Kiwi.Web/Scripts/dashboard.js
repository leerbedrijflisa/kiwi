$(function () {
    var reports = $.connection.reportsHub;

    reports.client.ReportDataChange = function () {
        location.reload();
    };

    $.connection.hub.url = window.ApiUrl + "signalr";
    
    $.connection.hub.start();
});