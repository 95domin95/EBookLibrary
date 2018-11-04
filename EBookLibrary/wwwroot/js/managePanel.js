window.addEventListener("load", () => {

    var isbn = document.getElementById('isbn');

    var id = document.getElementById('id');

    var copiesCount = document.getElementById('copies-count');

    var pages = document.getElementById('pages');

    var book = document.getElementById("book");

    var bookCovering = document.getElementById("book-covering");

    var imageExts = new Array("png", "jpg", "jpeg");

    var documentExts = new Array("pdf");

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

    isbn.addEventListener('input', () => {
        isbn.value = parseInt(String(page.value).replace(/[^0-9]/g, ''));
    }, false);

    copiesCount.addEventListener('input', () => {
        copiesCount.value = parseInt(String(page.value).replace(/[^0-9]/g, ''));
    }, false);

    id.addEventListener('input', () => {
        id.value = parseInt(String(page.value).replace(/[^0-9]/g, ''));
    }, false);

    pages.addEventListener('input', () => {
        id.value = parseInt(String(page.value).replace(/[^0-9]/g, ''));
    }, false);

}, false);