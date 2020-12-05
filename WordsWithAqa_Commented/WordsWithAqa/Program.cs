// Skeleton Program code for the AQA A Level Paper 1 2018 examination
// this code should be used in conjunction with the Preliminary Material
// written by the AQA Programmer Team
// developed using Visual Studio 2015

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordsWithAqa
{
    class Paper1Alevel2018CS
    {
        // Implementation of a queue
        // This will be used to store the queue of 15 tiles 
        class QueueOfTiles
        {
            private List<string> Contents = new List<string>();
            private int Rear; // Rear pointer - tells us the index of the last item, and how many items are in the queue
            private int MaxSize; // Specifies the size of the queue
            Random Rnd = new Random();

            // Constructor - takes one parameters --> the size of the queue
            public QueueOfTiles(int MaxSize)
            {
                this.MaxSize = MaxSize;
                this.Rear = -1; // Initialise the rear pointer as -1, to indicate there are no items
                for (int Count = 0; Count < this.MaxSize; Count++)
                {
                    Contents.Add(""); // Adds an empty string to the list
                    this.Add(); // Calls the Add method
                }
            }

            // Returns true if the queue is empty, otherwise returns false
            public bool IsEmpty()
            {
                if (this.Rear == -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            // Returns the first item from the queue, and moves all other items forward one in the queue
            public string Remove()
            {
                string Item = "";
                if (IsEmpty()) // If the queue is empty, we will return an empty string
                {
                    return "";
                }
                else
                {
                    Item = Contents[0]; // Retrieve the first item (Which will be returned) and store it in Item
                    for (int Count = 1; Count < Rear + 1; Count++) // Loop through all indexes, starting from 1
                    {
                        Contents[Count - 1] = Contents[Count]; // Moves previous items forward one in the queue (e.g. [0] = [1], [1] = [2]...)
                    }
                    Contents[Rear] = ""; // Overwrite the rear index with an empty string, to clear it (Otherwise it would be a duplicate of [Rear - 1])
                    Rear--; // Reduce the rear pointer by one, as there is now one less item in the queue 
                    return Item;
                }
            }

            // Add a random letter to the end of the queue
            public void Add()
            {
                int RandNo = 0;
                if (Rear < MaxSize - 1) // Check that the queue isn't full
                {
                    RandNo = Rnd.Next(0, 26); // Generates a random number between 0 & 25 (Inclusive)
                    Rear++; // Increase the rear pointer
                    Contents[Rear] = Convert.ToChar(65 + RandNo).ToString(); // Adds the corresponding character to the queue
                    // 65 + RandNo. Eg/ 65 = "A", 66 = "B", 67 = "C" ... 90 = "Z"
                }
            }

            // Outputs the contents of the queue
            public void Show()
            {
                if (Rear != -1) // As long as the queue isn't empty... The same as !IsEmpty()
                {
                    Console.WriteLine();
                    Console.Write("The contents of the queue are: ");
                    foreach (var item in Contents) // Loops through each item in the Contents list
                    {
                        Console.Write(item); // And outputs it - using Console.Write
                    }
                    Console.WriteLine(); // Ends the line
                }
            }
        }

        static void Main(string[] args)
        {
            // Declare the variables and data structures which will be used throughout the program
            List<String> AllowedWords = new List<string>();
            Dictionary<Char, int> TileDictionary = new Dictionary<char, int>();
            int MaxHandSize = 20;
            int MaxTilesPlayed = 50;
            int NoOfEndOfTurnTiles = 3;
            int StartHandSize = 15;
            string Choice = "";
            // Welcomes the user to the game
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("+ Welcome to the WORDS WITH AQA game +");
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("");
            Console.WriteLine("");

            // Loads in the list of allowed words, and the point value for each tile
            LoadAllowedWords(ref AllowedWords);
            CreateTileDictionary(ref TileDictionary);

            // Repeats until the user enters 9 as their choice (Quit)
            while (Choice != "9")
            {
                DisplayMenu();
                Console.Write("Enter your choice: ");
                Choice = Console.ReadLine();
                if (Choice == "1") // Random starting hand
                {
                    PlayGame(AllowedWords, TileDictionary, true, StartHandSize, MaxHandSize, MaxTilesPlayed, NoOfEndOfTurnTiles);
                }
                else if (Choice == "2") // Training starting hand
                {
                    PlayGame(AllowedWords, TileDictionary, false, 15, MaxHandSize, MaxTilesPlayed, NoOfEndOfTurnTiles);
                }
            }
        }

        // Populates the TileDictionary - passed in as a parameter
        private static void CreateTileDictionary(ref Dictionary<char, int> TileDictionary)
        {
            int[] Value1 = { 0, 4, 8, 13, 14, 17, 18, 19 }; // { A, E, I, N, O, R, S, T } - All 1 point letters
            int[] Value2 = { 1, 2, 3, 6, 11, 12, 15, 20 }; //  { B, C, D, G, L, M, P, U } - All 2 point letters
            int[] Value3 = { 5, 7, 10, 21, 22, 24 }; // { F, H, K, V, W, Y } - All 3 point letters
                                                    // If the a letter is not in the above lists, then it is a 5 point letter
            for (int Count = 0; Count < 26; Count++) // Loops from 0 - 25, (All letters in the alphabet)
            {
                if (Array.IndexOf(Value1, Count) > -1) // If the current character is a 1 Point letter...
                {
                    TileDictionary.Add((char)(65 + Count), 1); // Add the char to the dictionary, mapped to an int value of 1
                }
                else if (Array.IndexOf(Value2, Count) > -1) // If the current character is a 2 Point letter...
                {
                    TileDictionary.Add((char)(65 + Count), 2); // Add the char to the dictionary, mapped to an int value of 2
                }
                else if (Array.IndexOf(Value3, Count) > -1) // If the current character is a 3 Point letter...
                {
                    TileDictionary.Add((char)(65 + Count), 3); // Add the char to the dictionary, mapped to an int value of 3
                }
                else // If the current character is not present in any of the arrays, it must be a 5 Point letter
                {
                    TileDictionary.Add((char)(65 + Count), 5); // Add the char to the dictionary, mapped to an int value of 5
                }
            }
        }

        // Outputs all of the letters, along with their corresponding tile values
        private static void DisplayTileValues(Dictionary<char, int> TileDictionary, List<string> AllowedWords)
        {
            Console.WriteLine();
            Console.WriteLine("TILE VALUES");
            Console.WriteLine();
            foreach (var Tile in TileDictionary) // Loop through each item in the Tile Dictionary
            {
                // Tile.Key = The character
                // Tile.Value = The points value
                Console.WriteLine("Points for " + Tile.Key + ": " + Tile.Value); // Example: "Points for A: 1"
            }
            Console.WriteLine();
        }

        // Returns a starting hand of the specified size, collecting tiles from the Tile Queue
        private static string GetStartingHand(QueueOfTiles TileQueue, int StartHandSize)
        {
            string Hand = "";
            for (int Count = 0; Count < StartHandSize; Count++) // Repeat (StartHandSize) times
            {
                Hand = Hand + TileQueue.Remove(); // Add the next item from the Queue to our Hand
                TileQueue.Add(); // Add a new tile to the end of the TileQueue
            }
            return Hand;
        }

        // Reads in the list of allowed words from a text file, and populated the AllowedWords list
        private static void LoadAllowedWords(ref List<string> AllowedWords)
        {
            try // Use a Try/Catch in case an error occurs in loading the file
            {
                StreamReader FileReader = new StreamReader("aqawords.txt");
                while (!FileReader.EndOfStream) // While there are still lines left in the text file
                {
                    // 1) Reads the next line of the text file
                    // 2) Trims any whitespace
                    // 3) Converts the word to uppercase
                    // 4) Adds the word to the AllowedWords list
                    AllowedWords.Add(FileReader.ReadLine().Trim().ToUpper()); 
                }
                FileReader.Close(); // Close the StreamReader one we're done with it
            }
            catch (Exception) // If an error occurs...
            {
                AllowedWords.Clear(); // Clear the list of AllowedWords
            }
        }

        // Checks whether or not it is possible for the user to play the specified Word, using the tiles they currently have
        private static bool CheckWordIsInTiles(string Word, string PlayerTiles)
        {
            bool InTiles = true;
            string CopyOfTiles = PlayerTiles; // Make a copy of the player's current hand, so we can make temporary changes
            for (int Count = 0; Count < Word.Length; Count++) // Loops through each character in the Word
            {
                if (CopyOfTiles.Contains(Word[Count])) // Does the player's hand (CopyOfTiles) contain the current character?
                {
                    // Removes the first instance of the current character from their hand (CopyOfTiles)
                    CopyOfTiles = CopyOfTiles.Remove(CopyOfTiles.IndexOf(Word[Count].ToString()), 1); 
                }
                else // If their hand doesn't contain the necessary character
                {
                    InTiles = false; // Set InTiles to false
                }
            }
            return InTiles;
        }

        // Checks that the word exists in the list of AllowedWords
        private static bool CheckWordIsValid(string Word, List<string> AllowedWords)
        {
            bool ValidWord = false;
            int Count = 0;
            // Performs a Linear Search to establish whether or not the Word exists in AllowedWords
            // Could be replaced with a Binary Search - exam could ask about the theory of this
            while (Count < AllowedWords.Count && !ValidWord)
            {
                if (AllowedWords[Count] == Word)
                {
                    ValidWord = true;
                }
                Count++;
            }
            return ValidWord;
        }

        // Subroutine to add extra (or no) tiles at the end of each round
        private static void AddEndOfTurnTiles(ref QueueOfTiles TileQueue, ref string PlayerTiles, string NewTileChoice, string Choice)
        {
            int NoOfEndOfTurnTiles = 0;
            if (NewTileChoice == "1") // Add a number of tiles equal to the length of the users word
            {
                NoOfEndOfTurnTiles = Choice.Length;
            }
            else if (NewTileChoice == "2") // Add 3 tiles
            {
                NoOfEndOfTurnTiles = 3;
            }
            else // Add a number of tiles equal to the length of the users word + 3
            {
                NoOfEndOfTurnTiles = Choice.Length + 3;
            }
            for (int Count = 0; Count < NoOfEndOfTurnTiles; Count++) // Adds the requested number of tiles to the players hand
            {
                PlayerTiles = PlayerTiles + TileQueue.Remove();
                TileQueue.Add();
            }
        }

        // Fills a player's hands with tiles upto maximum hand size
        private static void FillHandWithTiles(ref QueueOfTiles TileQueue, ref string PlayerTiles, int MaxHandSize)
        {
            while (PlayerTiles.Length <= MaxHandSize)
            {
                PlayerTiles = PlayerTiles + TileQueue.Remove();
                TileQueue.Add();
            }
        }

        // Calculates the score for a word, by adding up the tile values & applying any applicable bonuses
        private static int GetScoreForWord(string Word, Dictionary<char, int> TileDictionary)
        {
            int Score = 0;
            for (int Count = 0; Count < Word.Length; Count++)
            {
                Score = Score + TileDictionary[Word[Count]];
            }
            if (Word.Length > 7)
            {
                Score = Score + 20;
            }
            else if (Word.Length > 5)
            {
                Score = Score + 5;
            }
            return Score;
        }

        // Increases the PlayerTilesPlayed variable bythe length of the word played
        // Removes the letters which have been played from the player's hand
        // Increases the player's score based on the value of the word which was played
        private static void UpdateAfterAllowedWord(string Word, ref string PlayerTiles, ref int PlayerScore, ref int PlayerTilesPlayed, Dictionary<char, int> TileDictionary, List<string> AllowedWords)
        {
            PlayerTilesPlayed = PlayerTilesPlayed + Word.Length;
            foreach (var Letter in Word)
            {
                PlayerTiles = PlayerTiles.Remove(PlayerTiles.IndexOf(Letter), 1);
            }
            PlayerScore = PlayerScore + GetScoreForWord(Word, TileDictionary);
        }

        // At the end of the game, add up value of the player's remaining tiles & subtract it from their score
        private static void UpdateScoreWithPenalty(ref int PlayerScore, string PlayerTiles, Dictionary<char, int> tileDictionary)
        {
            for (int Count = 0; Count < PlayerTiles.Length; Count++)
            {
                PlayerScore = PlayerScore - tileDictionary[PlayerTiles[Count]];
            }
        }

        // Shown when it's the player's turn
        // Asks them what they would like to do, converts their input to uppercase & returns it
        private static string GetChoice()
        {
            string Choice;
            Console.WriteLine();
            Console.WriteLine("Either:");
            Console.WriteLine("     enter the word you would like to play OR");
            Console.WriteLine("     press 1 to display the letter values OR");
            Console.WriteLine("     press 4 to view the tile queue OR");
            Console.WriteLine("     press 7 to view your tiles again OR");
            Console.WriteLine("     press 0 to fill hand and stop the game.");
            Console.Write("> ");
            Choice = Console.ReadLine();
            Console.WriteLine();
            Choice = Choice.ToUpper();
            return Choice;
        }

        // After the user successfully plays a word i.e. word is valid
        // Asks how many tiles they would like to get
        private static string GetNewTileChoice()
        {
            string NewTileChoice = "";
            string[] TileChoice = { "1", "2", "3", "4" };
            // Repeat until they enter a valid TileChoice (1, 2, 3 or 4)
            while (Array.IndexOf(TileChoice, NewTileChoice) == -1)
            {
                Console.WriteLine("Do you want to:");
                Console.WriteLine("     replace the tiles you used (1) OR");
                Console.WriteLine("     get three extra tiles (2) OR");
                Console.WriteLine("     replace the tiles you used and get three extra tiles (3) OR");
                Console.WriteLine("     get no new tiles (4)?");
                Console.Write("> ");
                NewTileChoice = Console.ReadLine();
            }
            return NewTileChoice;
        }

        // Outputs the player's hand
        private static void DisplayTilesInHand(string PlayerTiles)
        {
            Console.WriteLine();
            Console.WriteLine("Your current hand:" + PlayerTiles);
        }

        // 
        private static void HaveTurn(string PlayerName, ref string PlayerTiles, ref int PlayerTilesPlayed, ref int PlayerScore, Dictionary<char, int> TileDictionary, ref QueueOfTiles TileQueue, List<string> AllowedWords, int MaxHandSize, int NoOfEndOfTurnTiles)
        {
            // 1 - Tells the player it's their turn and shows them their hand
            Console.WriteLine();
            Console.WriteLine(PlayerName + " it is your turn.");
            DisplayTilesInHand(PlayerTiles);

            // 2 - Initialises variables
            string NewTileChoice = "2"; // Get three extra tiles. This is default num tiles for incorrect words
            bool ValidChoice = false;
            bool ValidWord = false;
            string Choice = "";
            while (!ValidChoice)
            {
                // 3 - Call GetChoice - sees whether user wants to play a word or view information
                Choice = GetChoice();
                // 4 - If choice is 1, output the value of all tiles
                if (Choice == "1")
                {
                    DisplayTileValues(TileDictionary, AllowedWords);
                }
                // 5 - If choice is 4, outputs contents of the tile queue
                else if (Choice == "4")
                {
                    TileQueue.Show();
                }
                // 6 - If choice is 7, outputs the player's hand
                else if (Choice == "7")
                {
                    DisplayTilesInHand(PlayerTiles);
                }
                // 7 - If choice is 0, fills the player's hand with tiles & set ValidChoice is true
                else if (Choice == "0")
                {
                    ValidChoice = true;
                    FillHandWithTiles(ref TileQueue, ref PlayerTiles, MaxHandSize);
                }
                // 8 - Assume the player is playing a word
                else
                {
                    ValidChoice = true; // Breaks out of the while loop
                    if (Choice.Length == 0) // 9 - If the word has no characters, set ValidWord to false
                    {
                        ValidWord = false;
                    }
                    else // 10 - If the player can't play the word with the tiles they have, set ValidWord false
                    {
                        ValidWord = CheckWordIsInTiles(Choice, PlayerTiles);
                    }
                    if (ValidWord) // 11 - If the word can actually be played with the tiles the user has
                    {
                        ValidWord = CheckWordIsValid(Choice, AllowedWords); // 12 - Checks if the word is in the list of allowed words
                        if (ValidWord) // If it is...
                        {
                            // Calls UpdateAfterAllowedWord which updates the player's score and the tile queue
                            // Finally, calles GetNewTileChoice to ask how many tiles the player wants
                            Console.WriteLine();
                            Console.WriteLine("Valid word");
                            Console.WriteLine();
                            UpdateAfterAllowedWord(Choice, ref PlayerTiles, ref PlayerScore, ref PlayerTilesPlayed, TileDictionary, AllowedWords);
                            NewTileChoice = GetNewTileChoice();
                        }
                    }
                    if (!ValidWord) // If the user enters an invalid word, they lose their turn
                    {
                        Console.WriteLine();
                        Console.WriteLine("Not a valid attempt, you lose your turn.");
                        Console.WriteLine();
                    }
                    if (NewTileChoice != "4") // Choice 4 means don't add extra tiles, if they picked anything else...
                    {
                        // Add the requested number of tiles to the player's hand
                        AddEndOfTurnTiles(ref TileQueue, ref PlayerTiles, NewTileChoice, Choice);
                    }
                    Console.WriteLine();
                    Console.WriteLine("Your word was:" + Choice);
                    Console.WriteLine("Your new score is:" + PlayerScore);
                    Console.WriteLine("You have played " + PlayerTilesPlayed + " tiles so far in this game.");
                }
            }
        }

        // Outputs the winner of the game
        private static void DisplayWinner(int PlayerOneScore, int PlayerTwoScore)
        {
            Console.WriteLine();
            Console.WriteLine("**** GAME OVER! ****");
            Console.WriteLine();
            Console.WriteLine("Player One your score is " + PlayerOneScore);
            Console.WriteLine("Player Two your score is " + PlayerTwoScore);
            if (PlayerOneScore > PlayerTwoScore)
            {
                Console.WriteLine("Player One wins!");
            }
            else if (PlayerTwoScore > PlayerOneScore)
            {
                Console.WriteLine("Player Two wins!");
            }
            else
            {
                Console.WriteLine("It is a draw!");
            }
            Console.WriteLine();
        }


        private static void PlayGame(List<string> AllowedWords, Dictionary<char, int> TileDictionary, bool RandomStart, int StartHandSize, int MaxHandSize, int MaxTilesPlayed, int NoOfEndOfTurnTiles)
        {
            int PlayerOneScore = 50;
            int PlayerTwoScore = 50;
            int PlayerOneTilesPlayed = 0;
            int PlayerTwoTilesPlayed = 0;
            string PlayerOneTiles = "";
            string PlayerTwoTiles = "";
            QueueOfTiles TileQueue = new QueueOfTiles(20);
            if (RandomStart) // If RandomStart is true, generate the starting hand randomly (Option 1)
            {
                PlayerOneTiles = GetStartingHand(TileQueue, StartHandSize);
                PlayerTwoTiles = GetStartingHand(TileQueue, StartHandSize);
            }
            else // Otherwise, load the training game with the pre-determined hands
            {
                PlayerOneTiles = "BTAHANDENONSARJ";
                PlayerTwoTiles = "CELZXIOTNESMUAA";
            }
            // This loop repeats for the duration of the game
            while (PlayerOneTilesPlayed <= MaxTilesPlayed && PlayerTwoTilesPlayed <= MaxTilesPlayed && PlayerOneTiles.Length < MaxHandSize && PlayerTwoTiles.Length < MaxHandSize)
            {
                // Player One has their turn
                HaveTurn("Player One", ref PlayerOneTiles, ref PlayerOneTilesPlayed, ref PlayerOneScore, TileDictionary, ref TileQueue, AllowedWords, MaxHandSize, NoOfEndOfTurnTiles);
                Console.WriteLine();
                Console.WriteLine("Press Enter to continue");
                Console.ReadLine();
                Console.WriteLine();
                // Player Two has their turn
                HaveTurn("Player Two", ref PlayerTwoTiles, ref PlayerTwoTilesPlayed, ref PlayerTwoScore, TileDictionary, ref TileQueue, AllowedWords, MaxHandSize, NoOfEndOfTurnTiles);
            }

            // Calculates the final scores by subtracting the values of each player's hand from the score
            UpdateScoreWithPenalty(ref PlayerOneScore, PlayerOneTiles, TileDictionary);
            UpdateScoreWithPenalty(ref PlayerTwoScore, PlayerTwoTiles, TileDictionary);
            DisplayWinner(PlayerOneScore, PlayerTwoScore);
        }

        // Displays the main menu
        private static void DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("=========");
            Console.WriteLine("MAIN MENU");
            Console.WriteLine("=========");
            Console.WriteLine();
            Console.WriteLine("1. Play game with random start hand");
            Console.WriteLine("2. Play game with training start hand");
            Console.WriteLine("9. Quit");
            Console.WriteLine();
        }
    }
}