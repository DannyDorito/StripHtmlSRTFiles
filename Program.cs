using System.Text.RegularExpressions;

namespace StripHtmlSRTFiles;

partial class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide the path to the SRT file as an argument.");
            return;
        }

        var srtFilePath = args[0];

        bool overwriteFile = false;
        if (args.Length > 1)
        {
            if (!bool.TryParse(args[1], out overwriteFile))
            {
                Console.WriteLine("Invalid value for overwriteFile argument. Using default: false.");
            }
        }

        if (srtFilePath is null)
        {
            Console.WriteLine("The provided SRT file path is null.");
            return;
        }
        if (!File.Exists(srtFilePath))
        {
            Console.WriteLine($"The file {srtFilePath} does not exist.");
            return;
        }

        var directoryName = Path.GetDirectoryName(srtFilePath);
        if (directoryName is null)
        {
            Console.WriteLine("Could not determine the directory of the provided SRT file path.");
            return;
        }

        string outputFilePath = Path.Combine(
            directoryName,
            Path.GetFileNameWithoutExtension(srtFilePath) + "_stripped.srt"
        );

        var file = File.ReadAllText(srtFilePath);

        //strip all html tags
        var strippedFile = StripHtmlRegex().Replace(file, string.Empty);
        File.WriteAllText(overwriteFile ? outputFilePath : srtFilePath, strippedFile);
    }


    [GeneratedRegex("<.*?>")]
    private static partial Regex StripHtmlRegex();
}