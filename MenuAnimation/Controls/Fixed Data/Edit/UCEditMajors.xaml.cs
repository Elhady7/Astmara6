using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Data;
using Astmara6Con.Controls;
using System;
using DAL;

namespace Astmara6.Controls.Fixed_Data.Edit
{
    /// <summary>
    /// Interaction logic for UCEditMajors.xaml
    /// </summary>
    public partial class UCEditMajors : UserControl
    {
        string STRNamePage;
        readonly Astmara6Con.FRMMainWindow Form = Application.Current.Windows[0] as Astmara6Con.FRMMainWindow;
        public UCEditMajors()
        {
            InitializeComponent();
        }
        public void loadData()
        {
            CollegeContext cd = new CollegeContext();

            var sections = (from p in cd.Sections
                           select p).ToList();

            sectionsDataGrid.ItemsSource = sections;

        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loadData();

            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }

        private void BTNEdit_Click(object sender, RoutedEventArgs e)
        {
            CollegeContext dataContext = new CollegeContext();
            var SectionRow = sectionsDataGrid.SelectedItem as Domain.Data.Entities.Section ;

          var sections = (from p in dataContext.Sections
                                              where p.Id == SectionRow.Id
                                              select p).Single();
            if (SectionRow.Name != sections.Name)
            {
                try
                {
                    var DepartmentRow = sectionsDataGrid.SelectedItem as Domain.Data.Entities.Section;

                    var departments = (from p in dataContext.Sections
                                                         where p.Id == DepartmentRow.Id
                                                         select p).Single();
                    departments.Name = DepartmentRow.Name;
                    dataContext.SaveChanges();
                    loadData();


                    MessageBox.Show("تم تعديل الصف بنجاح");

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    return;
                }
            }
            else
            {
                MessageBox.Show("لم يتم تعديل اي شئ برجاء عدل حتي يتم الحفظ!!");

            }
        }

        private void BTNRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                CollegeContext db = new CollegeContext();
               var BranchRow = sectionsDataGrid.SelectedItem as Domain.Data.Entities.Section;

                var sections = (from p in db.Sections
                                                  where p.Id == BranchRow.Id

                                                  select p).SingleOrDefault();
                db.Sections.Remove(sections);
                db.SaveChanges();
                loadData();

                MessageBox.Show("تم مسح العنصر بنجاح");
            }
            catch (Exception) { MessageBox.Show("حدث خطب ما برجاء المحاولة مرة أخري"); }
        }

        private void BTNRemoveAll_Click_1(object sender, RoutedEventArgs e)
        {
            CollegeContext cd = new CollegeContext();
            



            MessageBoxResult result = MessageBox.Show("هل انت متأكد من أنك تريد حذف الكل؟؟", "حذف الكل", MessageBoxButton.YesNo, MessageBoxImage.Question);


            if (result == MessageBoxResult.Yes)
            {

                try
                {

                    cd.Sections.RemoveRange(cd.Sections);

                    cd.SaveChanges();
                    loadData();

                    MessageBox.Show("تم مسح كل البيانات");
                }
                catch (Exception) { MessageBox.Show("حدث خطب ما برجاء المحاولة مرة أخري"); }
            }
            else if (result == MessageBoxResult.No)
            {
                MessageBox.Show("لم يتم حذف شئ");

            }

        }

        private void BTNBack_Click(object sender, RoutedEventArgs e)
        {

            Form.gridShow.Children.Clear();
            Form.gridShow.Children.Add(new UCMajors());
            STRNamePage = "الشعب";
            Form.ChFormName(STRNamePage);
        }

        private void SectionsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
