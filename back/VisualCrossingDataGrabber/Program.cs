// See https://aka.ms/new-console-template for more information

using log4net;
using log4net.Config;
using System.Numerics;
using VisualCrossingDataGrabber;


ILog log = LogManager.GetLogger(typeof(Program));
XmlConfigurator.Configure(new System.IO.FileInfo("log4net.config"));


// Renommer les fichiers si besoin en PowerShell:
// Get-ChildItem -Filter 'temperatures_paris_*.json' | Rename-Item -NewName {$_.name -replace 'paris','bordeaux' }
const string dataPath = "D:\\Development\\Web\\React\\climathistorydata";
const string temperatureDirName = "Temperatures";
const int minYear = 1973;
const int maxYear = 2023;
string[] fullTownNames = new string[] {
    // -------------------------------------
    // FRANCE
    // -------------------------------------
    "agde, france",
    "aix-en-provence, france",
    "ajaccio, france",
    "amiens, france",
    "angers, france",
    "annecy, france",
    "antibes, france",
    "aurillac, france",
    "avignon, france",
    "bayonne, france",
    "bergerac, france",
    "belfort, france",
    "besançon, france",
    "biarritz, france",
    "bordeau, france", // Sain-Martin-de-la-porte, Auvergne-Rhône-Alpes
    "bordeaux, france",
    "bourges, france",
    "brest, france",
    "briançon, france",
    "caen, france",
    "cannes, france",
    "castres, france",
    "chambéry, france",
    "champagnole, france",
    "clermont-ferrand, france",
    "colmar, france",
    "courchevel, france",
    "foix, france",
    "forbach, france",
    "digoin, france",
    "dijon, france",
    "dunkerque, france",
    "fort-de-france, france",
    "grenoble, france",
    "isola, france",
    "la rochelle, france",
    "laruns, france",
    "le havre, france",
    "le mans , france",
    "lembach, france",
    "les abymes, france",
    "les Sables-d'Olonne, france",
    "lille, france",
    "limoges, france",
    "lourdes, france",
    "lyon, france",
    "marseille, france",
    "mérignac, france",
    "metz, france",
    "mont-de-marsan, france",
    "montélimar, france",
    "montpellier, france",
    "mouthe, france",
    "mulhouse, france",
    "nancy, france",
    "nantes, france",
    "narbonne, france",
    "nice, france",
    "nîmes, france",
    "nouméa, france",
    "orleans, france",
    "paris, france",
    "pau, france",
    "poitiers, france",
    "pontarlier, france",
    "quimper, france",
    "reins, france",
    "rennes, france",
    "roubaix, france",
    "rouen, france",
    "saint-étienne, france",
    "saint-jean-de-maurienne, france",
    "saint-nazaire, france",
    "saint-pierre, france",
    "saint-tropez, france",
    "strasbourg, france",
    "toulon, france",
    "toulouse, france",
    "tourcoing, france",
    "tournus, france",
    "tours, france",
    "troyes, france",
    "valence, france",
    "versailles, france",
    "vichy, france",
    // -------------------------------------
    // ESPAGNE ; jusqu'à 40 depuis https://fr.wikipedia.org/wiki/Liste_de_villes_d%27Espagne
    // + quelques villes
    // -------------------------------------
    "albacete, espagne",
    "alcalá de henares, espagne",
    "alcorcón, espagne",
    "alicante, espagne",
    "almeria, espagne",
    "badalone, espagne",
    "barcelone, espagne",
    "bilbao, espagne",
    "burgos, espagne",
    "carthagène, espagne",
    "castellón de la plana, espagne",
    "cordoue, espagne",
    "elche, espagne",
    "fuenlabrada, espagne",
    "getafe, espagne",
    "gijón, espagne",
    "grenade, espagne",
    "jerez de la frontera, espagne",
    "l'hospitalet de llobregat, espagne",
    "la corogne, espagne",
    "las palmas de gran canaria, espagne",
    "leganés, espagne",
    "madrid, espagne",
    "malaga, espagne",
    "móstoles, espagne",
    "murcie, espagne",
    "orense, espagne",
    "oviedo, espagne",
    "palma de majorque, espagne",
    "pampelune, espagne",
    "sabadell, espagne",
    "saint-sébastien, espagne",
    "santa cruz de tenerife, espagne",
    "santander, espagne",
    "saragosse, espagne",
    "séville, espagne",
    "soria, espagne",
    "terrassa, espagne",
    "valence, espagne",
    "valladolid, espagne",
    "vigo, espagne",
    "vitoria-gasteiz, espagne",
    // -------------------------------------
    // ITALIE ; jusqu'à 44 depuis https://fr.wikipedia.org/wiki/Liste_des_villes_d%27Italie_par_nombre_d%27habitants
    // + quelques autres.
    // -------------------------------------
    "arezzo, italie",
    "bari, italie",
    "bologne, italie",
    "bergame, italie",
    "bolzane, italie",
    "brescia, italie",
    "campobasso, italie",
    "cagliari, italie",
    "catane, italie",
    "catanzaro, italie",
    "côme, italie",
    "cosenza, italie",
    "ferrare, italie",
    "forlì, italie",
    "florence, italie",
    "foggia, italie",
    "gênes, italie",
    "giugliano in campania, italie",
    "latina, italie",
    "livourne, italie",
    "massa, italie",
    "messine, italie",
    "milan, italie",
    "monza, italie",
    "modène, italie",
    "naples, italie",
    "padoue, italie",
    "parme, italie",
    "pérouse, italie",
    "pescara, italie",
    "plaisance, italie",
    "prato, italie",
    "ravenne, italie",
    "reggio d'émilie, italie",
    "reggio de calabre, italie",
    "rimini, italie",
    "rome, italie",
    "saint-marin, italie",
    "salerne, italie",
    "sassari, italie",
    "syracuse, italie",
    "tarente, italie",
    "terni, italie",
    "trente, italie",
    "trieste, italie",
    "turin, italie",
    "venise, italie",
    "vérone, italie",
    "vicence, italie",
    // -------------------------------------
    // ALLEMAGNE ; jusqu'à 48 depuis https://fr.wikipedia.org/wiki/Liste_des_plus_grandes_villes_d%27Allemagne
    // -------------------------------------
    "aix-la-chapelle, allemagne",
    "augsbourg, allemagne",
    "berlin, allemagne",
    "bielefeld, allemagne",
    "bochum, allemagne",
    "bonn, allemagne",
    "brême, allemagne",
    "brunswick, allemagne",
    "cassel, allemagne",
    "chemnitz, allemagne",
    "cologne, allemagne",
    "dortmund, allemagne",
    "dresde, allemagne",
    "duisbourg, allemagne",
    "düsseldorf, allemagne",
    "erfurt, allemagne",
    "essen, allemagne",
    "francfort, allemagne",
    "fribourg-en-brisgau, allemagne",
    "gelsenkirchen, allemagne",
    "halle, allemagne",
    "hambourg, allemagne",
    "hamm, allemagne",
    "hagen, allemagne",
    "hanovre, allemagne",
    "karlsruhe, allemagne",
    "kiel, allemagne",
    "krefeld, allemagne",
    "leipzig, allemagne",
    "leverkusen, allemagne",
    "lübeck, allemagne",
    "ludwigshafen, allemagne",
    "magdebourg, allemagne",
    "mannheim, allemagne",
    "mayence, allemagne",
    "mönchengladbach, allemagne",
    "mülheim, allemagne",
    "munich, allemagne",
    "münster, allemagne",
    "nuremberg, allemagne",
    "oberhausen, allemagne",
    "oldenbourg, allemagne",
    "potsdam, allemagne",
    "rostock, allemagne",
    "sarrebruck, allemagne",
    "stuttgart, allemagne",
    "wiesbaden, allemagne",
    "wuppertal, allemagne",
    // -------------------------------------
    // SUISSE ; jusqu'à 15 depuis https://fr.wikipedia.org/wiki/Liste_des_villes_de_Suisse
    // -------------------------------------
    "bâle, suisse",
    "bellinzone, suisse",
    "berne, suisse",
    "bienne, suisse",
    "fribourg, suisse",
    "köniz, suisse",
    "genève, suisse",
    "lausanne, suisse",
    "lucerne, suisse",
    "lugano, suisse",
    "neuchâtel, suisse",
    "saint-gall, suisse",
    "zurich, suisse",
    // -------------------------------------
    // BELGIQUE ; jusqu'à 21 depuis https://fr.wikipedia.org/wiki/Liste_des_communes_de_Belgique_par_population
    // -------------------------------------
    "anvers, belgique",
    "gand, belgique",
    "charleroi, belgique",
    "liège, belgique",
    "bruxelles, belgique",
    "schaerbeek, belgique",
    "anderlecht, belgique",
    "bruges, belgique",
    "namur, belgique",
    "louvain, belgique",
    "molenbeek-saint-jean, belgique",
    "mons, belgique",
    "alost, belgique",
    "ixelles, belgique",
    "malines, belgique",
    "uccle, belgique",
    "la louvière, belgique",
    "saint-nicolas, belgique",
    "hasselt, belgique",
    "courtrai, belgique",
    "ostende, belgique",
    // -------------------------------------
    // USA ; jusqu'à 36 depuis https://fr.wikipedia.org/wiki/Liste_des_villes_les_plus_peupl%C3%A9es_des_%C3%89tats-Unis
    // -------------------------------------
    "anchorage, usa",
    "new york, usa",
    "los angeles, usa",
    "chicago, usa",
    "houston, usa",
    "phoenix, usa",
    "philadelphie, usa",
    "san antonio, usa",
    "san diego, usa",
    "dallas, usa",
    "san josé, usa",
    "austin, usa",
    "jacksonville, usa",
    "fort worth, usa",
    "columbus, usa",
    "indianapolis, usa",
    "charlotte, usa",
    "san francisco, usa",
    "seattle, usa",
    "denver, usa",
    "washington DC, usa",
    "nashville, usa",
    "oklahoma city, usa",
    "el paso, usa",
    "boston, usa",
    "portland, usa",
    "las vegas, usa",
    "détroit, usa",
    "louisville, usa",
    "memphis, usa",
    "baltimore, usa",
    "milwaukee, usa",
    "albuquerque, usa",
    "fresno, usa",
    "tucson, usa",
    "sacramento, usa",
    "kansas city, usa",
    "mesa, usa",
    "atlanta, usa",
    "omaha, usa",
    "colorado springs, usa",
    "raleigh, usa",
    "long beach, usa",
    "virginia beach, usa",
    "miami, usa",
    "oakland, usa",
    "minneapolis, usa",
    "tulsa, usa",
    "bakersfield, usa",
    "wichita, usa",
    "arlington, usa",
    "aurora, usa",
    "la nouvelle-orléans, usa",
    // -------------------------------------
    // Bresil ; jusqu'à 8 depuis https://fr.wikipedia.org/wiki/Liste_de_villes_du_Br%C3%A9sil
    // -------------------------------------
    "são paulo, brésil",
    "rio de janeiro, brésil",
    "brasília, brésil",
    "salvador da bahia, brésil",
    "fortaleza, brésil",
    "belo horizonte, brésil",
    "curitiba, brésil",
    "manaus, brésil",
    // -------------------------------------
    // Japon ; jusqu'à 18 depuis https://fr.wikipedia.org/wiki/Liste_des_villes_du_Japon_par_nombre_d%27habitants
    // -------------------------------------
    "tokyo, japon",
    "yokohama, japon",
    "ōsaka, japon",
    "nagoya, japon",
    "sapporo, japon",
    "fukuoka, japon",
    "kawasaki, japon",
    "kōbe, japon",
    "kyōto, japon",
    "saitama, japon",
    "hiroshima, japon",
    "sendai, japon",
    "chiba, japon",
    "kitakyūshū, japon",
    "sakai, japon",
    "hamamatsu, japon",
    "niigata, japon",
    "kumamoto, japon",
    // -------------------------------------
    // Japon ; jusqu'à 10 depuis https://fr.wikipedia.org/wiki/Liste_des_villes_d%27Australie_par_nombre_d%27habitants
    // -------------------------------------
    "sydney, australie",
    "melbourne, australie",
    "brisbane, australie",
    "perth, australie",
    "adélaïde, australie",
    "gold coast, australie",
    "canberra, australie",
    "newcastle, australie",
    "wollongong, australie",
    "geelong, australie",
    // -------------------------------------
    // CANADA ; jusqu'à 14 depuis https://fr.wikipedia.org/wiki/Villes_du_Canada_par_population
    // -------------------------------------
    "toronto, canada",
    "montréal, canada",
    "calgary, canada",
    "ottawa, canada",
    "edmonton, canada",
    "winnipeg, canada",
    "mississauga, canada",
    "vancouver, canada",
    "brampton, canada",
    "hamilton, canada",
    "surrey, canada",
    "québec, canada",
    "halifax, canada",
    "laval, canada",
    // -------------------------------------
    // POLOGNE ; jusqu'à 15 depuis https://fr.wikipedia.org/wiki/Liste_des_plus_grandes_villes_de_Pologne
    // -------------------------------------
    "varsovie, pologne",
    "cracovie, pologne",
    "łódź, pologne",
    "poznań, pologne",
    "gdańsk, pologne",
    "szczecin, pologne",
    "bydgoszcz, pologne",
    "lublin, pologne",
    "katowice, pologne",
    "białystok, pologne",
    "gdynia, pologne",
    "częstochowa, pologne",
    "radom, pologne",
    "sosnowiec, pologne",
    // -------------------------------------
    // AFRIQUE DU SUD ; jusqu'à 12 depuis https://fr.wikipedia.org/wiki/Liste_de_villes_d%27Afrique_du_Sud
    // -------------------------------------
    "le cap, afrique du sud",
    "durban, afrique du sud",
    "johannesburg, afrique du sud",
    "soweto, afrique du sud",
    "pretoria, afrique du sud",
    "port elizabeth, afrique du sud",
    "pietermaritzburg, afrique du sud",
    "benoni, afrique du sud",
    "tembisa, afrique du sud",
    "vereeniging, afrique du sud",
    "bloemfontein, afrique du sud",
    "boksburg, afrique du sud",
    // -------------------------------------
    // NORVEGE ; depuis https://fr.wikipedia.org/wiki/Liste_de_villes_de_Norv%C3%A8ge
    // -------------------------------------
    "bergen, norvège",
    "oslo, norvège",
    "sandvika, norvège",
    "stavanger, norvège",
    "trondheim, norvège",
    // -------------------------------------
    // SINGAPOUR ; 
    // -------------------------------------
    "singapour, singapour",
    // -------------------------------------
    // HAWAÏ ; 
    // -------------------------------------
    "honolulu, hawaï",
    // -------------------------------------
    // MEXIQUE ; 
    // -------------------------------------
    "mexico, mexique",
    "ecatepec, mexique",
    "guadalajara, mexique",
    "puebla, mexique",
    "juárez, mexique",
    "tijuana, mexique",
    "león, mexique",
    "nezahualcóyotl, mexique",
    // -------------------------------------
    // COLOMBIE ; 
    // -------------------------------------
    "bogota, colombie",
    "medellín, colombie",
    "cali, colombie",
    "barranquilla, colombie",
    // -------------------------------------
    // ARGENTINE ; depuis https://fr.wikipedia.org/wiki/Liste_de_villes_d%27Argentine
    // -------------------------------------
    "buenos aires, argentine",
    "córdoba, argentine",
    "rosario, argentine",
    "mendoza, argentine",
    // -------------------------------------
    // ALGERIE ; depuis https://fr.wikipedia.org/wiki/Liste_de_villes_d%27Alg%C3%A9rie
    // -------------------------------------
    "alger, algérie",
    "oran, algérie",
    "constantine, algérie",
    "annaba, algérie",
    "blida, algérie",
    "batna, algérie",
    "bjelfa, algérie",
    "sétif, algérie",
    "sidi bel abbès, algérie",
    "biskra, algérie",
    // -------------------------------------
    // MAROC ; depuis https://fr.wikipedia.org/wiki/Liste_des_grandes_villes_du_Maroc_par_population
    // -------------------------------------
    "casablanca, maroc",
    "salé, maroc",
    "tanger, maroc",
    "fès, maroc",
    "marrakech, maroc",
    "safi, maroc",
    "meknès, maroc",
    "oujda, maroc",
    "rabat, maroc",
    "témara, maroc",
    "agadir, maroc",
    // -------------------------------------
    // ARABIE SAOUDITE ; depuis https://fr.wikipedia.org/wiki/Liste_des_grandes_villes_du_Maroc_par_population
    // -------------------------------------
    "riyad, arabie saoudite",
    "djeddah, arabie saoudite",
    "la mecque, arabie saoudite",
    "médine, arabie saoudite",
    // -------------------------------------
    // CHINE ; depuis https://fr.wikipedia.org/wiki/Liste_des_villes_de_Chine_par_nombre_d%27habitants
    // -------------------------------------
    "shanghai, chine",
    "pékin, chine",
    "shenzhen, chine",
    "dongguan, chine",
    "tianjin, chine",
    // -------------------------------------
    // INDE ; depuis https://fr.wikipedia.org/wiki/Liste_des_villes_d%27Inde
    // -------------------------------------
    "mumbai, inde",
    "delhi, inde",
    "bengaluru, inde",
    "hyderabad, inde",
    "ahmedabad, inde",
    "chennai, inde",
    // -------------------------------------
    // ROYAUME-UNI ; depuis https://fr.wikipedia.org/wiki/Liste_de_villes_du_Royaume-Uni
    // -------------------------------------
    "londres, royaume-uni",
    "birmingham, royaume-uni",
    "glasgow, royaume-uni",
    "manchester, royaume-uni",
    "édimbourg, royaume-uni",
    "liverpool, royaume-uni",
    "leeds, royaume-uni",
    "bristol, royaume-uni",
    "sheffield, royaume-uni",
    "newcastle, royaume-uni",
    "coventry, royaume-uni",
    "cardiff, royaume-uni",
    "kingston, royaume-uni",
    "bradford, royaume-uni",
    "belfast, royaume-uni",
    "stoke-on-trent, royaume-uni",
    // -------------------------------------
    // IRLANDE ; depuis https://fr.wikipedia.org/wiki/Liste_des_villes_de_l%27%C3%89tat_d%27Irlande
    // -------------------------------------
    "dublin, irlande",
    "cork, irlande",
    "limerick, irlande",
    "galway, irlande",
    "waterford, irlande",
    // -------------------------------------
    // ISLAND ; depuis https://fr.wikipedia.org/wiki/Liste_des_localit%C3%A9s_d%27Islande
    // -------------------------------------
    "reykjavik, island",
    "kópavogur, island",
    "hafnarfjörður, island",
    // -------------------------------------
    // MADAGASCAR ; depuis https://fr.wikipedia.org/wiki/Liste_des_villes_de_Madagascar
    // -------------------------------------
    "tananarive, madagascar",
    // -------------------------------------
    // PORTUGAL ; depuis https://herald-dick-magazine.blogspot.com/2015/06/top-10-des-plus-grandes-villes-du.html
    // -------------------------------------
    "lisbonne, portugal",
    "porto, portugal",
    "vila nova de gaia, portugal",
    "amadora, portugal",
    // -------------------------------------
    // PAYS-BAS ; depuis 
    // -------------------------------------
    "amsterdam, pays-bas",
    "la haye, pays-bas",
    "rotterdam, pays-bas",
    // -------------------------------------
    // GRECE ; depuis https://fr.wikipedia.org/wiki/Liste_de_villes_de_Gr%C3%A8ce
    // -------------------------------------
    "athènes, grèce",
    "thessalonique, grèce",
    "patras, grèce",
    "larissa, grèce",
    // -------------------------------------
    // TUNISIE ; depuis 
    // -------------------------------------
    "tunis, tunisie",
    "sfax, tunisie",
    "sousse, tunisie",
    "kairouan, tunisie",
    "bizerte, tunisie",
    // -------------------------------------
    // MALTE ; depuis https://fr.wikipedia.org/wiki/Liste_des_villes_de_Malte
    // -------------------------------------
    "birkirkara, malte",
    "il-mosta, malte",
    // -------------------------------------
    // ISRAËL ; depuis https://fr.wikipedia.org/wiki/Liste_des_villes_d%27Isra%C3%ABl
    // -------------------------------------
    "jérusalem, israël",
    "tel aviv, israël",
    "haïfa, israël",
    "rishon lezion, israël",
    "holon, israël",
    // -------------------------------------
    // COREE DU SUD ; depuis 
    // -------------------------------------
    "séoul, corée du sud",
    "busan, corée du sud",
    "incheon, corée du sud",
    // -------------------------------------
    // NEPAL ; depuis 
    // -------------------------------------
    "katmandou, népal",
    // -------------------------------------
    // RUSSIE ; depuis 
    // -------------------------------------
    "moscou, russie",
    "saint-pétersbourg, russie",
    "novossibirsk, russie",
    "iekaterinbourg, russie",
    "nijni novgorod, russie",
    "kazan, russie",
    "samara, russie",
    "tcheliabinsk, russie",
    "omsk, russie",
    // -------------------------------------
    // TCHEQUIE; depuis 
    // -------------------------------------
    "prague, tchéquie",
    "brno, tchéquie",
    // -------------------------------------
    // SLOVAQUIE; depuis 
    // -------------------------------------
    "bratislava, slovaquie",
    // -------------------------------------
    // ROUMANIE; depuis 
    // -------------------------------------
    "bucarest, roumanie",
    "cluj, roumanie",
    "constanta, roumanie",
    "timisoara, roumanie",
    // -------------------------------------
    // HONGRIE; depuis 
    // -------------------------------------
    "budapest, hongrie",
    // -------------------------------------
    // MOLDAVIE; depuis 
    // -------------------------------------
    "chisinau, moldavie",
    // -------------------------------------
    // UKRAINE; depuis 
    // -------------------------------------
    "kiev, ukraine",
    "kharkiv, ukraine",
    "odessa, ukraine",
    "dnipro, ukraine",
    "donetsk, ukraine",
    // -------------------------------------
    // SUEDE; depuis 
    // -------------------------------------
    "stockholm, suede",
    "göteborg, suede",
    "kiruna, suede",
    // -------------------------------------
    // DANEMARK; depuis 
    // -------------------------------------
    "copenhague, danemark",
    "aarhus, danemark",
    "odense, danemark",
    // -------------------------------------
    // FINLANDE; depuis 
    // -------------------------------------
    "helsinki, finlande",
    "oulu, finlande",
    "tampere, finlande",
    "rovaniemi, finlande",
    // -------------------------------------
    // GROENLAND; depuis 
    // -------------------------------------
    "nuuk, groenland",
    // -------------------------------------
    // COSTA RICA; depuis 
    // -------------------------------------
    "san josé, costa rica",
    // -------------------------------------
    // NIGERIA; depuis 
    // -------------------------------------
    "abuja, nigeria",
    // -------------------------------------
    // PHILIPPINES; depuis 
    // -------------------------------------
    "quezon city, philippines",
    "manille, philippines",
    "davao, philippines",
    "caloocan, philippines",
    // -------------------------------------
    // NOUVELLE-ZELANDE; depuis https://destination-nouvellezelande.com/villes-incontournables-nouvelle-zelande/
    // -------------------------------------
    "auckland, nouvelle-zélande",
    "christchurch, nouvelle-zélande",
    "wellington, nouvelle-zélande",
    "nelson, nouvelle-zélande",
    "dunedin, nouvelle-zélande",
};

