// Orders
function DisplayOrders() {
    $.get("/UserApi/Orders/", (result) => {
        $("#content-container").html(result);
    });
};

// Settings
function DisplaySettings() {
    $.get("/UserApi/Settings/", (result) => {
        $("#content-container").html(result);
    });
};
function EditNameAndSurnameSetting() {
    $("#name").prop("disabled", false);
    $("#surname").prop("disabled", false);
    $("#saveNameAndSurnameSetting").prop("hidden", false);

}
function SaveNameAndSurnameSetting() {

    const newName = $("#name").val().trim();
    const newSurname = $("#surname").val().trim();

    if (newName && newSurname) {
        $.ajax({
            url: `/UserApi/SaveNameAndSurnameSetting`,
            type: "PUT",
            data: JSON.stringify({
                    name: newName,
                    surname: newSurname
                }),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: (result) => {
                var resultParsed = JSON.parse(result);
                if (resultParsed.success) {
                    $("#saveNameAndSurnameSetting").prop("hidden", true);
                    $("#nameAndSurnameSuccessMsg").text("Zapisano").fadeIn().fadeOut(1600, "linear");
                    $("#nameAndSurnameErrorMsg").text("");
                    $("#name").prop("disabled", true);
                    $("#surname").prop("disabled", true);

                }
                else {
                    $("#nameAndSurnameSuccessMsg").text("");
                    $("#nameAndSurnameErrorMsg").text("Nie prawidlowe imie lub nazwisko");
                }
            }
        });
    }
    else {
        $("#nameAndSurnameErrorMsg").text("Imie lub nazwisko nie moze byc puste");
    }
}

function EditDeliveryAddressSetting() {
    $("#deliveryName").prop("disabled", false);
    $("#deliverySurname").prop("disabled", false);
    $("#deliveryCity").prop("disabled", false);
    $("#deliveryPostalCode").prop("disabled", false);
    $("#deliveryStreet").prop("disabled", false);
    $("#deliveryBlockNumber").prop("disabled", false);
    $("#deliveryApartmentNumber").prop("disabled", false);
    $("#deliveryEmail").prop("disabled", false);
    $("#saveDeliverySetting").prop("hidden", false);
}

