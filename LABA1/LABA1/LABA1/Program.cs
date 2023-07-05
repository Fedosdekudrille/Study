using System;

namespace std;

interface I1
{
    public string Property { get; set; }
    void HelloWorld();
    event Action ActionEvent;
    int this[int index] { get; set; }
}

class C1
{
    private const int _Id = 0;
    public const string Name = "";
    protected const int Width = 0;

    private string _privateString { get; set; }
    public string publicString { get; set; }
    protected string protectedString { get; set; }

    private string _privateField;
    public string publicField;
    protected string protectedField;
    
    public C1()
    {
        _privateString = "Default";
        _privateField = "Default Field";
    }
    
    public C1(string privateField, string publicfield, string protectedfield)
    {
        _privateField = privateField;
        publicField = publicfield;
        protectedField = protectedfield;
    }

    public C1(C1 c1)
    {
        publicField = c1.publicField;
        publicString = c1.publicString;
    }

    public void PublicHello() {Console.WriteLine("Public hello"); }
    private void PrivateHello() {Console.WriteLine("Private hello"); }
    protected void ProtectedHello() {Console.WriteLine("Protected hello"); }

    public void Info()
    {
        Console.WriteLine($"Info: private field - {_privateField}, public field - {publicField}, protected field - {protectedField}");
    }
}

class C2 : I1
{
    private const int _Id = 0;
    public const string Name = "";
    protected const int Width = 0;

    private string _privateString { get; set; }
    public string publicString { get; set; }
    protected string protectedString { get; set; }

    private string _privateField;
    public string publicField;
    protected string protectedField;
    
    public C2()
    {
        _privateString = "Default";
        _privateField = "Default Field";
    }
    
    public C2(string privateField, string publicfield, string protectedfield)
    {
        _privateField = privateField;
        publicField = publicfield;
        protectedField = protectedfield;
    }

    public C2(C2 c2)
    {
        publicField = c2.publicField;
        publicString = c2.publicString;
    }

    //I1
    public string Property { get; set; }
    public void HelloWorld()
    {
       Console.WriteLine("HelloWorld");
    }

    public event Action? ActionEvent;

    public int this[int index]
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }
    
    //!I1
    
    public void PublicHello() {Console.WriteLine("Public hello"); }
    private void PrivateHello() {Console.WriteLine("Private hello"); }
    protected void ProtectedHello() {Console.WriteLine("Protected hello"); }

    public void Info()
    {
        Console.WriteLine($"Info: private field - {_privateField}, public field - {publicField}, protected field - {protectedField}");
    }

}

interface I2
{
    void meow();
}
class C3 : I2
{
    public string publicString { get; set; }
    public virtual void publicHello() => Console.WriteLine("Public hello");
    
    protected string protectedString { get; set; }
    protected void protectedHello() => Console.WriteLine("protected hello");
    
    private string privateString { get; set; }
    private void privateHello() => Console.WriteLine("private hello");

    public C3() { publicString = "Default"; }
    public void meow() { Console.WriteLine("meow"); }
}

class C4 : C3
{
    public string myString = "my string";
    public C4() : base() { protectedString = "Default Protectrd"; }

    public C4(string pS, string ppS)
    {
        publicString = pS;
        protectedString = ppS;
    }

    public void mYvoid() { Console.WriteLine("its my void"); }
    
}
class Program
{
    public static void Main(string[] args)
    {
        C1 c1 = new C1();
        C1 c2 = new C1(c1);
        C1 c3 = new C1("1", "2", "3");
        
        c3.publicField = "4213";
        
        c1.PublicHello();
        c3.Info();

        C2 c2_1 = new C2();
        C2 c2_2 = new C2(c2_1);
        C2 c2_3 = new C2("3", "4", "5");
        
        c2_3.publicField = "666";
        c2_3.publicString = "2";
        
        c2.PublicHello();
        c2_3.Info();
        c2_3.HelloWorld();

        C4 c4 = new C4();
        c4.publicHello();
        c4.meow();
        c4.mYvoid();

        c4.myString = "mmmmm";
        c4.publicString = "PUUUUBLIC";


    }   
}