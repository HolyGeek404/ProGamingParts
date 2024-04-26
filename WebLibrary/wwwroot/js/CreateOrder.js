function CreateOrder(url, id) {
    $.ajax({
        url: url,
        type: "POST",
        cache: false,
        async: true,
        data: {
            productId: id
        },
        dataType: "html"
    });
    //.done(function (result) {
    //    console.log("Successful placed order");
    //});
}