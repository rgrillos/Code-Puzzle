namespace CodePuzzle.Entities
{
    public class PersonType(String id, String name, String customFields, List<String> customFieldsDetails)
    {
        public String Id { get; set; } = id;
        public String Name { get; set; } = name;
        public String CustomFields { get; set; } = customFields;
        public List<String> CustomFieldsDetails { get; set; } = customFieldsDetails;
    }
}
