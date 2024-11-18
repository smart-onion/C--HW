namespace hw4.Model
{
    public class User: IEntity
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
