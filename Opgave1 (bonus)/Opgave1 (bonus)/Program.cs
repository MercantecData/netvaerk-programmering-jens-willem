using System;

namespace Projekt
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "Some cool ascii text";
            int[] bytes = ByteConvert(text);
            foreach(int element in bytes)
            {
                Console.Write(element + " ");
            }
            
        }
        static int[] ByteConvert(string text)
        {
            int i = 0;
            int i2 = 0;
            char[] textArray = text.ToCharArray();
            char[] charArray = { ' ', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            int[] byteArray = { 00100000, 01100001, 01100010, 01100011, 01100100, 01100101, 01100110, 01100111, 01101000, 01101001, 01101010, 01101011, 01101100, 01101101, 01101110, 01101111, 01110000, 01110001, 01110010, 01110011, 01110100, 01110101, 01110110, 01110111, 01111000, 01111001, 01111010, 01000001, 01000010, 01000011, 01000100, 01000101, 01000110, 01000111, 01001000, 01001001, 01001010, 01001011, 01001100, 01001101, 01001110, 01001111, 01010000, 01010001, 01010010, 01010011, 01010100, 01010101, 01010110, 01010111, 01011000, 01011001, 01011010 };
            int[] textToByte = new int[textArray.Length];
            while (textArray.Length > i)
            {
                if (textArray[i] == charArray[i])
                {
                    textToByte[i] = byteArray[i];
                }
                else
                {
                    i2++;
                }
                i++;
            }
            return byteArray;
        }
    }
}
