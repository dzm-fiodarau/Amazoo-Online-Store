//To be adapted with proper database. Currently this code uses localstorage query which is a database using webn local stoarge (ex. google chrome application)
//To add the functionality of this code, add the following code segement to the end of the desiered PHP file
 /*----------------------------------------------------------------------------------------

<!--SHOPPING CART NAV-BAR-->
<div class="relative">
    <div class="absolute">
        <button style="font-size:30px;cursor:pointer" onclick="openNav()" class="shoppingCart" href="ShoppingCart.php"style="color:black;"><i class="fas fa-shopping-cart"></i> <span > 0 </span> </button>
    </div>
</div>
<div id="mySidenav" class="sidenav">
    <div class="topNav">
        <img  class="YourCart" src="../image/YourCart.png" alt = "Your Cart"> 
        <a href="javascript:void(0)" class="closebtn" onclick="closeNav()">&times;</a>
    </div>
    <div class="table-responsive-sm">
        <div class="content">
            <table class="table table-striped" id="dropDownCart">
                <!--Table Insert From JS-->
            </table> 
        </div>
    </div>
</div>
<script src="../js/ShoppingCart.js"></script>

----------------------------------------------------------------------------------------*/

console.log("running");


function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
  }

function openNav() {
    displayCart2();
    document.getElementById("mySidenav").style.width = "420px";
  }



//STORES ALL CLASS=".add-cart" TO carts VARIABLE FROM DOCUMENT//
let carts = document.querySelectorAll('.add-cart');

//LIST OF ALL PRODUCTS START//
let products = [
    {
        name: 'Jeans',
        tag: 'Jeans',
        price: 24.99,
        inCart: 0
    },
    {
        name: 'Joggings',
        tag: 'Joggings',
        price: 18.99,
        inCart: 0
    },
    {
        name: 'TShirt Set',
        tag: 'Tshirt Set',
        price: 30,
        inCart: 0
    },
    {
        name: 'Liquid Hand Soap',
        tag: 'Liquid Hand Soap',
        price: 5,
        inCart: 0
    },
    {
        name: 'Moisture Shampoo',
        tag: 'Moisture Shampoo',
        price: 3.99,
        inCart: 0
    },
    {
        name: 'Shampoo',
        tag: 'Shampoo',
        price: 2.66,
        inCart: 0
    },
    {
        name: 'Toothbrush',
        tag: 'Toothbrush',
        price: 8.97,
        inCart: 0
    },
    {
        name: 'Toothbrush Set',
        tag: 'Toothbrush Set',
        price: 13.13,
        inCart: 0
    },
    {
        name: 'Mario',
        tag: 'Mario',
        price: 89.99,
        inCart: 0
    },
    {
        name: 'Spiderman',
        tag: 'Spiderman',
        price: 77.99,
        inCart: 0
    }
];


//SEARCH THROUGH EACH BUTTON FOR MATCHING PRODUCT ON "CLICK" EVENT OF BUTTONS WITH CLASS=".add-cart"//
carts.forEach(carts => {
    carts.addEventListener('click', event => {
        for(x=0; x<products.length;x++){
            if (carts.getAttribute("data-target")==products[x].tag){
                console.log("ADDED", products[x], carts.getAttribute("data-target") )
                cartProducts(products[x]); 
                totalCost(products[x])
            }
            else{console.log("error")}
        }
    });
 });



//KEEP TRACK OF ALL PRODUCTS IN CART (as a #) AND DISPLAY RESULT TO FIRST CLASS=".shoppingCart" WITH SPAN//
function productInCart(){
    let x = localStorage.getItem('cartProducts');
    let cartItems = localStorage.getItem("productsInCart");
    var totalItems=0;
    totalItems=parseFloat(totalItems);
    if(localStorage.getItem("cartProducts")!=null) {
        cartItems = JSON.parse(cartItems);
        for(i=0; i<Object.values(cartItems).length; i++) {  
            totalItems += (Object.values(cartItems)[i].inCart);
        }
        localStorage.setItem('cartProducts', totalItems);
        document.querySelector('.shoppingCart span').textContent = totalItems;
    }
    else {
        localStorage.setItem('cartProducts', totalItems);
        document.querySelector('.shoppingCart span').textContent = totalItems;}


}

