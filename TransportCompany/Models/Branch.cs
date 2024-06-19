using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportCompany.Models
{
    [Table("branch")]

    public class Branch
    {
        [Key]
        public int Id { get; set; }
        public string Code {  get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;

        public List<User> Users { get; set; } = [];

        [InverseProperty("RouteFrom")]
        public List<Truck?> RoutesFrom { get; set; }
        [InverseProperty("RouteTo")]
        public List<Truck?> RoutesTo { get; set; }

    }
}
