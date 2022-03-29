
using Astmara6Con;
using Astmara6Con.Controls;
using MenuAnimado1.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Collections.Generic;
using Astmara6.Controls.Fixed_Data.Edit;
using System.Collections;
using System.Data;
using Microsoft.Win32;
using System.IO;
using Microsoft.Office.Interop;
using Astmara6.Controls.Changing_Data.Child.UCStudentStat;
using DAL;
using Domain.Data.Entities;
using Domain.Data.CustomeModels;

namespace Astmara6Con.Controls
{
    /// <summary>
    /// Interaction logic for student_inf.xaml
    /// </summary>
    public partial class UCStudentStatement : UserControl
    {
        Microsoft.Office.Interop.Excel.Application xlApp;
        Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
        Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
        Microsoft.Office.Interop.Excel.Range range;

        string strCodeColumn;
        string strStudentNumColumn;
        string strSubjectNameAColumn;
        string strRosterColumn;
        int rCnt;
        int cCnt;
        int rw = 0;
        int clBigger = 0;
        int clSmaller = 0;
        int codeColumn = 0;
        int studentNumColumn=0;
        int subjectNameAColumn = 0;
        int rosterColumn = 0;


        string STRNamePage;
        readonly FRMMainWindow Form = Application.Current.Windows[0] as FRMMainWindow;

        private CollegeContext cd { get;  set; }

