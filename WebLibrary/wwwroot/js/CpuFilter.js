class CpuFilter {
    constructor(quickFilter) {
        $('input[type="checkbox"][name="Teams"][value="' + quickFilter + '"]').prop('checked', true);
        $(".Filter").on("click", () => {
            const selectedFilters = this.GatherSelectedFilters();
            this.Filter(selectedFilters);
            this.FilterHeaders(selectedFilters);
        });
        $(".clearFilters").on("click", () => {
            $('input[type="checkbox"]').prop('checked', false);
            $(".Filter").trigger("click");
        });
        if (quickFilter.length != 0) {
            $(".Filter").trigger("click");
        }
    }
    Filter(selectedFilters) {
        $.post("Cpu/Filter", {
            param: selectedFilters
        }, (result) => {
            $("#products").html(result);
        });
    }
    FilterHeaders(selectedFilters) {
        $(".filters").empty();
        $(".filters").removeClass("border-bottom");
        if (selectedFilters.length > 0) {
            $(".filters").addClass("border-bottom");
            $(".filters").append('<span class="text-black-50 p-1 m-1 font-weight-bold">Filtry: </span>');
            if (selectedFilters.filter(obj => obj.Name == 'Sockets').length > 0) {
                const Sockets = selectedFilters.filter(obj => obj.Name == "Sockets");
                const values = Sockets.map(obj => obj.Value);
                const stringToDisplay = values.join(', ');
                $(".filters").append('<div class="rounded d-inline-block p-1 m-1 bg-light"><span class="text-dark font-weight-bold">Socket: </span>' + stringToDisplay + '</div>');
            }
            if (selectedFilters.filter(obj => obj.Name == 'Architectures').length > 0) {
                const Architectures = selectedFilters.filter(obj => obj.Name == "Architectures");
                const values = Architectures.map(obj => obj.Value);
                const stringToDisplay = values.join(', ');
                $(".filters").append('<div class="rounded d-inline-block p-1 m-1 bg-light"><span class="text-dark font-weight-bold">Architektura: </span>' + stringToDisplay + '</div>');
            }
            if (selectedFilters.filter(obj => obj.Name == 'UnlockedMultiplier').length > 0) {
                const UnlockedMultiplier = selectedFilters.filter(obj => obj.Name == "UnlockedMultiplier");
                const values = UnlockedMultiplier.map(obj => obj.Value);
                const stringToDisplay = values.join(', ');
                $(".filters").append('<div class="rounded d-inline-block p-1 m-1 bg-light"><span class="text-dark font-weight-bold">Odblokowany mnożnik: </span>' + stringToDisplay + "</div>");
            }
            if (selectedFilters.filter(obj => obj.Name == 'Teams').length > 0) {
                const Teams = selectedFilters.filter(obj => obj.Name == "Teams");
                const values = Teams.map(obj => obj.Value);
                const stringToDisplay = values.join(', ');
                $(".filters").append('<div class="rounded d-inline-block p-1 m-1 bg-light"><span class="text-dark font-weight-bold">Producent: </span>' + stringToDisplay + "</div>");
            }
        }
    }
    GatherSelectedFilters() {
        let paramArray = [];
        let serializeArray = $("#filterForm").serializeArray();
        for (let i = 0; i < serializeArray.length; i++) {
            const param = {
                Name: serializeArray[i].name,
                Value: serializeArray[i].value.toString()
            };
            paramArray.push(param);
        }
        return paramArray;
    }
}
//# sourceMappingURL=CpuFilter.js.map