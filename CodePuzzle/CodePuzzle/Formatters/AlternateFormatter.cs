using CodePuzzle.Entities;

namespace CodePuzzle.Formatters
{
    internal class AlternateFormatter : IPersonFormatter
    {
        public List<String> FormatPerson(Person person)
        {
            /*
- email
- externalId
- id
- name
- type
  - customFields
    - c1
    - c2
    - c3
  - id
  - name
             */
            var output = new List<String>();

            output.Add($"- {person.Email}");
            output.Add($"- {person.ExternalId}");
            output.Add($"- {person.Id}");
            output.Add($"- {person.Name}");
            output.Add($"- {person.Type}");
            output.Add($"  - {person.TypeDetails.CustomFields}");
            foreach (var customField in person.TypeDetails.CustomFieldsDetails)
            {
                output.Add($"    - {customField}");
            }
            output.Add($"  - {person.TypeDetails.Id}");
            output.Add($"  - {person.TypeDetails.Name}");

            return output;
        }
    }
}
