using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Astmara6.Controls.Changing_Data.Child.UCPlans
{
    /// <summary>
    /// Interaction logic for DialogForSubDoctors.xaml
    /// </summary>
    public partial class DialogForSubDoctors : Window
    {
        public List <NumStudents> queryForNumStudents { get; set; }
        public class NumStudents
        {
            public int? IdSubject { get; set; }
            public int? NumberOfStudent { get; set; }
        }
        public DialogForSubDoctors()
        {
            InitializeComponent();
            loadDataComboSubjectcode();
            numStudent.IsReadOnly = true;
        }
        public void loadDataComboSubjectcode()
        {
            try
            {
                //مواد
                var cd = new CollegeContext();


                //var query = cd.Subjects.Where(x=>x.Id == BranchCB.Id).Select(x => x.Name).ToList();
                var subjects= cd.Subjects.ToList();
                var queryForNameSub = subjects.Select(x => new { x.Name ,x.Id}).OrderBy(x=>x.Id).ToList();
                var query1ForCodeSub = subjects.Select(x => new { x.Code ,x.Id}).OrderBy(x=>x.Id).ToList();
                 queryForNumStudents = cd.StudentStatments.Select(x => new NumStudents {IdSubject= x.IdSubject,NumberOfStudent=x.NumberOfStudent}).OrderBy(x=>x.IdSubject).ToList();



                _SubjectsComboBox.ItemsSource = queryForNameSub;
                _SubjectsComboBox_Copy.ItemsSource = query1ForCodeSub;
                var query = queryForNumStudents.Where(x => x.IdSubject == int.Parse(_SubjectsComboBox.SelectedValue.ToString())).FirstOrDefault();
                numStudent.Text = query.NumberOfStudent.ToString();
            }
            catch (Exception) { }
        }

        private void _SubjectsComboBox_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _SubjectsComboBox.SelectedIndex = _SubjectsComboBox_Copy.SelectedIndex;
            var query = queryForNumStudents.Where(x => x.IdSubject == int.Parse(_SubjectsComboBox_Copy.SelectedValue.ToString())).FirstOrDefault();
            numStudent.Text = query.NumberOfStudent.ToString();
        }

        private void _SubjectsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _SubjectsComboBox_Copy.SelectedIndex = _SubjectsComboBox.SelectedIndex;
            var query = queryForNumStudents.Where(x => x.IdSubject == int.Parse(_SubjectsComboBox.SelectedValue.ToString())).FirstOrDefault();
            numStudent.Text = query.NumberOfStudent.ToString();
        }

        private void numStudent_chnged(object sender, TextChangedEventArgs e)
        {

        }

        private void numStudent_TextChanged(object sender, TextChangedEventArgs e)
        {
            var groups = Convert.ToDouble(numStudent.Text) / 150;
            numGroup.Text = Math.Ceiling(groups).ToString();


          
            if (Math.Ceiling(groups) == 1)
            {
               
                studentInGroup.Text = numStudent.Text;
                lastGroup.Text = "0";

            }
            else
            {
                studentInGroup.Text = "150";
                // frac num
                double frac = groups % 1;
                lastGroup.Text = (frac * 150).ToString();

            }




        }

        private void numGroup_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(numGroup.Text))
            {
                int numStudents = Convert.ToInt32(numGroup.Text);
                double fracStudents = (Convert.ToDouble(numStudent.Text) / numStudents);
                studentInGroup.Text = (Convert.ToDouble(numStudent.Text) / numStudents).ToString();
                lastGroup.Text = "0";
            }
           

        }
    }
}
