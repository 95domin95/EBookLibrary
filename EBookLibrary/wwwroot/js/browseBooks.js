function disableRangeInput(doubleInputMin, doubleInputMax, pageRangeSelector) {
    if (!pageRangeSelector.checked) {
        doubleInputMin.disabled = true;
        doubleInputMax.disabled = true;
        doubleInputMin.style.opacity = "0.5";
        doubleInputMax.style.opacity = "0.5";
    }
    else {
        doubleInputMin.disabled = false;
        doubleInputMax.disabled = false;
        doubleInputMin.style.opacity = "1";
        doubleInputMax.style.opacity = "1";
    }
}

window.addEventListener("load", () => {

    var collapseIcon = document.getElementById("collapse-icon");

    var doubleInputMin = document.getElementById("double-input-min");

    var doubleInputMax = document.getElementById("double-input-max");

    var pageRangeSelector = document.getElementById("page-range-checked");

    var menu = $("#menu");

    disableRangeInput(doubleInputMin, doubleInputMax, pageRangeSelector);

    pageRangeSelector.addEventListener("change", () => {
        disableRangeInput(doubleInputMin, doubleInputMax, pageRangeSelector);
    }, false);

    menu.on("show.bs.collapse", () => {
        collapseIcon.style.visibility = "hidden";
    });

    menu.on("hide.bs.collapse", () => {
        collapseIcon.style.visibility = "visible";
    });
}, false);