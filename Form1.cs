

using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml;
using System.Drawing.Drawing2D;
using System.Text;
using System.Text.RegularExpressions;
// Add these using statements at the top of your file
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TDD_Project
{
    public partial class Form1 : Form
    {

        private System.Windows.Forms.ToolTip toolTip1;
        public Form1()
        {

            this.MaximizeBox = false;
            InitializeComponent();
            this.toolTip1 = new System.Windows.Forms.ToolTip();
            this.toolTip1.IsBalloon = true; // set the IsBalloon property to true
            addcoloums();
            SortStudents();

        }


        void addcoloums()
        {
            listViewStudents.Columns.Add("ID                      ");
            listViewStudents.Columns.Add("First Name");
            listViewStudents.Columns.Add("Last Name");
            listViewStudents.Columns.Add("Email                                         ");
            listViewStudents.Columns.Add("Grade 1");
            listViewStudents.Columns.Add("Grade 2");
            listViewStudents.Columns.Add("Grade 3");
            listViewStudents.Columns.Add("Grade 4");
            listViewStudents.Columns.Add("Grade 5");
            listViewStudents.Columns.Add("Average Grade");
            listViewStudents.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

        }





        public int UnitTestExmple(int num)
        {

            if (num == 1)
            {
                return 2;
            }
            else { return 1000; }
        }



        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }


        public void buttonAddStudent_Click(object sender, EventArgs e)
        {
            string path = (@"C:\Users\or656\source\repos\TDD Project\students.txt");

            // Regular expression to validate ID (9 digits)
            Regex idRegex = new Regex(@"^\d{9}$");

            // Regular expression to validate name (letters only)
            Regex nameRegex = new Regex(@"^[a-zA-Z]+$");

            // Regular expression to validate grades (numbers only)
            Regex gradeRegex = new Regex(@"^\d+$");

            // Regular expression to validate grades (numbers only, between 0 and 100 or 777)
            Regex gradeRegex2 = new Regex(@"^(100|[1-9]\d|\d)(\.\d+)?|777$");

            System.Windows.Forms.TextBox[] textBoxes = { textBoxGrade1, textBoxGrade2, textBoxGrade3, textBoxGrade4, textBoxGrade5 };
            bool allInputsValid = true;

            foreach (System.Windows.Forms.TextBox textBox in textBoxes)
            {
                if (!int.TryParse(textBox.Text, out int grade) || grade < 0 || grade > 100)
                {
                    allInputsValid = false;
                    // If any input is invalid, break out of the loop early.
                    break;
                }
            }








            if (!idRegex.IsMatch(textBoxID.Text))
            {
                MessageBox.Show("Invalid ID format. Please enter 9 digits.");
                return;
            }
            if (!nameRegex.IsMatch(textBoxFirstName.Text) || !nameRegex.IsMatch(textBoxLastName.Text))
            {
                MessageBox.Show("Invalid name format. Please enter letters only.");
                return;
            }
            if (!gradeRegex.IsMatch(textBoxGrade1.Text) || !gradeRegex.IsMatch(textBoxGrade2.Text) || !gradeRegex.IsMatch(textBoxGrade3.Text) || !gradeRegex.IsMatch(textBoxGrade4.Text) || !gradeRegex.IsMatch(textBoxGrade5.Text))
            {
                MessageBox.Show("Invalid grade format. Please enter numbers only.");
                return;
            }

            if (!gradeRegex2.IsMatch(textBoxGrade1.Text) || !gradeRegex2.IsMatch(textBoxGrade2.Text) || !gradeRegex2.IsMatch(textBoxGrade3.Text) || !gradeRegex2.IsMatch(textBoxGrade4.Text) || !gradeRegex2.IsMatch(textBoxGrade5.Text))
            {
                MessageBox.Show("Invalid grade format. Grades need to be from 0 to 100, or 777.");
                return;
            }

            if (!allInputsValid)
            {
                MessageBox.Show("Invalid grade format. Grades need to be from 0 to 100, or 777.");
                return;
            }

            if (IsValidEmail(textBoxEmail.Text) == false)
            {
                MessageBox.Show("Invalid email format. Please enter email only.");
                return;
            }
            // Create a new StreamWriter object to write to the text file
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                // Write the new student data as a comma-separated string
                var studentData = string.Join(",", textBoxID.Text, textBoxFirstName.Text, textBoxLastName.Text, textBoxEmail.Text, textBoxGrade1.Text, textBoxGrade2.Text, textBoxGrade3.Text, textBoxGrade4.Text, textBoxGrade5.Text, "Avg");
                writer.WriteLine(studentData);
            }

            // Clear the text boxes after adding the student
            textBoxID.Clear();
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            textBoxEmail.Clear();
            textBoxGrade1.Clear();
            textBoxGrade2.Clear();
            textBoxGrade3.Clear();
            textBoxGrade4.Clear();
            textBoxGrade5.Clear();
            SortStudents();


        }





        public void DeleteAllStudents(string path)
        {
            File.WriteAllText(path, "");
        }



        public static readonly Random RandomGenerator = new Random();

        public static string GenerateRandomId()
        {
            return RandomGenerator.Next(100000000, 1000000000).ToString();
        }

        public static string GenerateRandomName()
        {
            string[] firstNames = { "Jon", "Daenerys", "Jaime", "Sansa", "Tyrion", "Cersei", "Arya", "Bran", "Theon", "Margaery" };
            string[] lastNames = { "Snow", "Targaryen", "Lannister", "Stark", "Lannister", "Lannister", "Stark", "Stark", "Greyjoy", "Tyrell" };
            string firstName = firstNames[RandomGenerator.Next(0, firstNames.Length)];
            string lastName = lastNames[RandomGenerator.Next(0, lastNames.Length)];
            return $"{firstName} {lastName}";
        }

        public static string GenerateRandomEmail()
        {
            string[] domains = { "gmail.com", "yahoo.com", "hotmail.com", "outlook.com", "aol.com" };
            string name = Regex.Replace(GenerateRandomName().ToLower(), @"\s", ".");
            string domain = domains[RandomGenerator.Next(0, domains.Length)];
            return $"{name}@{domain}";
        }

        public static string GenerateRandomGrade()
        {
            int randomNumber = RandomGenerator.Next(0, 101);
            return randomNumber == 100 ? "777" : randomNumber.ToString();
        }

        public void pictureBoxDeleteAll_Click(object sender, EventArgs e)
        {
            string path = @"C:\Users\or656\source\repos\TDD Project\students.txt";
            DeleteAllStudents(path);
            SortStudents();
        }

        public static void MergeSort(List<string[]> students, int left, int right)
        {
            if (left < right)
            {
                int mid = (left + right) / 2;
                MergeSort(students, left, mid);
                MergeSort(students, mid + 1, right);
                Merge(students, left, mid, right);
            }
        }


        //runing time of  O(n ∗ l o g n )
        public static void Merge(List<string[]> students, int left, int mid, int right)
        {
            int i = left, j = mid + 1, k = 0;
            string[][] temp = new string[right - left + 1][];

            while (i <= mid && j <= right)
            {
                if (Convert.ToDouble(students[i][9]) >= Convert.ToDouble(students[j][9]))
                {
                    temp[k] = students[i];
                    i++;
                }
                else
                {
                    temp[k] = students[j];
                    j++;
                }
                k++;
            }

            while (i <= mid)
            {
                temp[k] = students[i];
                i++;
                k++;
            }

            while (j <= right)
            {
                temp[k] = students[j];
                j++;
                k++;
            }

            for (i = left; i <= right; i++)
            {
                students[i] = temp[i - left];
            }
        }




        //// Sort the students by average grade using bubble sort of O(n^2)
        //void BubbleSort(List<string[]> students)
        //{
        //    // Sort the students by average grade using bubble sort of O(n^2)
        //    for (int i = 0; i < students.Count; i++)
        //    {
        //        for (int j = 0; j < students.Count - 1; j++)
        //        {
        //            double avg1 = Convert.ToDouble(students[j][9]);
        //            double avg2 = Convert.ToDouble(students[j + 1][9]);
        //            if (avg1 < avg2)
        //            {
        //                string[] temp = students[j];
        //                students[j] = students[j + 1];
        //                students[j + 1] = temp;
        //            }
        //        }
        //    }
        //}



        // Read all lines from the file and split each line into student data
        public List<string[]> ReadStudentDataFromFile(string filePath)
        {
            List<string[]> students = new List<string[]>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] studentData = line.Split(',');
                    students.Add(studentData);
                }
            }
            return students;
        }

        // Calculate the average grade of each student
        public void CalculateAverageGrades(List<string[]> students)
        {
            for (int s = 0; s < students.Count; s++)
            {
                string[] studentData = students[s];
                double sum = 0;
                int count = 0;
                double[] grades = new double[5];
                int gradesCount = 0;
                for (int i = 4; i <= 8; i++)
                {
                    if (studentData[i] == "777")
                    {
                        continue; // skip the grade of 777
                    }
                    if (double.TryParse(studentData[i], out double grade))
                    {
                        grades[gradesCount++] = grade;
                        if (gradesCount == 5)
                        {
                            sum += grades[0] + grades[1] + grades[2] + grades[3] + grades[4];
                            count += 5;
                            gradesCount = 0;
                        }
                    }
                }
                for (int i = 0; i < gradesCount; i++)
                {
                    sum += grades[i];
                    count++;
                }
                double avg = count > 0 ? sum / count : 0; // avoid division by zero
                studentData[9] = avg.ToString();
            }
        }


        // Perform a merge sort on the list of student data
        public void MergeSortStudentData(List<string[]> studentDataList)
        {
            MergeSort(studentDataList, 0, studentDataList.Count - 1);
        }

        // Update the UI with the sorted student data
        public void UpdateListViewWithStudentData(List<string[]> students)
        {
            var messageBuilder = new StringBuilder();
            var itemsToAdd = new List<ListViewItem>();
            for (int i = 0; i < students.Count; i++)
            {
                var studentData = students[i];
                var avg = Convert.ToDouble(studentData[9]);
                var item = new ListViewItem(studentData);
                itemsToAdd.Add(item);
            }

            // Add the items to the list view on the UI thread
            if (listViewStudents.InvokeRequired)
            {
                listViewStudents.Invoke(new Action(() => listViewStudents.Items.AddRange(itemsToAdd.ToArray())));
            }
            else
            {
                listViewStudents.Items.AddRange(itemsToAdd.ToArray());
            }
        }

        // Sort the student data and update the UI
        public void SortAndUpdateUI()
        {
            string path = @"C:\Users\or656\source\repos\TDD Project\students.txt";
            List<string[]> students = ReadStudentDataFromFile(path);
            CalculateAverageGrades(students);
            DateTime startTime = DateTime.Now;
            MergeSortStudentData(students);
            DateTime endTime = DateTime.Now;
            TimeSpan runTime = endTime - startTime;
            labelRuningTime.Text = "Run time Of MergeSort: " + runTime.Milliseconds + " ms";
            var studentCount = students.Count;
            labelAmountOfStudents.Text = "Amount of Students: " + studentCount;
            listViewStudents.Items.Clear();
            UpdateListViewWithStudentData(students);
            progressBar.Visible = false;
        }

        // Call this function to perform the entire process of sorting the student data and updating the UI
        public void SortStudents()
        {
            progressBar.Visible = true;

            SortAndUpdateUI();





        }


        public void buttonAddRandom_Click(object sender, EventArgs e)
        {


            CreateRandomStudentsAsync();



        }

        public async Task CreateRandomStudentsAsync()
        {


            string path = @"C:\Users\or656\source\repos\TDD Project\students.txt";
            int numStudents = 10000;
            // Start the progress bar
            progressBar.Maximum = numStudents;
            progressBar.Value = 0;
            progressBar.Visible = true;


            Random rand = new Random();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < numStudents; i++)
            {
                string id = GenerateRandomId();
                string name = GenerateRandomName();
                string[] nameParts = name.Split(' ');
                string firstName = nameParts[0];
                string lastName = nameParts[1];
                string email = GenerateRandomEmail();
                string grade1 = GenerateRandomGrade();
                string grade2 = GenerateRandomGrade();
                string grade3 = GenerateRandomGrade();
                string grade4 = GenerateRandomGrade();
                string grade5 = GenerateRandomGrade();
                string[] studentData = { id, firstName, lastName, email, grade1, grade2, grade3, grade4, grade5, "" };
                sb.AppendLine(string.Join(",", studentData));

                progressBar.Invoke((MethodInvoker)delegate
                {
                    progressBar.Value = i + 1;
                });

            }

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                await writer.WriteAsync(sb.ToString());
            }

            SortStudents();

            // Hide the progress bar
            progressBar.Visible = false;



        }

        private void label6_Paint(object sender, PaintEventArgs e)
        {
            // Get the label's client rectangle and subtract the border width
            System.Drawing.Rectangle rect = label6.ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;

            // Define the corner radius
            int radius = 10;

            // Create a graphics path for the rounded rectangle
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90); // Top-left corner
            path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90); // Top-right corner
            path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90); // Bottom-right corner
            path.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90); // Bottom-left corner
            path.CloseFigure();

            // Draw the rounded rectangle
            e.Graphics.DrawPath(new Pen(System.Drawing.Color.White, 1), path);
        }

        private void pictureBox9_MouseHover(object sender, EventArgs e)
        {

            this.toolTip1.SetToolTip(this.pictureBox9, "Winter is Coming!"); // set the tooltip for the PictureBox

        }
    }



}