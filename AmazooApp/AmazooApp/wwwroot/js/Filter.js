
//window.addEventListener('load', load)

$(document).ready(function () {
    
    const Save = () => {
        localStorage.clear();
        var checkbox = document.getElementsByName('chckBox');
        for (let i = 0; i < checkbox.length; i++) {
            console.log("The checkbox value is ", checkbox[i]);
            localStorage.setItem(checkbox[i].id, checkbox[i].checked);
        }
    }

    const load = () => {
        var checkbox = document.getElementsByName('chckBox');
        for (let i = 0; i < checkbox.length; i++) {
            var checked = JSON.parse(localStorage.getItem(checkbox[i]));
            console.log("The checkbox value is ", checked);
            document.getElementById(checkbox[i].id).checked = checked;
        }

    }
});