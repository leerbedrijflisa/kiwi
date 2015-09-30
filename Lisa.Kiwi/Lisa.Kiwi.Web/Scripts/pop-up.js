var perpetrators = [];
var vehicles = [];
var perpCount = -1;
var vehicleCount = -1;

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
                    if (sex === "0") {
                        sex = "Onbekend";
                    } else if (sex === "1") {
                        sex = "Man";
                    } else if (sex === "2") {
                        sex = "Vrouw";
                    }

                    var skincolor = $("#SkinColor").val();
                    if (skincolor === "Light") {
                        var skincolortrans = "Blank";
                    } else if (skincolor === "Tanned") {
                        var skincolortrans = "Licht getint";
                    } else if (skincolor === "Dark") {
                        var skincolortrans = "Donker";
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

                    if (name === "") {
                        name = Sex + ", " + skincolortrans + ", " + agerange;
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

                    if (numberplate != "") {
                        numberplate = ", " + numberplate;
                        var name = type + numberplate;
                    } else {
                        if (brand != "") {
                            brand = ", " + brand;
                        }

                        if (color != "") {
                            color = ", " + color;
                        }
                        var name = type + brand + color;
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