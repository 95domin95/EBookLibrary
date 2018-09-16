window.addEventListener("load", () => {

    var collapseIcon = document.getElementById("collapse-icon");

    var menu = $("#menu");

    menu.on("show.bs.collapse", () => {
        collapseIcon.style.visibility  = "hidden";
    });

    menu.on("hide.bs.collapse", () => {
        collapseIcon.style.visibility = "visible";
    });
}, false);