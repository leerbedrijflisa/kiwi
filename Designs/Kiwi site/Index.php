<!DOCTYPE html>
<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<title>Index</title>
		<link rel="stylesheet" type="text/css" href="style/style.css">
		<link href='http://fonts.googleapis.com/css?family=Ubuntu:400,700' rel='stylesheet' type='text/css'>
		<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
		<script src="script/Reporter.js"></script>
		<meta http-equiv="X-UA-Compatible" content="IE=edge">
    	<meta name="viewport" content="width=device-width, initial-scale=1">
	</head>
	<body>
		<form action="location.php" method="post">
			<header id="toolbar">
				<h1>Meldingen Applicatie</h1>
				<div id="attachPicture">
					<input class="overlayInput" type="file" accept="image/*">
				</div>
			</header>
		    <section class="icons">
		    	<div>
		    		<img src="images/EHBO-icon.png"/>
		    		<span>EHBO</span>
		    		<input class="overlayInput" type="submit" name="reportType" value="EHBO">
		    	</div>
		    	<div>
		    		<img src="images/Diefstal-icon.png"/>
		    		<span>Diefstal</span>
		    		<input class="overlayInput" type="submit" name="reportType" value="Diefstal">
		    	</div>
		    	<div>
		    		<img src="images/Drugs-icon.png"/>
		    		<span>Drugs</span>
		    		<input class="overlayInput" type="submit" name="reportType" value="Drugs">
		    	</div>
		    	<div>
		    		<img src="images/Geweld-icon.png"/>
		    		<span>Geweld</span>
		    		<input class="overlayInput" type="submit" name="reportType" value="Geweld">
		    	</div>
		    	<div>
		    		<img src="images/Wapens-icon.png"/>
		    		<span>Wapens</span>
		    		<input class="overlayInput" type="submit" name="reportType" value="Wapens">
		    	</div>
		    	<div>
		    		<img src="images/Overlast-icon.png"/>
		    		<span>Overlast</span>
		    		<input class="overlayInput" type="submit" name="reportType" value="Overlast">
		    	</div>
		    	<div>
		    		<img src="images/EHBO-icon.png"/>
		    		<span>Overig</span>
		    		<input class="overlayInput" type="submit" name="reportType" value="Overig">
		    	</div>
		    </section>
	    </form>
	</body>
</html>
