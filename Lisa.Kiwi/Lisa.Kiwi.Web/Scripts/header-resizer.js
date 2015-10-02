var headerElement = document.getElementById("header");

window.addEventListener("resize", function (e) {
    headerElement.style.height = headerElement.offsetWidth / 4.84;
});

headerElement.onload = headerElement.style.height = headerElement.offsetWidth / 4.84;