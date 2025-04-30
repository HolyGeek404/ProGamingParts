var category = "";
class CpuFilter {
    
    constructor(quickFilter,categoryType) {
        category = categoryType;
        $('input[type="checkbox"][name="TeamList"][value="' + quickFilter + '"]').prop('checked', true);
        $(".Filter").on("click", () => {
            const selectedFilters = this.GatherSelectedFilters();
            this.Filter(selectedFilters);
            this.FilterHeaders(selectedFilters);
        });
        $(".clearFilters").on("click", () => {
            $('input[type="checkbox"]').prop('checked', false);
            $('input[type="number"]').val('');
            $(".Filter").trigger("click");
        });
        if (quickFilter.length != 0) {
            this.FilterHeaders(this.GatherSelectedFilters());
        }
    }
    Filter(filterList) {
        $.ajax({
            url: `/ProductApi/${category}/Filter`,
            type: "POST",
            data: JSON.stringify(filterList),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (result) {
                $("#products").html(result);
            }
        });
    }

    FilterHeaders(selectedFilters) {
        $(".filters").empty();
        $(".filters").removeClass("border-bottom");
        if (selectedFilters.length > 0) {
            $(".filters").addClass("border-bottom");
            $(".filters").append('<span class="text-black-50 p-1 m-1 font-weight-bold">Filtry: </span>');
            if (selectedFilters.filter(obj => obj.Name == 'SocketList').length > 0) {
                const SocketList = selectedFilters.filter(obj => obj.Name == "SocketList");
                const values = SocketList.map(obj => obj.Value);
                const stringToDisplay = values.join(', ');
                $(".filters").append('<div class="rounded d-inline-block p-1 m-1 bg-light"><span class="text-dark font-weight-bold">Socket: </span>' + stringToDisplay + '</div>');
            }
            if (selectedFilters.filter(obj => obj.Name == 'ArchitectureList').length > 0) {
                const ArchitectureList = selectedFilters.filter(obj => obj.Name == "ArchitectureList");
                const values = ArchitectureList.map(obj => obj.Value);
                const stringToDisplay = values.join(', ');
                $(".filters").append('<div class="rounded d-inline-block p-1 m-1 bg-light"><span class="text-dark font-weight-bold">Architektura: </span>' + stringToDisplay + '</div>');
            }
            if (selectedFilters.filter(obj => obj.Name == 'UnlockedMultiplier').length > 0) {
                const UnlockedMultiplier = selectedFilters.filter(obj => obj.Name == "UnlockedMultiplier");
                const values = UnlockedMultiplier.map(obj => obj.Value);
                const stringToDisplay = values.join(', ');
                $(".filters").append('<div class="rounded d-inline-block p-1 m-1 bg-light"><span class="text-dark font-weight-bold">Odblokowany mnożnik: </span>' + stringToDisplay + "</div>");
            }
            if (selectedFilters.filter(obj => obj.Name == 'TeamList').length > 0) {
                const TeamList = selectedFilters.filter(obj => obj.Name == "TeamList");
                const values = TeamList.map(obj => obj.Value);
                const stringToDisplay = values.join(', ');
                $(".filters").append('<div class="rounded d-inline-block p-1 m-1 bg-light"><span class="text-dark font-weight-bold">Producent: </span>' + stringToDisplay + "</div>");
            }

            if (selectedFilters.filter(obj => obj.Name == 'PriceMin').length > 0) {
                const TeamList = selectedFilters.filter(obj => obj.Name == "PriceMin");
                const values = TeamList.map(obj => obj.Value);
                const stringToDisplay = values + " zl"
                $(".filters").append('<div class="rounded d-inline-block p-1 m-1 bg-light"><span class="text-dark font-weight-bold">Cena min: </span>' + stringToDisplay + "</div>");
            }

            if (selectedFilters.filter(obj => obj.Name == 'PriceMax').length > 0) {
                const TeamList = selectedFilters.filter(obj => obj.Name == "PriceMax");
                const values = TeamList.map(obj => obj.Value);
                const stringToDisplay = values + " zl"
                $(".filters").append('<div class="rounded d-inline-block p-1 m-1 bg-light"><span class="text-dark font-weight-bold">Cena max: </span>' + stringToDisplay + "</div>");
            }
        }
    }
    GatherSelectedFilters() {
        let paramArray = [];
        let serializeArray = $("#filterForm").serializeArray();
        for (let i = 0; i < serializeArray.length; i++) {
            if (serializeArray[i].value.toString().trim() != '') {
                const param = {
                    Name: serializeArray[i].name,
                    Value: serializeArray[i].value.toString()
                };
                paramArray.push(param);
            }
        }
        return paramArray;
    }
}