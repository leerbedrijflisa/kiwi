$(function(){
	$('#attachPicture input').change(function(){
		if (this.files && this.files[0]) {
			$('#attachPicture').css('background-image', 'url("../images/Picture-Ok-Icon.png")');
		}
		else{
			$('#attachPicture').css('background-image', 'url("../images/Camera-Icon.png")');			
		}
	});
});