namespace PaperBoy.ContentProcessor;

public class LanguageModelConnectionString(string connectionString)
{
    private Dictionary<string, string> _elements = ParseConnectionString(connectionString);

    private static Dictionary<string,string> ParseConnectionString(string s)
    {
        var results = new Dictionary<string, string>();
        var elements = s.Split(";");

        foreach (var element in elements)
        {
            var elementParts = element.Split("=");
            
            if (elementParts.Length != 2)
            {
                throw new ArgumentException("Invalid connection string");
            }
            
            results.Add(elementParts[0], elementParts[1]);
        }
        
        return results; 
    }

    public string this[string key] => _elements[key];
}