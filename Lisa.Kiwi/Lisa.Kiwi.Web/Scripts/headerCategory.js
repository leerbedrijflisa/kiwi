function isNullOrWhitespace(variable) {
    return variable.length === 0
        || !variable.trim()
        || variable === null
        || typeof (variable) === "undefined";
}

if (typeof (Storage) !== "undefined")
{
    if (localStorage.Category !== category && !isNullOrEmpty(category)) {
        localStorage.Category = category;
    } else {
        // Sorry! No Web Storage support...
    }

    if (!isNullOrEmpty(localStorage.Category) && !$('#iframe').length) {
        $('legend').first().prepend(localStorage.Category + " - ");
    }
}