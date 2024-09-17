// See https://aka.ms/new-console-template for more information
///<summary>
///Вернуть прямоугольник в виде массива строк
///</summary>
namespace cnsPrintRectangle
{
    internal class Program
    {
        /// <summary> 
        /// лол 
        /// </summary> 

        static string[] GetRectangle(int width, int height, char symbol = '*', bool isFill = true, char clear = ' ')
        {

            List<string> result = new();
            for (int i = 0; i < height; i++)
                result.Add((isFill || i == 0 || i == height - 1) ? (new string(symbol, width)) : (symbol + new string(clear, width - 2) + symbol));

            return result.ToArray();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Ширина высота символ рисования заполнить фигуру?");
            int width = Convert.ToInt32(Console.ReadLine());
            int height = Convert.ToInt32(Console.ReadLine());
            char symbol = Convert.ToChar(Console.ReadLine());
            bool isFill = Console.ReadLine().ToLower() == "y";

            var shape = GetRectangle(10, 5);

            foreach (var line in shape)
                Console.WriteLine(line);

        }
    }
}
