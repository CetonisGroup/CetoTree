using CetoTree;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples
{
    public class TreeSQLStorage
    {
        TreeRelationalModelConverter converter = new TreeRelationalModelConverter();

        public void StoreTree<T>(Tree<T> tree, DbContext _context)
        {
            var data = converter.ConvertToRelationalModel(tree);

            //Add all data to store to the db context
            _context.Add(data.Item1);
            _context.AddRange(data.Item2);
            _context.AddRange(data.Item3);
        }


        public Tree<T> LoadTree<T>(int id, DbContext _context)
        {
            var inStorageTree = _context.Set<RelationalTree>().FirstOrDefault(x => x.Id == id);
            var associatedNodes = _context.Set<RelationalTreeNode<T>>()
                .Where(x => x.TreeId == id)
                .Include(x => x.Data)
            .ToList();

            return converter.ConvertFromRelationalModel(inStorageTree, associatedNodes);
        }
    }
}
