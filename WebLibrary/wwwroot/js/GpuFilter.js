class GpuFilter {
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
        if (quickFilter.length == 0) {
            $(".Filter").trigger("click");
        }
    }
    Filter(selectedFilters) {
        $.post("Gpu/Filter", {
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
            if (selectedFilters.filter(obj => obj.Name == 'Manufactures').length > 0) {
                const manufactures = selectedFilters.filter(obj => obj.Name == "Manufactures");
                const values = manufactures.map(obj => obj.Value);
                const stringToDisplay = values.join(', ');
                $(".filters").append('<div class="rounded d-inline-block p-1 m-1 bg-light"><span class="text-dark font-weight-bold">Producent: </span>' + stringToDisplay + '</div>');
            }
            if (selectedFilters.filter(obj => obj.Name == 'AmdGpuProcessorNames' || obj.Name == 'NvidiaGpuProcessorNames').length > 0) {
                const gpuProcessorLine = selectedFilters.filter(obj => obj.Name == 'AmdGpuProcessorNames' || obj.Name == 'NvidiaGpuProcessorNames');
                const values = gpuProcessorLine.map(obj => obj.Value);
                const stringToDisplay = values.join(', ');
                $(".filters").append('<div class="rounded d-inline-block p-1 m-1 bg-light"><span class="text-dark font-weight-bold">Procesor graficzny: </span>' + stringToDisplay + '</div>');
            }
            if (selectedFilters.filter(obj => obj.Name == 'MemorySizes').length > 0) {
                const memorySize = selectedFilters.filter(obj => obj.Name == "MemorySizes");
                const values = memorySize.map(obj => obj.Value);
                const stringToDisplay = values.join(', ');
                $(".filters").append('<div class="rounded d-inline-block p-1 m-1 bg-light"><span class="text-dark font-weight-bold">Rozmiar pamieci: </span>' + stringToDisplay + '</div>');
            }
            if (selectedFilters.filter(obj => obj.Name == 'MemoryTypes').length > 0) {
                const memoryType = selectedFilters.filter(obj => obj.Name == "MemoryTypes");
                const values = memoryType.map(obj => obj.Value);
                const stringToDisplay = values.join(', ');
                $(".filters").append('<div class="rounded d-inline-block p-1 m-1 bg-light"><span class="text-dark font-weight-bold">Rodzaj pamieci: </span>' + stringToDisplay + "</div>");
            }
            if (selectedFilters.filter(obj => obj.Name == 'Teams').length > 0) {
                const teams = selectedFilters.filter(obj => obj.Name == "Teams");
                const values = teams.map(obj => obj.Value);
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
//# sourceMappingURL=GpuFilter.js.map