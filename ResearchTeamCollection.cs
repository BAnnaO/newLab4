using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newLab4
{
    class ResearchTeamCollection : IEnumerable
    {
        private List<ResearchTeam> SomeResearchTeams = new List<ResearchTeam>();

        public void AddDefaults()
        {
            for (int i = 0; i < 3; i++)
            {
                SomeResearchTeams.Add(new ResearchTeam());
            }
            ResearchTeamAdded(SomeResearchTeams[SomeResearchTeams.Count-1], new TeamListEventArgs(this.CollectionName, "Last element added", 2));
        }
        public void AddResearchTeams(params ResearchTeam[] AdditionalTeams)
        {
            SomeResearchTeams.AddRange(AdditionalTeams);
            ResearchTeamAdded(SomeResearchTeams[SomeResearchTeams.Count-1], new TeamListEventArgs(this.CollectionName, "Last element added", SomeResearchTeams.Count-1));
        }
        public override string ToString()
        {
            string ResTeamString = "";
            foreach (ResearchTeam team in SomeResearchTeams)
            {
                ResTeamString += team.ToString();
            }
            return ResTeamString;
        }
        public string CollectionName { get; set; }

        public string ToShortString()
        {
            string ResTeamString = "";
            foreach (ResearchTeam team in SomeResearchTeams)
            {
                ResTeamString += team.ToShortString();
            }
            return ResTeamString;
        }
        public void ToSortByRegistrNumber()
        {
            SomeResearchTeams.Sort((x, y) => x.RegistrationNumber.CompareTo(y.RegistrationNumber));
        }
        public void SortByString()
        {
            SomeResearchTeams.Sort();
        }

        public void SortByPublications()
        {
            Res_Team_Comparer comp = new Res_Team_Comparer();
            SomeResearchTeams.Sort(comp);
        }
        public int MinRegNumber
        {
            get
            {
                if (SomeResearchTeams.Count == 0)
                {
                    return 0;
                }
                return SomeResearchTeams.Min(teams => teams.RegistrationNumber);
            }
        }

        public IEnumerable<ResearchTeam> TwoYearsLong
        {
            get
            {
                IEnumerable<ResearchTeam> TwoTearsL = SomeResearchTeams.Where(time => time.ResDuration == TimeFrame.TwoYears);
                return TwoTearsL;
            }
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < SomeResearchTeams.Count; i++)
            {
                yield return SomeResearchTeams[i];
            }
        }
        public List<ResearchTeam> NGroup(int value)
        {
            IEnumerable<IGrouping<int, ResearchTeam>> someGroup = SomeResearchTeams.GroupBy(team => team.ListOfParticipants.Count);

            foreach (IGrouping<int, ResearchTeam> teams in someGroup)
            {
                if (teams.Key == value)
                {
                    return teams.ToList<ResearchTeam>();
                }
                else
                {
                    throw new ArgumentNullException("There are no such number!");
                }
            }
            return null;
        }
        public void InsertAt(int j, ResearchTeam rt) {
            if (SomeResearchTeams.Count < j) {
                SomeResearchTeams[SomeResearchTeams.Count - 1] = rt;
                ResearchTeamAdded(SomeResearchTeams[SomeResearchTeams.Count-1], new TeamListEventArgs(this.CollectionName, "Last element added", SomeResearchTeams.Count - 1));
            }
            for (int i = 0; i < SomeResearchTeams.Count; i++) {
                if (i - 1 == j) {
                    SomeResearchTeams.Insert(i,rt);
                    ResearchTeamInserted(SomeResearchTeams[i],new TeamListEventArgs(this.CollectionName,"Element was added",j));
                }
            }
        }
        public ResearchTeam this[int index] {
            get { return SomeResearchTeams[index]; }
            set { SomeResearchTeams[index] = value; }
        }
        public delegate void TeamListHandler(object source, TeamListEventArgs args);
        public event TeamListHandler ResearchTeamAdded;
        public event TeamListHandler ResearchTeamInserted;
    }
}
