$(window).load(function () {
    if($("#OtherType").val() != "")
    {
        $("#Type").val("Anders");
    }
    else
    {
        $("#Sub").hide();
    }
});

$(function () {
    $("#Type").change(function () {
        if ($(this).val() === "Anders") {
            $("#Sub").show();
        } else {
            $("#Sub").hide();
        }
    });
})

function toggleWeaponOther() {
    if ($("#WeaponType").val() === "Anders") {
        $(".toggle-weapontype-different").show();
    } else {
        $(".toggle-weapontype-different").hide();
    };
}

window.onload = toggleWeaponOther();
$("#WeaponType").change(function () { toggleWeaponOther() });