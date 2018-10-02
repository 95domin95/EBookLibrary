window.addEventListener("load", () => {

    var page = document.getElementById("page-input");

    if (page !== null) {
        var form = document.getElementById("browse-books-form");

        var prev = document.getElementById("prev-btn");

        var next = document.getElementById("next-btn");

        var maxPage = document.getElementById("max-page");

        prev.addEventListener("click", () => {
            if (isNaN(parseInt(page.value))) {
                page.value = 1;
            }
            else {
                if (parseInt(page.value) > 1) {
                    page.value = parseInt(page.value) - 1;
                }
            }
        }, false);

        next.addEventListener("click", () => {
            if (isNaN(parseInt(page.value))) {
                page.value = 1;
            }
            else {
                if (parseInt(page.value) < parseInt(maxPage.value)) {
                    page.value = parseInt(page.value) + 1;
                }
            }
        }, false);

        maxPage.addEventListener("click", () => {
            page.value = parseInt(maxPage.value);
        }, false);

        page.addEventListener("input", () => {
            page.value = parseInt(String(page.value).replace(/[^0-9]/g, ''));
        }, false);

        page.addEventListener("change", () => {
            if (parseInt(page.value) <= parseInt(maxPage)) {
                form.submit();
            }
            else {
                page.value = parseInt(maxPage.value);
                form.submit();
            }
        }, false);
    }
}, false);