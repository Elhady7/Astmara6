using DAL;
using Domain.Data.Entities;
using System;
using System.Linq;
using System.Windows;

namespace Astmara6.Controls.Changing_Data.Child.UCStudentStat
{
    /// <summary>
    /// Interaction logic for DialogStudentStat.xaml
    /// </summary>
    public partial class DialogStudentStat : Window
    {
      
        public string studentNum
        {
            get { return stud_num.Text; }
            set { stud_num.Text = value; }
        }
        public string subjectName
        {
            get { return name_Subject.Text; }
            set { name_Subject.Text = value; }
        }
        public string subjectCode
        {
            get { return subject_code.Text; }
            set { subject_code.Text = value; }
        }
        //لائحة
        public string roster
        {
            get { return Roster.Text; }
            set { Roster.Text = value; }
        }

        private CollegeContext db { get;  set; }

        public void loadDataExcelLimitations()
        {
            try
            {
                CollegeContext cd = new CollegeContext();

                var Excel = (from p in cd.ExcelLimitations
                             select p).FirstOrDefault();
                stud_num.Text = Excel.studentNumRange;
                subject_code.Text = Excel.subjectCodeRange;
                name_Subject.Text = Excel.subjectNameRange;
                Roster.Text = Excel.rosterRange;
            }
            catch (Exception) { };
        }
        public DialogStudentStat()
        {
            InitializeComponent();
            loadDataExcelLimitations();
             db = new CollegeContext();

        }

        private void Edit_Button(object sender, RoutedEventArgs e)
        {

            var ExcelLimitation = (from p in db.ExcelLimitations
                            select p).FirstOrDefault();
            ExcelLimitation.rosterRange = Roster.Text;
            ExcelLimitation.studentNumRange = stud_num.Text;
            ExcelLimitation.subjectCodeRange = subject_code.Text;
            ExcelLimitation.subjectNameRange = name_Subject.Text;
            db.SaveChanges();
            MessageBox.Show("تم تعديل الرينج بنجاح");
        }
    }
}
