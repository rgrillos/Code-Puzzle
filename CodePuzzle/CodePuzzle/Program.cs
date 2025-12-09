using CodePuzzle.Formatters;
using CodePuzzle.Transformers;

public class Program
{
    public static void Main(string[] args)
    {
        String inputString = "(id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)";
        // Example of complex value with comma in it:
        //inputString = "(id, \"lastname, firstname\", email, type(id, name, customFields(c1, c2, c3)), externalId)";
        if (args.Length > 0)
        {
            inputString = args[0];
        }
        else
        {
            Console.WriteLine("Usage: CodePuzzle.exe \"input string\"");
        }

        Console.WriteLine("Using the following input string");
        Console.WriteLine(inputString);
        Console.WriteLine();

        // TODO: Use Dependency Injection and/or a Factory Pattern to instantiate the correct IPersonTransformer class
        // This implementation only supports the single schema in a CSV format, but additional transformers or deserializers can be used
        IPersonTransformer transformer = new CSVPersonTransformer();

        try
        {
            var person = transformer.Transform(inputString);

            List<IPersonFormatter> formatters = new List<IPersonFormatter>() { new StadardFormatter(), new AlternateFormatter() };

            foreach (var formatter in formatters)
            { // This list of formatters could be supplied through a Factory pattern or hardcoded, and can be extended to support additional formats
                var formattedPerson = formatter.FormatPerson(person);
                foreach (var line in formattedPerson)
                {
                    Console.WriteLine(line);
                }
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            // Log exception and re-throw for consumer to handle
            Console.WriteLine(ex.Message);
            throw;
        }

        Console.WriteLine();
    }
}
