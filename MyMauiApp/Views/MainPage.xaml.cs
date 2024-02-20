using Microsoft.Maui.Controls;
using MyMauiApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyMauiApp.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly List<Person> people = new List<Person>();
        private List<string> classList = new List<string>();
        private int luckyRollNumber;

        public MainPage()
        {
            InitializeComponent();

            LoadPeopleFromFile();

            classPicker.ItemsSource = classList;

            GenerateLuckyRollNumber();

            UpdatePageTitle();
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtClass.Text) || string.IsNullOrWhiteSpace(txtRollNumber.Text))
            {
                DisplayAlert("Error", "Wprowadź wszystkie dane.", "OK");
                return;
            }

            var person = new Person
            {
                Name = txtName.Text,
                Class = txtClass.Text,
                RollNumber = int.Parse(txtRollNumber.Text)
            };

            people.Add(person);
            SavePeopleToFile();

            UpdateClassList();
        }

        private void UpdatePageTitle()
        {
            Title = $"System Losowania, szczęśliwy numerek: {luckyRollNumber}";
        }

        private void GenerateLuckyRollNumber()
        {
            var random = new Random();
            luckyRollNumber = random.Next(1, 35);
        }

        private void SavePeopleToFile()
        {
            var sortedPeople = people.OrderByDescending(p => p.RollNumber).ToList();

            var filePath = GetFilePath();

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var person in sortedPeople)
                {
                    writer.WriteLine($"{person.Name},{person.Class},{person.RollNumber},{person.IsPresent}");
                }
            }
        }

        private void OnLoadClicked(object sender, EventArgs e)
        {
            LoadPeopleFromFile();
        }

        private void LoadPeopleFromFile()
        {
            var filePath = GetFilePath();
            if (File.Exists(filePath))
            {
                people.Clear();
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(',');
                        if (parts.Length == 4)
                        {
                            var person = new Person
                            {
                                Name = parts[0],
                                Class = parts[1],
                                RollNumber = int.Parse(parts[2]),
                                IsPresent = bool.Parse(parts[3])
                            };
                            people.Add(person);
                        }
                    }
                }

                UpdateClassList();
            }
        }

        private void OnPickRandomClicked(object sender, EventArgs e)
        {
            if (people.Any())
            {
                if (classPicker.SelectedItem == null)
                {
                    DisplayAlert("Error", "Wybierz klasę.", "OK");
                    return;
                }

                var random = new Random();
                var selectedClass = classPicker.SelectedItem.ToString();
                var selectedPeople = people.Where(p => p.Class == selectedClass && p.RollNumber != luckyRollNumber && p.IsPresent).ToList();

                if (selectedPeople.Any())
                {
                    Person randomPerson;
                    do
                    {
                        randomPerson = selectedPeople[random.Next(selectedPeople.Count)];
                    } while (randomPerson.RollNumber == luckyRollNumber);

                    DisplayAlert("Wylosowano osobę!", $"Imię i nazwisko: {randomPerson.Name}, Klasa: {randomPerson.Class}, Numer w dzienniku: {randomPerson.RollNumber}", "OK");
                }
                else
                {
                    DisplayAlert("Error", "Brak kwalifikujących się osób w wybranej klasie.", "OK");
                }
            }
            else
            {
                DisplayAlert("Error", "Brak dostępnych osób.", "OK");
            }
        }

        private void OnEditStudentsClicked(object sender, EventArgs e)
        {
            if (people != null)
            {
                Navigation.PushAsync(new StudentsPage(people));
            }
            else
            {
                DisplayAlert("Error", "Brak dostępnych danych uczniów.", "OK");
            }
        }

        private void UpdateClassList()
        {
            classList = people.Select(p => p.Class).Distinct().ToList();
            classPicker.ItemsSource = classList;
        }

        private string GetFilePath()
        {
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var fileName = "people.txt";
            var filePath = Path.Combine(desktopPath, fileName);

            return filePath;
        }
    }
}
