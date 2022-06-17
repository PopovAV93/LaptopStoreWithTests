function validatePassword(el) {

    var pass = el.elements.password;
    var message = "Password must be at least 6 characters";
    pass.setCustomValidity(pass.value.length < 6 ? message : "");
    pass.reportValidity();
}

function confirmPassword(el) {

    var pass = el.elements.password;
    var confirmPass = el.elements.passwordConfirm;
    var message = "Passwords do not match";

    confirmPass.setCustomValidity(pass.value != confirmPass.value ? message : "");
    confirmPass.reportValidity();
    return message;
}

function validateAge(el) {

    var age = el.age;
    var message = "Age range must be between 0 and 150";

    age.setCustomValidity(age.value < 0 || age.value > 150 ? message : "");
    age.reportValidity();
    return message;
}


