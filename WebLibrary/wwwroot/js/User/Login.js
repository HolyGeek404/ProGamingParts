class Login {
    constructor() {
        this.emailRegex = /^(?=.{1,254}$)(?=.{1,64}@)[-!#$%&'*+/0-9=?A-Z^_`a-z{|}~]+(\.[-!#$%&'*+/0-9=?A-Z^_`a-z{|}~]+)*@[A-Za-z0-9]([A-Za-z0-9-]{0,61}[A-Za-z0-9])?(\.[A-Za-z0-9]([A-Za-z0-9-]{0,61}[A-Za-z0-9])?)*$/;
        this.passUpperCaseRegex = /.*[A-Z].*/;
        this.digitRegex = /.*\d.*/;
        this.specialCharsRegex = /.*\W.*/;
        this.LoginValidation = () => {
            const loginForm = new FormData(document.querySelector("#loginForm"));
            const isEmailValid = this.emailValidation(loginForm.get("email").toString());
            const isPasswordValid = this.passwordValidation(loginForm.get("password").toString());
            if (isEmailValid && isPasswordValid) {
                $.ajax({
                    url: "/UserApi/login",
                    type: "GET",
                    headers: { 'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val() },
                    data: {
                        email: loginForm.get("email").toString(),
                        password: loginForm.get("password").toString()
                    },
                    success: function (result) {
                        if (result.success) {
                            $("#loginFailMsg").attr("hidden", true);
                            $("#loginSuccessMsg").removeAttr("hidden");
                            setTimeout(function () {
                                if (result.redirectLink.length > 0) {
                                    window.location.href = result.redirectLink;
                                } else {
                                    window.location.href = "/User/";
                                }
                            }, 500);
                        } else {
                            $("#emailErrorMsg").text("Email lub has�o jest nieprawid�owe");
                            $("#email").addClass("is-invalid");
                            $("#loginSuccessMsg").attr("hidden", true);
                            $("#loginFailMsg").removeAttr("hidden");
                        }
                    },
                    error: function (xhr, status, error) {
                        console.log("Wyst�pi� b��d: " + error);
                        $("#loginSuccessMsg").attr("hidden", true);
                        $("#loginFailMsg").removeAttr("hidden");
                        $("#emailErrorMsg").text("Wyst�pi� b��d podczas logowania");
                        $("#email").addClass("is-invalid");
                    }
                });

            }
        };
        this.emailValidation = (email) => {
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
            if (!this.emailRegex.test(email)) {
                $("#emailErrorMsg").text("Email jest nieprawidlowy.");
                $("#email").addClass("is-invalid");
                return false;
            }
            $(`#email`).removeClass("is-invalid");
            $('#email').addClass("is-valid");
            $('#emailErrorMsg').text("");
            return true;
        };
        this.passwordValidation = (password) => {
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
            if (!this.checkPasswordReq(password)) {
                $("#passwordErrorMsg").text("Haslo ma niepoprawny format");
                $("#password").addClass("is-invalid");
                return false;
            }
            $(`#password`).removeClass("is-invalid");
            $(`#password`).addClass("is-valid");
            $('#passwordErrorMsg').text("");
            return true;
        };
        this.checkPasswordReq = (password) => {
            let eightCharsIsValid;
            let upperCaseDigitIsValid;
            let specialCharsIsValid;
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
            if (this.passUpperCaseRegex.test(password) && this.digitRegex.test(password)) {
                $("#lettersAndNumbers").removeClass("text-danger");
                $("#lettersAndNumbers").addClass("text-success");
                upperCaseDigitIsValid = true;
            }
            else {
                $("#lettersAndNumbers").removeClass("text-success");
                $("#lettersAndNumbers").addClass("text-danger");
                upperCaseDigitIsValid = false;
            }
            if (this.specialCharsRegex.test(password)) {
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
        $("#loginFormSubmit").on("click", () => {
            this.LoginValidation();
        });
    }
}
//# sourceMappingURL=Login.js.map