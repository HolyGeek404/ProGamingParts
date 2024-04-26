class User{
    
    constructor() {
        $("#callDisplayOrders").on("click", this.DisplayOrders);
        $("#callDisplaySettings").on("click", this.DisplaySettings);
    }
    
    public DisplayOrders = (): void => {
        $.post("User/Orders/",(result) => {
           $("#content-container").html(result);
        });
    }

    public DisplaySettings = (): void => {
        $.post("User/Settings/", (result) => {
            $("#content-container").html(result);
        });
    }
}