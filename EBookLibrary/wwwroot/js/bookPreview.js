window.addEventListener("load", () => {
    let addDays = days => {
        var date = new Date();
        date.setDate(date.getDate() + days);
        var dd = date.getDate();
        var MM = date.getMonth() + 1;
        var yyyy = date.getFullYear();
        var hh = date.getHours();
        var mm = date.getMinutes();
        if (dd < 10) {
            dd = '0' + dd;
        }
        if (MM < 10) {
            MM = '0' + MM;
        }
        if (mm < 10) {
            mm = '0' + mm;
        }
        if (hh < 10) {
            hh = '0' + hh;
        }

        return yyyy + '-' + MM + '-' + dd + 'T' + hh + ':' + mm;
    };

    document.getElementById("date-time-picker").setAttribute("max", addDays(7));
    document.getElementById("date-time-picker").setAttribute("min", addDays(1));
}, false);