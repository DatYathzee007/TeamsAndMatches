using CommunityToolkit.Mvvm.ComponentModel;

namespace WPFApp
{
    public partial class TeamEditorViewModel //: ObservableObject
    {
        public Team Team { get; set; }
        public TeamEditorViewModel(Team team)
        {
            Team = team;
        }

        //[ObservableProperty]
        //private Team team;
        //public TeamEditorViewModel(Team team)
        //{
        //    this.team = team;
        //}
    }
}