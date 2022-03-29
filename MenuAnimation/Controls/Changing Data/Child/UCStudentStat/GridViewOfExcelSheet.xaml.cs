using DAL;
using Domain.Data.CustomeModels;
using Domain.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Astmara6.Controls.Changing_Data.Child.UCStudentStat
{
    /// <summary>
    /// Interaction logic for GridViewOfExcelSheet.xaml
    /// </summary>
    public partial class GridViewOfExcelSheet : Window
    {
        private List<StudentNumbers> _listStudentStatment;

            int countSubjects = 0;


        public GridViewOfExcelSheet(List<StudentNumbers> listStudentStatment)
        {
            InitializeComponent();

            _listStudentStatment = listStudentStatment;
            var _listStudentCollectAndCheckExixtence = CollectAndCheckExixtence(_listStudentStatment);
            var bindingList = new BindingList<StudentNumbers>(_listStudentCollectAndCheckExixtence);
            var source = new BindingSource(bindingList, null);
            studentStatmentsDataGrid.ItemsSource = source;

        }

        private List<StudentNumbers> CollectAndCheckExixtence(List<StudentNumbers> listStudentStatment)
        {
            var listStudentStatmentAfterCollecting =new  List<StudentNumbers>();
            string[] listOfCodesEnded = new string[listStudentStatment.Count];

         
            List<StudentNumbers> result = listStudentStatment
                .GroupBy(l => l.codeSubject)
                .Select(cl => new StudentNumbers
                {
                    codeSubject=cl.First().codeSubject,
                    La2e7a= string.Join(", ", cl.Select(x => x.La2e7a).ToList()),
                    nameSubject= cl.First().nameSubject,
                    NumberOfStudent=cl.Sum(c=>c.NumberOfStudent)
                }).ToList();
           
            var db = new CollegeContext();
            // adding subject in db if not exists
            int cntCheckExist = 0;
            var subjects = db.Subjects.Select(x=>x).ToList();
            foreach (var item in result)
            {
                foreach (var item2 in subjects)
                {
                    if (item.codeSubject == item2.Code )
                             cntCheckExist++;

                }

                if (cntCheckExist>0)
                {
                    cntCheckExist = 0;
                }
                else
                {
                    cntCheckExist = 0;
                    countSubjects++;
                    var sub = new Subject()
                    {
                        Code = item.codeSubject,
                        Name = item.nameSubject
                    };
                    db.Subjects.Add(sub);
                }
            }
            db.SaveChanges();
            if (countSubjects > 0)
            {
                System.Windows.MessageBox.Show("تم اضافة المواد الجديدة بنجاح ^_^");

            }
            



            return result;
        }

        private void BTNAdd_Click(object sender, RoutedEventArgs e)
        {
            studentStatmentsDataGrid.SelectAll();
            var StudentNumbers = new List<StudentNumbers>(studentStatmentsDataGrid.SelectedItems.Count);

            for (int index = 0; index < studentStatmentsDataGrid.SelectedItems.Count-1; index++)
            {
                var selectedRow = studentStatmentsDataGrid.SelectedItems[index];
              
                    var StudentNumber = (StudentNumbers)selectedRow;
                    StudentNumbers.Add(StudentNumber);
            }
            var StudentNumbersCodes = StudentNumbers.Select(x => x.codeSubject).ToList();
            var db = new CollegeContext();
            var subjects = db.Subjects
                              .Where(t => StudentNumbersCodes.Contains(t.Code));


            var subjectsIds = subjects.Select(x => x.Id).ToList();
            var StudentStatments = db.StudentStatments
                                   .Where(t => subjectsIds.Contains((int)t.IdSubject)).ToList();

           
           

                    int cnt = 0;
                    foreach (var item in subjects)
                    {
                        foreach (var item2 in StudentNumbers)
                        {
                           
                            if (item.Code == item2.codeSubject)
                            {
                              var check = StudentStatments.FirstOrDefault(x=>x.IdSubject==item.Id);
                        //if us IdSubject already exists in db
                              if (check != null)
                                    continue;
                                       string levelFromCode = Regex.Match(item2.codeSubject, @"\d+").Value;
                                        levelFromCode = levelFromCode.Substring(0, 1);
                                        int levelFromCodeInt = int.Parse(levelFromCode);
                                        db.StudentStatments.Add(new Domain.Data.Entities.StudentStatment()
                                        {
                                          IdSubject   = item.Id,
                                          La2e7a=item2.La2e7a,
                                          NumberOfStudent=item2.NumberOfStudent,
                                          IdLevel= levelFromCodeInt


                                        });
                              cnt++;
                            }
                        }
               
                
                    }
                    if (cnt>0)
                    {
                        db.SaveChanges();
                        System.Windows.MessageBox.Show(" تم إضافة العناصر التي لم يتم إضافتها من قبل بنجاح وعددها"+cnt);
                        this.Close();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("لم يتم إضافة اي عناصر من الاكسل شيت لانها موجودة كلها بالفعل ^_^");

                    }















        }

        private void studentStatmentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void studentStatmentsDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
