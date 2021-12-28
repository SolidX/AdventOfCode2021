using System.Collections.Generic;
using System.Linq;

namespace AoC_Day_20
{
    public static class ImageExtensions
    {
        public static readonly char LIGHT_PX = '#';
        public static readonly char DARK_PX = '.';

        public static char CalculatePixelValue(this List<List<char>> image, Coordinate pt, string enhancementAlgo, char padWith)
        {
            var minX = 0;
            var minY = 0;
            var maxX = image[0].Count;
            var maxY = image.Count;
            var bitString = new List<bool>();
            var defaultBit = padWith == DARK_PX ? false : true;

            //Top Row
            if (pt.Y - 1 >= minY && pt.Y - 1 < maxY)
            {
                if (pt.X - 1 >= minX && pt.X - 1 < maxX)
                    bitString.Add(image[pt.Y - 1][pt.X - 1] == LIGHT_PX);
                else
                    bitString.Add(defaultBit);

                if (pt.X >= minX && pt.X < maxX)
                    bitString.Add(image[pt.Y - 1][pt.X] == LIGHT_PX);
                else
                    bitString.Add(defaultBit);

                if (pt.X + 1 >= minX && pt.X + 1 < maxX)
                    bitString.Add(image[pt.Y - 1][pt.X + 1] == LIGHT_PX);
                else
                    bitString.Add(defaultBit);
            }
            else
            {
                bitString.AddRange(Enumerable.Repeat(false, 3));
            }

            //Middle Row
            if (pt.Y >= minY && pt.Y < maxY)
            {
                if (pt.X - 1 >= minX && pt.X - 1 < maxX)
                    bitString.Add(image[pt.Y][pt.X - 1] == LIGHT_PX);
                else
                    bitString.Add(defaultBit);

                if (pt.X >= minX && pt.X < maxX)
                    bitString.Add(image[pt.Y][pt.X] == LIGHT_PX);
                else
                    bitString.Add(defaultBit);

                if (pt.X + 1 >= minX && pt.X + 1 < maxX)
                    bitString.Add(image[pt.Y][pt.X + 1] == LIGHT_PX);
                else
                    bitString.Add(defaultBit);
            }
            else
            {
                bitString.AddRange(Enumerable.Repeat(defaultBit, 3));
            }

            //Bottom Row
            if (pt.Y + 1 >= minY && pt.Y + 1 < maxY)
            {
                if (pt.X - 1 >= minX && pt.X - 1 < maxX)
                    bitString.Add(image[pt.Y + 1][pt.X - 1] == LIGHT_PX);
                else
                    bitString.Add(defaultBit);

                if (pt.X >= minX && pt.X < maxX)
                    bitString.Add(image[pt.Y + 1][pt.X] == LIGHT_PX);
                else
                    bitString.Add(defaultBit);

                if (pt.X + 1 >= minX && pt.X + 1 < maxX)
                    bitString.Add(image[pt.Y + 1][pt.X + 1] == LIGHT_PX);
                else
                    bitString.Add(defaultBit);
            }
            else
            {
                bitString.AddRange(Enumerable.Repeat(defaultBit, 3));
            }

            var idx = (int)bitString.ToBytes().ToUInt64();

            return enhancementAlgo[idx];
        }

        public static List<List<char>> Enhance(this List<List<char>> image, string enhancementAlgo, char padWith)
        {
            var padding = 0;
            var newMaxX = image[0].Count + padding;
            var newMaxY = image.Count + padding;

            var output = new List<List<char>>(newMaxY + padding);

            for (int y = (padding * -1); y < newMaxY; y++)
            {
                var row = new List<char>(newMaxX + padding);
                for (int x = (padding * -1); x < newMaxY; x++)
                {
                    var px = new Coordinate(x, y);
                    row.Add(image.CalculatePixelValue(px, enhancementAlgo, padWith));
                }
                output.Add(row);
            }

            return output;
        }

        public static List<List<char>> Pad(this List<List<char>> image, int padding, char padWith)
        {
            var newX = image[0].Count + (2 * padding);
            var newY = image.Count + (2 * padding);

            var output = new List<List<char>>(newY);

            for (int i = 0; i < padding; i++)
                output.Add(Enumerable.Repeat(padWith, newX).ToList());

            foreach (var line in image)
            {
                var newLine = Enumerable.Repeat(padWith, padding).ToList();
                newLine.AddRange(line);
                newLine.AddRange(Enumerable.Repeat(padWith, padding));
                output.Add(newLine);
            }

            for (int i = 0; i < padding; i++)
                output.Add(Enumerable.Repeat(padWith, newX).ToList());

            return output;
        }
    }
}
