namespace ProektAleks.Data
{
    public class ReservationApartment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User Users { get; set; }
        public int ApartmentId {  get; set; }
        public Apartment Apartments { get; set; }
        public string Comment { get; set; }
        public DateTime DateReview { get; set; }
        public DateTime DateChoice { get; set; }
        
    }
}
