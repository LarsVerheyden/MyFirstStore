using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace MyFirstStore
{
    internal class Program
    {

        static string storeName = "House of cards";
        static string adminPassword = "Lars123";
        static bool adminActive = false;
        static bool stopProgram = false;
        static string userName;

        static string[] itemNames = { "Charizard", "Blastoise", "Venusaur", "Pikachu", "Mew", "Bulbasaur", "Ivysaur", "Squirtle", "Wartortle", "Charmander" };
        static int[] numberOfCards = { 5, 10, 10, 20, 5, 30, 30, 30, 20, 25 };
        static double[] itemPrices = { 19.99, 16.99, 14.99, 5.99, 29.99, 5.99, 5.99, 5.99, 9.99, 8.99 };
        static string[] itemDescription = {
                "Charizard is één van de meest waardevolle en populairste kaarten van onze collectie. Deze vuur type Pokémon is de derde evolutie van Charmander, één van de startpokémon uit de Kanto regio.",
                "Blastoise is één van de populairste water pokémon. Hij is de derde evolutie van Squirtle, één van de startpokémon uit de Kanto regio.",
                "Venusaur is een gras pokémon. Hij is de derde evolutie van Bulbasaur, één van de startpokémon uit de Kanto regio.",
                "Pikachu is de bekendste Pokémon. Hij is de start pokémon van Ash in de serie.",
                "Mew is de eerste Pokémon die bestond. Ook is hij de enige mythische Pokémon in ons aanbod.",
                "Bulbasaur is een gras Pokémon en tevens de eerste evolutie van Venusaur.",
                "Ivysaur is een gras Pokémon, de volgende evolutie van Bulbasaur en de tweede evolutie van Venusaur.",
                "Squirtle is een water Pokémon en de eerste evolutie van Blastoise.",
                "Wartortle is de volgende evolutie van Squirtle en de tweede evolutie van Blastoise.",
                "Charmander is de eerste evolutie van de welgekende Charizard en staat gekend om de vlam aan het einde van zijn staart." };
        static string[] itemRarity = { "Zeldzaam", "Niet veel voorkomend", "Niet veel voorkomend", "Veel voorkomend", "Zeer zeldzaam", "Veel voorkomend", "Veel voorkomend", "Veel voorkomend", "Niet veel voorkomend", "Veel voorkomend" };
        static string[] reviews = {
                "\"Beste pokemonshop in de streek!\" – June Coppens",
                "\"Super snelle service!\" – Dries Verheyden",
                "\"Collectie die ik nergens anders zag.\" – Remco Hofmans",
                "\"Ik kom hier zeker terug voor mijn volgende aankoop!\" – Arne Van Gucht",
                "\"Heel overzichtelijke webshop!\" – Anne Merckx",
                "\"Goede prijzen die ik nergens anders terug vond!\" – Kilian Van Britsom",
                "\"Prijs-kwaliteit is top.\" – Yash Hanssens",
                "\"Heel breed aanbod, voor iedereen wel iets.\" – Jente Meskens",
                "\"Hele snelle levering, de dag erna al in huis.\" – Pieter-Jan Van Capellen",
                "\"Door de uitstekende verpakking kwam alles in perfecte staat aan.\" – Pieter Cleirbaut" };

        static void Main(string[] args)
        {
            ShowLogo();
            Console.WriteLine("\nHallo! Wat is je naam?");
            userName = Console.ReadLine();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Dag {userName}. Welkom in {storeName}. Hier kan je Pokémonkaarten kopen of bekijken.\n");
            Console.ResetColor();

            while (stopProgram == false)
            {

                ShowAllItems();

                ShowMenu();
                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Welke Pokémonkaart wilt u bekijken?");
                        int viewChoice = ItemChoice();
                        ShowItem(viewChoice);
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Welke Pokémonkaart wilt u kopen?");

                        int buyChoice = ItemChoice();
                        Console.Clear();
                        Console.Write("Hoeveel wilt u er kopen? ");
                        int howManyCards = Convert.ToInt32(Console.ReadLine());
                        BuyItem(buyChoice, howManyCards);
                        break;
                    case 3:
                        AdminLogin(userName, adminPassword);
                        break;
                    case 4:
                        Quit();
                        break;
                    case 5:
                        Refill();
                        break;
                    default:
                        Error();
                        break;
                }
            }
        }
        public static void ShowAllItems()
        {
            Console.WriteLine("\nDit is ons aanbod: ");

            for (int i = 0; i < itemNames.Length; i++)
            {
                if (numberOfCards[i] == 0)
                {
                    Console.Write($"Naam kaart: {itemNames[i]}  --- Aantal op voorraad: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("uitverkocht");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"Naam kaart: {itemNames[i]}  --- Aantal op voorraad: {numberOfCards[i]}");
                }
            }
        }
        public static void ShowMenu()
        {
            Console.WriteLine("\nWat wilt u doen?");
            Console.WriteLine("1. item x bekijken");
            Console.WriteLine("2. Item x kopen");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("3. Admin login");
            Console.ResetColor();
            Console.WriteLine("4. Stoppen");
            if (adminActive == true)
            {
                Console.WriteLine("5. Bijvullen");
            }
            Review();
        }
        public static void ShowItem(int itemIndex)
        {
            if (itemIndex < 0 || itemIndex >= itemNames.Length)
            {
                Error();
                return;
            }

            Console.Clear();
            Console.WriteLine($"Naam kaart: {itemNames[itemIndex]}");

            if (numberOfCards[itemIndex] == 0)
            {
                Console.Write($"Aantal kaarten nog op voorraad: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("uitverkocht");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"Aantal kaarten nog op voorraad: {numberOfCards[itemIndex]}");
            }

            Console.WriteLine($"Beschrijving: {itemDescription[itemIndex]}");
            Console.WriteLine($"Zeldzaamheid: {itemRarity[itemIndex]}");
            Console.WriteLine($"Prijs: {itemPrices[itemIndex]} euro.");
            Console.WriteLine("\n(Druk op enter om verder te gaan.)");
            Console.ReadLine();
            Console.Clear();
        }
        public static int ItemChoice()
        {
            for (int i = 0; i < itemNames.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {itemNames[i]}");
            }
            return Convert.ToInt32(Console.ReadLine()) - 1;
        }
        public static void BuyItem(int itemIndex, int howManyCards)
        {
            Console.Clear();

            if (itemIndex < 0 || itemIndex >= itemNames.Length)
            {
                Console.WriteLine("Er is iets misgegaan, u hebt geen kaarten aangekocht.");
                Console.WriteLine("\n(Druk op enter om verder te gaan.)");
                Console.ReadLine();
                Console.Clear();
                return;
            }
            else if (numberOfCards[itemIndex] == 0)
            {
                Console.WriteLine("Onze excuses, deze kaart is helaas uitverkocht.");
                Console.WriteLine("\n(Druk op enter om verder te gaan.)");
                Console.ReadLine();
                Console.Clear();
                return;
            }
            else if (howManyCards > numberOfCards[itemIndex])
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Onze excuses. We hebben slechts {numberOfCards[itemIndex]} kaarten op voorraad.");
                Console.ResetColor();
                Console.WriteLine("\n(Druk op enter om verder te gaan.)");
                Console.ReadLine();
                Console.Clear();
                return;
            }
            else if (howManyCards <= 0)
            {
                Console.WriteLine("U hebt geen kaarten aangekocht");
                Console.WriteLine("\n(Druk op enter om verder te gaan.)");
                Console.ReadLine();
                Console.Clear();
                return;
            }
            else
            {
                numberOfCards[itemIndex] -= howManyCards;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"U hebt {howManyCards} Pokémonkaarten van {itemNames[itemIndex]} aangekocht.");
                Console.ResetColor();

                double totalPrice = howManyCards * itemPrices[itemIndex];
                double btw = totalPrice * 0.21;
                Console.Write($"De totaalprijs voor deze aankoop is {totalPrice:F2} euro, inclusief de BTW komt dit op ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{totalPrice + btw:F2} euro.");
                Console.ResetColor();
                Console.WriteLine("\nDruk op enter om verder te gaan.");
                Console.ReadLine();
                Console.Clear();
            }
        }

        public static void AdminLogin(string userName, string adminPassword)
        {
            Console.Clear();
            Console.Write("Geef het wachtwoord in: ");
            string passwordGuess = Console.ReadLine();
            if (passwordGuess == adminPassword)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nWachtwoord correct! U bent succesvol ingelogd {userName}.");
                Console.ResetColor();
                adminActive = true;
            }
            else
            {
                Console.WriteLine("Ingegeven wachtwoord is fout.");
            }
            Console.WriteLine("\n(Druk op enter om verder te gaan.)");
            Console.ReadLine();
            Console.Clear();
        }
        public static void Quit()
        {
            stopProgram = true;
            Console.Clear();
            Console.WriteLine($"Tot volgende keer {userName}, go catch them all!");
            Console.WriteLine("\n(Druk op enter om af te sluiten.)");
            Console.ReadLine();
            Console.Clear();
        }
        public static void Refill()
        {
            if (adminActive == true)
            {
                Console.Clear();
                Console.WriteLine("Kies een item om bij te vullen: ");

                for (int i = 0; i < itemNames.Length; i++)
                {
                    if (numberOfCards[i] == 0)
                    {
                        Console.Write($"{i + 1}.  Naam kaart: {itemNames[i]}  --- Aantal op voorraad: ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("uitverkocht");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"{i + 1}.  Naam kaart: {itemNames[i]}  --- Aantal op voorraad: {numberOfCards[i]}");
                    }
                }

                int itemChoice = Convert.ToInt32(Console.ReadLine());

                if (itemChoice -1 < 0 || itemChoice - 1 >= itemNames.Length)
                {
                    Error();
                    return;
                }

                Console.Clear();

                Console.Write("Met hoeveel wilt u de hoeveelheid verhogen? ");
                int addition = Convert.ToInt32(Console.ReadLine());
                Console.Clear();

                if (addition <= 0)
                {
                    Console.WriteLine("De hoeveelheid moet groter zijn dan 0.");
                    Console.WriteLine("\n(Druk op enter om verder te gaan.)");
                    Console.ReadLine();
                    Console.Clear();
                    return;
                }

                numberOfCards[itemChoice - 1] = numberOfCards[itemChoice - 1] + addition;
                Console.WriteLine($"Het aantal kaarten van {itemNames[itemChoice - 1]} is verhoogd met {addition} stuks. Er zijn er nu in totaal {numberOfCards[itemChoice - 1]} op voorraad.");
                Console.WriteLine("\n(Druk op enter om verder te gaan.)");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Error();
            }
        }
        public static void ShowLogo()
        {
        string[,] logo = {
        {" "," ", " ", " ", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", " ", " ", " ", " "},
        {" "," ", " ", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", " ", " ", " "},
        {" "," ", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", " ", " "},
        {" ","█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", " "},
        {"█","█", "█", "█", "█", "█", "█", "H", "O", "U", "S", "E", "█", "█", "█", "█", "█", "█", "█"},
        {"█","█", "█", "█", "█", "█", "█", "█", " ", "O", " ", "█", "█", "█", "█", "█", "█", "█", "█"},
        {"█","█", "█", "█", "█", "█", "█", "█", " ", "F", " ", "█", "█", "█", "█", "█", "█", "█", "█"},
        {"█","█", "█", "█", "█", "█", "█", "C", "A", "R", "D", "S", "█", "█", "█", "█", "█", "█", "█"},
        {" ","█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", " "},
        {" "," ", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", " ", " "},
        {" "," ", " ", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", " ", " ", " "},
        {" "," ", " ", " ", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", " ", " ", " ", " "} };

            for (int i = 0; i < logo.GetLength(0); i++)
            {
                for (int j = 0; j < logo.GetLength(1); j++)
                {
                    if (i < 6)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write(logo[i, j]);
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }
        public static void Review()
        {
            Random rand = new Random();
            int randomIndex = rand.Next(reviews.Length);
            Console.WriteLine("\n\n");
            Console.WriteLine(reviews[randomIndex]);
        }
        public static void Error()
        {
            Console.Clear();
            Console.WriteLine("Ongeldige keuze.");
            Console.WriteLine("\n(Druk op enter om verder te gaan.)");
            Console.ReadLine();
            Console.Clear();
        }
    }
}