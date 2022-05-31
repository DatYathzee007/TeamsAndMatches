using CommunityToolkit.Mvvm.ComponentModel;


namespace WPFApp
{
    public class Team //: ObservableObject
    {
        public string Name { get; set; }
        public int Strength { get; set; }

        /*public  Team DeepCopy()
        {
            Team team = new()
            {
                Name = this.Name,
                Strength = this.Strength,
            };
            //team.Name = Name;
            //team.Strength = Strength;
            return team;
        }
        public void CopyFrom(Team source)
        {
            this.Name = source.Name;
            OnPropertyChanged(nameof(Name));
            this.Strength = source.Strength;
            OnPropertyChanged(nameof(Strength));
 
        }*/
    }
}
