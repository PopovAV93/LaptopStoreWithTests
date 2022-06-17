function toRegister() {
    document.getElementById("login").setAttribute("hidden", "hidden");
    document.getElementById("register").removeAttribute("hidden");
}

function toLogin() {
    document.getElementById("register").setAttribute("hidden", "hidden");
    document.getElementById("login").removeAttribute("hidden");
}