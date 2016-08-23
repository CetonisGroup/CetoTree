using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CetoTree.UnitTests.TestDataClasses
{
    public class TestContent
    {
        public string Content { get; set; }

        public override bool Equals(object obj)
        {
            var secondContent = (TestContent)obj;
            return Content == secondContent.Content;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
