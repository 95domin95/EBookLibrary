window.addEventListener("load", () => {

    var book = document.getElementById("book");

    var bookCovering = document.getElementById("book-covering");

    var operationSelect = document.getElementById("operation-types");

    var imageExts = new Array("png", "jpg", "jpeg");

    var documentExts = new Array("pdf");

    var operation = {
        "add": [
            "id",
            "pages-min",
            "pages-max"
        ],
        "update": [
            "pages-min",
            "pages-max"
        ],
        "delete": [
            "isbn",
            "title",
            "pages",
            "pages-min",
            "pages-max",
            "publisher",
            "category",
            "author",
            "book-covering",
            "book"
        ],
        "select": [
            "book-covering",
            "book"
        ]
    };

    var formField = [
        "id",
        "isbn",
        "title",
        "pages",
        "pages-min",
        "pages-max",
        "publisher",
        "category",
        "author",
        "book-covering",
        "book"
    ];

    operationSelect.addEventListener("change", () => {
        for (i in formField) {
            document.getElementById(formField[i]).disabled = false;
        }
        for (i in operation[operationSelect.value]) {
            document.getElementById(operation[operationSelect.value][i]).disabled = true;
        }
    }, false);

    bookCovering.addEventListener("change", e => {
        var preview = document.getElementById("book-covering-image");
        var file = e.target.files[0];
        var reader = new FileReader();

        reader.onloadend = function () {
            preview.src = reader.result;
        };

        if (file && checkForValidFileExtension(file.name, imageExts)) {
            reader.readAsDataURL(file);
        }
        else {
            preview.src = window.location.origin + "/images/Icons-master/picol_latest_prerelease_svg/document_image_add.svg";
        }
    }, false);

    book.addEventListener("change", e => {
        var preview = document.getElementById("book-image");
        var file = e.target.files[0];
        var reader = new FileReader();

        if (file && checkForValidFileExtension(file.name, documentExts)) {
            reader.readAsDataURL(file);
        }
    }, false);

    function checkForValidFileExtension(elemVal, allowedExts) {
        var fp = elemVal;
        if (fp.indexOf('.') === -1)
            return false;

        var ext = fp.substring(fp.lastIndexOf('.') + 1).
            toLowerCase();

        for (var i = 0; i < allowedExts.length; i++) {
            if (ext === allowedExts[i]) return true;
        }

        return false;
    }

}, false);