//KEEP TRACK OF ALL PRODUCTS IN CART (as a #) AND DISPLAY RESULT TO FIRST CLASS=".shoppingCart" WITH SPAN//
function cartProducts(products) {
    let cartItems = localStorage.getItem('cartProducts');
    
    cartItems = parseInt(cartItems);

    if (cartItems) {
        localStorage.setItem('cartProducts', cartItems + 1);
        document.querySelector('.shoppingCart span').textContent = cartItems + 1;
    }
    else{
        localStorage.setItem('cartProducts', 1);
        document.querySelector('.shoppingCart span').textContent = 1;
    }
    setItems(products);
}


 
//ADDS LOCAL STORAGE OF TOTAL COST OF ALL PRODUCTS IN LOCAL STORAGE//
function totalCost(){
    var totalCostUpdate=0;
    let cartCost = localStorage.getItem('totalCost2');
    if(localStorage.getItem("productsInCart")!=null) {
        totalCostUpdate=parseFloat(totalCostUpdate);
        let cartItems=localStorage.getItem("productsInCart");
        cartItems = JSON.parse(cartItems);
        for(i=0; i<Object.values(cartItems).length; i++) {                     
            totalCostUpdate += (Object.values(cartItems)[i].inCart*Object.values(cartItems)[i].price);
            }
        localStorage.setItem("totalCost2", totalCostUpdate);
        }
    else {
         localStorage.setItem("totalCost2", 0);
         } 
}

//DISPLAYS THE CART AS A TABLE AT FIRST CLASS=".product"//
function displayCart(){
    let cartItems=localStorage.getItem("productsInCart");
    cartItems = JSON.parse(cartItems);

    let productContainer = document.querySelector(".product");
    if(productContainer && cartItems) {     
        productContainer.innerHTML = '';
        var i;
        for(i=0; i<Object.values(cartItems).length; i++) {         
            productContainer.innerHTML += `                                                                                       
                                    <td class="name">${Object.values(cartItems)[i].name}</td>
                                    <td class="price">${Math.round((Object.values(cartItems)[i].price) * 100) / 100}</td>
                                    <td class="quantity">
                                            <button class="btn" type="button" id="addItem" ><i class="fas fa-plus-square" onclick="addItem(${Object.values(cartItems)[i].name})" ></i></button> 
                                            ${Object.values(cartItems)[i].inCart} 
                                            <button class="btn" type="button" id="subItem" ><i class="fas fa-minus-square" onclick="subItem(${Object.values(cartItems)[i].name})"></i></button>
                                    </td>
                                    <td >${Math.round((Object.values(cartItems)[i].price)*(Object.values(cartItems)[i].inCart) * 100) / 100}</td> 
                                    <td >
                                        <button class="btn btn-primary" id="${Object.values(cartItems)[i].name}" type="button" aria-expanded="false" aria-controls="contentId"  onclick="deleteItem(${Object.values(cartItems)[i].name})">
                                        <i class="fas fa-trash-alt" ></i> 
                                        </button>
                                    </td>                                                                                                                                                                               
            `
    };    
    }
}

//DISPLAYS THE CART AS A TABLE AT FIRST CLASS="#dropDownCart"//
function displayCart2(){
    let cartItems=localStorage.getItem("productsInCart");
    cartItems = JSON.parse(cartItems);

    let totalCost=localStorage.getItem("totalCost2");
    totalCost = JSON.parse(totalCost);

    let productContainer = document.querySelector("#dropDownCart");
    if(productContainer && cartItems) {     
        productContainer.innerHTML = `
        
                    <thead>
                        <tr>
                            <th scope="col" colspan="6"> 
                                
                            </th>
                        </tr>
                      <tr>
                        <th scope="col">Product</th>
                        <th scope="col">Price</th>
                        <th scope="col">Quantity</th>
                        <th scope="col">Total Price</th>
                        <th scope="col"></th>
                        
                      </tr>
                    </thead>
                    `

        var i;
        for(i=0; i<Object.values(cartItems).length; i++) {         
            productContainer.innerHTML += ` 
             
                    <tbody class="productNav">                                                                             
                                    <td class="name">${Object.values(cartItems)[i].name}</td>
                                    <td class="price">${Math.round((Object.values(cartItems)[i].price) * 100) / 100}</td>
                                    <td class="quantity">
                                            <button class="btn" type="button" id="addItem" ><i class="fas fa-plus-square" onclick="addItem(${Object.values(cartItems)[i].name})" ></i></button> 
                                            ${Object.values(cartItems)[i].inCart} 
                                            <button class="btn" type="button" id="subItem" ><i class="fas fa-minus-square" onclick="subItem(${Object.values(cartItems)[i].name})"></i></button>
                                    </td>
                                    <td >${Math.round((Object.values(cartItems)[i].price)*(Object.values(cartItems)[i].inCart) * 100) / 100}</td> 
                                    <td >
                                        <button class="btn btn-primary" id="${Object.values(cartItems)[i].name}" type="button" aria-expanded="false" aria-controls="contentId"  onclick="deleteItem(${Object.values(cartItems)[i].name})">
                                        <i class="fas fa-trash-alt"></i> 
                                        </button>
                                    </td>      
                                    </tbody>                                                                                                                                                                       
                         `
        };    
        productContainer.innerHTML += `
        <thead>
          <tr>
            <th scope="col" colspan="2">Cart Summary</th>
          </tr>
        </thead>
        <tbody>
          <tr>
              <th> QST </th>
              <th class="qst">
              ${Math.round((qstTax()) * 100) / 100}
              </th>
          </tr>
          <tr> <th> GST </th>
            <th>
            ${Math.round((gstTax()) * 100) / 100}
            </th>
        </tr>
        <tr> <th> Total </th>
            <th>
            ${Math.round((qstTax()+gstTax()+totalCost) * 100) / 100}
            </th>
        </tr>
        <tr>  
            <th colspan="3">
                
                <button class="btn btn-primary" type="button" data-toggle="collapse" data-target="#contentId" aria-expanded="false"
                aria-controls="contentId" onclick="placeOrder()" style="background-color:green;
                width:100%; height:100%;
                color:rgba(255, 255, 255, 0.815);
                font-family:Georgia, 'Times New Roman', Times, serif;">                                      
                Place Order
              </button>
            
            </th>
            
        </tr>
        </tbody> 
        `
    }   
}


