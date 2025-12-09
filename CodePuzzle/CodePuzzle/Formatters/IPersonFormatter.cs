using CodePuzzle.Entities;

namespace CodePuzzle.Formatters
{
    internal interface IPersonFormatter
    {
        public List<String> FormatPerson(Person person);
    }
}
