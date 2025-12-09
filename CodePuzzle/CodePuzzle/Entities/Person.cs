namespace CodePuzzle.Entities
{
    public class Person(String id, String name, String email, String type, PersonType typeDetails, String externalId)
    {
        public String Id { get; set; } = id;
        public String Name { get; set; } = name;
        public String Email { get; set; } = email;
        public String Type { get; set; } = type;
        public PersonType TypeDetails { get; set; } = typeDetails;
        public String ExternalId { get; set; } = externalId;
    }
}
