namespace ProektAleks.Data
{
    public class Category
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public DateTime DateRegister { get; set; }
        public ICollection<Apartment> Apartments { get; set; }
        public ICollection<House> Houses { get; set; }
    }
}
