// See https://aka.ms/new-console-template for more information

using VisualCrossingDataGrabber;

// Renommer les fichiers si besoin en PowerShell:
// Get-ChildItem -Filter 'temperatures_paris_*.json' | Rename-Item -NewName {$_.name -replace 'paris','bordeaux' }
const string dataPath = "D:\\Development\\Web\\React\\climathistorydata";
const string temperatureDirName = "Temperatures";
string[] townNames = new string[] {
    "amiens",
    "avignon",
    "bayonne",
    "belfort",
    "besançon",
    "biarritz",
    "bordeau", // Sain-Martin-de-la-porte, Auvergne-Rhône-Alpes
    "bordeaux",
    "brest",
    "cannes",
    "clermont-ferrand",
    "dijon",
    "lille",
    "lyon",
    "marseille",
    "montpellier",
    "mouthe",
    "mulhouse",
    "nancy",
    "nantes",
    "orleans",
    "paris",
    "pau",
    "poitiers",
    "reins",
    "rennes",
    "rouen",
    "strasbourg", 
    "toulouse",
    "tournus",
    "tours",
};

Console.WriteLine("Starting...");
VisualCrossingReader reader = new();

foreach (string townName in townNames)
{
    Console.WriteLine($"Reading information for {townName}");
    // The town directory has its first character upper case
    string townPath = Path.Combine(dataPath, char.ToUpper(townName[0]) + townName.Substring(1));
    if (!Directory.Exists(townPath))
    {
        Console.WriteLine($"Creating directory {townPath}...");
        Directory.CreateDirectory(townPath);
    }
    string temperaturePath = Path.Combine(townPath, temperatureDirName);
    if (!Directory.Exists(temperaturePath))
    {
        Console.WriteLine($"Creating directory {temperaturePath}...");
        Directory.CreateDirectory(temperaturePath);
    }

    for (int year = 1973; year < 2023; year++)
    {
        string fileName = string.Format("temperatures_{0}_{1}.json", townName, year);
        string filePath = Path.Combine(temperaturePath, fileName);
        if (File.Exists(filePath))
        {
            Console.WriteLine($"The file {fileName} already exists. Skipping API call...");
            continue;
        }
        
        Console.WriteLine($"Creating the file {filePath}");
        string result = await reader.ReadAsync(year, townName);
        if (result.Length > 0)
        {
            result = result.Insert(1, string.Format("\t\"year\": {0},\r\n", year));

            File.WriteAllText(filePath, result);
            Console.WriteLine("    -> File created.");
        }
    }
}

Console.WriteLine("Completed. Press a key to close the program.");
Console.ReadKey();

