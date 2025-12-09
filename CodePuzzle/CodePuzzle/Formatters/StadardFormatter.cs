using CodePuzzle.Entities;

namespace CodePuzzle.Formatters
{
    internal class StadardFormatter : IPersonFormatter
    {
        public List<String> FormatPerson(Person person)
        {
            /*
- id
- name
- email
- type
  - id
  - name
  - customFields
    - c1
    - c2
    - c3
- externalId
             */
            var output = new List<String>();

            output.Add($"- {person.Id}");
            output.Add($"- {person.Name}");
            output.Add($"- {person.Email}");
            output.Add($"- {person.Type}");
            output.Add($"  - {person.TypeDetails.Id}");
            output.Add($"  - {person.TypeDetails.Name}");
            output.Add($"  - {person.TypeDetails.CustomFields}");
            foreach (var customField in person.TypeDetails.CustomFieldsDetails)
            {
                output.Add($"    - {customField}");
            }
            output.Add($"- {person.ExternalId}");

            return output;
        }
    }
}
