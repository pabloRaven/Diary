using Diary.Commands;
using Diary.Models;
using Diary.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace Diary.ViewModels
{
    class MainViewModels : ViewModelsBase
    {
        public MainViewModels()
        {
            AddStudentCommand = new RelayCommand(AddEditStudent);
            EditStudentCommand = new RelayCommand(AddEditStudent, CanEditDeleteStudent);
            DeleteStudentCommand = new AsyncRelayCommand(DeleteStudent, CanEditDeleteStudent);
            RefreshStudentsCommand = new RelayCommand(RefreshStudents);
            RefreshDiary();
            InitGruops();

        }
              

        public RelayCommand RefreshStudentsCommand { get; set; }
        public RelayCommand AddStudentCommand { get; set; }
        public RelayCommand EditStudentCommand { get; set; }
        public RelayCommand DeleteStudentCommand { get; set; }


        private Student _selectedStudent;

        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
            }

        }
        
        private int _selectedGroupId;

        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set 
            { _selectedGroupId = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Group> _group;

        public ObservableCollection<Group> Groups
        {
            get { return _group; }
            set
            {
                _group = value;
                OnPropertyChanged();
            }

        }
        private ObservableCollection<Student> _students;

        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged();
            }

        }
        private void RefreshStudents(object obj)
        {
            RefreshDiary();
        }
        private bool CanEditDeleteStudent(object obj)
        {
            return SelectedStudent != null;
        }

        private async Task DeleteStudent(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync(
                "Usuwanie Ucznia", $"Czy napewno chcesz ususnąc " +
                  $"ucznia { SelectedStudent.FirstName} { SelectedStudent.LastName} ?",
                  MessageDialogStyle.AffirmativeAndNegative);

            if (dialog != MessageDialogResult.Affirmative)
                    // usuwanie z bazy danych

                    RefreshDiary();
        }


        private void AddEditStudent(object obj)
        {
            var addEditStudentWindow = new AddEditStudentView();
            addEditStudentWindow.Closed += AddEditStudentWindow_Closed;
            addEditStudentWindow.ShowDialog();
        }

        private void AddEditStudentWindow_Closed(object sender, System.EventArgs e)
        {
            RefreshDiary();
        }

        private void InitGruops()
        {
            Groups = new ObservableCollection<Group>
            {
                new Group  { Id = 0, Name = "Wszystkie" },
                new Group  { Id = 1, Name = "1A" },
                new Group  { Id = 2, Name = "2A" }

            };

            SelectedGroupId = 0;
        }
        private void RefreshDiary()
        {
            Students = new ObservableCollection<Student>
            {
                new Student
                {
                    FirstName = " Paweł",
                LastName =" Nowy",
                Group = new Group {Id =1}
                },

                new Student
                {
                    FirstName = " Ewa",
                LastName =" Adamczyk",
                Group = new Group {Id =2}
                },
                new Student
                {
                    FirstName = " Monika",
                LastName =" Grelewska",
                Group = new Group {Id =3}
                },
            };
        }


    }
}
