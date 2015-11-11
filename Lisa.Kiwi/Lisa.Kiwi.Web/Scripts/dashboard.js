$(function () {
    updateReportData();
    
    $.connection.hub.start();
});

// Get the date of 28 days ago
var maxReportAgeDate = Date.today().addDays(-28).toString("yyyy-MM-dd");

// Get all reports created less than a month ago. Only show nuisance reports when contact details are supplied. Order by youngest report.
var reportsUrl = ApiUrl + "reports?$orderby=Created desc&$filter=(Category ne 'Nuisance' or Contact ne null) and Created ge datetimeoffset'" + maxReportAgeDate + "'";

$.connection.hub.url = ApiUrl + "signalr";

// Create the ReportDataChange method in the signalR hub. This method can be called by the server
$.connection.reportsHub.client.ReportDataChange = updateReportData;

var firstLoad = true;
var reportCount = 0;

// Ajax call to the Web API for an update
function updateReportData() {
    $.ajax({
        dataType: "json",
        url: reportsUrl,
        headers: {
            'Authorization': 'bearer ' + getCookie('token')
        },
        success: updateReports
    });
}

function updateReports(data) {
    var source = $('#reportsTemplate').html();
    var template = Handlebars.compile(source);
    var html = template(data);
    $('#container').html(html);

    if (!firstLoad) {
        if (reportCount === data.length) {
            setTimeout(updatedReport, 1);
        }
        else {
            setTimeout(newReport, 1);
        }
    }

    firstLoad = false;
}

function updatedReport() {
    var audio = new Audio('/Content/update.mp3');
    audio.play();
}

function newReport() {
    var audio = new Audio('/Content/alert.mp3');
    audio.volume = 0.3;
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
            if (report.weaponType === "Pepperspray") {
                result = "Het gaat om " + report.weaponType;
            } else if (report.weaponType === "Anders") {
                result = "Het wapen is zelf ingevoerd namelijk: " + report.weaponTypeOther;
            } else if (report.weaponType === "een onbekend wapen") {
                result = "Het gaat om " + report.weaponType;
            } else {
                result = "Het gaat om een " + report.weaponType;
    }
            
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
    date = new Date(date);

    var today = new Date(),
        todayDate = today.getDate() + "-" + (today.getMonth() + 1) + "-" + today.getFullYear(),
        dateDate = date.getDate() + "-" + (date.getMonth() + 1) + "-" + date.getFullYear(),
        dateTime = date.getHours() + ':' + ((date.getMinutes()
            < 10 ? '0' : '') + date.getMinutes());

    return todayDate === dateDate ? dateTime : dateDate;
});

Handlebars.registerHelper('ifCond', function (v1, v2, options) {
    if (v1 === v2) {
        return options.fn(this);
    }
    return options.inverse(this);
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
        console.warn('Key: "' + key + '" does not exists in the category "' + category + '" of the translations');
        return key;
    }

    return translation;
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') c = c.substring(1);
        if (c.indexOf(name) === 0) return c.substring(name.length, c.length);
    }
    return "";
}
// #endregion