log.Info("Starting...");
VisualCrossingReader reader = new();

try
{
    foreach (string fullTownName in fullTownNames)
    {
        log.Info($"Reading information for {fullTownName}");

        string[] tokens = fullTownName.Split(',');
        if (tokens.Length != 2)
            throw new ArgumentException($"Invalid town name: ${fullTownName}");

        string townName = tokens[0].Trim();
        string countryName = tokens[1].Trim();

        string countryPath = Path.Combine(dataPath, char.ToUpper(countryName[0]) + countryName.Substring(1));
        if (!Directory.Exists(countryPath))
        {
            log.Info($"Creating directory {countryPath}...");
            Directory.CreateDirectory(countryPath);
        }

        // The town directory has its first character upper case
        string townPath = Path.Combine(countryPath, char.ToUpper(townName[0]) + townName.Substring(1));
        if (!Directory.Exists(townPath))
        {
            log.Info($"Creating directory {townPath}...");
            Directory.CreateDirectory(townPath);
        }
        string temperaturePath = Path.Combine(townPath, temperatureDirName);
        if (!Directory.Exists(temperaturePath))
        {
            log.Info($"Creating directory {temperaturePath}...");
            Directory.CreateDirectory(temperaturePath);
        }

        for (int year = minYear; year <= maxYear; year++)
        {
            string fileName = string.Format("temperatures_{0}_{1}.json", townName.ToLower(), year);
            string filePath = Path.Combine(temperaturePath, fileName);
            if (File.Exists(filePath))
            {
                log.Info($"The file {fileName} already exists. Skipping API call...");
                continue;
            }

            log.Info($"Creating the file {filePath}");
            string result = await reader.ReadAsync(year, fullTownName);
            if (result.Length > 0)
            {
                result = result.Insert(1, string.Format("\t\"year\": {0},\r\n", year));

                File.WriteAllText(filePath, result);
                log.Info("    -> File created.");
            }
        }
    }
}
catch (Exception e)
{
    log.Fatal("An fatal error occured.", e);
}

log.Info("Completed. Press a key to close the program.");
Console.ReadKey();

