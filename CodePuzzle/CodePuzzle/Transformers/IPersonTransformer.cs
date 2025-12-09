using CodePuzzle.Entities;

namespace CodePuzzle.Transformers
{
    internal interface IPersonTransformer
    {
        public void ValidateInput(String input);
        public Person Transform(String input);
    }
}
