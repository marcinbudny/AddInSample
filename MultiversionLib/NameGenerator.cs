using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: AssemblyVersion("2.0.0.0")]
[assembly: AssemblyFileVersion("2.0.0.0")]

namespace MultiversionLib
{

    public class NameGenerator
    {
        private static string[] _names = {"Geralt", "Belzebub", "Kopernik"};
        private static Random _random = new Random();

        public string GetName()
        {
            return "Mc" + _names[_random.Next(0, _names.Length)];
        }
    }
}

// UNCOMMENT FOR VERSION 1.5

//[assembly: AssemblyVersion("1.5.0.0")]
//[assembly: AssemblyFileVersion("1.5.0.0")]

//namespace MultiversionLib
//{

//    public class NameGenerator
//    {
//        private static string[] _names = {"Geralt", "Belzebub", "Kopernik"};
//        private static Random _random = new Random();

//        public string GetName()
//        {
//            return _names[_random.Next(0, _names.Length)] + "-chan";
//        }
//    }
//}


// UNCOMMENT FOR VERSION 1.0

//[assembly: AssemblyVersion("1.0.0.0")]
//[assembly: AssemblyFileVersion("1.0.0.0")]

//namespace MultiversionLib
//{

//    public class NameGenerator
//    {
//        private static string[] _names = {"Geralt", "Belzebub", "Kopernik"};
//        private static Random _random = new Random();
        
//        public string GetName()
//        {
//            return _names[_random.Next(0, _names.Length)];
//        }
//    }
//}
