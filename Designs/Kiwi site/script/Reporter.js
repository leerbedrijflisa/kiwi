var buildingList = [
	{
	    "name": "Wartburg College",
	    "alt": 4.679397940635681,
	    "lat": 51.801330542575634
	}, {
	    "name": "Azzuro",
	    "alt": 4.683828949928284,
	    "lat": 51.79900173555325
	}, {
	    "name": "Syndion",
	    "alt": 4.683828949928284,
	    "lat": 51.799379926070955
	}, {
	    "name": "Romboutslaan",
	    "alt": 4.683378338813782,
	    "lat": 51.79811927867567
	}, {
	    "name": "Samenwerkingsgebouw",
	    "alt": 4.682798981666565,
	    "lat": 51.79846430146596
	}, {
	    "name": "Drechtstedencollege",
	    "alt": 4.681704640388489,
	    "lat": 51.798484206546384
	}, {
	    "name": "Appartementen",
	    "alt": 4.680696129798889,
	    "lat": 51.799180878825474
	}, {
	    "name": "Brandweerkazerne",
	    "alt": 4.681532979011536,
	    "lat": 51.79763491530391
	}, {
	    "name": "Lilla",
	    "alt": 4.680996537208557,
	    "lat": 51.7982387099403
	}, {
	    "name": "Marrone",
	    "alt": 4.680749773979187,
	    "lat": 51.7980330225656
	}, {
	    "name": "Rosa",
	    "alt": 4.680739045143127,
	    "lat": 51.79775434785147
	}, {
	    "name": "Verde",
	    "alt": 4.6806639432907104,
	    "lat": 51.79754202310376
	}, {
	    "name": "Giallo",
	    "alt": 4.680610299110413,
	    "lat": 51.79737614369895
	}, {
	    "name": "Indaco",
	    "alt": 4.6802884340286255,
	    "lat": 51.7973628733202
	}, {
	    "name": "Bianco",
	    "alt": 4.680127501487732,
	    "lat": 51.79819226448608
	}, {
	    "name": "Ocra",
	    "alt": 4.679741263389587,
	    "lat": 51.797774253245315
	}, {
	    "name": "Arcobleno",
	    "alt": 4.679580330848694,
	    "lat": 51.79809273835169
	}, {
	    "name": "Celeste",
	    "alt": 4.679784178733826,
	    "lat": 51.798424491278745
	}, {
	    "name": "Duurzaamheidsfabriek",
	    "alt": 4.679387211799622,
	    "lat": 51.79735623812936
	}, {
	    "name": "Parkeerplaats Brandweerkazerne",
	    "alt": 4.681801199913025,
	    "lat": 51.79740931962874
	}, {
	    "name": "Parkeerplaats Ocra",
	    "alt": 4.678518176078796,
	    "lat": 51.7980330225656
	}, {
	    "name": "Schippers Internaat",
	    "alt": 4.678046107292175,
	    "lat": 51.79689177234445
	}, {
	    "name": "Sporthal",
	    "alt": 4.679022431373596,
	    "lat": 51.79872970181585
	}, {
	    "name": "Bogermanschool",
	    "alt": 4.679376482963562,
	    "lat": 51.80072678934213
	}, {
	    "name": "Parkeerplaats Duurzaamheidsfabriek",
	    "alt": 4.680191874504089,
	    "lat": 51.79716381717036
	}],
	markersArray = [],
	infoWindow;


$(function(){
	$('#attachPicture input').change(function(){
		if (this.files && this.files[0]) {
			$('#attachPicture').css('background-image', 'url("images/Picture-Ok-Icon.png")');
		}
		else{
			$('#attachPicture').css('background-image', 'url("images/Camera-Icon.png")');			
		}
	});

	var mapOptions = {
		zoom: 17,
		minZoom: 15
		
	};
	
	window.map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);

	// Try HTML5 geolocation
	if(navigator.geolocation) {
		navigator.geolocation.getCurrentPosition(function(position) {
			var pos = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);

			infoWindow = new google.maps.InfoWindow({
				map: map,
				position: pos,
				content: 'Hier bevind je je nu'
			});

			map.setCenter(pos);
		}, function() {
			handleNoGeolocation(true);
		});
	} 
	else {
		// Browser doesn't support Geolocation
		handleNoGeolocation(false);
	}

	google.maps.event.addListener(map, "click", function(event)
    {
        // place a marker
        placeMarker(event.latLng);

        //console.log(event.latLng.lat(), event.latLng.lng());
    });
});

function handleNoGeolocation(errorFlag) {
	var content = 'Het ophalen van locatie is mislukt. Selecteer het dichtsbijzijnde gebouw.';

	var options = {
		map: map,
		position: new google.maps.LatLng(51.798265236388616, 4.680490493774441),
		content: content
	};

	infowindow = new google.maps.InfoWindow(options);
	map.setCenter(options.position);
}

function placeMarker(location) {
    // first remove all markers if there are any
    deleteOverlays();

    var nearestLocationArray = getNearestLocations(location),
    	nearestLocation = nearestLocationArray[0];

    var marker = new google.maps.Marker({
        position: nearestLocation.latLng, 
        map: map
    });

    map.panTo(nearestLocation.latLng);
    markersArray.push(marker);

    setSuggestions(nearestLocationArray);
}

function getNearestLocations(latLng) {
	var sortedArray = [];
	for (var i = buildingList.length - 1; i >= 0; i--) {
		var building = buildingList[i];

		building.id = i;
		building.latLng = new google.maps.LatLng(building.lat, building.alt);
		building.distanceBetween = google.maps.geometry.spherical.computeDistanceBetween(latLng, building.latLng);

		sortedArray.push(building);
	};

	return sortedArray.sort(distanceBetweenComparer).slice(0, 3);		
}

function distanceBetweenComparer(a, b) {
	if (a.distanceBetween < b.distanceBetween)
	   return -1;
	if (a.distanceBetween > b.distanceBetween)
	    return 1;
	return 0;
}

// Deletes all markers in the array by removing references to them
function deleteOverlays() {
	if(infoWindow){
		infoWindow.close();
		infowindow = new google.maps.InfoWindow();
	}

    if (markersArray) {
        for (i in markersArray) {
            markersArray[i].setMap(null);
        }
    	markersArray.length = 0;
    }
}

function setSuggestions(suggestionArray) {
	var $suggestions = $('#suggestions').empty();

	for (var i = 0; i < suggestionArray.length; i++) {
		var listItem = $("<button></button>")
			.text(suggestionArray[i].name)
			.val(suggestionArray[i].id);

		console.log(suggestionArray[i].id);
		$suggestions.append(listItem);
	};

	$suggestions.find('button').click(function(e){
		//e.preventDefault();
		alert("sda");


		var buildingId = $(this).val(),
			building = buildingList[buildingId];
console.log($(this));
			var buildingLocation = google.maps.latLng(building.lat, building.alt);

		placeMarker(buildingLocation);
	});
}

