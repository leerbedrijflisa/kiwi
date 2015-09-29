$(function () {
    var date = new Date();
    // set date to now minus 28 days
    date = new Date(date.getTime() - 2419000000);

    window.ReportsUrl = window.ApiUrl + "reports?$orderby=Created desc&$filter=(Category ne 'Nuisance' or Contact ne null) and (year(Created) ge " + date.getFullYear() + " and month(Created) ge " + (date.getMonth() + 1) + " and day(Created) ge " + date.getDay() + ")";
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

var firstLoad = true;
var reportCount = 0;

function updateReports(data) {

    var source = $('#reportsTemplate').html();
    var template = Handlebars.compile(source);
    var html = template(data);
    $('#container').html(html);

    if (!firstLoad) {
        if (reportCount == data.length) {
            updatedReport();
        }
        else {
            newReport();
        }
    }

    firstLoad = false;
}

function updatedReport() {
    var audio = new Audio('/Content/update.mp3');
    audio.volume = 0.5;
    audio.play();
}

function newReport() {
    var audio = new Audio('/Content/alert.mp3');
    audio.play();
}

//#region Handlebars helpers
Handlebars.registerHelper('detailsSummary', function (report) {

    var result;
    switch (report.category) {
        case "FirstAid":
            if (report.isUnconscious) {
                result = "Het slachtoffer is bewusteloos";
            } else {
                result = "Het slachtoffer is niet bewusteloos";
            }
            break;
        case "Fight":
            if (report.fighterCount == null)
            {
                report.fighterCount = "een onbekende hoeveelheid";
            }
            result = "Er zijn " + report.fighterCount + " personen aan het vechten.";
            break;
        case "Theft":
            if (report.stolenObject == null)
            {
                report.stolenObject = "een onbekend voorwerp";
            }
            result = "Er is " + report.stolenObject + " gestolen";
            break;
        case "Weapons":
            if (report.weaponType == null)
            {
                report.weaponType = "een onbekend wapen";
            }
            result = "Het gaat om een " + report.weaponType;
            break;
        default:
            result = report.description || "";
            break;
    }

    reportCount = $('section').length;

    return new Handlebars.SafeString(result);
});

Handlebars.registerHelper('translate', translate);

Handlebars.registerHelper('prettyDate', function (date) {
    var today = new Date(),
        todayDate = today.getDate() + "-" + (today.getMonth() + 1) + "-" + today.getFullYear(),
        date = new Date(date),
        dateDate = date.getDate() + "-" + (date.getMonth() + 1) + "-" + date.getFullYear(),
        dateTime = date.getHours() + ':' + date.getMinutes();

    return todayDate == dateDate ? dateTime : dateDate;
});
//#endregion

// #region Extentions
function translate(category, key) {
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
}

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