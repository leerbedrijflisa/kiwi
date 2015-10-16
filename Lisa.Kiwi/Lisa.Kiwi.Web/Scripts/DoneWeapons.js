$(document).ready(function() {
    $("select[name=WeaponType]").change(function() {
        if ($(this).val() === "Anders") {
            $("input[name=WeaponTypeOther]").parent().show();
        } else {
            $("input[name=WeaponTypeOther]").parent().hide();
            $("input[name=WeaponTypeOther]").val("");
        }
    });
});