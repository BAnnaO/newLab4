using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newLab4
{
    class Program
    {
        static void Main(string[] args)
        {
            ResearchTeamCollection collection1 = new ResearchTeamCollection();
            ResearchTeamCollection collection2 = new ResearchTeamCollection();

            TeamsJournal journal1 = new TeamsJournal();
            TeamsJournal journal2 = new TeamsJournal();

            collection1.ResearchTeamAdded += journal1.TeamEventHandler;
            collection1.ResearchTeamInserted += journal1.TeamEventHandler;

            collection1.ResearchTeamAdded += journal2.TeamEventHandler;
            collection1.ResearchTeamInserted += journal2.TeamEventHandler;

            collection2.ResearchTeamAdded += journal2.TeamEventHandler;
            collection2.ResearchTeamInserted += journal2.TeamEventHandler;

            collection1.AddDefaults();
            collection1.AddResearchTeams(new ResearchTeam("Chaos","IRE",1,TimeFrame.Year));
            collection2.AddDefaults();
            collection2.InsertAt(1, new ResearchTeam("Another", "IRE", 1, TimeFrame.Year));
            collection2.InsertAt(6, new ResearchTeam("Another", "IRE", 1, TimeFrame.Year));

            Console.WriteLine(journal1.ToString());
            Console.WriteLine();
            Console.WriteLine(journal2.ToString());
            Console.ReadLine();
        }
    }
}
