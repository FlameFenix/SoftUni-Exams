using System;
using System.Collections.Generic;
using System.Linq;

namespace The_Pianist
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfPieces = int.Parse(Console.ReadLine());

            List<Pieces> piecesList = new List<Pieces>();

            for (int i = 0; i < numberOfPieces; i++)
            {
                string[] piecesInfo = Console.ReadLine().Split("|", StringSplitOptions.RemoveEmptyEntries)
                                                        .ToArray();
                string piece = piecesInfo[0];
                string composer = piecesInfo[1];
                string key = piecesInfo[2];

                Pieces newPiece = new Pieces(piece, composer, key);

                piecesList.Add(newPiece);

            }

            string command = string.Empty;

            while ((command = Console.ReadLine()) != "Stop")
            {
                string[] cmdArgs = command.Split("|", StringSplitOptions.RemoveEmptyEntries)
                                          .ToArray();
                string option = cmdArgs[0];

                string piece = cmdArgs[1];

                Pieces pieceOption = piecesList.FirstOrDefault(x => x.Name == piece);

                if(option == "Add")
                {
                    string composer = cmdArgs[2];
                    string key = cmdArgs[3];
                    if(piecesList.Contains(pieceOption))
                    {
                        Console.WriteLine($"{piece} is already in the collection!");
                    }
                    else
                    {
                        Pieces newPiece = new Pieces(piece, composer, key);
                        piecesList.Add(newPiece);
                        Console.WriteLine($"{piece} by {composer} in {key} added to the collection!");
                    }
                }
                else if(option == "Remove")
                {
                    if(piecesList.Contains(pieceOption))
                    {
                        piecesList.Remove(pieceOption);
                        Console.WriteLine($"Successfully removed {piece}!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid operation! {piece} does not exist in the collection.");
                    }
                }
                else if(option == "ChangeKey")
                {
                    string newKey = cmdArgs[2];
                    if(piecesList.Contains(pieceOption))
                    {
                        Console.WriteLine($"Changed the key of {piece} to {newKey}!");
                        pieceOption.Key = newKey;
                    }
                    else
                    {
                        Console.WriteLine($"Invalid operation! {piece} does not exist in the collection.");
                    }
                }
            }

            foreach (var item in piecesList.OrderBy(x => x.Name).ThenBy(x => x.Composer))
            {
                Console.WriteLine(item.ToString());
            }
        }
    }

    class Pieces
    {
        public Pieces(string name, string composer, string key)
        {
            Name = name;
            Composer = composer;
            Key = key;
        }
        public string Name { get; set; }
        public string Composer { get; set; }
        public string Key { get; set; }

        public override string ToString()
        {
            return $"{Name} -> Composer: {Composer}, Key: {Key}";
        }
    }
}
