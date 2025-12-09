using CodePuzzle.Entities;
using System.Runtime.CompilerServices;

namespace CodePuzzle.Transformers
{
    public class CSVPersonTransformer : IPersonTransformer
    {
        internal enum PersonFields { Id, Name, Email, Type, ExternalId }
        internal enum PersonTypeFields { Id, Name, CustomFields }

        /// <summary>
        /// Basic Validation of the Input String
        /// </summary>
        /// <param name="input">Input String Format: "(id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)"</param>
        /// <exception cref="ArgumentNullException">Input Null or Empty</exception>
        /// <exception cref="FormatException">Input Not Formatted Correctly</exception>
        public void ValidateInput(String input)
        {
            input = input.Trim();

            if (input == null || input.Length == 0)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (!input.StartsWith("(") || !input.EndsWith(")"))
            {
                throw new FormatException("Input not wrapped in ()");
            }

            if (!input.Contains(","))
            {
                throw new FormatException("Input not Comma Delimited");
            }

            // TODO: Additional basic validations
            // Intentionally left undone to keep the exercise simple
        }

        /// <summary>
        /// Transform the Input string into a Person object, including the PersonType sub-object
        /// Perform more complex validations here, including any field level validations or type casting
        /// </summary>
        /// <param name="input">Input String Format: "(id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)"</param>
        /// <returns>Person object generated from the Input string</returns>
        /// <exception cref="FormatException">Input Not Formatted Correctly</exception>
        public Person Transform(String input)
        {
            input = input.Trim();
            ValidateInput(input);

            var personData = SplitElements(input);
            if (personData.Count != 5)
            {
                throw new FormatException($"Input Missing Person Fields.  5 expected, found {personData.Count}");
            }

            var (personTypeValue, personTypeInput) = SplitNameFromElements(personData[(int)PersonFields.Type]);
            if (personTypeInput == String.Empty)
            {
                throw new FormatException("Input Missing Person Type Data");
            }

            var personTypeData = SplitElements(personTypeInput);
            if (personTypeData.Count != 3)
            {
                throw new FormatException($"Input Missing Person Type Data Fields.  3 expected, found {personTypeData.Count}");
            }

            var (customFieldsValue, customFieldsInput) = SplitNameFromElements(personTypeData[(int)PersonTypeFields.CustomFields]);
            if (customFieldsInput == String.Empty)
            {
                throw new FormatException("Input Missing Person Type Custom Fields");
            }

            var customFieldsData = SplitElements(customFieldsInput); // Support any number of Custom Fields

            // TODO: Add any Field Level Validations around here
            // TODO: Handle any instances where we cast from a String to a different variable type here
            // Intentionally left out due to no requiremnts and to keep the exercise simple

            var personType = new PersonType(personTypeData[(int)PersonTypeFields.Id], 
                personTypeData[(int)PersonTypeFields.Name], 
                customFieldsValue, 
                customFieldsData);

            var person = new Person(personData[(int)PersonFields.Id],
                personData[(int)PersonFields.Name],
                personData[(int)PersonFields.Email],
                personTypeValue,
                personType,
                personData[(int)PersonFields.ExternalId]);

            return person;
        }

        // This logic is intentionally build without an understanding of the actual schema/data model so that it can be easily shared or swapped
        // This could be swapped out with different logic if something other than Comma's and Parentheses are used
        // This logic could be shared by a different transformer that used the same Comma and Parentheses pattern but with different data fields or a different ordering of those fields
        // For simplicity it is placed in this class, but could be moved to a different class (into the base class or leveraged through a factory pattern) as appropriate
        // Alternatively, a hardcoded approach could be taken by building logic to extract each of the fields explicitly based on the schema of the input

        internal List<String> SplitElements(String input)
        {
            var results = new List<String>();

            if (!input.StartsWith("(") || !input.EndsWith(")"))
            {
                throw new FormatException("Elements not wrapped in Parentheses");
            }

            input = input.Substring(1, input.Length - 2).Trim();

            while (input.Length > 0)
            {
                int quoteSkip = 0;
                int commaPos = input.IndexOf(',');
                if (commaPos == -1)
                {
                    results.Add(input.Trim());
                    break;
                }

                if (input.StartsWith("\""))
                {
                    int endQuotePos = input.IndexOf("\"", 1);
                    if (endQuotePos == -1)
                    {
                        throw new FormatException("Input contains Begin Quotes without matching End Quotes");
                    }

                    commaPos = input.IndexOf(',', endQuotePos);
                    quoteSkip = 1;

                    if (commaPos == -1)
                    {
                        results.Add(input.Trim());
                        break;
                    }
                }

                var splitValue = input.Substring(quoteSkip, commaPos - (quoteSkip * 2)).Trim();
                if (splitValue.Contains("\""))
                {
                    throw new FormatException("Elements cannot contain Quotes");
                }

                if (splitValue.Contains('('))
                {
                    int endParen = input.IndexOf(')');
                    if (endParen == -1)
                    {
                        throw new FormatException("Element Contains Open Parenthese without Close Parenthese");
                    }
                    
                    var snippet = input.Substring(0, endParen + 1).Trim();

                    while (snippet.Count(open => open == '(') > snippet.Count(close => close == ')'))
                    {
                        endParen = input.IndexOf(')', endParen + 1);
                        snippet = input.Substring(0, endParen + 1).Trim();
                    }

                    commaPos = input.IndexOf(',', endParen);

                    if (commaPos == -1)
                    {
                        results.Add(input.Substring(0, endParen + 1).Trim());
                        break;
                    }

                    splitValue = input.Substring(0, commaPos).Trim();
                }

                results.Add(splitValue.Trim());
                input = input.Substring(commaPos + 1).Trim();
            }

            return results;
        }

        internal (String, String) SplitNameFromElements(String input)
        {
            if (!input.Contains("(") || !input.Contains(")"))
            {
                // Without both open and close parentheses, fall back to no elements and only a name
                return (input.Replace("(", "").Replace(")", ""), String.Empty);
            }

            if (!input.EndsWith(')'))
            {
                throw new FormatException("Element cannot contain values after a Close Parenthese");
            }

            var name = input.Substring(0, input.IndexOf('(')).Trim();
            var elements = input.Substring(input.IndexOf('(')).Trim();

            return (name, elements);
        }
    }
}
