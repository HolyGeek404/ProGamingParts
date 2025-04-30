class Registration {
    constructor() {
        this.emailRegex = /^(?=.{1,254}$)(?=.{1,64}@)[-!#$%&'*+/0-9=?A-Z^_`a-z{|}~]+(\.[-!#$%&'*+/0-9=?A-Z^_`a-z{|}~]+)*@[A-Za-z0-9]([A-Za-z0-9-]{0,61}[A-Za-z0-9])?(\.[A-Za-z0-9]([A-Za-z0-9-]{0,61}[A-Za-z0-9])?)*$/;
        this.nameAndSurnameRegex = /[ `!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?~]/;
        this.passUpperCaseRegex = /.*[A-Z].*/;
        this.digitRegex = /.*\d.*/;
        this.specialCharsRegex = /.*\W.*/;
        this.preValidation = () => {
            // variables preparing
            const registrationFrom = new FormData(document.querySelector("#registrationForm"));
            const hellContract = $("#hellContract");
            const irritatingEmails = $("#irritatingEmails");
            const personalDataSell = $("#personalDataSell");
            //form validation
            const isNameValid = this.nameValidation(registrationFrom.get("name").toString());
            const isSurnameValid = this.surnameValidation(registrationFrom.get("surname").toString());
            const isEmailValid = this.emailValidation(registrationFrom.get("email").toString());
            const isPasswordsValid = this.passwordValidation(registrationFrom.get("password").toString(), registrationFrom.get("password2").toString());
            //checkbox validation
            const areCheckBoxValid = this.checkBoxValidation(hellContract, irritatingEmails, personalDataSell);
            if (this.finalPreValidation(isNameValid, isSurnameValid, isEmailValid, isPasswordsValid, areCheckBoxValid)) {

                $.ajax({
                    url: `/UserApi/registration`,
                    type: "POST",
                    headers: { 'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val() },
                    data: JSON.stringify({
                        FirstName: registrationFrom.get("name").toString(),
                        LastName: registrationFrom.get("surname").toString(),
                        Email: registrationFrom.get("email").toString(),
                        Password: registrationFrom.get("password").toString(),
                        Password2: registrationFrom.get("password2").toString()
                    }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "html",
                    success: (result) => {
                        result = JSON.parse(result);
                        if (result.success) {
                            $("#registrationFailMsg").attr("hidden");
                            $("#registrationSuccessMsg").removeAttr("hidden");
                            setTimeout(() => {
                                window.location.href = "/User";
                            }, 4000);
                        }
                        else {
                            $("#emailErrorMsg").text("Email aktualnie zajety");
                            $("#email").addClass("is-invalid");
                            $("#registrationSuccessMsg").attr("hidden");
                            $("#registrationFailMsg").removeAttr("hidden");
                        }
                    }
                });
            }
            else {
                $("#registrationSuccessMsg").attr("hidden");
                $("#registrationFailMsg").removeAttr("hidden");
            }
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
        this.nameValidation = (name) => {
            if (name.length === 0) {
                $("#nameErrorMsg").text("Imie nie moze byc puste");
                $("#name").addClass("is-invalid");
                return false;
            }
            if (name.length <= 2) {
                $("#nameErrorMsg").text("Imie musi byc dluzsze");
                $("#name").addClass("is-invalid");
                return false;
            }
            if (this.digitRegex.test(name) || this.nameAndSurnameRegex.test(name)) {
                $("#nameErrorMsg").text("Imie nie moze zawierac cyfry ani znakow specjalnych.");
                $("#name").addClass("is-invalid");
                return false;
            }
            $(`#name`).removeClass("is-invalid");
            $('#name').addClass("is-valid");
            $('#nameErrorMsg').text("");
            return true;
        };
        this.surnameValidation = (surname) => {
            if (surname.length === 0) {
                $("#surnameErrorMsg").text("Nazwisko nie moze byc puste");
                $("#surname").addClass("is-invalid");
                return false;
            }
            if (surname.length <= 2) {
                $("#surnameErrorMsg").text("Nazwisko musi byc dluzsze");
                $("#surname").addClass("is-invalid");
                return false;
            }
            if (this.digitRegex.test(surname) || this.nameAndSurnameRegex.test(surname)) {
                $("#surnameErrorMsg").text("Nazwisko nie moze zawierac cyfry ani znakow specjalnych.");
                $("#surname").addClass("is-invalid");
                return false;
            }
            $(`#surname`).removeClass("is-invalid");
            $('#surname').addClass("is-valid");
            $('#surnameErrorMsg').text("");
            return true;
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
        this.passwordValidation = (password, password2) => {
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
            if (!this.checkPasswordReq(password))
                return false;
            if (password !== password2) {
                $("#passwordErrorMsg").text("Hasla musza byc takie same");
                $("#password").addClass("is-invalid");
                $("#password2").addClass("is-invalid");
                return false;
            }
            $(`#password`).removeClass("is-invalid");
            $("#password2").removeClass("is-invalid");
            $(`#password`).addClass("is-valid");
            $("#password2").addClass("is-valid");
            $('#passwordErrorMsg').text("");
            return true;
        };
        this.checkBoxValidation = (hellContract, irritatingEmails, personalDataSell) => {
            let isValid = true;
            if (hellContract.is(":checked")) {
                $("#hellContract").removeClass("text-danger");
                $("#hellContract").addClass("text-success");
            }
            else {
                $("#hellContract").removeClass("text-success");
                $("#hellContract").addClass("text-danger");
                isValid = false;
            }
            if (irritatingEmails.is(":checked")) {
                $("#irritatingEmails").removeClass("text-danger");
                $("#irritatingEmails").addClass("text-success");
            }
            else {
                $("#irritatingEmails").removeClass("text-success");
                $("#irritatingEmails").addClass("text-danger");
                isValid = false;
            }
            if (personalDataSell.is(":checked")) {
                $("#personalDataSell").removeClass("text-danger");
                $("#personalDataSell").addClass("text-success");
            }
            else {
                $("#personalDataSell").removeClass("text-success");
                $("#personalDataSell").addClass("text-danger");
                isValid = false;
            }
            if (isValid) {
                $("#agreementsErrorMsg").text("");
                return isValid;
            }
            else {
                $("#agreementsErrorMsg").text("Wszystkie zgody sa wymagane");
                return isValid;
            }
        };
        this.finalPreValidation = (isNameValid, isSurnameValid, isEmailValid, isPasswordsValid, areCheckBoxValid) => {
            return isNameValid && isSurnameValid && isEmailValid && isPasswordsValid && areCheckBoxValid;
        };
        $("#registrationFormSubmit").on("click", () => {
            this.preValidation();
        });
        $("#password").on("input", () => {
            this.checkPasswordReq($("#password").val().toString());
        });
    }
}
//# sourceMappingURL=Registration.js.map