//DELETES SELECT ITEM FROM THE LOCAL STORAGE//
function deleteItem(itemName){
    let cartItems=localStorage.getItem("productsInCart");
    cartItems = JSON.parse(cartItems);
    var cartItems3=[Object.values(cartItems).length];
    for(i=0; i<Object.values(cartItems).length; i++){
        cartItems3[i]=Object.values(cartItems)[i];
    }

    var i;
    for(i=0; i<Object.values(cartItems).length; i++) { 
        if (itemName.id==Object.values(cartItems)[i].name)  {
            console.log(Object.values(cartItems)[i].tag);
         cartItems3.splice(i,1);
         console.log(Object.values(cartItems)[i]);
         localStorage.setItem('productsInCart', JSON.stringify(cartItems3));
        }
    }
    onAction();
}



//ADDS SELECT ITEM TO THE LOCAL STORAGE//
function addItem(itemName) {
    console.log("In Add Item");
    let cartItems=localStorage.getItem("productsInCart");
    cartItems = JSON.parse(cartItems);
    for(i=0; i<Object.values(cartItems).length; i++) { 
        if (itemName.id==Object.values(cartItems)[i].name)  {
            Object.values(cartItems)[i].inCart += 1
            
            localStorage.setItem('productsInCart', JSON.stringify(cartItems));
        }
    }
    onAction();
}


//ADDS LOCAL STORAGE OF CORRESPONDING PRODUCT//
function setItems(products) {
    let cartItems = localStorage.getItem('productsInCart');
    cartItems = JSON.parse(cartItems);
      
    if( cartItems != null) {
        if(cartItems[products.tag] == undefined) {
            cartItems = {
                ...cartItems,
                [products.tag]: products
            }
        }
        cartItems[products.tag].inCart += 1;
    }
    else {
        products.inCart = 1;
        cartItems = {
            [products.tag]: products
        }
    }
    localStorage.setItem('productsInCart', JSON.stringify(cartItems));
}



//SUBTRACTS SELECT ITEM TO THE LOCAL STORAGE//
function subItem(itemName){
    console.log("In Sub Item");
    let cartItems=localStorage.getItem("productsInCart");
    cartItems = JSON.parse(cartItems);
    for(i=0; i<Object.values(cartItems).length; i++) { 
        if (itemName.id==Object.values(cartItems)[i].name)  {
            if (Object.values(cartItems)[i].inCart>1){
                Object.values(cartItems)[i].inCart -= 1
             }
            else {alert("Cannot have less than '0' Quantity. Please delete Item")
                console.log("out of bounds")}
            localStorage.setItem('productsInCart', JSON.stringify(cartItems));
        }
    }
    onAction();
}



//CALCULATES QST TAX//
function qstTax() {
    let totalCost=localStorage.getItem("totalCost2");
    totalCost = JSON.parse(totalCost);
    var qst = 0.0975;
    var qstTotal = totalCost*qst;
    return qstTotal;
}

//CALCULATES GST TAX//
function gstTax() {
    let totalCost=localStorage.getItem("totalCost2");
    totalCost = JSON.parse(totalCost);
    var gst = 0.05;
    var gstTaxTotal = totalCost*gst;
    return gstTaxTotal;
}

//CLEARS THE LOCAL STORAGE ON ORDER PLACED//
function placeOrder(){
    alert("Order has been accepted");
    localStorage.clear();
    location.reload();
}


  //LIST OF FUNCTIONS TO RUN ON PAGE ACTION//
function onAction(){
    totalCost()
    displayCart()
    productInCart()
    displayCart2()
    qstTax()
    gstTax()
}

  //LIST OF FUNCTIONS TO RUN ON PAGE REFRESH//
  productInCart();
  totalCost();
  displayCart();
  displayCart2();
  



