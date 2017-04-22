namespace newLab4
{
    public class TeamListEventArgs:System.EventArgs
    {
        public string CollectionName { get; set; }
        public string Changes { get; set; }
        public int NumberOfEnement { get; set; }
        public TeamListEventArgs(string Colname, string Ch,int numOfEl) {
            CollectionName = Colname;
            Changes = Ch;
            NumberOfEnement = numOfEl;
        }
        public TeamListEventArgs():this("SomCollection","Some changes",0) { }
        public override string ToString()
        {
            return string.Format("Name of collection {0} \n Changes {1} \n Number of new element {2} \n",CollectionName,Changes,NumberOfEnement.ToString());
        }
    }

}