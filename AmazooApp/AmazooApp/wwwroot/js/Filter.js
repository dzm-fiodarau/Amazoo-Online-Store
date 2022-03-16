
const load = () => {
    var checkbox = document.getElementsByName('chckBox');
    const myArray = document.cookie.split(";");
    for (let i = 0; i < myArray.length; i++) {

        for (let j = 0; j < checkbox.length; j++) {
            let t = myArray[i].split("=");
            //console.log("The id before eval ", t[0], t[1], checkbox[i].id);
            if ((t[1] === "true") && checkbox[j].id === t[0].trim()) {
                console.log("The id found "+ checkbox[j].id+ " truth val "+t[1]);
                document.getElementById(checkbox[j].id).checked = true;
            }
        }

    }

}

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

const Save = () => {
    localStorage.clear();
    var checkbox = document.getElementsByName('chckBox');
    for (let i = 0; i < checkbox.length; i++) {
        console.log("The checkbox value is ", checkbox[i]);
        document.cookie= checkbox[i].id + "=" + checkbox[i].checked;
    }
}

$(document).ready(function () {
    const Save = () => {
        localStorage.clear();
        var checkbox = document.getElementsByName('chckBox');
        for (let i = 0; i < checkbox.length; i++) {
            console.log("The checkbox value is ", checkbox[i]);
            document.cookie = checkbox[i].id + "=" + checkbox[i].checked + "secured";
        }
    }
});