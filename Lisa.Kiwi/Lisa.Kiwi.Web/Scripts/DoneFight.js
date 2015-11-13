$(document).ready(function () {
    checkWeapon();
    $("select[name=IsWeaponPresent]").attr("onchange", "checkWeapon()");
});

function checkWeapon() {
    if ($("select[name=IsWeaponPresent]").val() === "false") {
        $("select[name=WeaponType]").parent().slideUp(300);
        $("input[name=WeaponTypeOther]").parent().slideUp(300);
        $("textarea[name=WeaponLocation]").parent().slideUp(300);
    } else {
        $("select[name=WeaponType]").parent().slideDown(300);
        $("textarea[name=WeaponLocation]").parent().slideDown(300);

        if ($("select[name=WeaponType]").val() === "Anders") {
            $("input[name=WeaponTypeOther]").parent().slideDown(300);
        }
        else
        {
            $("input[name=WeaponTypeOther]").parent().slideUp(300);
        }
    }
}