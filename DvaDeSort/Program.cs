using System.Drawing;
using System.Reflection;
using Pastel;

namespace DvaDeSort;

static class Program
{
    static void Main()
    {
        
        Console.WriteLine("Neuspořádané pole:"
            .Pastel(Color.FromArgb(255,255,255))
            .PastelBg(Color.FromArgb(0, 0, 0))
        );
        int[,] array = new int[5, 5];
        FillArray(array);
        FancyPrintArray(array);
        
        Console.WriteLine("Uspořádané pole:"
            .Pastel(Color.FromArgb(255,255,255))
            .PastelBg(Color.FromArgb(0, 0, 0))
        );
        int[,] sorted = InsertionSort2D(array);
        FancyPrintArray(sorted);

        Console.WriteLine("Hadí pole:"
            .Pastel(Color.FromArgb(255,255,255))
            .PastelBg(Color.FromArgb(0, 0, 0))
        );
        int[,] snake = Snake(sorted);
        FancyPrintArray(snake);

        Console.ReadKey();
    }

    private static int[,] Snake(int[,] sorted)
    {
        int[,] result = new int[sorted.GetLength(0), sorted.GetLength(1)];
        
        for (int row = 0; row < sorted.GetLength(0); row++)
        {
            if (row % 2 == 1)
            {
                int[] temp = new int[sorted.GetLength(1)];
                for (int i = 0; i < sorted.GetLength(1); i++)
                {
                    temp[i] = sorted[row, i];
                }
                temp = ReverseRow(temp);
                for (int i = 0; i < sorted.GetLength(1); i++)
                {
                    result[row, i] = temp[i];
                }
            }
            else
            {
                for (int i = 0; i < sorted.GetLength(1); i++)
                {
                    result[row, i] = sorted[row, i];
                }
            }
        }

        return result;
    }

    private static int[] ConvertTo1D(int[,] array)
    {
        int[] result = new int[array.Length];
        int index = 0;
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                result[index] = array[i, j];
                index++;
            }
        }
        return result;
    }

    private static int[,] ConvertTo2D(int[] array)
    {
        int[,] result = new int[5, 5];
        int index = 0;
        for (int i = 0; i < result.GetLength(0); i++)
        {
            for (int j = 0; j < result.GetLength(1); j++)
            {
                result[i, j] = array[index];
                index++;
            }
        }
        return result;
    }

    private static int[] ReverseRow(int[] rowToReverse)
    {
        int[] result = new int[rowToReverse.Length];
        int index = 0;
        for (int i = rowToReverse.Length - 1; i >= 0; i--)
        {
            result[index] = rowToReverse[i];
            index++;
        }
        return result;
    }
    
    private static int[,] InsertionSort2D(int[,] arrayToSort)
    {
        // převedeme pole na jednorozměrné pole
        int[] array = ConvertTo1D(arrayToSort);
        
        // procházíme celé pole od druhého prvku
        for (int i = 1; i < array.Length; i++) { 
            // uložíme si aktuální prvek
            int temp = array[i]; 
            // uložíme si aktuální pozici
            int j = i; 
            // dokud nenarazíme na začátek pole nebo na prvek větší než aktuální, tak posouváme prvky doprava
            while (j > 0 && array[j - 1] > temp)
            {
                array[j] = array[j - 1]; j--;
            } 
            // na aktuální pozici vložíme uložený prvek
            array[j] = temp; 
        }
        
        // převedeme pole zpět na dvourozměrné pole
        return ConvertTo2D(array);
    }
    
    private static int[] InsertionSort(int[] arrayToSort)
    {
        // procházíme celé pole od druhého prvku
        for (int i = 1; i < arrayToSort.Length; i++) { 
            // uložíme si aktuální prvek
            int temp = arrayToSort[i]; 
            // uložíme si aktuální pozici
            int j = i; 
            // dokud nenarazíme na začátek pole nebo na prvek větší než aktuální, tak posouváme prvky doprava
            while (j > 0 && arrayToSort[j - 1] > temp)
            {
                arrayToSort[j] = arrayToSort[j - 1]; j--;
            } 
            // na aktuální pozici vložíme uložený prvek
            arrayToSort[j] = temp; 
        } 
        return arrayToSort;
    }

    private static void PrintArray(int[,] array, ConsoleColor bg)
    {
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = bg;
        
        for (int i = 0; i < array.GetLength(0); i++)
        {
            Console.Write(" ");
            for (int j = 0; j < array.GetLength(1); j++)
            {
                Console.Write(array[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
    
    private static void FancyPrintArray(int[,] array)
    {
        int rows = array.GetLength(0);
        int cols = array.GetLength(1);

        int maxLength = int.MinValue;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int length = array[i, j].ToString().Length;
                if (length > maxLength)
                {
                    maxLength = length;
                }
            }
        }

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                string numStr = array[i, j].ToString();
                int padding = maxLength - numStr.Length + 1;
                string padded = new string(' ', padding) + numStr;
                // background color
                int rgbValue = (array[i, j] + 10) * 12;
                // contrast color for foreground base on background color
                int contrast = rgbValue < 128 ? 255 : 0;
                Console.Write(padded
                    .Pastel(Color.FromArgb(contrast, contrast, contrast))
                    .PastelBg(Color.FromArgb(rgbValue, 25, 25))
                    );
            }
            Console.Write(" ");
            Console.WriteLine();
        }
    }

    private static void FillArray(int[,] array)
    {
        Random random = new Random();
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                array[i, j] = random.Next(-10, 11);
            }
        }
    }
}