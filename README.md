# TeamsAndMatches
WPF application, CRUD
1. There should be 2 extra model classes: Team {Name, Strength}, Match {WinningTeam, LosingTeam} (1 points)
1./b Create a text file with some teams, the list of teams should be initialized from this file of the ctor  of the main window. (1 points)
2. The teams should be displayed in the first listbox control, using an ItemTemplate, which should consists of 2 rows, 
   the first should show the team's name as text, the second row
   should render a slider, and the strength should be bound to that.(4 points)
3. The second Listbox should display the list of teams selected for a match, using an ItemTemplate that shows the name of the teams.(1 points)
4. Use a Dialog window to edit the first listbox (add/edit teams)! if no items are selected in the listbox,
   the window should open with empty values and should add a new team to the list when the
   dialog is closed. The Dialog window should show a textbox for the name and a slider for the Strength. (3 points)
4./b If an Item is selected, that item's values should be bound to the controls in the window and the item should be updated when the window is closed. (3 points)
5. Now we can assemble a match between 2 teams: One match is represented by the second listbox control,
   there should be 2 buttons that move items between the first and second listbox controls. (1 points)
5./b There is a special rule here, The second listbox can have a number of maximum 2 teams added to it. The Add button should be disabled this case. (1 points)
6. Finally we can assemble the betting ticket. There should be a button that allows the user to save a match into the third listbox. (1 points)
6./b Once the button is clicked, the new match should appear in the third listbox
   (the the first item in the second listbox should be the winning team and the second item should be the losing team.) The second listbox
   value's should be cleared (all elements should be removed) (2 points)
6./c The Third listbox should display the list of assembled matches using another ItemTemplate,
   which shows the first team's name on the left side, then an x and the second team's name on the right. (eg. Real Madrid X Barcelona) (2 points)
