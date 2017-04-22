using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace newLab4
{
    enum TimeFrame { Year, TwoYears, Long }
    [Serializable]
    class ResearchTeam:Team
    {
        private string Theme;

        private TimeFrame ResearchDuration;
        public TimeFrame ResDuration { get { return ResearchDuration; } }

        List<Person> ProjectParticipants = new List<Person>();
        List<Paper> Publications = new List<Paper>();

        public List<Paper> ListOfPublication { get { return Publications; } set { Publications = value; } }
        public ResearchTeam(string InvestigationTheme, string Organisation, int RegistrationNumber, TimeFrame InvestigationDuration)
        {
            ResearchDuration = InvestigationDuration;
            Theme = InvestigationTheme;
            _RegistrationNumber = RegistrationNumber;
            _Organisation = Organisation;
        }
        public ResearchTeam() : this("Chaos", "IRE", 13, TimeFrame.Year) { }

        public Paper LastPaper
        {
            get
            {
                if (Publications.Count == 0)
                {
                    return null;
                }
                int MaxIndex = 0;
                DateTime MaxDateTime = Publications[0]._TimeOfPublication;
                for (int i = 0; i < Publications.Count; i++)
                {
                    if (Publications[i]._TimeOfPublication > MaxDateTime)
                    {
                        MaxIndex = i;
                        MaxDateTime = Publications[i]._TimeOfPublication;
                    }
                }
                return Publications[MaxIndex];
            }
        }
        public void AddPapers(params Paper[] AdditionalPapers)
        {
            Publications.AddRange(AdditionalPapers);
        }
        public override string ToString()
        {
            string stringListOfPublications = "";
            foreach (Paper pap in Publications)
            {
                stringListOfPublications += pap.ToString() + "\r\n";
            }
            string stringListOfParticipants = "";
            foreach (Person pers in ProjectParticipants)
            {
                stringListOfParticipants += pers.ToString() + "\r\n";
            }
            return base.ToString() + string.Format("\r\n Theme: {0}, Duration: {1} \r\n Participants: {2} \r\n Publications: {3}", Theme, ResearchDuration, stringListOfParticipants, stringListOfPublications);
        }
        public string ToShortString()
        {
            return base.ToString() + string.Format("\r\n Theme: {0}, Duration: {1} \r\n ", Theme, ResearchDuration);
        }
        public List<Person> ListOfParticipants { get { return ProjectParticipants; } set { ProjectParticipants = value; } }
        public void AddMembers(params Person[] AdditionalMembers)
        {
            ProjectParticipants.AddRange(AdditionalMembers);
        }
        public Team getTeamType
        {
            get
            {
                return new Team(Organisation, RegistrationNumber);
            }
            set
            {
                this.Organisation = value.Organisation;
                this.RegistrationNumber = value.RegistrationNumber;
            }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        //public override object DeepCopy()
        //{
        //    ResearchTeam CopyTeam = new ResearchTeam(this.Theme, this.Organisation, this.RegistrationNumber, this.ResearchDuration);
        //    CopyTeam.ListOfParticipants = ListOfParticipants;
        //    CopyTeam.ListOfPublication = ListOfPublication;
        //    return CopyTeam;

        //}
        public IEnumerable<Person> MembersWithoutPublications()
        {

            ArrayList AutorsWithoutP = new ArrayList();
            bool someBool;
            foreach (Person pers in ProjectParticipants)
            {
                someBool = true;
                foreach (Paper pap in Publications)
                {
                    if (pap._Author == pers)
                    {
                        someBool = false;
                        break;
                    }
                }
                if (someBool)
                {
                    AutorsWithoutP.Add(pers);
                    //Console.WriteLine(pers.ToShortString());
                }

            }
            for (int i = 0; i < AutorsWithoutP.Count; i++)
            {
                yield return (Person)AutorsWithoutP[i];
                //Console.Write(((Person)AutorsWithoutP[i]).ToShortString());
            }

        }
        public IEnumerable<Paper> LastPapers(int N_years)
        {
            for (int i = 0; i < Publications.Count; i++)
            {
                if (((Paper)Publications[i])._TimeOfPublication.Year >= DateTime.Now.Year - N_years)
                {
                    yield return (Paper)Publications[i];
                    //Console.Write(((Paper)Publications[i]).ToString());
                }
            }
        }

        public int CompareTo(ResearchTeam other)
        {
            if (other == null)
            {
                throw new ArgumentException("Wrong argument!");
            }
            return Theme.CompareTo(other.Theme);
        }

        public delegate void TeamListHandler(object source,TeamListEventArgs args);

        // this method return deserialise copy of the 
        // ResearchTeam object 
        public new ResearchTeam DeepCopy() {

            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream,this);
            return (ResearchTeam)formatter.Deserialize(stream);
        }

        public bool Save(string filename) {
            
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                using (FileStream fs = new FileStream(filename,FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, this);
                }
            }
            catch (IOException ex) {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool Load(string filename) {
            try {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(filename,FileMode.Open)) {
                    ResearchTeam rt =  (ResearchTeam)formatter.Deserialize(fs);
                    this.Name = rt.Name;
                    this.Organisation = rt.Organisation;
                    this.RegistrationNumber = rt.RegistrationNumber;
                    
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            return true ;
        }

        public bool Save(string filename, ResearchTeam rt) {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate)) {
                    formatter.Serialize(fs,rt);
                }
            }
            catch (IOException ex){
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool Load(string filename, ResearchTeam rt) {
            try {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(filename,FileMode.Open)) {
                   ResearchTeam tmp = (ResearchTeam)formatter.Deserialize(fs);
                    rt.Name = tmp.Name;
                    rt.Organisation = tmp.Organisation;
                    rt.RegistrationNumber = tmp.RegistrationNumber;
                    
                }
            }
            catch (IOException ex) {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool AddFromConsole() {
            Console.WriteLine("Add the Name, Surname and date of birth in format year.month.day of the new member");
            Console.WriteLine("Use coma as a separation symbol");
            string ConsoleInput = Console.ReadLine();
            try
            {
                string[] diff = ConsoleInput.Split(',');
                ListOfParticipants.Add(new Person(diff[0], diff[1], Convert.ToDateTime(diff[2])));

            }
            catch (IOException ex) {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            catch (ArgumentException ex) {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            return true;
        }
    }
}