        public UCStudentStatement()
        {
            InitializeComponent();
            loadDatagrid();
           
             cd = new CollegeContext();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCPlan());
            STRNamePage = "الخطة";
            Form.ChFormName(STRNamePage);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCCourses());
            STRNamePage = "المقرارت";
            Form.ChFormName(STRNamePage);

        }

        private void Search(object sender, RoutedEventArgs e)
        {

            try
            {

           
                    xlApp = new Microsoft.Office.Interop.Excel.Application();
                     OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == true)
                        FileName.Text = File.ReadAllText(openFileDialog.FileName);

                    xlWorkBook = xlApp.Workbooks.Open(openFileDialog.FileName, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                    xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                    ///
                    string studentNum, subjectName, subjectCode, roster;
                    if (MessageBox.Show("هل تريد اضافة ابعاد جديدة للإكسيل شيت؟", "رسالة تأكيد", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        //do no stuff

                        var excel = cd.ExcelLimitations.Select(x => x).FirstOrDefault();
                        studentNum = excel.studentNumRange;
                        subjectCode = excel.subjectCodeRange;
                        subjectName = excel.subjectNameRange;
                        roster = excel.rosterRange;

                    }
                    else
                    {
                        DialogStudentStat inputDialog = new DialogStudentStat();

                        inputDialog.ShowDialog();
                         studentNum = inputDialog.studentNum;
                         subjectName = inputDialog.subjectName;
                         subjectCode = inputDialog.subjectCode;
                         roster = inputDialog.roster;
                    }




                    range = xlWorkSheet.UsedRange;
                    //rw = range.Rows.Count;
                    var cl = range.Columns.Count;


                    Split(studentNum, subjectName, subjectCode, roster, out rw, out clBigger,out clSmaller ,out codeColumn,
                          out studentNumColumn,out subjectNameAColumn,out rosterColumn);


                    Microsoft.Office.Interop.Excel.Range objRange = null;
                    var listStudentStatment = new List<StudentNumbers>();

                    for (rCnt = 11; rCnt <= xlWorkSheet.UsedRange.Cells.Rows.Count; rCnt++)
                    {
                        objRange = range.Cells[rCnt, codeColumn];
                         strCodeColumn = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)objRange.MergeArea[1, 1]).Text).Replace(" ", string.Empty);




                        if (strCodeColumn.Contains("ر"))
                        {
                            objRange = range.Cells[rCnt, studentNumColumn];

                                     strStudentNumColumn = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)objRange.MergeArea[1, 1]).Text).Replace(" ", string.Empty);
                            objRange = range.Cells[rCnt, subjectNameAColumn];
                                   strSubjectNameAColumn = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)objRange.MergeArea[1, 1]).Text);
                            objRange = range.Cells[rCnt, rosterColumn];
                                   strRosterColumn= Convert.ToString(((Microsoft.Office.Interop.Excel.Range)objRange.MergeArea[1, 1]).Text).Trim();
                            // add dataset and create another dialog to view data on it 
                            var StudentStatment = new StudentNumbers()
                            {

                                NumberOfStudent =Convert.ToInt32(strStudentNumColumn.Replace(" ", string.Empty)),
                                La2e7a= strRosterColumn,
                                codeSubject=strCodeColumn,
                                nameSubject=strSubjectNameAColumn
                            };

                            listStudentStatment.Add(StudentStatment);
                        }



                    }
                    var gridViewStudentStat = new GridViewOfExcelSheet(listStudentStatment);
                    gridViewStudentStat.ShowDialog();


                    loadDatagrid();
                xlWorkBook.Close(0);
                xlApp.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                xlWorkBook.Close();
                xlApp.Quit();
            }
        }

        private void Split(string studentNum, string subjectName, string subjectCode, string roster,
            out int rw, out int clBigger, out int clSmaller ,out int codeColumn, 
            out int studentNumColumn,out int subjectNameAColumn,out int rosterColumn)
        {
          


       
           string all= studentNum + subjectName + subjectCode + roster;

            char[] subjectCodeArr = subjectCode.ToCharArray();
            char[] studentNumArr = studentNum.ToCharArray();
            char[] subjectNameArr = subjectName.ToCharArray();
            char[] rosterArr = roster.ToCharArray();

            // code column
            codeColumn = subjectCodeArr[0]-64;
            studentNumColumn = studentNumArr[0] - 64;
            subjectNameAColumn = subjectNameArr[0] - 64;
            rosterColumn = rosterArr[0] - 64;


            var cBigger = 0;
            var cSmaller = 100;
            foreach (var c in all)
            {
                if (char.ToUpperInvariant(c) == 58)
                     continue;
                
                if (char.ToUpperInvariant(c) > cBigger)
                    cBigger = c;
                if (char.ToUpperInvariant(c) < cSmaller)
                    cSmaller = c;
            }


           
            cBigger = cBigger - 64;
            cSmaller = cSmaller - 64;
            clSmaller = cSmaller;
            clBigger = cBigger;
            rw = 500;

        }

        public class DGItem
        {
            public String NameSub { get; set; }
            public String TypeOfBranch { get; set; }
            public String Code { get; set; }
            public String NameLev { get; set; }
            public int? NumOfStudent { get; set; }
            public int BranchSelectedId { get; set; }
            public List<BranchComboGrid> branchComboGrid { get; set; }

        }
        public class BranchComboGrid
        {
            public int BranchId { get; set; }

            public string TypeOfBranch1 { get; set; }

        }
          


    
        public void loadDatagrid()
        {
            var db = new CollegeContext();

            var query = (from sc in db.StudentStatments
                         
                         join l in db.Levels on sc.IdLevel equals l.Id
                         join su in db.Subjects on sc.IdSubject equals su.Id

                         select new DGItem
                         {
                             NameSub = su.Name
                            ,
                             branchComboGrid = db.Branches.Select(x => new BranchComboGrid
                             { BranchId = x.Id, TypeOfBranch1 = x.TypeOfBranch }).ToList()
                            
                             ,
                             Code = su.Code,
                             NameLev=l.Name,
                             NumOfStudent = sc.NumberOfStudent
                         }).ToList();

        

            studentStatmentsDataGrid.ItemsSource = query;
            


        }
       
        private void department1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void NumberValidationTextBox(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            //can't write but numbers for TBPaper
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CBTypeOfBranch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
       
            
        }

      

        private void BTNRemove_Click_1(object sender, RoutedEventArgs e)
        {


            DGItem dataRow = (DGItem)studentStatmentsDataGrid.SelectedItem;

           
            try
            {

                if (true)
                {

                    string NameSub = dataRow.NameSub==null ?"": dataRow.NameSub.ToString();
                    string TypeOfBranch = dataRow.TypeOfBranch ==null?"": dataRow.TypeOfBranch.ToString();
                    string Code = dataRow.Code==null?"": dataRow.Code.ToString();
                    string NameLev = dataRow.NameLev==null?"": dataRow.NameLev.ToString();
                    int? NumOfStudent = dataRow.NumOfStudent;
        
          




            try
            {
                Subject sub =
                                             (from p in cd.Subjects
                                              where (p.Name == NameSub)
                                              select p).SingleOrDefault();

              
                Branch bran =
                                           (from p in cd.Branches
                                            where (p.TypeOfBranch == TypeOfBranch)
                                            select p).SingleOrDefault();
                    Level level =
                                         (from p in cd.Levels
                                          where (p.Name == NameLev)
                                          select p).SingleOrDefault();
             



                StudentStatment studentStatment = (from p in cd.StudentStatments
                                                  where ((p.IdSubject == sub.Id) && (p.NumberOfStudent == NumOfStudent))
                                                   select p).FirstOrDefault();
                cd.StudentStatments.Remove(studentStatment);
                cd.SaveChanges();
                loadDatagrid();

                MessageBox.Show("تم حذف الصف بنجاح");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            studentStatmentsDataGrid.SelectAll();
            var StudentNumbers = new List<DGItem>(studentStatmentsDataGrid.SelectedItems.Count);

            for (int index = 0; index < studentStatmentsDataGrid.SelectedItems.Count - 1; index++)
            {
                var selectedRow = studentStatmentsDataGrid.SelectedItems[index];

                var StudentNumber = (DGItem)selectedRow;
                StudentNumbers.Add(StudentNumber);
            }

        }

        private void RemoveAll(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("هل انت متأكد من أنك تريد حذف الكل؟؟", "حذف الكل", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try { 
                var db = new CollegeContext();
                db.StudentStatments.RemoveRange(db.StudentStatments);
                    db.SaveChanges();
                loadDatagrid();
                MessageBox.Show("تم مسح كل البيانات");
            }
            catch (Exception) { MessageBox.Show("حدث خطب ما برجاء المحاولة مرة أخري"); }
        }
        }
    }
}
