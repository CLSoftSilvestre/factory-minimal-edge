
function setCookie(cname, cvalue, exdays) {
    const d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    let expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function RestartTour() {
    setCookie("tourHomeComplete", false, 90);
    setCookie("tourNotificationsComplete", false, 90);
    setCookie("tourEquipmentsComplete", false, 90);
}

function SelectTours(e) {
    var a = e.checked;
    if (a == true) {
        RestartTour();
    } else {
        //Disable all tours
        setCookie("tourHomeComplete", true, 90);
        setCookie("tourNotificationsComplete", true, 90);
        setCookie("tourEquipmentsComplete", true, 90);
    }
}

// Change the status of the toggle to false if all tours complete
let a = getCookie("tourHomeComplete");
let b = getCookie("tourNotificationsComplete");
let c = getCookie("tourEquipmentsComplete");

if (a == "true" && b == "true" && c == "true") {
    document.getElementById("enableToursSw").checked = false;
}