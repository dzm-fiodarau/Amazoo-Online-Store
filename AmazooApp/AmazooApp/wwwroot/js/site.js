//NAVIGATION BAR FUNCTIONS
function showMenu() {
    var menu_img = document.getElementById("menu_img");
    menu_img.src = "/media/close_logo.png";

    var menu_but = document.getElementById("menu_but")
    menu_but.onclick = hideMenu;

    var login = document.getElementById("menu_item_1");
    var signUp = document.getElementById("menu_item_2");

    login.textContent = "Login";
    login.className = "menu_item";

    signUp.className = "menu_item";
    signUp.textContent = "Sign Up";

}

function hideMenu() {
    var menu_img = document.getElementById("menu_img");
    menu_img.src = "/media/menu_logo.png";

    var menu_but = document.getElementById("menu_but")
    menu_but.onclick = showMenu;

    var login = document.getElementById("menu_item_1");
    var signUp = document.getElementById("menu_item_2");
    login.textContent = "";
    signUp.textContent = "";
}

function showMenuAdmin() {
    if (document.getElementById("menu_item_7").textContent != "") {
        hideAccount();
    }
    var menu_img = document.getElementById("menu_img");
    menu_img.src = "/media/close_logo.png";

    var menu_but = document.getElementById("menu_but")
    menu_but.onclick = hideMenuAdmin;

    var products = document.getElementById("menu_item_3");
    var users = document.getElementById("menu_item_4");
    var orders = document.getElementById("menu_item_5");


    products.className = "menu_item";
    products.textContent = "Products";
    products.style.margin = "10px 0 0 0";

    users.className = "menu_item";
    users.textContent = "Users";

    orders.className = "menu_item";
    orders.textContent = "Orders";
}

function hideMenuAdmin() {
    var menu_img = document.getElementById("menu_img");
    menu_img.src = "/media/menu_logo.png";

    var menu_but = document.getElementById("menu_but")
    menu_but.onclick = showMenuAdmin;

    var products = document.getElementById("menu_item_3");
    var users = document.getElementById("menu_item_4");
    var orders = document.getElementById("menu_item_5");

    products.textContent = "";
    users.textContent = "";
    orders.textContent = "";
}

function showAccount() {
    if (document.getElementById("menu_item_3").textContent != "") {
        hideMenuAdmin();
    }

    var account_but = document.getElementById("account_but")
    account_but.onclick = hideAccount;

    var hiUser = document.getElementById("menu_item_6");
    var logout = document.getElementById("menu_item_9");
    var myAccount = document.getElementById("menu_item_7");
    var myOrders = document.getElementById("menu_item_8");

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

}

function hideAccount() {
    var account_but = document.getElementById("account_but")
    account_but.onclick = showAccount;

    var hiUser = document.getElementById("menu_item_6");
    var logout = document.getElementById("menu_item_9");
    var myAccount = document.getElementById("menu_item_7");
    var myOrders = document.getElementById("menu_item_8");

    hiUser.textContent = "";
    logout.textContent = "";
    myAccount.textContent = "";
    myOrders.textContent = "";
}
//NAVIGATION BAR FUNCTIONS END