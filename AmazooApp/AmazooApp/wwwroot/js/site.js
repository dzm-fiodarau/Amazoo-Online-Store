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

function showAccount() {
    if (document.getElementById("menu")) {
        hideMenuAdmin();
    }
    var account_but = document.getElementById("account_but")
    account_but.onclick = hideAccount;

    var parent = document.getElementById("container");

    var container = document.createElement("div");
    container.id = "acc_menu";
    container.style.width = "fit-content";
    container.style.height = "fit-content";
    container.style.position = "absolute";
    container.style.right = "0px";
    container.style.backgroundColor = "black";

    var hiUser = document.createElement("p");
    var logout = document.createElement("a");
    var myAccount = document.createElement("a");
    var myOrders = document.createElement("a");

    hiUser.style.display = "block";
    hiUser.style.fontSize = "calc(18px + 0.5vw)";
    hiUser.textContent = "Hi, " + "<User's Name>"; //Implement user's name detection

    myAccount.className = "menu_account_item";
    myAccount.textContent = "My Account";
    myAccount.style.margin = "10px 0 0 0";

    myOrders.className = "menu_account_item";
    myOrders.textContent = "My Orders";

    logout.className = "menu_account_item";
    logout.textContent = "Logout";

    container.appendChild(hiUser);
    container.appendChild(myAccount);
    container.appendChild(myOrders);
    container.appendChild(logout);

    parent.appendChild(container);
}

function hideAccount() {
    var account_but = document.getElementById("account_but")
    account_but.onclick = showAccount;

    var container = document.getElementById("acc_menu");
    container.parentNode.removeChild(container);
}
//NAVIGATION BAR FUNCTIONS END