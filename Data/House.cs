namespace ProektAleks.Data
{
    public class House
    {
        public int Id { get; set; }
        public int CatalogNumberId { get; set; }
        public string NameProperty { get; set; }
        public int Floors { get; set; }
        public int CategoryId { get; set; }//FK m:1
        public Category Categories { get; set; }//m:1
        public int Quadrature { get; set; }
        public int YardSpace {  get; set; }
        public string Annex { get; set; }
        public string Garage { get; set; }
        public string AddressProperty { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime DateRegister { get; set; }
        public ICollection<ReservationHouse> ReservationsHouses { get; set; }
    }
}
