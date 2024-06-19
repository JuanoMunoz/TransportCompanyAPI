using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportCompany.Models
{
    public class Truck
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TruckNro { get; set; }
        public string TruckModel { get; set; }

        public string InsuranceName { get; set; }
        public string Owner { get; set; }
        public string Mobile { get; set; }

        public int? RouteFromId { get; set; }
        public int StatusId { get; set; }

        [ForeignKey(nameof(RouteFromId))]
        public Branch RouteFrom {  get; set; }
        public int? RouteToId { get; set; }

        [ForeignKey(nameof(RouteToId))]
        public Branch RouteTo { get; set; }


        [ForeignKey(nameof(StatusId))]
        public Status Status { get; set; }
    }
}
