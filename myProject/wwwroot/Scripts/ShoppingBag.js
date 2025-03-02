document.addEventListener("DOMContentLoaded", () => {
    drawBasket();
});

const drawBasket = () => {
    const productsArr = getProductsFromCart();
    clearBasket();

    if (!productsArr) {
        updateCartSummary(0, 0);
        return;
    }

    let template = document.getElementById("temp-row");
    let totalSum = 0;

    productsArr.forEach(product => {
        if (!product) return;

        totalSum += product.price * (product.quantity || 1);
        const row = createRowFromTemplate(template, product);

        document.getElementById("cistGroup").appendChild(row);
    });

    updateCartSummary(productsArr.length, totalSum);
};

const getProductsFromCart = () => {
    return JSON.parse(sessionStorage.getItem("cart"));
};

const clearBasket = () => {
    document.getElementById("cistGroup").replaceChildren();
};

const createRowFromTemplate = (template, product) => {
    const row = template.content.cloneNode(true);
    const quantity = product.quantity || 1;

    row.querySelector(".price").innerText = product.price;
    row.querySelector('.imageColumn img').src = product.imageUrl;
    row.querySelector(".descriptionColumn").innerText = product.description;
    row.querySelector('.imageColumn a').href = product.imageUrl;

    row.querySelector('.totalColumndelete a').addEventListener('click', () => removeProductFromCart(product.productId));
    row.querySelector(".quantity").innerText = quantity;
    row.querySelector('.quantity-increase').addEventListener('click', () => updateQuantity(product.productId, 1));
    row.querySelector('.quantity-decrease').addEventListener('click', () => updateQuantity(product.productId, -1));

    return row;
};

const updateCartSummary = (itemCount, totalAmount) => {
    document.getElementById('itemCount').innerText = itemCount;
    document.getElementById('totalAmount').innerText = totalAmount;
};

const removeProductFromCart = (productId) => {
    let cart = getProductsFromCart();
    if (!cart) return;

    const updatedCart = cart.filter(item => item.productId !== productId);
    sessionStorage.setItem('cart', JSON.stringify(updatedCart));
    redrawBasket(updatedCart);
};

const redrawBasket = (updatedCart) => {
    clearBasket();
    if (updatedCart && updatedCart.length > 0) {
        drawBasket();
    } else {
        updateCartSummary(0, 0);
    }
};

const placeOrder = async (event) => {
    const button = event.target;
    button.disabled = true;
    let cart = getProductsFromCart();
    if (!cart || cart.length === 0) {
        alert("The cart is empty!!");
        return;
    }

    const items = cart.map(p => ({ productId: p.productId, quantity: p.quantity }));
    const user = JSON.parse(sessionStorage.getItem('user'));
    const orderSum = parseInt(document.getElementById('totalAmount').innerText);

    const data = {
        orderSum,
        userId: user.userId,
        userName: user.email,
        orderItems: items
    };

    const response = await fetch('api/Orders', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    });
    if (response.ok) {
        const responseData = await response.json(); // תפיסת הנתונים שהשרת החזיר
        sessionStorage.removeItem('cart');
        alert(`The order: ${responseData.orderId} was successfully placed.`);
         window.location.href = "Products.html"; // פתיחת REDIRECT כשההזמנה מצליחה
    } else {
        alert("The order failed!!");
    }
};

const updateQuantity = (productId, change) => {
    let cart = getProductsFromCart();
    if (!cart) return;

    const product = cart.find(item => item.productId === productId);
    if (!product) return;

    product.quantity += change;
    if (product.quantity <= 0) {
        cart = cart.filter(item => item.productId !== productId);
    }

    sessionStorage.setItem('cart', JSON.stringify(cart));
    redrawBasket(cart);
};
