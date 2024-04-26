var Login = /** @class */ (function () {
    function Login() {
        var _this = this;
        this.emailRegex = /^(?=.{1,254}$)(?=.{1,64}@)[-!#$%&'*+/0-9=?A-Z^_`a-z{|}~]+(\.[-!#$%&'*+/0-9=?A-Z^_`a-z{|}~]+)*@[A-Za-z0-9]([A-Za-z0-9-]{0,61}[A-Za-z0-9])?(\.[A-Za-z0-9]([A-Za-z0-9-]{0,61}[A-Za-z0-9])?)*$/;
        this.passUpperCaseRegex = /.*[A-Z].*/;
        this.digitRegex = /.*\d.*/;
        this.specialCharsRegex = /.*\W.*/;
        this.LoginValidation = function () {
            var loginForm = new FormData(document.querySelector("#loginForm"));
            var isEmailValid = _this.emailValidation(loginForm.get("email").toString());
            var isPasswordValid = _this.passwordValidation(loginForm.get("password").toString());
            if (isEmailValid && isPasswordValid) {
                window.location.href = "/User";
            }
        };
        this.emailValidation = function (email) {
            if (email.length === 0) {
                $("#emailErrorMsg").text("Email nie moze byc pusty");
                $("#email").addClass("is-invalid");
                return false;
            }
            if (email.length <= 2) {
                $("#emailErrorMsg").text("Email musi byc dluzszy");
                $("#email").addClass("is-invalid");
                return false;
            }
            if (!_this.emailRegex.test(email)) {
                $("#emailErrorMsg").text("Email jest nieprawidlowy.");
                $("#email").addClass("is-invalid");
                return false;
            }
            $("#email").removeClass("is-invalid");
            $('#email').addClass("is-valid");
            $('#emailErrorMsg').text("");
            return true;
        };
        this.passwordValidation = function (password) {
            if (password.length == 0) {
                $("#passwordErrorMsg").text("Haslo nie moze byc puste");
                $("#password").addClass("is-invalid");
                return false;
            }
            if (password.length <= 2) {
                $("#passwordErrorMsg").text("Haslo musi byc dluzsze");
                $("#password").addClass("is-invalid");
                return false;
            }
            if (!_this.checkPasswordReq(password))
                return false;
            $("#password").removeClass("is-invalid");
            $("#password").addClass("is-valid");
            $('#passwordErrorMsg').text("");
            return true;
        };
        this.checkPasswordReq = function (password) {
            var eightCharsIsValid;
            var upperCaseDigitIsValid;
            var specialCharsIsValid;
            $("#passwordLengthReq").removeClass("text-muted");
            $("#lettersAndNumbers").removeClass("text-muted");
            $("#specialChars").removeClass("text-muted");
            if (password.length > 8) {
                $("#passwordLengthReq").removeClass("text-danger");
                $("#passwordLengthReq").addClass("text-success");
                eightCharsIsValid = true;
            }
            else {
                $("#passwordLengthReq").removeClass("text-success");
                $("#passwordLengthReq").addClass("text-danger");
                eightCharsIsValid = false;
            }
            if (_this.passUpperCaseRegex.test(password) && _this.digitRegex.test(password)) {
                $("#lettersAndNumbers").removeClass("text-danger");
                $("#lettersAndNumbers").addClass("text-success");
                upperCaseDigitIsValid = true;
            }
            else {
                $("#lettersAndNumbers").removeClass("text-success");
                $("#lettersAndNumbers").addClass("text-danger");
                upperCaseDigitIsValid = false;
            }
            if (_this.specialCharsRegex.test(password)) {
                $("#specialChars").removeClass("text-danger");
                $("#specialChars").addClass("text-success");
                specialCharsIsValid = true;
            }
            else {
                $("#specialChars").removeClass("text-success");
                $("#specialChars").addClass("text-danger");
                specialCharsIsValid = false;
            }
            $("#passwordLengthReq").addClass("text-success");
            $("#lettersAndNumbers").addClass("text-success");
            $("#specialChars").addClass("text-success");
            return eightCharsIsValid && upperCaseDigitIsValid && specialCharsIsValid;
        };
        $("#loginFormSubmit").on("click", function () {
            _this.LoginValidation();
        });
    }
    return Login;
}());
