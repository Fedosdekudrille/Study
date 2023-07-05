using System.Reflection;
using System.Text;
using Name;
namespace Lab11 
{
    static class Reflector
    {
        static public void getAssemblyName(string? str) => Console.WriteLine(Type.GetType(str, true, false).Assembly.GetName().Name);
        static public bool hasPublicConstructors(string str)
        {
            var info = Type.GetType(str).GetConstructors();
            if(info.Length > 0)
            {
                Console.WriteLine(info[0]);
                return true;
            }
            return false;
        }
        static public IEnumerable<string> getMethods(string str)
        {
            return from m in Type.GetType(str).GetMethods()
                   where m.IsPublic == true
                   select m.Name;
        }
        static  public IEnumerable<string> getFieldsProperties(string str)
        {
            return from m in Type.GetType(str).GetFields()
                   select m.Name;
        }
        static public IEnumerable<string> getInterfaces(string str)
        {
            return from m in Type.GetType(str).GetInterfaces()
                   select m.Name;
        }
        static public void getMethodsWithParams(string str, Type t)
        {
            var methodNames = Type.GetType(str).GetMethods().Where(m =>
            {
                foreach (ParameterInfo pi in m.GetParameters())
                {
                    if (pi.ParameterType == t)
                    {
                        return true;
                    }
                }
                return false;
            }).Select(m => m.Name);
            foreach (string s in methodNames)
            {
                Console.WriteLine(s);
            }
        }
        public static void Invoke(string type, string method)
        {
            StreamReader reader = new(@"C:\Study\C#\Lab11\Params.txt");
            List<string> args = new List<string>();
            string allArgs = reader.ReadToEnd();
            string str = "";
            foreach(char ch in allArgs)
            {
                if(ch != ' ')
                {
                    str += ch;
                }
                else
                {
                    args.Add(str);
                    str = "";
                }
            }
            args.Add(str);
            object[] objArgs = new object[args.Count];
            int k;
            for(int i = 0; i < args.Count; i++)
            {
                if(int.TryParse(args[i], out k)) 
                {
                    objArgs[i] = k;
                }
                else
                {
                    objArgs[i] = args[i];
                }
            }

            Type.GetType(type).GetMethod(method).Invoke(Type.GetType(type), objArgs);
        }
        public static void Invoke(string type, string method, bool readFromFile)
        {
            ParameterInfo[] parameters = Type.GetType(type).GetMethod(method).GetParameters();
            object[] objArgs = new object[parameters.Length];
            Random random = new Random();
            for(int i = 0; i < parameters.Length; i++)
            {
                switch (parameters[i].Name)
                {
                    case "str":
                        objArgs[i] = "" + (char)('а' + random.Next(0, 33));
                        break;
                    case "i":
                        objArgs[i] = random.Next(int.MinValue, int.MaxValue);
                        break;
                }
            }
            Console.WriteLine(Type.GetType(type).GetMethod(method).Invoke(Type.GetType(type), objArgs));
        }
        public static object Create(Type type)
        {
            return type.GetConstructor(new Type[0]).Invoke(null);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Researched researched = new();
            string strResearched = researched.ToString();
            Reflector.getAssemblyName(strResearched);
            Console.WriteLine(Reflector.hasPublicConstructors(strResearched));
            foreach(string name in Reflector.getMethods(strResearched))
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();

            foreach(string name in Reflector.getFieldsProperties(strResearched))
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();
            foreach(string name in Reflector.getInterfaces(strResearched))
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();

            Reflector.getMethodsWithParams(strResearched, typeof(string));
            Reflector.getMethodsWithParams(strResearched, typeof(Researched));

            Reflector.Invoke(strResearched, "Method1"); // Вызов методов по их названию
            Reflector.Invoke(strResearched, "Method2", false);

            Researched researched1 = (Researched)Reflector.Create(typeof(Researched)); // Создание экземпляра по переданному типу
            Console.WriteLine(researched1.i);
        }
    }
}