using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Runtime.ConstrainedExecution;
using System.Text.Json.Nodes;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PR6
{
    class Note
    {
        public string Name { get; set; } 
        public string Date { get; set; }
        public string Description { get; set; }
        public Note(string name, string date, string description)
        {
            Name = name;
            Date = date;
            Description = description;
        }
    }
    internal class Program
    {
        public static Note Final = new ("", "", "");
        private static void Main()
        {
            Console.WriteLine("Введите полный путь до файла");
            Console.WriteLine("------------------------------");
            string path = Console.ReadLine();
            if (path.EndsWith(".txt"))
            {
                Console.Clear();
                Console.WriteLine("Для пересохранения файла в json и xml нажмите F1, для выхода из программы нажмите Escape");
                Console.WriteLine("-----------------------------------------------------------------------------------------");
                string[] text = File.ReadAllLines(path);
                
                Final.Name = text[0];
                Final.Date = text[1];
                Final.Description = text[2];

                Console.Write(Final.Name);
                Console.Write(Final.Date);
                Console.Write(Final.Description);

                ConsoleKeyInfo a = Console.ReadKey();
                if (a.Key == ConsoleKey.F1)
                {
                    Save();
                }
                else if (a.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
            }
            else if (path.EndsWith(".json"))
            {
                Console.Clear();
                Console.WriteLine("Для пересохранения файла в txt и xml нажмите F1, для выхода из программы нажмите Escape");
                Console.WriteLine("-----------------------------------------------------------------------------------------");

                string text = File.ReadAllText(path);
                dynamic lines = JsonConvert.DeserializeObject(text);

                Final.Name = lines[0];
                Final.Date = lines[1];
                Final.Description = lines[2];
                
                Console.Write(Final.Name);
                Console.Write(Final.Date);
                Console.Write(Final.Description);

                ConsoleKeyInfo a = Console.ReadKey();
                if (a.Key == ConsoleKey.F1)
                {
                    Save();
                }
                else if (a.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
            }
            else if (path.EndsWith(".xml"))
            {
                Console.Clear();
                Console.WriteLine("Для пересохранения файла в json и txt нажмите F1, для выхода из программы нажмите Escape");
                Console.WriteLine("-----------------------------------------------------------------------------------------");

                string[] x;
                XmlSerializer xml = new XmlSerializer(typeof(string[]));
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    x = (string[])xml.Deserialize(fs);
                }

                Final.Name = x[0];
                Final.Date = x[1];
                Final.Description = x[2];

                Console.WriteLine(Final.Name);
                Console.WriteLine(Final.Date);
                Console.WriteLine(Final.Description);

                ConsoleKeyInfo a = Console.ReadKey();
                if (a.Key == ConsoleKey.F1)
                {
                    Save();
                }
                else if (a.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
            }
        }
        public static void Save()
        {
            Console.Clear();
            Console.WriteLine("Введите полный путь куда вы хотите сохранить файл");
            Console.WriteLine("-------------------------------------------------");
            string path = Console.ReadLine();
            if (path.EndsWith(".txt"))
            {
                File.WriteAllText(path, Final.Name);
                File.WriteAllText(path, Final.Date);
                File.WriteAllText(path, Final.Description);
            }
            else if (path.EndsWith(".json"))
            {
                string json1 = JsonConvert.SerializeObject(Final);
                File.WriteAllText(path, json1);
            }
            else if (path.EndsWith(".xml"))
            {
                XmlSerializer xml = new XmlSerializer(typeof(string));
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    xml.Serialize(fs, Final);
                }
            }
        }
    }
}
