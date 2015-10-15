var perpetrators = [],
    vehicles = [],
    editing = false,
    editingIndex;

$(document).ready(function () {
    // if the vehicles input already has data in it, print vehicles to the page
    if (typeof $("input[name=Vehicles") != "undefined" && $("input[name=Vehicles]").val() != "") {
        vehicles = JSON.parse($("input[name=Vehicles]").val());

        setVehiclesText();
    }

    // if the perpetrators input has data in it, print the perpetrators to the page
    if (typeof $("input[name=Perpetrators") != "undefined" && $("input[name=Perpetrators]").val() != "") {
        perpetrators = JSON.parse($("input[name=Perpetrators]").val());

        setPerpetratorsText();
    }
});

function setVehiclesText() {
    $("#vehicleData").html("");

    for (var i = 0; i < vehicles.length; i++) {
        var vehicle = vehicles[i],
            vehicleType,
            text = "";

        switch (vehicle.Type) {
            case "Car":
                vehicleType = "Auto";
                break;
            case "Moped":
                vehicleType = "Brommer";
                break;
            case "MotorCycle":
                vehicleType = "Motor";
                break;
            case "BiCycle":
                vehicleType = "Fiets";
                break;
            default:
                vehicleType = "Onbekend";
                break;
        }

        if (vehicle.NumberPlate != "") {
            text += vehicleType + ", " + vehicle.NumberPlate;
        } else {
            if (vehicle.Brand != "") {
                text += ", " + vehicle.Brand;
            }

            if (vehicle.Color != "") {
                text += ", " + vehicle.Color;
            }
        }

        text += "<button class='action' type='button' onclick='removeVehicle(" + i + ")'>verwijderen</button>";
        text += "<button class='action' type='button' onclick='editVehicle(" + i + ")'>aanpassen</button>";

        $("#vehicleData").append("<li>" + text + "</li>");
    }
}

function setPerpetratorsText() {
    $("#perpetratorData").html("");

    for (var i = 0; i < perpetrators.length; i++) {
        var perpetrator = perpetrators[i],
            skinColorTrans = "Blank",
            sex = " Onbekend",
            text = perpetrator.Name;

        if (perpetrator.SkinColor === "Tanned") {
            skinColorTrans = "Licht getint";
        } else if (perpetrator.SkinColor === "Dark") {
            skinColorTrans = "Donker";
        }

        if (perpetrator.Sex === "Unknown") {
            sex = "Onbekend";
        } else if (perpetrator.Sex === "Male") {
            sex = "Man";
        } else if (perpetrator.Sex === "Female") {
            sex = "Vrouw";
        }

        if (perpetrator.Name === "") {
            text = sex + ", " + skinColorTrans + ", " + perpetrator.AgeRange;
        }

        text += "<button class='action' type='button' onclick='removePerpetrator(" + i + ")'>verwijderen</button>";
        text += "<button class='action' type='button' onclick='editPerpetrator(" + i + ")'>aanpassen</button>";

        $("#perpetratorData").append("<li>" + text + "</li>");
    }
}

// show the popup with id: e
function popUpShow(e) {
    $( ".overlay" ).show();
    $( "#" + e + "" ).show();
};

function popUpHide(e) {
    $("#" + e + " fieldset select").prop('selectedIndex', 0);
    $("#" + e + " fieldset").children("input, textarea").val(null);
    $(".overlay").hide();
    $("#" + e + "").hide();
    editing = false;
}

function popUpReport(e, a) {
    switch (e) {
        case "perpetrator":
            switch (a) {
                case "confirm":
                    if (editing) {
                        perpetrators[editingIndex] = {
                            Id: perpetrators[editingIndex].Id,
                            Name: $("#Name").val(),
                            Sex: $("#Sex").val(),
                            SkinColor: $("#SkinColor").val(),
                            AgeRange: $("#AgeRange").val(),
                            Clothing: $("#Clothing").val(),
                            UniqueProperties: $("#UniqueProperties").val()
                        };

                        editing = false;
                    } else {
                        // create a perpetrator from input values
                        var perpetrator = {
                            Id: perpetrators.length,
                            Name: $("#Name").val(),
                            Sex: $("#Sex").val(),
                            SkinColor: $("#SkinColor").val(),
                            AgeRange: $("#AgeRange").val(),
                            Clothing: $("#Clothing").val(),
                            UniqueProperties: $("#UniqueProperties").val()
                        };

                        // add perpetrator to array
                        perpetrators.splice(perpetrators.length, 0, perpetrator);
                    }

                    // put the new JSON in the perpetrators input field
                    $("input[name=Perpetrators]").val(JSON.stringify(perpetrators));

                    // display new perpetrators text
                    setPerpetratorsText();

                    popUpHide(e);

                    break;
            }
            break;

        case "vehicle":
            switch (a) {
                case "confirm":
                    if (editing) {
                        vehicles[editingIndex] = {
                            Id: vehicles[editingIndex].Id,
                            Type: $("#VehicleType").val(),
                            Brand: $("#Brand").val(),
                            NumberPlate: $("#NumberPlate").val(),
                            Color: $("#Color").val(),
                            AdditionalFeatures: $("#AdditionalFeatures").val()
                        };

                        editing = false;
                    } else {
                        var vehicle = {
                            Id: vehicles.length,
                            Type: $("#VehicleType").val(),
                            Brand: $("#Brand").val(),
                            NumberPlate: $("#NumberPlate").val(),
                            Color: $("#Color").val(),
                            AdditionalFeatures: $("#AdditionalFeatures").val()
                        };

                        vehicles.splice(vehicles.length, 0, vehicle);
                    }

                    $("input[name=Vehicles]").val(JSON.stringify(vehicles));

                    setVehiclesText();

                    popUpHide(e);

                    break;
            }
            break;
    }
    
}

function removeVehicle(i) {
    // remove element at position i from array of vehicles
    vehicles.splice(i, 1);

    // put the json again in the input field
    $("input[name=Vehicles]").val(JSON.stringify(vehicles));

    // print vehicles in array
    setVehiclesText();
}

function editVehicle(i) {
    // get vehicle from array
    var vehicle = vehicles[i];

    // set input values from vehicle
    $("select[name=VehicleType]").val(vehicle.Type);
    $("input[name=Brand]").val(vehicle.Brand);
    $("input[name=NumberPlate]").val(vehicle.NumberPlate);
    $("input[name=Color]").val(vehicle.Color);
    $("textarea[name=AdditionalFeatures]").val(vehicle.AdditionalFeatures);

    editing = true;
    editingIndex = i;

    // show the popup
    popUpShow("vehicle");
}

function removePerpetrator(i) {
    // remove element at posistion i from array of perpetrators
    perpetrators.splice(i, 1);

    // put the new json in the input
    $("input[name=Perpetrators]").val(JSON.stringify(perpetrators));

    // print text
    setPerpetratorsText();
}

function editPerpetrator(i) {
    // get perpetrator from array
    var perpetrator = perpetrators[i];

    $("input[name=Name]").val(perpetrator.Name);
    $("select[name=Sex]").val(perpetrator.Sex);
    $("select[name=SkinColor]").val(perpetrator.SkinColor);
    $("select[name=AgeRange]").val(perpetrator.AgeRange);
    $("textarea[name=Clothing]").val(perpetrator.Clothing);
    $("textarea[name=UniqueProperties]").val(perpetrator.UniqueProperties);

    editing = true;
    editingIndex = i;

    popUpShow("perpetrator");
}