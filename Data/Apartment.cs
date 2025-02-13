using System.ComponentModel;

namespace ProektAleks.Data
{
    public class Apartment
    {
        public int Id { get; set; }
        public int CatalogNumberId {  get; set; }
        public string NameApartments {  get; set; }
        public int Floor {  get; set; }
        public int CategoryId {  get; set; }//fk
        public Category Categories { get; set; }//fk category
        public int Quadrature {  get; set; }
        public string Garage {  get; set; }
        public string AddressProperty { get; set; }
        public string Description {  get; set; }
        public decimal Price {  get; set; }
        public DateTime DateRegister { get; set; }
        public ICollection<ReservationApartment> ReservationsApartments { get; set; }

    }
}
