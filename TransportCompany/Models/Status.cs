namespace TransportCompany.Models
{
    public class Status
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Truck> Trucks { get; set; } = [];
    }
}
