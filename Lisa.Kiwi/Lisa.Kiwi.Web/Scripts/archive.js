$(document).ready(function () {
    window.ReportsUrl = ApiUrl + '/reports';
    window.Translations;
    window.DataTable;

    loadReports();
    $('#allData').on('click', 'tr', showDetailsPage);
});

function loadReports() {
    $.ajax({
        dataType: 'json',
        url: ReportsUrl,
        headers: {
            'Authorization': 'bearer ' + getCookie('token')
        },
        success: buildDataTable
    });
}

function buildDataTable(json) {
    DataTable = $('#allData').DataTable({
        sorting: [[0, 'desc']],
        paginate: false,
        data: json,
        columns: getReportColumns(),
        columnDefs: [ {
            targets: '_all',
            searchable: true,
            visible: false,
            defaultContent: 'Onbekend',
            data: 'Onbekend'
        }],
        language: {
            sProcessing: 'Bezig...',
            sLengthMenu: '_MENU_ resultaten weergeven',
            sZeroRecords: 'Geen resultaten gevonden',
            sInfo: '',
            sInfoEmpty: 'Geen resultaten om weer te geven',
            sInfoFiltered: ' (gefilterd uit _MAX_ resultaten)',
            sInfoPostFix: '',
            sSearch: 'Zoeken:',
            sEmptyTable: 'Geen resultaten aanwezig in de tabel',
            sInfoThousands: '.',
            sLoadingRecords: 'Een moment geduld aub - bezig met laden...',
            oPaginate: {
                sFirst: 'Eerste',
                sLast: 'Laatste',
                sNext: 'Volgende',
                sPrevious: 'Vorige'
            }
        }
    });
}

function getReportColumns() {
    return [
        {
            title: 'Datum',
            data: function (report) {
                var date = new Date(report['created']);
                return date.getDate() + '-' + date.getMonth() + '-' + date.getFullYear() + ' ' + date.getHours() + ':' + date.getMinutes();
            },
            visible: true
        },
        {
            title: 'Categorie',
            data: function (r) {
                return translate('Categories', r['category']);
            },
            visible: true
        },
        {
            title: 'Locatie',
            data: function (r) {
                if (r['location'] == undefined) {
                    return 'Onbekend';
                }

                return translate('Buildings', r['location']['building']);
            },
            visible: true
        },
        {
            title: 'Naam Contact',
            data: 'contact.name',
            visible: true
        },
        { data: 'id' },
        { data: 'status' },
        { data: 'description' },
        { data: 'stolenObject' },
        { data: 'weaponType' },
        { data: 'weaponLocation' },
        { data: 'victimName' },
        { data: 'victim' },
        { data: 'location.description' },
        { data: 'contact.name' },
        { data: 'contact.phoneNumber' },
        { data: 'contact.emailAddress' },
        { data: 'perptrator.name' },
        { data: 'perptrator.clothing' },
        { data: 'perptrator.uniqueProperties' },
        { data: 'vehicle.brand' },
        { data: 'vehicle.numberPlate' },
        { data: 'vehicle.color' },
        { data: 'vehicle.additionalFeatures' }
    ];
}

function showDetailsPage() {
    var id = DataTable.row(this).data()['id'];
    window.location = '/dashboard/details?id=' + id;
}

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
    var name = cname + '=';
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1);
        if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
    }
    return '';
}
// #endregion