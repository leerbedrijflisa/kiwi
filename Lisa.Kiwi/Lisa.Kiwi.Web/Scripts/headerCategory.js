function isNullOrWhitespace(variable) {
    return variable.length === 0
        || !variable.trim()
        || variable === null
        || typeof (variable) === "undefined";
}

if (typeof (Storage) !== "undefined")
{
    if (localStorage.Category !== category && !isNullOrWhitespace(category)) {
        localStorage.Category = category;
    } else {
        // Sorry! No Web Storage support...
    }

    if (!isNullOrWhitespace(localStorage.Category) && !$('#iframe').length) {
        $('legend').first().prepend(localStorage.Category + " - ");
    }
}