using Model.DataTransfer;

namespace Model.Models.General
{
    public class ProductManagmentModel
    {
        public List<ProductManagmentDto> CpuList { get; set; }
        public List<ProductManagmentDto> GpuList { get; set; }
        public List<ProductManagmentDto> CoolerList { get; set; }
    }
}
