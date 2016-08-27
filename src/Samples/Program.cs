using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CetoTree.UnitTests.TestUtil;
using CetoTree;
using System.Text;
using CetoTree.UnitTests.TestDataClasses;
using Microsoft.EntityFrameworkCore;

namespace Samples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the CetoTree sample");
            //Generate a tree
            var tree = TestTreeGenerator.GenerateTree(3, 3);
            Console.WriteLine("Generated tree");

            var converter = new TreeRelationalModelConverter();
            var _context = new AppDbContext();

            //convert the content to a relational model
            var data = converter.ConvertToRelationalModel(tree);

            //Add all data to store to the db context
            _context.Add(data.Item1);
            _context.AddRange(data.Item2);
            data.Item3.ForEach(x => _context.Add(x));
            _context.SaveChanges();
            Console.WriteLine("Saved tree");

            //Load the data from db context
            var inStorageTree = _context.Set<RelationalTree>().FirstOrDefault(x => x.Id == data.Item1.Id);
            var associatedNodes = _context.Set<RelationalTreeNode<TestContent>>()
                .Where(x => x.TreeId == data.Item1.Id)
                .Include(x => x.Data)
            .ToList();

            //Convert the loaded data to a tree
            var loadedTree = converter.ConvertFromRelationalModel(inStorageTree, associatedNodes);
            Console.WriteLine("Loaded tree");

            Console.WriteLine("Finished sample.");
            Console.WriteLine("Press 'Enter' to exit the sample");
            Console.ReadLine();
        }
    }
}
