class User {
    constructor() {
        this.DisplayOrders = () => {
            $.post("User/Orders/", (result) => {
                $("#content-container").html(result);
            });
        };
        this.DisplaySettings = () => {
            $.post("User/Settings/", (result) => {
                $("#content-container").html(result);
            });
        };
        $("#callDisplayOrders").on("click", this.DisplayOrders);
        $("#callDisplaySettings").on("click", this.DisplaySettings);
    }
}
//# sourceMappingURL=User.js.map