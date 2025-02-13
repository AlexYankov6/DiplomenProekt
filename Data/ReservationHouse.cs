namespace ProektAleks.Data
{
    public class ReservationHouse
    {
        public int Id {  get; set; }
        public string UserId {  get; set; }
        public User Users { get; set; }
       public int HouseId {  get; set; }
        public House Houses { get; set; }
        public string Comment {  get; set; }
        public DateTime DateReview { get; set; }
        public DateTime DateChoice {  get; set; }
    }
}
