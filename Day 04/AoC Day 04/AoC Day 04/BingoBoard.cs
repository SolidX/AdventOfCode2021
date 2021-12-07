using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC_Day_04
{
    public class BingoBoardSpace
    {
        public ushort Value { get; set; }
        public bool Marked { get; set; }
    }

    public class BingoBoard
    {
        public Dictionary<ushort, BingoBoardSpace> BoardMembers { get; }
        public List<BingoBoardSpace[]> Board { get; private set; }

        public ushort MarkedSpaces { get; private set; }

        public BingoBoard()
        {
            //Numbers may not appear on all boards. If all numbers in any row or any column of a board are marked, that board wins. (Diagonals don't count.
            Board = new List<BingoBoardSpace[]>(5) {
                new BingoBoardSpace[5],
                new BingoBoardSpace[5],
                new BingoBoardSpace[5],
                new BingoBoardSpace[5],
                new BingoBoardSpace[5]
            };

            BoardMembers = new Dictionary<ushort, BingoBoardSpace>(25);
        }

        public BingoBoard(string[] rows)
        {
            if (rows.Length != 5 || !rows.All(r => r.IsBingoBoardRowString()))
                throw new ArgumentException();
            
            var board = rows.Select(r => r.ToBingoBoardRow().ToArray()).ToList();
            Board = board;
            BoardMembers = new Dictionary<ushort, BingoBoardSpace>(25);

            foreach (var row in Board)
            {
                foreach (var item in row)
                {
                    BoardMembers.Add(item.Value, item);
                }
            }
        }

        public void Mark(ushort number)
        {
            if (BoardMembers.ContainsKey(number))
            {
                BoardMembers[number].Marked = true;
                MarkedSpaces++;
            }
        }

        public bool IsWinner()
        {
            if (MarkedSpaces < 5)
                return false;

            foreach (var row in Board)
            {
                if (row.All(r => r.Marked))
                    return true;
            }

            for (var c = 0; c < Board.Count(); c++)
            {
                var tmp = new List<BingoBoardSpace>();
                foreach (var row in Board)
                    tmp.Add(row[c]);

                if (tmp.All(x => x.Marked))
                    return true;
            }
            
            return false;
        }

        public override string ToString()
        {
            var str = "";

            foreach (var row in Board)
            {
                foreach (var item in row)
                    str += $"{item.Value.ToString("D2")} ";
                str = str.Substring(0, str.Length - 1) + Environment.NewLine;
            }
            
            return str;
        }
    }
}
