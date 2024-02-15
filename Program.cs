using System;
using System.Collections.Generic;
using System.IO;
using QueryBuilder;
using QueryBuilder.Models;

namespace QueryBuilder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Database file path
            string dbPath = @"C:\Users\villa\Desktop\Lab3\QueryBuilder\Data\data.db";

            // File paths for CSV files
            string pokemonCsvPath = @"C:\Users\villa\Desktop\Lab3\QueryBuilder\Models\AllPokemon.csv";
            string bannedGamesCsvPath = @"C:\Users\villa\Desktop\Lab3\QueryBuilder\Models\BannedGames.csv";

            using (var queryBuilder = new QueryBuilder(dbPath))
            {
                Console.WriteLine("Choose below what you want to do by typing it's corresponding number");
                Console.WriteLine("1. Delete all Pokemon records");
                Console.WriteLine("2. Delete all BannedGame records");
                Console.WriteLine("3. Write Pokemon records from CSV");
                Console.WriteLine("4. Write BannedGame records from CSV");
                Console.WriteLine("5. Write a single Pokemon to the database");
                Console.WriteLine("6. Write a single BannedGame to the database");

                // All User choices below (if i have time implement it so it doesn't stop the program when one option is chosen)
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("You did not do what i said to do try again or the program just doesn't work.");
                    return;
                }

                switch (choice)
                {
                    case 1:
                        // Delete all Pokemon records
                        Console.WriteLine("Deleting all Pokemon records..........");
                        queryBuilder.DeleteAll<Pokemon>();
                        Console.WriteLine("All Pokemon records deleted successfully I think, idk go check data.db in SQLite.");
                        break;
                    case 2:
                        // Delete all BannedGame records
                        Console.WriteLine("Deleting all BannedGame records........");
                        queryBuilder.DeleteAll<BannedGame>();
                        Console.WriteLine("All BannedGame records deleted successfully I think, idk go check data.db in SQLite.");
                        break;
                    case 3:
                        // Write Pokemon records from provided csv file AllPokemon.CSV
                        Console.WriteLine("Writing Pokemon records from CSV... Give it like 10 seconds or so depending where this is being run from");
                        List<Pokemon> pokemonList = ReadPokemonFromCsv(pokemonCsvPath);
                        foreach (var pokemon in pokemonList)
                        {
                            queryBuilder.Create(pokemon);
                        }
                        Console.WriteLine("Pokemon records written successfully.");
                        break;
                    case 4:
                        // Write BannedGame records from CSV
                        Console.WriteLine("Writing BannedGame records from CSV... Give it like 10 seconds or so depending where this is being run from");
                        List<BannedGame> bannedGamesList = ReadBannedGamesFromCsv(bannedGamesCsvPath);
                        foreach (var bannedGame in bannedGamesList)
                        {
                            queryBuilder.Create(bannedGame);
                        }
                        Console.WriteLine("BannedGame records written successfully.");
                        break;
                    case 5:
                        // Make your own pokemon OC 
                        WriteSinglePokemon(queryBuilder);
                        break;
                    case 6:
                        // Banish a game from your country
                        WriteSingleBannedGame(queryBuilder);
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        static List<Pokemon> ReadPokemonFromCsv(string filePath)
        {
            List<Pokemon> pokemonList = new List<Pokemon>();

            // Read lines from CSV file
            string[] lines = File.ReadAllLines(filePath);
            // I had it like this cause i was skippin the header line for some reason when i first did this, so i just changed it to 0
            for (int i = 0; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(',');

                Pokemon pokemon = new Pokemon
                {
                    DexNumber = int.Parse(values[0]),
                    Name = values[1],
                    Form = values[2],
                    Type1 = values[3],
                    Type2 = values[4],
                    Total = int.Parse(values[5]),
                    HP = int.Parse(values[6]),
                    Attack = int.Parse(values[7]),
                    Defense = int.Parse(values[8]),
                    SpecialAttack = int.Parse(values[9]),
                    SpecialDefense = int.Parse(values[10]),
                    Speed = int.Parse(values[11]),
                    Generation = int.Parse(values[12])
                };

                pokemonList.Add(pokemon);
            }

            return pokemonList;
        }

        static List<BannedGame> ReadBannedGamesFromCsv(string filePath)
        {
            List<BannedGame> bannedGamesList = new List<BannedGame>();

            // Read lines from CSV file
            string[] lines = File.ReadAllLines(filePath);
            // I had it like this cause i was skippin the header line for some reason when i first did this, so i just changed it to 0
            for (int i = 0; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(',');

                BannedGame bannedGame = new BannedGame
                {
                    Title = values[0],
                    Series = values[1],
                    Country = values[2],
                    Details = values[3]
                    // Add other properties as needed
                };

                bannedGamesList.Add(bannedGame);
            }

            return bannedGamesList;
        }

        static void WriteSinglePokemon(QueryBuilder queryBuilder)
        {
            Console.WriteLine("Enter the details of the OC Pokemon:");

            // Reading the Users OC
            Console.Write("DexNumber: ");
            int dexNumber = int.Parse(Console.ReadLine());

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Form: ");
            string form = Console.ReadLine();

            Console.Write("Type1: ");
            string type1 = Console.ReadLine();

            Console.Write("Type2: ");
            string type2 = Console.ReadLine();

            Console.Write("Total: ");
            int total = int.Parse(Console.ReadLine());

            Console.Write("HP: ");
            int hp = int.Parse(Console.ReadLine());

            Console.Write("Attack: ");
            int attack = int.Parse(Console.ReadLine());

            Console.Write("Defense: ");
            int defense = int.Parse(Console.ReadLine());

            Console.Write("SpecialAttack: ");
            int specialAttack = int.Parse(Console.ReadLine());

            Console.Write("SpecialDefense: ");
            int specialDefense = int.Parse(Console.ReadLine());

            Console.Write("Speed: ");
            int speed = int.Parse(Console.ReadLine());

            Console.Write("Generation: ");
            int generation = int.Parse(Console.ReadLine());

            // Slap them together here like legos
            Pokemon pokemon = new Pokemon
            {
                DexNumber = dexNumber,
                Name = name,
                Form = form,
                Type1 = type1,
                Type2 = type2,
                Total = total,
                HP = hp,
                Attack = attack,
                Defense = defense,
                SpecialAttack = specialAttack,
                SpecialDefense = specialDefense,
                Speed = speed,
                Generation = generation
            };

            // Write the Pokemon object to the database using the .create method in Querybuilder.cs
            queryBuilder.Create(pokemon);
        }

        static void WriteSingleBannedGame(QueryBuilder queryBuilder)
        {
            Console.WriteLine("Enter the details of the Game you wanna ban:");

            // Read user input for BannedGame properties
            Console.Write("Title: ");
            string title = Console.ReadLine();

            Console.Write("Series: ");
            string series = Console.ReadLine();

            Console.Write("Country: ");
            string country = Console.ReadLine();

            Console.Write("Details: ");
            string details = Console.ReadLine();

            // Our new Banned game below 
            BannedGame bannedGame = new BannedGame
            {
                Title = title,
                Series = series,
                Country = country,
                Details = details
            };

            // Write the BannedGame object to the database
            queryBuilder.Create(bannedGame);
        }
    }
}