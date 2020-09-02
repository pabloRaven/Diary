using Diary.Commands;
using Diary.Models;
using Diary.Models.Wrappers;
using Diary.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Diary.ViewModels
{
    class MainViewModels : ViewModelsBase
    {
        public MainViewModels()
        {
            using (var context = new ApplicationDbContext())
            {
                var students = context.Students.ToList();
            }

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
        public AsyncRelayCommand DeleteStudentCommand { get; set; }


        private StudentWrapper _selectedStudent;

        public StudentWrapper SelectedStudent
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
            {
                _selectedGroupId = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<GroupWrapper> _group;

        public ObservableCollection<GroupWrapper> Groups
        {
            get { return _group; }
            set
            {
                _group = value;
                OnPropertyChanged();
            }

        }
        private ObservableCollection<StudentWrapper> _students;

        public ObservableCollection<StudentWrapper> Students
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
            Groups = new ObservableCollection<GroupWrapper>
            {
                new GroupWrapper  { Id = 0, Name = "Wszystkie" },
                new GroupWrapper  { Id = 1, Name = "1A" },
                new GroupWrapper  { Id = 2, Name = "2A" }

            };

            SelectedGroupId = 0;
        }
        private void RefreshDiary()
        {
            Students = new ObservableCollection<StudentWrapper>
            {
                new StudentWrapper
                {
                    FirstName = " Paweł",
                LastName =" Nowy",
                Group = new GroupWrapper {Id =1}
                },

                new StudentWrapper
                {
                    FirstName = " Ewa",
                LastName =" Adamczyk",
                Group = new GroupWrapper {Id =2}
                },
                new StudentWrapper
                {
                    FirstName = " Monika",
                LastName =" Grelewska",
                Group = new GroupWrapper {Id =3}
                },
            };
        }


    }
}
