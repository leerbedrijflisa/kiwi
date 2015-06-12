$(function () {
    var date = new Date();
    // set date to now minus 28 days
    date = new Date(date.getTime() - 2419000000);
    
    window.ReportsUrl = window.ApiUrl + "reports?$orderby=Created desc&$filter=year(Created) ge " + date.getFullYear() + " and month(Created) ge " + (date.getMonth() + 1) + " and day(Created) ge " + date.getDay();
    var reports = $.connection.reportsHub;

    window.update = reports.client.ReportDataChange = function () {
        $.ajax({
            dataType: "json",
            url: window.ReportsUrl,
            headers: {
                'Authorization': 'bearer ' + getCookie('token')
            },
            success: updateReports
        });
    };
    
    update();

    $.connection.hub.url = window.ApiUrl + "signalr";
    $.connection.hub.start();
});

var reportCount,
    firstLoad = true;

function updateReports(data) {
    
    var source = $('#reportsTemplate').html();
    var template = Handlebars.compile(source);
    var html = template(data);
    $('#container').html(html);

    if (!firstLoad) {
        if (reportCount <= data.length) {
            updatedReport();
        }
        else {
            newReport();
        }
    }

    firstLoad = false;
}

function updatedReport() {
    var audio = new Audio('/Content/air-horn.mp3');
    audio.play();
}

function newReport() {
    var audio = new Audio('/Content/air-horn.mp3');
    audio.play();
}

//#region Handlebars helpers
Handlebars.registerHelper('detailsSummary', function (report) {

    var result;
    switch (report.category) {
        case "FirstAid":
            if (report.isUnconscious) {
                result = "<span><img src='/Content/warning.svg'>Het slachtoffer is bewusteloos</span>";
            } else {
                result = "<span>Het slachtoffer is niet bewusteloos</span>";
            }
            break;
        case "Fight":
            result = "<span>Er zijn " + report.fighterCount + " personen aan het vechten.";
            break;
        default:
            result = "<span>De categorie is: " + report.category + "</span>";
            break;
    }

    return new Handlebars.SafeString(result);
});

Handlebars.registerHelper('translate', function (category, key) {
    if (typeof Translations === 'undefined') {
        $.ajax({
            dataType: "json",
            url: '/Resources/dutch',
            async: false,
            success: function (data) {
                window.Translations = data;
           }
        });
    }

    var categoryTranslations = Translations[category];
    if (typeof categoryTranslations === 'undefined') {
        console.warn('Category: "' + category + '" does not exists in the translations');
        return key;
    }

    var translation = categoryTranslations[key];
    if (typeof translation === 'undefined') {
        console.warn('Key: "' + key + '" does not exists in the category "' + category + '" of the translations')
        return key;
    }

    return translation;
});

Handlebars.registerHelper('prettyDate', function (date) {
    var today       = new Date(),
        todayDate   = today.getDate() + "-" + (today.getMonth() + 1) + "-" + today.getFullYear(),
        date        = new Date(date),
        dateDate    = date.getDate() + "-" + (date.getMonth() + 1) + "-" + date.getFullYear(),
        dateTime    = date.getHours() + ':' + date.getMinutes();

    return todayDate == dateDate ? dateTime : dateDate;
});
//#endregion

// #region Extentions
function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1);
        if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
    }
    return "";
}
// #endregion