function GetSetsPriceRanges(url, data) {
    $.ajax({
        url: url,
        type: "POST",
        cache: false,
        async: true,
        data: {
            timeIntervalId: data
        },
        dataType: "html"
    })
        .done(function (result) {
            $(".price-range").html(result);
        });
}