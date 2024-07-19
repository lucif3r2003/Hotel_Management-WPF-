using Hotel_App_Library.DAO;
using Hotel_App_Library.Models;
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

namespace Hotel_App_View.Admin
{
    /// <summary>
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        public ReportWindow()
        {
            InitializeComponent();
            TransactionDAO transactionDAO = new TransactionDAO();
            var list =transactionDAO.getListTransaction();
            lvTransaction.ItemsSource = list;
            lvTransaction.Items.Refresh();
        }

        private void lvTransaction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lvTransaction.SelectedItem is Transaction t)
            {
                int id = t.TransactionId;
                TransactionDetail transactionDetail = new TransactionDetail();
                transactionDetail.Show();
            }
        }
    }
}
