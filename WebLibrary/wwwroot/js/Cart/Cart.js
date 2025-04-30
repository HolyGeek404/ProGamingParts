class Cart {
    constructor() {
        this.calculateTotalCartCost();
        $("#AddToCart").on("click", () => {
            this.addToCart();
        });
        $(".Remove").on("click", (event) => {
            const productId = $(event.target).attr("data-bind");
            this.removeFromCart(productId);
        });
    }

    checkHowManyProduct = () => {
        if ($(".product").length === 0) {
            location.reload();
        }
    };

    addToCart = () => {
        $.ajax({
            url: `/CartApi/Add`,
            type: "POST",
            data: JSON.stringify({
                productId: $("#ProductId").val(),
                quantity: $("#Quantity").val(),
                type: $("#Type").val()
            },),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: (result) => {
                var resultParsed = JSON.parse(result);
                if (resultParsed.success) {
                    $("#addingFailMsg").attr("hidden");
                    $("#addingSuccessMsg").removeAttr("hidden");
                }
                else {
                    $("#addingSuccessMsg").attr("hidden");
                    $("#addingFailMsg").removeAttr("hidden");
                }
            }
        });
    };

    calculateTotalCartCost = () => {
        let totalCartCost = 0;
        $(".totalValueOfProduct").each(function () {
            let value = parseInt($(this).val());
            if (!isNaN(value)) {
                totalCartCost += value;
            }
        });
        $("#totalCartCost").text("Razem: " + totalCartCost.toString() + " zł");
    };
    removeFromCart = (productId) => {

        $.ajax({
            url: `/CartApi/Remove/${productId}`,
            type: "DELETE",
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: (result) => {
                var resultParsed = JSON.parse(result);
                if (resultParsed.success) {
                    $("#".concat(productId)).remove();
                    this.checkHowManyProduct();
                    this.calculateTotalCartCost();
                }
                else {
                    $("#addingSuccessMsg").attr("hidden");
                    $("#addingFailMsg").removeAttr("hidden");
                }
            }
        });
    };
}