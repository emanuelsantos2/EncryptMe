using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace EncryptMe
{
    class Program
    {
        static void Main(string[] args)
        {
            Handling handle = new Handling();
            Console.WriteLine("Please enter your password:");
            //handle.password = Console.ReadLine();
            bool continueLoop = true;
            while (continueLoop)
            {
                ConsoleKeyInfo read = Console.ReadKey();
                ConsoleKey read_ = read.Key;
                ClearCurrentConsoleLine();
                if(read_ != ConsoleKey.Backspace && read_ != ConsoleKey.Enter)
                {
                    handle.setPassword.Add(read.KeyChar);
                }else if (read_ == ConsoleKey.Backspace)
                {
                    if (handle.setPassword.Count > 0)
                    {
                        handle.setPassword.RemoveAt(handle.setPassword.Count - 1);
                    }
                }else if(read_ == ConsoleKey.Enter)
                {
                    handle.SetPassword();
                    continueLoop = false;
                }
            }
            inicio:
            Console.Clear();
            Console.WriteLine("Password entered successfuly");
            Console.WriteLine("1 - Encrypt | 2 - Decrypt | 3 - Remove Line");
            string rep = Console.ReadLine();
            switch (rep)
            {
                case "1":
                    Console.WriteLine("Please enter the string you would like to add to your encrypted file");
                    handle.AddToFile(Console.ReadLine());

                    break;
                case "2":
                    handle.DecryptFile();
                    break;
                case "3":
                    handle.DecryptFile();
                    Console.WriteLine();
                    Console.Write("Insert the number to remove: ");
                    handle.RemoveLine(int.Parse(Console.ReadLine()));
                    break;
            }

            if(Console.ReadKey().Key == ConsoleKey.Enter)
            {
                goto inicio;
            }
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }

    class Handling
    {
        const string encryptedFile = "myFile";
        public List<char> setPassword = new List<char>();
        public string password = "";

        public Handling()
        {
            if (File.Exists(encryptedFile))
            {
                //currentLines = GetCurrentLines();
            }else
            {
                File.Create(encryptedFile).Close();
            }
        }

        public void SetPassword()
        {
            foreach(char letter in setPassword)
            {
                password += letter;
            }
        }


        public void AddToFile(string toEncrypt)
        {
            string Encrypted = StringCipher.Encrypt(toEncrypt, password);
            string toAdd = Encrypted + ";";

            List<string> lines = File.ReadAllLines(encryptedFile).ToList();
            lines.Add(toAdd);

            File.WriteAllLines(encryptedFile, lines.ToArray());
            //GetCurrentLines(); 
        }

        public void DecryptFile()
        {
            int count = 0;
            foreach(string line in File.ReadAllText(encryptedFile).Split(';'))
            {
                try
                {
                    Console.WriteLine("{0} - {1}", count, StringCipher.Decrypt(line, password));
                    
                }catch(Exception e) {
                    if (line.Length > 5)
                    {
                        Console.WriteLine("{0} - This line couldn't be decrypted", count);
                    }
}
                count++;
            }
        }

        public void RemoveLine(int lineNum)
        {
            List<string> lines =  File.ReadAllText(encryptedFile).Split(';').ToList();

            lines.RemoveAt(lineNum);

            File.WriteAllLines(encryptedFile, lines.ToArray());
         
        }
    }
}
