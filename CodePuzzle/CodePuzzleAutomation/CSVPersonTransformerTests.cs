using CodePuzzle.Transformers;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace CodePuzzleAutomation
{
    [TestFixture]
    public sealed class CSVPersonTransformerTests
    {
        private CSVPersonTransformer? _transformer;

        [SetUp]
        public void SetUp()
        {
            _transformer = new CSVPersonTransformer();
        }

        [Test]
        public void ValidateInputPasses()
        {
            String inputString = "(id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)";

            _transformer.ValidateInput(inputString);
        }

        [Test]
        public void ValidateEmptyInputFails()
        {
            String inputString = " ";

            Assert.Throws<ArgumentNullException>(() => _transformer.ValidateInput(inputString));
        }

        [Test]
        public void ValidateUnwrappedInputFails()
        {
            String inputString = "id, name, email, type(id, name, customFields(c1, c2, c3)), externalId";

            Assert.Throws<FormatException>(() => _transformer.ValidateInput(inputString));
        }

        [Test]
        public void TransformBadQuotesFails()
        {
            String inputString = "(\"id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)";

            Assert.Throws<FormatException>(() => _transformer.Transform(inputString));
        }

        // TODO: Test rest of validation errors

        [Test]
        public void TransformsCorrectly()
        {
            String inputString = "(id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)";

            var person = _transformer.Transform(inputString);

            Assert.That(person.Id.Equals("id"), "Id not transformed correctly");
            Assert.That(person.Name.Equals("name"), "Name not transformed correctly");

            Assert.That(person.TypeDetails.CustomFieldsDetails.Count == 3, "Custom Fields not transformed correctly");

            // TODO: Test rest of fields
        }
    }
}
