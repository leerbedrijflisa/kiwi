$(document).ready(function() {
    $("select[name=WeaponType]").change(function() {
        if ($(this).val() === "Anders") {
            $("input[name=WeaponTypeOther]").parent().slideDown(400);
        } else {
            $("input[name=WeaponTypeOther]").parent().slideUp(400);
            $("input[name=WeaponTypeOther]").parent().slideUp(400);
            $("input[name=WeaponTypeOther]").val("");
        }
    });
});