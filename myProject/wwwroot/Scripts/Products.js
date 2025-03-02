let categoriesList = [];

const templateToElement = (templateId) => document.getElementById(templateId).content.cloneNode(true);

const appendElement = (parentId, element) => document.getElementById(parentId).appendChild(element);

const updateElementText = (elementId, text) => document.getElementById(elementId).innerHTML = text;

const fetchData = async (url, options = {}) => {
    const response = await fetch(url, options);
    if (!response.ok) {
        throw new Error(`Network response was not ok: ${response.statusText}`);
    }
    return response.json();
};

const drawCategories = (data) => {
    data.forEach(category => {
        const card = templateToElement("temp-category");
        const checkbox = card.querySelector('.cb.checkbox');
        const opt = card.querySelector('.opt');
        opt.id = category.categoryId;
        card.querySelector('label .OptionName').textContent = category.categoryName;
        opt.addEventListener("click", (event) => handleChange(event, category.categoryId));
        appendElement("categoryList", card);
    });
};

const getAllCategories = async () => {
    try {
        const data = await fetchData('api/Categories');
        drawCategories(data);
    } catch (error) {
        console.error('Failed to fetch categories:', error);
    }
};

const handleChange = (event, categoryId) => {
    if (event.target.checked) {
        categoriesList = [...categoriesList, categoryId];
    } else {
        const index = categoriesList.indexOf(categoryId);
        if (index > -1) {
            categoriesList.splice(index, 1);
        }
    }
    FilterProduct();
};

const drawProducts = (products) => {
    const cart = JSON.parse(sessionStorage.getItem('cart')) || [];
    updateElementText('counter', products.length);
    updateElementText('ItemsCountText', cart.reduce((sum, item) => sum + item.quantity, 0));

    products.forEach(product => {
        const clone = templateToElement("temp-card");
        clone.querySelector('img').src = product.imageUrl;
        clone.querySelector('h1').textContent = product.productName;
        clone.querySelector('.price').textContent = product.price;
        clone.querySelector('.description').textContent = product.description;
        clone.querySelector('button').addEventListener('click', () => addProductToCart(product));
        appendElement('ProductList', clone);
    });
};

const getAllProducts = async () => {
    try {
        const data = await fetchData('api/Products');
        drawProducts(data);
        document.getElementById('maxPrice').value = Math.max(...data.map(product => product.price));
        document.getElementById('minPrice').value = Math.min(...data.map(product => product.price));
    } catch (error) {
        console.error('Failed to fetch products:', error);
    }
};

const FilterProduct = async () => {
    const minPrice = document.getElementById("minPrice").value;
    const maxPrice = document.getElementById("maxPrice").value;
    const description = document.getElementById("nameSearch").value || "";

    const categoriesString = categoriesList.map(c => `&categoriesId=${c}`).join("");

    try {
        const query = `api/Products?minPrice=${minPrice}&maxPrice=${maxPrice}${categoriesString}&description=${description}`;
        const data = await fetchData(query);
        document.getElementById('ProductList').replaceChildren();
        if (data.length) {
            drawProducts(data);
        }
    } catch (error) {
        console.error('Failed to fetch filtered products:', error);
    }
};

const addProductToCart = (product) => {
    let cart = JSON.parse(sessionStorage.getItem('cart')) || [];
    const existingProduct = cart.find(item => item.productId === product.productId);

    if (existingProduct) {
        existingProduct.quantity += 1;
    } else {
        cart.push({ ...product, quantity: 1 });
    }

    sessionStorage.setItem('cart', JSON.stringify(cart));
    updateElementText('ItemsCountText', cart.reduce((sum, item) => sum + item.quantity, 0));
};

document.addEventListener("DOMContentLoaded", () => {
    getAllProducts();
    getAllCategories();
});