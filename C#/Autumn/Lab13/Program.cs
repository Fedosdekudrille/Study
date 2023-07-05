using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Lab13
{
    static class Write
    {
        public static void Red(string str)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    class Program { 
        static async void Client()
        {
            int port = 8005;
            string adress = "127.0.0.1";
            try
            {
                IPEndPoint iPEndPoint = new(IPAddress.Parse(adress), port);
                Socket socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(iPEndPoint);
                string message = "Дай объект";
                byte[] data = Encoding.Unicode.GetBytes(message);
                socket.Send(data);
                data = new byte[256];
                int bytes;
                using (FileStream fileStream = new(@"C:\Study\C#\Lab13\FromServerSerialized.bin", FileMode.Create))
                    do
                    {
                    bytes = socket.Receive(data, data.Length, 0);
                    fileStream.Write(data);
                    }
                    while (socket.Available > 0);
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                BinaryFormatter formatter = new BinaryFormatter();
                foreach (Human human in CustomSerializer.DeserializeBinary(@"C:\Study\C#\Lab13\FromServerSerialized.bin"))
                {
                        Printer.IAmPrinting(human);
                }
            }
            catch(Exception ex)
            {
                Write.Red(ex.Message);
            }
        }
        static void Rebuild(string str, (int, int) table)
        {
            string result = "";
            string[] rows = new string[table.Item1];
            int currentRow = 0;
            for(int i = 0; i < str.Length; i++)
            {
                if(i % table.Item2 == 0 && i != 0)
                {
                    if (currentRow == table.Item1 - 1)
                        break;
                    currentRow++;
                }
                rows[currentRow] += str[i];
            }
            int currentColumn = 0;
            for(int i = 0; i < table.Item2; i++)
            {
                foreach (string row in rows)
                {
                    if (currentColumn < row.Length)
                    {
                        result += row[currentColumn];
                    }
                    else
                    {
                        break;
                    }
                }
                currentColumn++;
            }
            Console.WriteLine(result);
        }
        static void Rebuild(string str, (int rows, int columns) table, string key)
        {
            string result = "";
            string[] rows = new string[table.Item1];
            int currentRow = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (i % table.Item2 == 0 && i != 0)
                {
                    if (currentRow == table.Item1 - 1)
                        break;
                    currentRow++;
                }
                rows[currentRow] += str[i];
            }
            int[] keys = new int[key.Length];
            for(int i = 0; i < keys.Length; i++)
            {
                keys[i] = key[i];
            }
            for(int i = 0; i < keys.Length; i++)
            {
                for(int j = i + 1; j < keys.Length; j++)
                {
                    if (keys[j] < keys[i])
                    {
                        (keys[i], keys[j]) = (keys[j], keys[i]);
                        for (int l = 0; l < rows.Length; l++)
                        {
                            string row = rows[l];
                            char[] c = row.ToCharArray();
                            (c[i], c[j]) = (c[j], c[i]);
                            rows[l] = new string(c);
                        }
                    }
                }
            }
            int currentColumn = 0;
            for (int i = 0; i < table.Item2; i++)
            {
                foreach (string row in rows)
                {
                    if (currentColumn < row.Length)
                    {
                            result += row[currentColumn];
                    }
                    else
                    {
                        break;
                    }
                }
                currentColumn++;
            }
            Console.WriteLine(result);
        }
        static void Main(string[] args)
        {
            CustomSerializer.SerializeBinary(new Human());
            foreach(Human human in CustomSerializer.DeserializeBinary(@"C:\Study\C#\Lab13\Serialized.bin"))
            {
                Anounsment.Info(human);
            }
            CustomSerializer.SerializeJson(new Human());
            Anounsment.Info(CustomSerializer.DeserializeJson<Human>(0));
            CustomSerializer.SerializeXml<Human>(new Human());
            Anounsment.Info(CustomSerializer.DeserializeXml<Human>());
            CustomSerializer.SerializeSoap(new Human());
            Anounsment.Info(CustomSerializer.DeserializeSoap<Human>());

            CustomSerializer.CleanJson();

            Army army = new(10);
            army.Write();
            foreach(Sentient sentient in army)
            {
                if(sentient is Human human)
                    CustomSerializer.SerializeJson(human);
                else if(sentient is Trans trans)
                    CustomSerializer.SerializeJson(trans);
            }
            Army newArmy = Army.CreateEmpty(CustomSerializer.Json.Length);
            for(int i = 0; i < newArmy.Length; i++)
            {
                if (CustomSerializer.Json[i].Split(' ')[0] == typeof(Human).ToString())
                {
                    newArmy[i] = CustomSerializer.DeserializeJson<Human>(i);
                }
                else if(CustomSerializer.Json[i].Split(' ')[0] == typeof(Trans).ToString())
                {
                    newArmy[i] = CustomSerializer.DeserializeJson<Trans>(i);
                }
            }
            newArmy.Write();

            XmlDocument xmlDocument = new();
            xmlDocument.Load(@"C:\Study\C#\Lab13\Serialized.xml");
            XmlElement xmlElements = xmlDocument.DocumentElement;
            XmlNodeList xmlNodes = xmlElements.SelectNodes("*");
            foreach(XmlNode xmlNode in xmlNodes)
            {
                Console.WriteLine(xmlNode.OuterXml);
            }
            XmlNodeList xmlNodeList = xmlElements.SelectNodes("./Iq");
            foreach(XmlNode xmlNode in xmlNodeList)
            {
                Console.WriteLine(xmlNode.InnerText);
            }

            XDocument xmlDocument1 = XDocument.Load(@"C:\Study\C#\Lab13\Document.xml");
            var microsoft = xmlDocument1.Element("people").Elements("person")
                .Where(p => p.Element("company").Value == "Microsoft")
                .Select(p => new
                {
                    name = p.Attribute("name").Value,
                    age = p.Element("age").Value,
                    company = p.Element("company").Value,
                });
            if(microsoft != null)
            {
                foreach(var person in microsoft)
                {
                    Console.WriteLine($"Имя: {person.name}, возраст: {person.age}");
                }
            }
            Client();
            Rebuild("Боитдиултоьтдгь-псеоснояшмяил_ьу-бу_дччуч", (7, 6));
            Rebuild("Онлгвишлеиоутньмшттьишньо__мио_всп_нгоиеодсичтгзнтеесодев_няднможь_не__и_жяеб", (11, 7), "октябрь");
        }
    }
}