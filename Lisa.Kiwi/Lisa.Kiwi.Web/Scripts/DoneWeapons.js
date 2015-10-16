$(window).load(function () {
    if($("select[name=WeaponType]").val() == "Anders") {
        $("input[name=WeaponTypeOther]").parent().show();
    }
    else
    {
        $("input[name=WeaponTypeOther]").parent().hide();
        $("input[name=WeaponTypeOther]").val("");
    }
});

$(function() {
    $("select[name=WeaponType]").change(function() {
        if ($(this).val() === "Anders") {
            $("input[name=WeaponTypeOther]").parent().show();
        } else {
            $("input[name=WeaponTypeOther]").parent().hide();
            $("input[name=WeaponTypeOther]").val("");
        }
    });
});