function SaveDeliveryAddress() {
    const newName = $("#deliveryName").val().trim();
    const newSurname = $("#deliverySurname").val().trim();
    const newCity = $("#deliveryCity").val().trim();
    const newPostalCode = $("#deliveryPostalCode").val().trim();
    const newStreet = $("#deliveryStreet").val().trim();
    const newBlockNumber = $("#deliveryBlockNumber").val().trim();
    const newApartmentNumber = $("#deliveryApartmentNumber").val().trim();
    const newEmail = $("#deliveryEmail").val().trim();

    if (newName && newSurname && newCity && newPostalCode && newStreet && newBlockNumber && newApartmentNumber && newEmail) {
        $.ajax({
            url: `/UserApi/SaveDeliveryAddress`,
            type: "POST",
            headers: { 'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val() },
            data: JSON.stringify({
                name: newName,
                surname: newSurname,
                city: newCity,
                postalCode: newPostalCode,
                street: newStreet,
                blockNumber: newBlockNumber,
                apartmentNumber: newApartmentNumber,
                email: newEmail
            }),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: (result) => {
                result = JSON.parse(result);
                if (result.success) {
                    $("#saveDeliverySetting").prop("hidden", true);
                    $("#deliverySaveFailed").prop("hidden", true);
                    $("#deliverySaveSuccess").text("Zapisano").fadeIn().fadeOut(1600, "linear");
                }
                else {
                    $("#deliverySaveFailed").text("Nie prawidlowe dane");
                }
            }
        });
    }
}



function EditEmailSetting() {
    $("#email").prop("disabled", false);
    $("#saveEmailSetting").prop("hidden", false);

}
function SaveEmailSetting() {

    const newEmail = $("#email").val().trim();

    if (newEmail) {

        $.ajax({
            url: `/UserApi/SaveEmailSetting`,
            type: "PUT",
            data: JSON.stringify(newEmail),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: (result) => {
                var resultParsed = JSON.parse(result);
                if (resultParsed.success) {
                    $("#saveEmailSetting").prop("hidden", true);
                    $("#emailSuccessMsg").text("Zapisano").fadeIn().fadeOut(1600, "linear");
                    $("#emailErrorMsg").text("");
                    $("#email").prop("disabled", true);
                }
                else {
                    $("#emailSuccessMsg").text("");
                    $("#emailErrorMsg").text("Nie prawidlowy mail");
                }
            }
        });
    }
    else {
        $("#emailErrorMsg").text("Email nie moze byc pusty");
    }
}

function EditPasswordSetting() {
    $("#password").prop("disabled", false);
    $("#password2").prop("disabled", false);
    $("#savePasswordSetting").prop("hidden", false);

}
function SavePasswordSetting() {

    const password = $("#password").val().trim();
    const password2 = $("#password2").val().trim();

    if (password && password2) {

        $.ajax({
            url: `/UserApi/SavePasswordSetting`,
            type: "PUT",
            data: JSON.stringify({
                password1: password,
                password2: password2
            }),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: (result) => {
                var resultParsed = JSON.parse(result);
                if (resultParsed.success) {
                    $("#savePasswordSetting").prop("hidden", true);
                    $("#passwordSuccessMsg").text("Zapisano").fadeIn().fadeOut(1600, "linear");
                    $("#passwordErrorMsg").text("");
                    $("#email").prop("disabled", true);
                }
                else {
                    $("#passwordSuccessMsg").text("");
                    $("#passwordErrorMsg").text("Haslo nie spelnia wymagan");
                }
            }
        });
    }
    else {
        $("#passwordErrorMsg").text("Oba hasla nie moga byc puste");
    }
}

function validateDeliveryAddressForm()  {
    const deliveryAddressForm = new FormData(document.querySelector("#userDeliveryAddressForm"));
    const isNameValid = this.nameValidation(deliveryAddressForm.get("deliveryName").toString());
    const isSurnameValid = this.surnameValidation(deliveryAddressForm.get("deliverySurname").toString());
    const isEmailValid = this.emailValidation(deliveryAddressForm.get("deliveryEmail").toString());
    const isPostalCodeValid = this.postalCodeValidation(deliveryAddressForm.get("deliveryPostalCode").toString());
    const isBlockNumValid = this.blockNumValidation(deliveryAddressForm.get("deliveryBlockNumber").toString());
    const isFlatValid = this.flatValidation(deliveryAddressForm.get("deliveryApartmentNumber").toString());
    const isStreetValid = this.streetValidation(deliveryAddressForm.get("deliveryStreet").toString());
    const isCityValid = this.cityValidation(deliveryAddressForm.get("deliveryCity").toString());

    if (isNameValid && isSurnameValid && isEmailValid && isPostalCodeValid
        && isBlockNumValid && isFlatValid && isStreetValid && isCityValid)
    {
        return true;
    }
    else
    {
        return false;
    }
};

// Delivery address form validation
function nameValidation(name) {
    if (name.length === 0) {
        $("#deliveryNameErrorMsg").text("Imiê nie mo¿e byæ puste");
        $("#deliveryName").addClass("is-invalid");
        return false;
    }
    if (name.length <= 2) {
        $("#deliveryNameErrorMsg").text("Imiê musi byæ d³u¿sze");
        $("#deliveryName").addClass("is-invalid");
        return false;
    }
    if (this.digitRegex.test(name) || this.nameAndSurnameRegex.test(name)) {
        $("#deliveryNameErrorMsg").text("Imiê nie mo¿e zawieraæ cyfry ani znaków specjalnych.");
        $("#deliveryName").addClass("is-invalid");
        return false;
    }
    $(`#deliveryName`).removeClass("is-invalid");
    $('#deliveryName').addClass("is-valid");
    $('#deliveryNameErrorMsg').text("");
    return true;
};

function surnameValidation(surname) {
    if (surname.length === 0) {
        $("#deliverySurnameErrorMsg").text("Nazwisko nie mo¿e byæ puste");
        $("#deliverySurname").addClass("is-invalid");
        return false;
    }
    if (surname.length <= 2) {
        $("#deliverySurnameErrorMsg").text("Nazwisko musi byæ d³u¿sze");
        $("#deliverySurname").addClass("is-invalid");
        return false;
    }
    if (this.digitRegex.test(surname) || this.nameAndSurnameRegex.test(surname)) {
        $("#deliverySurnameErrorMsg").text("Nazwisko nie mo¿e zawieraæ cyfry ani znaków specjalnych.");
        $("#deliverySurname").addClass("is-invalid");
        return false;
    }
    $(`#deliverySurname`).removeClass("is-invalid");
    $('#deliverySurname').addClass("is-valid");
    $('#deliverySurnameErrorMsg').text("");
    return true;
};

function emailValidation(email) {
    if (email.length === 0) {
        $("#deliveryEmailErrorMsg").text("Email nie mo¿e byæ pusty");
        $("#deliveryEmail").addClass("is-invalid");
        return false;
    }
    if (email.length <= 2) {
        $("#deliveryEmailErrorMsg").text("Email musi byæ d³u¿szy");
        $("#deliveryEmail").addClass("is-invalid");
        return false;
    }
    if (!this.emailRegex.test(email)) {
        $("#deliveryEmailErrorMsg").text("Email jest nieprawid³owy.");
        $("#deliveryEmail").addClass("is-invalid");
        return false;
    }
    $(`#deliveryEmail`).removeClass("is-invalid");
    $('#deliveryEmail').addClass("is-valid");
    $('#deliveryEmailErrorMsg').text("");
    return true;
};

function postalCodeValidation(postalCode) {
    if (!this.postalCodeRegex.test(postalCode)) {
        $("#deliveryPostalCode").removeClass("is-valid");
        $("#deliveryPostalCode").addClass("is-invalid");
        return false;
    }
    $("#deliveryPostalCode").removeClass("is-invalid");
    $("#deliveryPostalCode").addClass("is-valid");
    return true;
};

function blockNumValidation(blockNum) {
    if (blockNum.trim() == "") {
        $("#deliveryBlockNumber").addClass("is-invalid");
        $("#deliveryBlockNumber").removeClass("is-valid");
        return false;
    }
    $("#deliveryBlockNumber").removeClass("is-invalid");
    $("#deliveryBlockNumber").addClass("is-valid");
    return true;
};

function flatValidation(flat) {
    if (flat.trim() == "") {
        $("#deliveryApartmentNumber").addClass("is-invalid");
        $("#deliveryApartmentNumber").removeClass("is-valid");
        return false;
    }
    $("#deliveryApartmentNumber").addClass("is-valid");
    $("#deliveryApartmentNumber").removeClass("is-invalid");
    return true;
};

function streetValidation(street) {
    if (street.trim() == "") {
        $("#deliveryStreet").addClass("is-invalid");
        $("#deliveryStreet").removeClass("is-valid");
        return false;
    }
    $("#deliveryStreet").addClass("is-valid");
    $("#deliveryStreet").removeClass("is-invalid");
    return true;
};

function cityValidation(city) {
    if (city.trim() == "") {
        $("#deliveryCity").addClass("is-invalid");
        $("#deliveryCity").removeClass("is-valid");
        return false;
    }
    $("#deliveryCity").addClass("is-valid");
    $("#deliveryCity").removeClass("is-invalid");
    return true;
};