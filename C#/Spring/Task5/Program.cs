using System.IO;
using System.Text.Json;

class Phone
{
    public long Number { get; set; }

    public Phone(long number)
    {
        this.Number = number;
    }

    public override bool Equals(object obj)
    {
        var phone = obj as Phone;
        if (phone == null) return false;
        return this.Number % 10000 == phone.Number % 10000;
    }
    public override string ToString()
    {
        return Number.ToString();
    }
}

class Contact
{
    public string Name { get; set; }
    public Phone? Phone { get; set; }

    public Contact(string name, Phone phone)
    {
        this.Name = name;
        this.Phone = phone;
    }
    public static bool operator >(Contact a, Contact b)
    {
        return a.Phone.Number > b.Phone.Number;
    }
    public static bool operator <(Contact a, Contact b)
    {
        return a.Phone.Number < b.Phone.Number;
    }
    public static Contact operator -(Contact a)
    {
        a.Phone = null;
        return a;
    }
    public override string ToString()
    {
        return Name + " - " + Phone.ToString();
    }
}

class ContactList
{
    public Dictionary<char, Contact> Contacts { get; set;}
    public ContactList()
    {
        Contacts = new Dictionary<char, Contact>();
    }
    public void Add(Contact contact)
    {
        Contacts.Add(contact.Name[0], contact);
    }
    public void Delete(Contact contact) 
    {
        Contacts.Remove(contact.Name[0]);
    }
    public void Delete(char key)
    {
        Contacts.Remove(key);
    }
    public void ShowAll()
    {
        foreach(var contact in Contacts)
        {
            Console.WriteLine(contact.Value.ToString());
        }
    }
    public Contact GetByKey(char key)
    {
        return Contacts[key];
    }
}
static class ContactExtensions
{
    public static bool ContainsKey(this ContactList contactList, char key)
    {
        return contactList.Contacts.ContainsKey(key);
    }
}
class Program
{
    static void Main(string[] args)
    {
        var phone1 = new Phone(1234567890);
        var phone2 = new Phone(9876543210);
        var phone3 = new Phone(1023456789);
        Console.WriteLine(phone1.Equals(phone2)); // False
        Console.WriteLine(phone1.Equals(phone3)); // True

        var contact1 = new Contact("Mike", phone1);
        var contact2 = new Contact("Jane", phone2);
        Console.WriteLine(contact1.Phone.Equals(contact2.Phone)); // False

        Console.WriteLine(contact1 > contact2);
        contact1 = -contact1;
        Console.WriteLine(contact1.Phone == null);
        contact1.Phone = phone1;

        using(FileStream fileStream = new("../../../file.json", FileMode.OpenOrCreate))
            JsonSerializer.Serialize(fileStream, contact2);
        using(FileStream fileStream = new("../../../file.json", FileMode.OpenOrCreate))
        {
            Contact contact3 = JsonSerializer.Deserialize<Contact>(fileStream);
            Console.WriteLine(contact3.Phone.Number);
        }

        ContactList contactList = new();
        contactList.Add(contact1);
        contactList.Add(contact2);
        contactList.ShowAll();
        Console.WriteLine(contactList.GetByKey('J'));
        Console.WriteLine(contactList.ContainsKey('M'));
    }
}