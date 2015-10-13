var perpetrators = [];
var vehicles = [];
var perpCount = -1;
var vehicleCount = -1;

$(document).ready(function() {
    if (typeof $("input[name=Vehicles") != "undefined" && $("input[name=Vehicles]").val() != "") {
        vehicles = JSON.parse($("input[name=Vehicles]").val());
        vehicleCount = vehicles.length - 1;

        vehicles.forEach(function (value) {
            var name;
            if (value.NumberPlate != "") {
                value.NumberPlate = ", " + value.NumberPlate;
                name = value.Type + value.NumberPlate;
            } else {
                if (value.Brand != "") {
                    value.Brand = ", " + value.Brand;
                }

                if (value.Color != "") {
                    value.Color = ", " + value.Color;
                }
                name = value.Type + value.Brand + value.Color;
            }

            $("#vehicleData").append("<li>" + name + "</li>");
        });
    }

    if (typeof $("input[name=Perpetrators") != "undefined" && $("input[name=Perpetrators]").val() != "") {
        perpetrators = JSON.parse($("input[name=Perpetrators]").val());
        perpCount = perpetrators.length - 1;

        perpetrators.forEach(function (value) {
            var name = value.Name;

            if (value.SkinColor === "Light") {
                value.SkinColor = "Blank";
            } else if (value.SkinColor === "Tanned") {
                value.SkinColor = "Licht getint";
            } else if (value.SkinColor === "Dark") {
                value.SkinColor = "Donker";
            }

            if (name === "") {
                name = value.Sex + ", " + value.SkinColor + ", " + value.AgeRange;
            }

            $("#perpetratorData").append("<li>" + name + "</li>");
        });
    }
});



function popUpShow(e) {
    $( ".overlay" ).show();
    $( "#" + e + "" ).show();
};

function popUpReport(e, a) {
    switch (e) {
        case "perpetrator":
            switch (a) {
                case "confirm":
                    perpCount++;
                    var name = $("#Name").val();
                    var sex = $("#Sex").val();
                    

                    var skincolor = $("#SkinColor").val();
                    var skinColorTrans = "Blank";

                    if (skincolor === "Tanned") {
                        skinColorTrans = "Licht getint";
                    } else if (skincolor === "Dark") {
                        skinColorTrans = "Donker";
                    }

                    var agerange = $("#AgeRange").val();
                    
                    var clothing = $("#Clothing").val();
                    var uniqueproperties = $("#UniqueProperties").val();
                    var perpArray = {
                            Id: perpCount,
                            Name: name,
                            Sex: sex,
                            SkinColor: skincolor,
                            AgeRange: agerange,
                            Clothing: clothing,
                            UniqueProperties: uniqueproperties
                    };
                    perpetrators.splice(perpetrators.length, 0, perpArray);

                    $("input[name=Perpetrators]").val(JSON.stringify(perpetrators));

                    if (sex === "Unknown") {
                        sex = "Onbekend";
                    } else if (sex === "Male") {
                        sex = "Man";
                    } else if (sex === "Female") {
                        sex = "Vrouw";
                    }

                    if (name === "") {
                        name = sex + ", " + skinColorTrans + ", " + agerange;
                    }

                    $("#perpetratorData").append("<li>" + name +  "</li>");
                    break;
            }
            break;

        case "vehicle":
            switch (a) {
                case "confirm":
                    vehicleCount++;
                    var type = $("#VehicleType").val();
                    var brand = $("#Brand").val();
                    var numberplate = $("#NumberPlate").val();
                    var color = $("#Color").val();
                    var features = $("#AdditionalFeatures").val();

                    var vehicleArray = {
                        Id: vehicleCount,
                        Type: type,
                        Brand: brand,
                        NumberPlate: numberplate,
                        Color: color,
                        AdditionalFeatures: features
                    };
                    vehicles.splice(vehicles.length, 0, vehicleArray);

                    $("input[name=Vehicles]").val(JSON.stringify(vehicles));

                    switch(type) {
                        case "Car":
                            type = "Auto";
                            break;
                        case "Moped":
                            type = "Brommer";
                            break;
                        case "MotorCycle":
                            type = "Motor";
                            break;
                        case "BiCycle":
                            type = "Fiets";
                            break;
                        default:
                            type = "Onbekend";
                            break;
                    }

                    var name;

                    if (numberplate != "") {
                        numberplate = ", " + numberplate;
                        name = type + numberplate;
                    } else {
                        if (brand != "") {
                            brand = ", " + brand;
                        }

                        if (color != "") {
                            color = ", " + color;
                        }
                        name = type + brand + color;
                    }

                    $("#vehicleData").append("<li>" + name + "</li>");
                    break;
            }
            break;
    }

    $("#" + e + " fieldset select").prop('selectedIndex', 0);
    $("#" + e + " fieldset").children("input, textarea").val(null);
    $(".overlay").hide();
    $("#" + e + "").hide();
}