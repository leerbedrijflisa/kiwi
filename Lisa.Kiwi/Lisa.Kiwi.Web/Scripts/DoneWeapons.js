﻿$(document).ready(function() {
    $("select[name=WeaponType]").change(function() {
        if ($(this).val() === "Anders") {
            $("input[name=WeaponTypeOther]").parent().slideDown(300);
        } else {
            $("input[name=WeaponTypeOther]").parent().slideUp(300);
            $("input[name=WeaponTypeOther]").parent().slideUp(300);
            $("input[name=WeaponTypeOther]").val("");
        }
    });
});