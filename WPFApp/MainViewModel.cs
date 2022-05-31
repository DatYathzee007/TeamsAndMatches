using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace WPFApp
{
    public partial class MainViewModel : ObservableObject
    {
        /*
        1. There should be 2 extra model classes: Team {Name, Strength}, Match {WinningTeam, LosingTeam} (1 points)
        1./b Create a text file with some teams, the list of teams should be initialized from this file of the ctor  of the main window. (1 points)
        2. The teams should be displayed in the first listbox control, using an ItemTemplate, which should consists of 2 rows, the first should show the team's name as text, the second row
            should render a slider, and the strength should be bound to that.(4 points)
        3. The second Listbox should display the list of teams selected for a match, using an ItemTemplate that shows the name of the teams.(1 points)
        4. Use a Dialog window to edit the first listbox (add/edit teams)! if no items are selected in the listbox, the window should open with empty values and should add a new team to the list when the
            dialog is closed. The Dialog window should show a textbox for the name and a slider for the Strength. (3 points)
        4./b If an Item is selected, that item's values should be bound to the controls in the window and the item should be updated when the window is closed. (3 points)
        5. Now we can assemble a match between 2 teams: One match is represented by the second listbox control, there should be 2 buttons that move items between the first and second listbox controls. (1 points)
        5./b There is a special rule here, The second listbox can have a number of maximum 2 teams added to it. The Add button should be disabled this case. (1 points)
        6. Finally we can assemble the betting ticket. There should be a button that allows the user to save a match into the third listbox. (1 points)
        6./b Once the button is clicked, the new match should appear in the third listbox (the the first item in the second listbox should be the winning team and the second item should be the losing team.) The second listbox
            value's should be cleared (all elements should be removed) (2 points)
        6./c The Third listbox should display the list of assembled matches using another ItemTemplate, which shows the first team's name on the left side, then an x and the second team's name on the right. (eg. Real Madrid X Barcelona) (2 points)
         */
        public ObservableCollection<Team> Teams { get; set; } // original collection
        public ObservableCollection<Match> Matches { get; set; } //final collection
        public ObservableCollection<Team> TeamsInMatch { get; set; } // selected collection /max 2/
        public bool decider = true;

        private Team selectedTeam;
        public Team SelectedTeam
        {
            get => selectedTeam;
            set
            {
                SetProperty(ref selectedTeam, value);
                if (TeamsInMatch.Count > 0)
                {
                    if (TeamsInMatch.First() == SelectedTeam)
                    {
                        decider = false;
                    }
                    else
                    {
                        decider = true;
                    }
                }
                AddToMatchCommand.NotifyCanExecuteChanged();
            }
        }

        private Team selectedTeamInMatch;
        public Team SelectedTeamInMatch
        {
            get => selectedTeamInMatch;
            set
            {
                SetProperty(ref selectedTeamInMatch, value);
            }
        }

        private Match selectedMatch;
        public Match SelectedMatch
        {
            get => selectedMatch;
            set
            {
                SetProperty(ref selectedMatch, value);
            }
        }
        public IRelayCommand AddOrEditTeamsCommand { get; set; }
        public IRelayCommand AddToMatchCommand { get; set; }
        public IRelayCommand RemoveFromMatchCommand { get; set; }
        public IRelayCommand SaveMatchCommand { get; set; }


        public MainViewModel()
        {
            Teams = new();
            TeamsInMatch = new();
            Matches = new();
            ReadData("Teams.txt");
            AddOrEditTeamsCommand = new RelayCommand(AddOrEditTeams);
            AddToMatchCommand = new RelayCommand(AddToMatch, () => TeamsInMatch.Count < 2 && decider);
            RemoveFromMatchCommand = new RelayCommand(RemoveFromMatch, () => TeamsInMatch.Count > 0);
            SaveMatchCommand = new RelayCommand(SaveMatch, () => TeamsInMatch.Count == 2);
        }

        public void ReadData(string filename)
        {
            StreamReader sr = new(filename);
            while (!sr.EndOfStream)
            {
                var row = sr.ReadLine().Split(",");
                Teams.Add(new Team() { Name = row[0], Strength = int.Parse(row[1]) });
            }
        }
        private void AddOrEditTeams()
        {
            if (SelectedTeam == null)
            {
                SelectedTeam = new();
                if (new TeamEditorWindow(SelectedTeam).ShowDialog() is true)
                {
                    Teams.Add(SelectedTeam);
                }
                SelectedTeam = null;
                return;
            }
            new TeamEditorWindow(SelectedTeam).ShowDialog();


            /*bool isNULL = SelectedTeam is null;
            Team tempTeam;
            if (isNULL) 
            {
                tempTeam = new Team(); // if no selected Team create new instance to use.
            }
            else
            {
                tempTeam = SelectedTeam.DeepCopy(); //if selected, copy its content to temporary var.
            }

            if (new TeamEditorWindow(tempTeam).ShowDialog() is false or null)
            {
                return; //if ShowDialog() returns false or null quit.
            }
            if (isNULL)
            {
                Teams.Add(tempTeam); return; // if there was no selected team, add a NEW to the list.
            }
            var duplicates = Teams.Where(team => ReferenceEquals(team, SelectedTeam));
            foreach (var item in duplicates)
            {
                item.CopyFrom(tempTeam);
            }*/
        }

        private void AddToMatch()
        {
            if (selectedTeam is null) return; // if selected add to other list.
            TeamsInMatch.Add(selectedTeam);
            if (TeamsInMatch is not null)
            {
                if (TeamsInMatch.First() == SelectedTeam)
                {
                    decider = false;
                }
                else
                {
                    decider = true;
                }
            }
            RemoveFromMatchCommand.NotifyCanExecuteChanged();
            AddToMatchCommand.NotifyCanExecuteChanged();
            SaveMatchCommand.NotifyCanExecuteChanged();
        }
        private void RemoveFromMatch()
        {
            if (selectedTeamInMatch is null) return;
            TeamsInMatch.Remove(selectedTeamInMatch);
            RemoveFromMatchCommand.NotifyCanExecuteChanged();
            AddToMatchCommand.NotifyCanExecuteChanged();
            SaveMatchCommand.NotifyCanExecuteChanged();
        }
        private void SaveMatch()
        {
            Match match = new();
            match.WinningTeam = TeamsInMatch[0];
            match.LosingTeam = TeamsInMatch[1];
            Matches.Add(match);

            TeamsInMatch.Clear();
            RemoveFromMatchCommand.NotifyCanExecuteChanged();
            SaveMatchCommand.NotifyCanExecuteChanged();
            AddToMatchCommand.NotifyCanExecuteChanged();
        }
    }
}