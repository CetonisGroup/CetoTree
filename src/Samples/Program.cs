using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CetoTree.UnitTests.TestUtil;
using CetoTree;
using System.Text;
using CetoTree.UnitTests.TestDataClasses;

namespace Samples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the CetoTree sample");
            var tree = TestTreeGenerator.GenerateTree(3, 3);
            Console.WriteLine("Generated tree");

            var storage = new TreeSQLStorage<TestContent>();
            var _context = new AppDbContext();
            storage.StoreTree(tree, _context);
            _context.SaveChanges();
            Console.WriteLine("Saved tree");

            var loadedTree = storage.LoadTree(1, _context);
            Console.WriteLine("Loaded tree");

            Console.WriteLine("Finished sample.");
            Console.WriteLine("Press 'Enter' to exit the sample");
            Console.ReadLine();
        }
    }
}
