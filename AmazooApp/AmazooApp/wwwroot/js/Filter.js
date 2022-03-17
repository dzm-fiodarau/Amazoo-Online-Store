
const load = () => {
    var checkbox = document.getElementsByName('chckBox');
    const myArray = document.cookie.split(";");
    for (let i = 0; i < myArray.length; i++) {

        for (let j = 0; j < checkbox.length; j++) {
            let t = myArray[i].split("=");
            if ((t[1] === "true") && checkbox[j].id === t[0].trim()) {
                document.getElementById(checkbox[j].id).checked = true;
            }
        }

    }

}

/**
 * Load Cookies for the filter system,
 * if the home page is loaded clear the cookies for the filter
 * 
 * */
const preload = () => {
    var url = document.baseURI
    if (url.includes("Filter")) {
        load();
    } else {
        console.log("Don't load cookies");
        var checkbox = document.getElementsByName('chckBox');
        for (let j = 0; j < checkbox.length; j++) {
            document.cookie = checkbox[j] + '=; Max-Age=-99999999;'
        }
    }
}

window.addEventListener('load', preload);

/**
 * Save the Filter options in the cookies 
 * 
 * */
const Save = () => {
    var checkbox = document.getElementsByName('chckBox');
    for (let i = 0; i < checkbox.length; i++) {
        console.log("The checkbox value is ", checkbox[i]);
        document.cookie= checkbox[i].id + "=" + checkbox[i].checked;
    }
}

$(document).ready(function () {
    const Save = () => {
        var checkbox = document.getElementsByName('chckBox');
        for (let i = 0; i < checkbox.length; i++) {
            console.log("The checkbox value is ", checkbox[i]);
            document.cookie = checkbox[i].id + "=" + checkbox[i].checked + "secured";
        }
    }
});