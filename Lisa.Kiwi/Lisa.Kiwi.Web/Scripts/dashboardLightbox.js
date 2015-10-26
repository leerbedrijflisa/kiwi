$('html').click(function (e) {
    if (e.target != $('#lightBox img') && e.target.className !== 'dashboardImage') {
        removeLightbox();
    }
});

$('.dashboardImage').click(function () {
    var thumbnail = $(this);
    var url = thumbnail.data('original');
    displayBackground();
    displayImage(url);
});

function displayBackground() {
    $('#lightBox').show();
};

function displayImage(url) {
    var img = $('#lightBox').children('img');
    img.attr('src', url);
};

function removeLightbox() {
    $('#lightBox').hide();
};