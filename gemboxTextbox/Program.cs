using GemBox.Document;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        // If using the Professional version, put your serial key below.
        ComponentInfo.SetLicense("FREE-LIMITED-KEY");

        var document = DocumentModel.Load("title.docx");

        var findDummyText = "##################### find #####################";
        var replaceDummyText = "///////////////////// replace ////////////////////////////";
        
        foreach (ContentRange searchedContent in document.Content.Find("%UsingFind%").Reverse())
        {
            searchedContent.LoadText(findDummyText);
        }
        
        document.Content.Replace(new Regex("%UsingReplace%"), range =>
        {
            var format = ((Run)range.Start.Parent).CharacterFormat;
            var run = new Run(document, replaceDummyText) { CharacterFormat = format.Clone() };
            return run.Content;
        });
        
        document.Save("FoundAndReplacedContent.docx");
    }
}