
namespace HW1
{
    public class Train
    {
        public int Id { get; set; }
        public string UniqNumber { get; set; }
        public string Model { get; set; }
        public string Company { get; set; }
        public int? numberOfCarriage { get; set; }
        public string Cargo { get; set; }

        public void Copy(Train train)
        {
            UniqNumber = train.UniqNumber ?? UniqNumber;
            Model = train.Model ?? Model;
            Company = train.Company ?? Company;
            numberOfCarriage = train.numberOfCarriage ?? numberOfCarriage;
            Cargo = train.Cargo ?? Cargo;

        }
    }
}
