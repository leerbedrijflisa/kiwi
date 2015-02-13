<!doctype html>
<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<title>Locatie</title>
		<link rel="stylesheet" type="text/css" href="style/style.css">
		<link href='http://fonts.googleapis.com/css?family=Ubuntu:400,700' rel='stylesheet' type='text/css'>
		<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
		<script src="https://maps.googleapis.com/maps/api/js?libraries=geometry"></script>
		<script src="script/Reporter.js"></script>
		<meta http-equiv="X-UA-Compatible" content="IE=edge">
    	<meta name="viewport" content="width=device-width, initial-scale=1">
	</head>
	<body>
		<form action="location.html" method="post">
			<header id="toolbar">
				<h1>Meldingen Applicatie</h1>
				<div id="process">
					<span>Stap 2 van 3</span>
				</div>
				<div id="attachPicture">
					<input class="overlayInput" type="file" accept="image/*">
				</div>
			</header>
			<section>
				<div id="map-canvas"></div>
				<ul id="map-suggestions"></ul>
				<select name="building" id="buildingSelect"></select>
			</section>
		</form>
	</body>
</html>