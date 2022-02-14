//NAVIGATION BAR FUNCTIONS
function showMenu() {
    var menu_img = document.getElementById("menu_img");
    menu_img.src = "media/close_logo.png";

    var menu_but = document.getElementById("menu_but")
    menu_but.onclick = hideMenu;

    var parent = document.getElementById("container");

    var container = document.createElement("div");
    var login = document.createElement("a");
    var signUp = document.createElement("a");

    container.style.textAlign = "center";
    container.id = "menu";

    login.className = "menu_item";
    login.textContent = "Login";
    login.style.margin = "10px 0 0 0";

    signUp.className = "menu_item";
    signUp.textContent = "Sign Up";

    container.appendChild(login);
    container.appendChild(signUp);
    parent.appendChild(container);
}

function hideMenu() {
    var menu_img = document.getElementById("menu_img");
    menu_img.src = "media/menu_logo.png";

    var menu_but = document.getElementById("menu_but")
    menu_but.onclick = showMenu;

    var container = document.getElementById("menu");
    container.parentNode.removeChild(container);
}

function showMenuAdmin() {
    if (document.getElementById("acc_menu")) {
        hideAccount();
    }
    var menu_img = document.getElementById("menu_img");
    menu_img.src = "media/close_logo.png";

    var menu_but = document.getElementById("menu_but")
    menu_but.onclick = hideMenuAdmin;

    var parent = document.getElementById("container");

    var container = document.createElement("div");
    var products = document.createElement("a");
    var users = document.createElement("a");
    var orders = document.createElement("a");

    container.style.textAlign = "center";
    container.id = "menu";

    products.className = "menu_item";
    products.textContent = "Products";
    products.style.margin = "10px 0 0 0";

    users.className = "menu_item";
    users.textContent = "Users";

    orders.className = "menu_item";
    orders.textContent = "Orders";

    container.appendChild(products);
    container.appendChild(users);
    container.appendChild(orders);
    parent.appendChild(container);

}

function hideMenuAdmin() {
    var menu_img = document.getElementById("menu_img");
    menu_img.src = "media/menu_logo.png";

    var menu_but = document.getElementById("menu_but")
    menu_but.onclick = showMenuAdmin;

    var container = document.getElementById("menu");
    container.parentNode.removeChild(container);
}
//NAVIGATION BAR FUNCTIONS END