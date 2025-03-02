const checkPassword = async () => {
        password = document.getElementById("password").value;
        const responsePost = await fetch('api/Users/checkPassword', {
            method: 'POST',
            headers: {
                'Content-Type': "application/json"
            },
            body: JSON.stringify(password)
        })
    const dataPost = await responsePost.json();
       let color=''
        document.getElementById("progress").setAttribute("value", dataPost)
        if (dataPost <= 1)
            color = '#ff0000';
        else if(dataPost <=3)
            color = 'blue'
        else
            color = '#4cff00'
        document.getElementById("progress").style.setProperty("accent-color", color)
    document.getElementById("strength").innerHTML = "strength: "+dataPost;
}

const Register = async () => {
    const userData = {
        email: document.getElementById("emailRegister").value,
        password: document.getElementById("password").value,
        firstName: document.getElementById("firstNameRegister").value,
        lastName: document.getElementById("lastNameRegister").value
    };

    const response = await fetch('api/Users', {
        method: 'POST',
        headers: {
            'Content-Type': "application/json"
        },
        body: JSON.stringify(userData)
    });

    if (response.ok) {
        await response.json();
        alert("The user registered successfully!")
        window.location.href = "Login.html";
    } else {
        alert("At least one field is not valid");
    }
};

const Login = async () => {
    const userData = {
        email: document.getElementById("emailLogin").value,
        password: document.getElementById("password").value
    };

    const response = await fetch('api/Users/login', {
        method: 'POST',
        headers: {
            'Content-Type': "application/json"
        },
        body: JSON.stringify(userData)
    });

    if (response.ok) {
        const data = await response.json();
        sessionStorage.setItem("user", JSON.stringify(data));
        window.location.href = "Products.html";
    } else {
        alert("The user name or the password is not valid");
    }
};

const UpdatUser = async () => {
    const userData = {
        email: document.getElementById("email").value,
        password: document.getElementById("password").value,
        firstName: document.getElementById("firstName").value,
        lastName: document.getElementById("lastName").value
    };

    const userId = JSON.parse(sessionStorage.getItem("user")).userId;
    const response = await fetch(`api/Users/${userId}`, {
        method: 'PUT',
        headers: {
            'Content-Type': "application/json"
        },
        body: JSON.stringify(userData)
    });

    if (response.ok) {
        const data = await response.json();
        sessionStorage.setItem("user", JSON.stringify(data));
        alert("Update successful!");
        window.location.href = "Products.html";
    } else {
        alert("At least one field is not valid");
    }
};