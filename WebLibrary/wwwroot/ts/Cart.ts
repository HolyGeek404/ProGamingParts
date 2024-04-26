declare var $: any;

class Cart {
    
    constructor() {
        this.calculateTotalCartCost();
        
        $("#AddToCart").on("click", () => {
            this.addToCart();
        })
        $(".Remove").on("click", (event: Event) => {
            const productId =  $(event.target).attr("data-bind")
            this.removeFromCart(productId);
        })
    }
    
    removeFromCart = (productId: string): void => {
        $.post("api/Remove",{
            productId: productId
        }, (result) => {
            if (result.success)
            {
               $("#".concat(productId)).remove();
                this.checkHowManyProduct()
                this.calculateTotalCartCost()
            }
            else{
                $("#addingSuccessMsg").attr("hidden");
                $("#addingFailMsg").removeAttr("hidden");
            }
        })
    }
    checkHowManyProduct = (): void => {
        if ($(".product").length === 0) {
            location.reload();
        }
    } 
    addToCart = (): void => {
        $.post("Add",{
            productId: $("#ProductId").val(),
            quantity: $("#Quantity").val()
        }, (result) => {
            if (result.success)
            {
                $("#addingFailMsg").attr("hidden");
                $("#addingSuccessMsg").removeAttr("hidden");
            }
            else{
                $("#addingSuccessMsg").attr("hidden");
                $("#addingFailMsg").removeAttr("hidden");
            }
        })
    }
    calculateTotalCartCost = (): void => {
        let totalCartCost = 0;

        $(".totalValueOfProduct").each(function() {
            let value = parseInt(<string>$(this).val());
            if (!isNaN(value)) {
                totalCartCost += value;
            }
        });

        $("#totalCartCost").text("Razem: "+totalCartCost.toString()+" zł");
    }
}