using GridisBackend.DTOs.Address;
using GridisBackend.DTOs.Bill;
using GridisBackend.DTOs.InstalledMeter;
using GridisBackend.DTOs.Person;

namespace GridisBackend.DTOs.Residence
{
    public class Residence_GET_DTO
    {
        public int Id { get; set; }
        public int? EntranceNumber { get; set; }
        public int? ApartmentNumber { get; set; }
        public int? FloorNumber { get; set; }
        public decimal Size { get; set; }


        public virtual Address_GET_DTO Address { get; set; } = null!;
        public virtual InstalledMeter_GET_DTO InstalledMeter { get; set; } = null!;
        public virtual Person_GET_POST_DTO Resident { get; set; } = null!;
        public virtual ICollection<Bill_GET_DTO> Bills { get; set; }
    }
}
