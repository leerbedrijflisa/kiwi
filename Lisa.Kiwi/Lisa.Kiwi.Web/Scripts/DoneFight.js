function boolChange(element) {
    return element.toLowerCase() == "true";
}

function convertStringToBool(elementVal) {
    var elementBool = boolChange(elementVal);
    if (elementBool) {
        // The value is true
        $(".toggle-weapon-present").show();
    } else {
        // The value is false
        $(".toggle-weapon-present").hide();
    }
}
function close_menu() {
    $(".success_updated_report").hide();
}

window.onload = convertStringToBool($("#IsWeaponPresent").val());
$("#IsWeaponPresent").change(function () { convertStringToBool($(this).val()) });
