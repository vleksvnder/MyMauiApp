using Microsoft.Maui.Controls;
using MyMauiApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyMauiApp.Views
{
    public partial class StudentsPage : ContentPage
    {
        private List<Person> _allStudents;
        private List<Person> _displayedStudents;
        private List<string> _classList;

        public StudentsPage(List<Person> students)
        {
            InitializeComponent();
            _allStudents = students;
            _displayedStudents = new List<Person>(_allStudents);

            MessagingCenter.Subscribe<EditStudentPage, List<Person>>(this, "StudentsUpdated", (sender, updatedStudents) =>
            {
                _allStudents = updatedStudents;
                UpdateDisplayedStudents();
            });

            InitializeClassPicker();
            RefreshStudentsList();
        }

        private void InitializeClassPicker()
        {
            _classList = _allStudents.Select(s => s.Class).Distinct().ToList();
            classPicker.ItemsSource = _classList;
        }

        private void RefreshStudentsList()
        {
            studentsListView.ItemsSource = _displayedStudents;
        }

        private void UpdateDisplayedStudents()
        {
            _displayedStudents = _allStudents.Where(s => _classList.Contains(s.Class)).ToList();
            _displayedStudents = _displayedStudents.OrderBy(s => s.RollNumber).ToList();
            RefreshStudentsList();
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var selectedStudent = (Person)e.SelectedItem;
                Navigation.PushAsync(new EditStudentPage(selectedStudent, _allStudents));
                studentsListView.SelectedItem = null;
            }
        }

        private void OnTogglePresenceToggled(object sender, ToggledEventArgs e)
        {
            var switchControl = sender as Switch;
            var student = switchControl?.BindingContext as Person;

            if (student != null)
            {
                student.IsPresent = switchControl.IsToggled;
                SaveStudentsToFile();
                UpdateDisplayedStudents();
            }
        }

        private void OnDeleteClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var student = button?.CommandParameter as Person;

            if (student != null)
            {
                _allStudents.Remove(student);
                SaveStudentsToFile();
                UpdateDisplayedStudents();
                RefreshPicker();
            }
        }

        private void SaveStudentsToFile()
        {
            var filePath = GetFilePath();

            using (var writer = new StreamWriter(filePath))
            {
                foreach (var student in _allStudents)
                {
                    writer.WriteLine($"{student.Name},{student.Class},{student.RollNumber},{student.IsPresent}");
                }
            }
        }

        private void OnClassPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedClass = classPicker.SelectedItem as string;
            if (selectedClass != null)
            {
                _displayedStudents = _allStudents.Where(s => s.Class == selectedClass).ToList();
                _displayedStudents = _displayedStudents.OrderBy(s => s.RollNumber).ToList();
                RefreshStudentsList();
            }
        }

        private void RefreshPicker()
        {
            var selectedClass = classPicker.SelectedItem as string;
            InitializeClassPicker();
            if (!string.IsNullOrEmpty(selectedClass) && _classList.Contains(selectedClass))
            {
                classPicker.SelectedItem = selectedClass;
            }
        }

        private string GetFilePath()
        {
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var fileName = "people.txt";
            return Path.Combine(desktopPath, fileName);
        }
    }
}
