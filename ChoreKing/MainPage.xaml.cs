using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ChoreKing
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<Chore> ToDoChores { get; private set; }
        public ObservableCollection<Chore> DoneChores { get; private set; }

        public MainPage()
        {
            InitializeComponent();
            var choresFromStorage = new ObservableCollection<Chore>(LoadChores());
            ToDoChores = new ObservableCollection<Chore>(choresFromStorage.Where(x => x.WhenNext.Date <= DateTimeOffset.Now.Date));
            DoneChores = new ObservableCollection<Chore>(choresFromStorage.Where(x => x.WhenNext.Date > DateTimeOffset.Now.Date));
        }

        private ObservableCollection<Chore> LoadChores()
        {
            ObservableCollection<Chore> chores = new ObservableCollection<Chore>();

            chores.Add(new Chore("Hoover", DateTimeOffset.Now.AddDays(-1).Date));
            chores.Add(new Chore("Wash up", DateTimeOffset.Now.Date));
            chores.Add(new Chore("Dust", DateTimeOffset.Now.AddDays(1).Date));

            return chores;
        }

        private void ToDoGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DoneGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var doneChore = (Chore)button.DataContext;

            doneChore.WhenNext = DateTimeOffset.Now.Date.AddDays(1);

            MoveToDone(doneChore);
        }

        private void WhenNext_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            var datePicker = (DatePicker)sender;
            var chore = (Chore)datePicker.DataContext;
            if (chore != null)
            {
                if (e.NewDate <= DateTimeOffset.Now.Date)
                {
                    MoveToToDo(chore);
                }
                else
                {
                    MoveToDone(chore);
                }
            }
        }

        private void MoveToDone(Chore chore)
        {
            RemoveOldChore(chore);
            DoneChores.Add(chore);
        }

        private void MoveToToDo(Chore chore)
        {
            RemoveOldChore(chore);
            ToDoChores.Add(chore);
        }

        private void RemoveOldChore(Chore chore)
        {
            DoneChores.Remove(DoneChores.FirstOrDefault(x => x.Name == chore.Name));
            ToDoChores.Remove(ToDoChores.FirstOrDefault(x => x.Name == chore.Name));
        }
    }
}
