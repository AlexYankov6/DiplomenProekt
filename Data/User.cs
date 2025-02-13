using Microsoft.AspNetCore.Identity;

namespace ProektAleks.Data
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateRegister { get; set; }
        public ICollection<ReservationApartment> ReservationsApartments { get; set; }
        public ICollection<ReservationHouse> ReservationsHouses { get; set; }
    }
}
