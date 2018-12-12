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

            var canvas = document.createElement("canvas");
            var ctx = canvas.getContext("2d");
            ctx.drawImage(preview, 0, 0);

            var MAX_WIDTH = 10;
            var MAX_HEIGHT = 600;
            var width = preview.width;
            var height = preview.height;

            if (width > height) {
                if (width > MAX_WIDTH) {
                    height *= MAX_WIDTH / width;
                    width = MAX_WIDTH;
                }
            } else {
                if (height > MAX_HEIGHT) {
                    width *= MAX_HEIGHT / height;
                    height = MAX_HEIGHT;
                }
            }
            canvas.width = width;
            canvas.height = height;
            ctx.drawImage(preview, 0, 0, width, height);

            var dataurl = canvas.toDataURL("image/png");
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