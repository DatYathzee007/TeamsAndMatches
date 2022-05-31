using System.Windows;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for TeamEditor.xaml
    /// </summary>
    public partial class TeamEditorWindow : Window
    {
        public TeamEditorWindow(Team team)
        {
            InitializeComponent();
            DataContext = new TeamEditorViewModel(team);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
