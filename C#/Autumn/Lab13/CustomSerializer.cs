using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab13
{
    static class CustomSerializer
    {
        public static string[]? Json { get; private set; } = Array.Empty<string>();
        public static void CleanJson()
        {
            Json = Array.Empty<string>();
        } 
        public static void SerializeBinary(object obj)
        {
            SerializeBinary(new object[] { obj });
        }
        public static void SerializeBinary(object[] obj)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fileStream = new(@"C:\Study\C#\Lab13\Serialized.bin", FileMode.Create))
            {
                formatter.Serialize(fileStream, obj);
            }
        }
        public static object[] DeserializeBinary(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fileStream = new(path, FileMode.Open))
            {
                object[] obj = (object[])formatter.Deserialize(fileStream);
                return obj;
            }
        }
        public static void SerializeJson<T>(T obj)
        {
            string[] newJson = new string[Json.Length + 1];
            for(int i = 0; i < Json.Length; i++)
            {
                newJson[i] = Json[i];
            }
            newJson[Json.Length] = typeof(T).ToString() + " " + JsonSerializer.Serialize(obj);
            Json = newJson;
        }
        public static T? DeserializeJson<T>(int index)
        {
            T? obj = JsonSerializer.Deserialize<T>(Json[index].Split(' ')[1]);
            return obj;
        }
        public static void SerializeXml<T>(object obj)
        {
            XmlSerializer xmlSerializer = new(typeof(T));
            using (FileStream fileStream = new(@"C:\Study\C#\Lab13\Serialized.xml", FileMode.Create))
            {
                xmlSerializer.Serialize(fileStream, obj);
            }
        }
        public static T DeserializeXml<T>()
        {
            XmlSerializer xmlSerializer = new(typeof(T));
            using (FileStream fileStream = new(@"C:\Study\C#\Lab13\Serialized.xml", FileMode.Open))
            {
                return (T)xmlSerializer.Deserialize(fileStream);
            }
        }
        public static void SerializeSoap<T>(T obj)
        {
            XmlTypeMapping xmlTypeMapping = new SoapReflectionImporter().ImportTypeMapping(typeof(T));
            XmlSerializer xmlSerializer = new(xmlTypeMapping);
            using (FileStream fileStream = new(@"C:\Study\C#\Lab13\SerializedSOAP.xml", FileMode.Create))
            {
                xmlSerializer.Serialize(fileStream, obj);
            }
        }
        public static T DeserializeSoap<T>()
        {
            XmlTypeMapping xmlTypeMapping = new SoapReflectionImporter().ImportTypeMapping(typeof(T));
            XmlSerializer xmlSerializer = new(xmlTypeMapping);
            using (FileStream fileStream = new(@"C:\Study\C#\Lab13\SerializedSOAP.xml", FileMode.Open))
            {
                return (T)xmlSerializer.Deserialize(fileStream);
            }
        }
    }
}
