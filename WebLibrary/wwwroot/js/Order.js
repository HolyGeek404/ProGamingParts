// noinspection JSUnresolvedReference
class Order {
    constructor() {
        this.emailRegex = /^(?=.{1,254}$)(?=.{1,64}@)[-!#$%&'*+/0-9=?A-Z^_`a-z{|}~]+(\.[-!#$%&'*+/0-9=?A-Z^_`a-z{|}~]+)*@[A-Za-z0-9]([A-Za-z0-9-]{0,61}[A-Za-z0-9])?(\.[A-Za-z0-9]([A-Za-z0-9-]{0,61}[A-Za-z0-9])?)*$/;
        this.nameAndSurnameRegex = /[ `!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?~]/;
        this.digitRegex = /.*\d.*/;
        this.postalCodeRegex = /^\d{2}-\d{3}$/;
        this.validateDataBeforeSubmitting = () => {
            if (this.validateUserDataForm() && this.validatePayAndShippingForm()) {
                $("#submit").removeAttr("hidden");
            }
            else {
                $("#submit").attr("hidden", "hidden");
            }
        };
        this.createOrder = () => {
            const userData = new FormData(document.querySelector("#userDataForm"));
            const name = userData.get("name").toString();
            const surname = userData.get("surname").toString();
            const street = userData.get("street").toString();
            const block = userData.get("block").toString();
            const flat = userData.get("flat").toString();
            const city = userData.get("city").toString();
            const postalCode = userData.get("postalCode").toString();
            const pay = $('input[name="payOptionGroup"]:checked').val();
            const shipping = $('input[name="shippingOptionGroup"]:checked').val();
            let productList = [];
            $(".product").each(function () {
                let productId = $(this).attr("data-bind-ProductId");
                let quantity = $(this).attr("data-bind-Quantity");
                let product = {
                    Quantity: Number(quantity),
                    ProductId: Number(productId),
                };
                productList.push(product);
            });
            const formData = {
                name: name,
                surname: surname,
                street: street,
                block: block,
                flat: flat,
                city: city,
                postalCode: postalCode,
                pay: pay,
                shipping: shipping,
                Details: productList
            };
            $.post("Cart/CreateOrder", formData, (result) => {
                if (result.success) {
                    $("#orderFailMsg").attr("hidden");
                    $("#orderSuccessMsg").removeAttr("hidden");
                    setTimeout(() => { window.location.href = "/User"; }, 2000);
                }
                else {
                    $("#orderSuccessMsg").attr("hidden");
                    $("#orderFailMsg").removeAttr("hidden");
                }
            });
        };
        this.showSubmitView = () => {
            $("#summary").removeAttr("hidden");
            $("#summary").addClass("carousel-item");
            this.gatherSubmitUserData();
            $(".carousel-control-next").trigger("click");
        };
        this.gatherSubmitUserData = () => {
            const userData = new FormData(document.querySelector("#userDataForm"));
            const name = userData.get("name").toString();
            const surname = userData.get("surname").toString();
            const street = userData.get("street").toString();
            const block = userData.get("block").toString();
            const flat = userData.get("flat").toString();
            const city = userData.get("city").toString();
            const postalCode = userData.get("postalCode").toString();
            const pay = $('input[name="payOptionGroup"]:checked').val();
            const shipping = $('input[name="shippingOptionGroup"]:checked').val();
            $("#nameAddress").text(name + " " + surname);
            $("#streetAddress").text(street + " " + block + "/" + flat);
            $("#cityAddress").text(city + " " + postalCode);
            $("#payment").text(pay);
            $("#shipping").text(shipping);
        };
        this.validatePayAndShippingForm = () => {
            return this.validatePayForm() && this.validateShippingForm();
        };
        this.validateUserDataForm = () => {
            const userDataForm = new FormData(document.querySelector("#userDataForm"));
            const isNameValid = this.nameValidation(userDataForm.get("name").toString());
            const isSurnameValid = this.surnameValidation(userDataForm.get("surname").toString());
            const isEmailValid = this.emailValidation(userDataForm.get("email").toString());
            const isPostalCodeValid = this.postalCodeValidation(userDataForm.get("postalCode").toString());
            const isblockNumValid = this.blockNumValidation(userDataForm.get("block").toString());
            const isFlatValid = this.flatValidation(userDataForm.get("flat").toString());
            const isStreetValid = this.streetValidation(userDataForm.get("street").toString());
            const isCityValid = this.cityValidation(userDataForm.get("city").toString());
            if (isNameValid && isSurnameValid && isEmailValid && isPostalCodeValid
                && isblockNumValid && isFlatValid && isStreetValid && isCityValid) {
                $(".carousel-control-next").removeAttr("hidden");
                $(".carousel-control-prev").removeAttr("hidden");
                return true;
            }
            else {
                $(".carousel-control-next").attr("hidden", "hidden");
                $(".carousel-control-prev").attr("hidden", "hidden");
                return false;
            }
        };
        // user data form validation
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
        this.postalCodeValidation = (postalCode) => {
            if (!this.postalCodeRegex.test(postalCode)) {
                $("#postalCode").removeClass("is-valid");
                $("#postalCode").addClass("is-invalid");
                return false;
            }
            $("#postalCode").removeClass("is-invalid");
            $("#postalCode").addClass("is-valid");
            return true;
        };
        this.blockNumValidation = (blockNum) => {
            if (blockNum.trim() == "") {
                $("#block").addClass("is-invalid");
                $("#block").removeClass("is-valid");
                return false;
            }
            $("#block").removeClass("is-invalid");
            $("#block").addClass("is-valid");
            return true;
        };
        this.flatValidation = (flat) => {
            if (flat.trim() == "") {
                $("#flat").addClass("is-invalid");
                $("#flat").removeClass("is-valid");
                return false;
            }
            $("#flat").addClass("is-valid");
            $("#flat").removeClass("is-invalid");
            return true;
        };
        this.streetValidation = (street) => {
            if (street.trim() == "") {
                $("#street").addClass("is-invalid");
                $("#street").removeClass("is-valid");
                return false;
            }
            $("#street").addClass("is-valid");
            $("#street").removeClass("is-invalid");
            return true;
        };
        this.cityValidation = (city) => {
            if (city.trim() == "") {
                $("#city").addClass("is-invalid");
                $("#city").removeClass("is-valid");
                return false;
            }
            $("#city").addClass("is-valid");
            $("#city").removeClass("is-invalid");
            return true;
        };
        // pay form validation 
        this.validatePayForm = () => {
            return $('input[name="payOptionGroup"]:checked').val() !== undefined;
        };
        this.validateShippingForm = () => {
            return $('input[name="shippingOptionGroup"]:checked').val() !== undefined;
        };
        $("#userDataForm, #shippingForm, #payForm").on("change", this.validateDataBeforeSubmitting);
        $("#submit").on("click", this.showSubmitView);
        $("#executeOrder").on("click", this.createOrder);
    }
}
//# sourceMappingURL=Order.js.map