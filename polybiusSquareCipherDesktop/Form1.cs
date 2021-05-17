using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace polybiusSquareCipherDesktop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private char[,] square(string word)
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

        private string encryptPolybius(string plaintext, char[,] grid)
        {
            StringBuilder index = new StringBuilder("");
            plaintext = plaintext.Replace("I", "J");
            plaintext = plaintext.Replace(" ", "");




            for (int i = 0; i < plaintext.Length; i++)
            {
                char chA = plaintext[i];
                int row = 0, col = 0;
                for (int j = 0; j < 5; j++)
                    for (int a = 0; a < 5; a++)
                    {
                        if (grid[j, a] == chA)
                        {
                            row = j + 1;
                            col = a + 1;
                        }
                    }
                index.Append(row.ToString() + col.ToString());
            }

            return index.ToString();
        }
        private string decryptPolybius(string ciphertext, char[,] grid)
        {
            StringBuilder letters = new StringBuilder();
            for (int i = 0; i < ciphertext.Length; i += 2)
            {
                char enc11 = ciphertext[i];
                char enc21 = ciphertext[i + 1];
                int enc1 = int.Parse(enc11.ToString()) - 1;
                int enc2 = int.Parse(enc21.ToString()) - 1;
                char dec = 'j';
                for (int j = 0; j < 5; j++)
                    for (int a = 0; a < 5; a++)
                    {
                        if (j == enc1 && a == enc2)
                        {
                            dec = grid[j, a];
                        }
                    }
                letters.Append(dec);
            }

            return letters.ToString().ToLower();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            encBox.Clear();
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                char[,] grid = square(keyBox.Text);
                string encryptedText = encryptPolybius(txtBox.Text.ToUpper(), grid);
                encBox.Text = encryptedText;
            }


        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            dctBox.Clear()
            char[,] grid = square(keyBox.Text);
            string decryptedText = decryptPolybius(txtBox.Text, grid);
            dctBox.Text = decryptedText;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            txtBox.Clear();
            keyBox.Clear();
            encBox.Clear();
            dctBox.Clear();
        }

        private void txtBox_Validating(object sender, CancelEventArgs e)
        {
            string word = txtBox.Text;
            if (string.IsNullOrEmpty(word))
            {
                e.Cancel = true;
                txtBox.Focus();
                errorProvider1.SetError(txtBox, "Please type something");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtBox, null);
            }
            for (int i = 0; i < word.Length; i++)
            {
                if (!char.IsLetterOrDigit(word[i]))
                {
                    e.Cancel = true;
                    txtBox.Focus();
                    errorProvider1.SetError(txtBox, "Input must be a digit or a number");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(txtBox, null);
                }

            }
        }
        private void encBox_Validating(object sender, CancelEventArgs e)
        {
            string word = txtBox.Text;
            for(int i = 0; i< word.Length; i++)
            {
                if (char.IsDigit(word[i]))
                {
                    e.Cancel = true;
                    txtBox.Focus();
                    errorProvider2.SetError(encBox, "Can't encrypt message! ");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider2.SetError(encBox, null);
                }
            }
        }
    }
}
