using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polybiusSquareCipherConsole
{
    class Program
    {
        static char[,] square(string word)
        {
            char[,] grid = new char[5, 5];

            StringBuilder alphabet = new StringBuilder();
            for (int i = 'A'; i < 'A' + 26; i++)
                if ((char)i != 'J')
                    alphabet.Append((char)i);

            word = word.ToUpper();

            for (int i = 0; i < word.Length; i++)
                alphabet.Replace(word[i].ToString(), "");

            int a = 0;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    if (a < word.Length)
                    {
                        for (int l = 0; l < a; l++)
                        {
                            if (a < word.Length && word[l] == word[a])
                            {
                                a++;
                                l = -1;
                            }
                        }
                        if (a < word.Length)
                        {
                            grid[i, j] = word[a];
                            a++;
                        }
                    }
                    else
                    {
                        grid[i, j] = alphabet[0];
                        alphabet.Remove(0, 1);
                    }
                }
            return grid;
        }
        static string encryptPolibius(string plaintext, char[,] matrica)
        {

            StringBuilder sb = new StringBuilder("");
            plaintext = plaintext.Replace("I", "J");
            plaintext = plaintext.Replace(" ", "");
            plaintext = plaintext.ToUpper();

            for (int i = 0; i < plaintext.Length; i++)
            {
                char chA = plaintext[i];
                int row = 0, col = 0;
                for (int j = 0; j < 5; j++)
                    for (int k = 0; k < 5; k++)
                        if (matrica[j, k] == chA)
                        {
                            row = j + 1;
                            col = k + 1;
                        }
                sb.Append(row.ToString() + col.ToString());
            }

            return sb.ToString();
        }
        static string decryptPolybius(string ciphertext, char[,] grid)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ciphertext.Length; i += 2)
            {
                char enc11 = ciphertext[i];
                char enc21 = ciphertext[i + 1];
                int enc1 = int.Parse(enc11.ToString()) - 1;
                int enc2 = int.Parse(enc21.ToString()) - 1;
                char dec = 'j';
                for (int j = 0; j < 5; j++)
                    for (int k = 0; k < 5; k++)
                    {
                        if (j == enc1 && k == enc2)
                        {
                            dec = grid[j, k];
                        }

                    }
                sb.Append(dec);
            }

            return sb.ToString().ToLower();
        }


        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("POLYBIUS SQUARE: ");
                int k = 0;
                Console.WriteLine();
                for (int i = 'A'; i < 'A' + 26; i++)
                    if ((char)i != 'J')
                    {
                        Console.Write((char)i + " ");
                        k++;
                        if (k % 5 == 0)
                        {
                            Console.WriteLine();
                        }
                    }

                jeptekstin:
                Console.Write("\nSheno tekstin: ");
                string teksti = Console.ReadLine();
                for (int i = 0; i < teksti.Length; i++)
                {
                    if (char.IsDigit(teksti[i]))
                    {
                        Console.Write("Teksti duhet te kete vetem shkronja ! \n");
                        goto jeptekstin;
                    }
                    else if (!char.IsLetterOrDigit(teksti[i]))
                    {
                        Console.Write("Teksti nuk duhet te kete karaktere speciale ! \n");
                        goto jeptekstin;
                    }
                }
                if (teksti == "")
                {
                    Console.Write("Ju nuk keni shenuar asgje! \n");
                    goto jeptekstin;
                }

                Console.Write("Sheno celesin: ");
                string key = Console.ReadLine();


                char[,] grid = square(key);





                Console.Write("Teksti i enkriptuar : ");
                string encrypted = encryptPolibius(teksti, grid);
                Console.WriteLine(encrypted);

                Console.Write("Teksti i dekriptuar : ");
                string decrypted = decryptPolybius(encrypted, grid);
                Console.WriteLine(decrypted + "\n");
            }

        }

    }
}
