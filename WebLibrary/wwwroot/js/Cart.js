class Cart {
    constructor() {
        this.removeFromCart = (productId) => {
            $.post("api/Remove", {
                productId: productId
            }, (result) => {
                if (result.success) {
                    $("#".concat(productId)).remove();
                    this.checkHowManyProduct();
                    this.calculateTotalCartCost();
                }
                else {
                    $("#addingSuccessMsg").attr("hidden");
                    $("#addingFailMsg").removeAttr("hidden");
                }
            });
        };
        this.checkHowManyProduct = () => {
            if ($(".product").length === 0) {
                location.reload();
            }
        };
        this.addToCart = () => {
            $.post("Add", {
                productId: $("#ProductId").val(),
                quantity: $("#Quantity").val()
            }, (result) => {
                if (result.success) {
                    $("#addingFailMsg").attr("hidden");
                    $("#addingSuccessMsg").removeAttr("hidden");
                }
                else {
                    $("#addingSuccessMsg").attr("hidden");
                    $("#addingFailMsg").removeAttr("hidden");
                }
            });
        };
        this.calculateTotalCartCost = () => {
            let totalCartCost = 0;
            $(".totalValueOfProduct").each(function () {
                let value = parseInt($(this).val());
                if (!isNaN(value)) {
                    totalCartCost += value;
                }
            });
            $("#totalCartCost").text("Razem: " + totalCartCost.toString() + " zł");
        };
        this.calculateTotalCartCost();
        $("#AddToCart").on("click", () => {
            this.addToCart();
        });
        $(".Remove").on("click", (event) => {
            const productId = $(event.target).attr("data-bind");
            this.removeFromCart(productId);
        });
    }
}
//# sourceMappingURL=Cart.js.map