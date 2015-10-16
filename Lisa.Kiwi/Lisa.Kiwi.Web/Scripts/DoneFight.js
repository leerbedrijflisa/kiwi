$(document).ready(function () {
    checkWeapon();

    $("select[name=IsWeaponPresent]").attr("onchange", "checkWeapon()");
});

function checkWeapon() {
    if ($("select[name=IsWeaponPresent]").val() == "false") {
        $("select[name=WeaponType]").parent().hide();
        $("input[name=WeaponTypeOther]").parent().hide();
        $("textarea[name=WeaponLocation]").parent().hide();
    } else {
        $("select[name=WeaponType]").parent().show();
        $("textarea[name=WeaponLocation]").parent().show();

        if ($("select[name=WeaponType]").val() == "Anders") {
            $("input[name=WeaponTypeOther]").parent().show();
        }
    }
}