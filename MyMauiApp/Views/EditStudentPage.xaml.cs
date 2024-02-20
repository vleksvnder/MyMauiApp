using MyMauiApp.Models;

namespace MyMauiApp.Views
{
    public partial class EditStudentPage : ContentPage
    {
        private readonly Person _student;
        private readonly List<Person> _students;
        private readonly string _originalName;
        private readonly string _originalClass;
        private readonly int _originalRollNumber;

        public EditStudentPage(Person student, List<Person> students)
        {
            InitializeComponent();
            _student = student;
            _students = students;
            BindingContext = _student;

            _originalName = _student.Name;
            _originalClass = _student.Class;
            _originalRollNumber = _student.RollNumber;

            txtName.Text = _originalName;
            txtClass.Text = _originalClass;
            txtRollNumber.Text = _originalRollNumber.ToString();
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            _student.Name = string.IsNullOrEmpty(txtName.Text) ? _originalName : txtName.Text;
            _student.Class = string.IsNullOrEmpty(txtClass.Text) ? _originalClass : txtClass.Text;
            _student.RollNumber = string.IsNullOrEmpty(txtRollNumber.Text) ? _originalRollNumber : int.Parse(txtRollNumber.Text);

            SaveStudentsToFile();
            Navigation.PopToRootAsync();
        }

        private void SaveStudentsToFile()
        {
            var filePath = GetFilePath();

            using (var writer = new StreamWriter(filePath))
            {
                foreach (var student in _students)
                {
                    writer.WriteLine($"{student.Name},{student.Class},{student.RollNumber},{student.IsPresent}");
                }
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
