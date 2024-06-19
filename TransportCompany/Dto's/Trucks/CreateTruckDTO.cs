using System.ComponentModel.DataAnnotations;

namespace TransportCompany.Dto_s.Trucks
{
    public class CreateTruckDTO
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
        [Required]
        public int StatusId {  get; set; }
    }
}
