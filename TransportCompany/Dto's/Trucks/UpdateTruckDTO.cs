using System.ComponentModel.DataAnnotations;
using TransportCompany.Dto_s.Branches;
using TransportCompany.Dto_s.Statuses;

namespace TransportCompany.Dto_s.Trucks
{
    public class UpdateTruckDTO
    {
        [Required]
        public string TruckModel { get; set; }
        [Required]

        public string InsuranceName { get; set; }
        [Required]
        public string Owner { get; set; }
        [Required]
        [MinLength(9)]
        public string Mobile { get; set; }
        [Required]  

        public int RouteFromId { get; set; }

        [Required]  
        public int RouteToId { get; set; }
    }
}
