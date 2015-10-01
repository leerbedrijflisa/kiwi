// Create IE + others compatible event handler
var eventMethod = window.addEventListener ? "addEventListener" : "attachEvent";
var eventer = window[eventMethod];
var messageEvent = eventMethod == "attachEvent" ? "onmessage" : "message";

// Listen to message from child window
eventer(messageEvent, function (e) {
    console.log(e.data);
    if (e.data === "continue") {
        continueNavigation();
    }
    if (e.data === "resize") {
        resizeIFrame();
    }
}, false);

function continueNavigation() {
    $('#iframe').remove();

    // Generate new table row for the added file
    var button = $('<button></button>', {
        type: "submit",
        name: "save",
        onclick: "upload();"
    });

    $('<img />').attr("src", "/Content/done.svg").appendTo(button);

    button.appendTo('#fileUpload');
}

function resizeIFrame() {
    iframe = $('#iframe');
    iframe.height = 0;
    iframe.height = obj.contentWindow.document.body.scrollHeight + 'px';
}