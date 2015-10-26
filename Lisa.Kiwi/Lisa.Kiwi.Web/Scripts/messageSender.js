function postContinueMessage() {
    parent.postMessage("continue", "*");
}

function postResizeMessage() {
    parent.postMessage("resize", "*");
}