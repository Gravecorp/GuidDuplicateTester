using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GuidDuplicateTester
{
    class Program
    {
        Dictionary<string, List<GuidDuplicateTester.Set.SetGuid>> dict = new Dictionary<string, List<GuidDuplicateTester.Set.SetGuid>>();
        List<Game> list = new List<Game>();
        internal string path;
        public Program()
        {
            path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Octgn", "GameDatabase");
            DirectoryInfo d = new DirectoryInfo(path);
            foreach (DirectoryInfo dInfo in d.GetDirectories())
            {
                list.Add(new Game(dInfo.FullName));
            }
        }

        public void GetGuids()
        {
            foreach (Game g in list)
            {
                List<GuidDuplicateTester.Set.SetGuid> guids = g.GetAllGuids();
                string game = g.path.Substring(g.path.LastIndexOf(Path.DirectorySeparatorChar) +1);
                dict.Add(game, guids);
            }
        }

        public void TestDuplicates()
        {
            foreach (KeyValuePair<string, List<GuidDuplicateTester.Set.SetGuid>> kvi1 in dict)
            {
                foreach (KeyValuePair<string, List<GuidDuplicateTester.Set.SetGuid>> kvi2 in dict)
                {
                    if (kvi1.Key != kvi2.Key)
                    {
                        CheckLists(kvi1.Value, kvi1.Key, kvi2.Value, kvi2.Key);
                    }
                }
            }
        }

        private void CheckLists(List<GuidDuplicateTester.Set.SetGuid> list1,string game1, List<GuidDuplicateTester.Set.SetGuid> list2, string game2)
        {
            foreach (GuidDuplicateTester.Set.SetGuid g1 in list1)
            {
                foreach (GuidDuplicateTester.Set.SetGuid g2 in list2)
                {
                    if (g1.card == g2.card)
                    {
                        Console.WriteLine(string.Format("Duplicate Guid found in games: {0} and {1}", game1, game2));
                        Console.WriteLine(string.Format("Gameid: {0} setid: {1} cardid: {2}",game1, g1.set, g1.card));
                        Console.WriteLine(string.Format("Gameid: {0} setid: {1} cardid: {2}", game2, g2.set, g2.card));
                        Console.WriteLine();
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.GetGuids();
            p.TestDuplicates();
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
