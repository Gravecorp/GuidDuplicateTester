using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GuidDuplicateTester
{
    class Game
    {
        public string path;
        List<Set> sets = new List<Set>();
        public Game(string path)
        {
            this.path = path;
            DirectoryInfo d = new DirectoryInfo(Path.Combine(path, "Sets"));
            foreach (DirectoryInfo dInfo in d.GetDirectories())
            {
                sets.Add(new Set(dInfo.FullName));
            }
        }

        public List<GuidDuplicateTester.Set.SetGuid> GetAllGuids()
        {
            List<GuidDuplicateTester.Set.SetGuid> ret = new List<GuidDuplicateTester.Set.SetGuid>();
            foreach (Set set in sets)
            {
                ret.AddRange(set.GetGuids());
            }
            return (ret);
        }
    }
}
