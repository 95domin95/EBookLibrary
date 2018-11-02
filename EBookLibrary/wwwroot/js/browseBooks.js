window.addEventListener("load", () => {
    var collapseIcon = document.getElementById("collapse-icon");

    var menu = $("#menu");

    var isbn = document.getElementById('isbn');

    var elementsOnPage = document.getElementById('elements-on-page');

    isbn.addEventListener('input', () => {
        isbn.value = parseInt(String(page.value).replace(/[^0-9]/g, ''));
    }, false);

    elementsOnPage.addEventListener('input', () => {
        elementsOnPage.value = parseInt(String(page.value).replace(/[^0-9]/g, ''));
    }, false);

    menu.on("show.bs.collapse", () => {
        collapseIcon.style.visibility = "hidden";
    });

    menu.on("hide.bs.collapse", () => {
        collapseIcon.style.visibility = "visible";
    });
}, false);