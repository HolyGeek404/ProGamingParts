function search() {
    const input = $("#globalSearchInput").val();
    $.ajax({
        url: `HomeApi/Search?input=${input}`,
        type: "GET",
        headers: { 'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val() },
        success: function (result) {
            $("#globalSearchResult").html(result);
        },
        error: function () {
            $("#globalSearchResult").html("<p>Wystąpił błąd podczas przetwarzania żądania.</p>");
        }
    });
}