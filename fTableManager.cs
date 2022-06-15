using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Personal_PBL.DTO;
using Personal_PBL.DAO;

namespace Personal_PBL
{
    public partial class fTableManager : Form
    {
        private Account loginAccount;
        public static int TableWidth = 70;
        public static int TableHeight = 70;
        float totalPrice = 0;

        public Account LoginAccount 
        {
            get { return loginAccount; }
            set { loginAccount = value; SetStatus(loginAccount.Status); }
        }

        public fTableManager(Account loginAccount)
        {
            InitializeComponent();
            LoadTable();
            LoadCategory();
            LoginAccount = loginAccount;
        }
        public void SetStatus(String status)
        {
            if(status.Equals("Bình thường")){
                adminToolStripMenuItem.Enabled = false;
            }else adminToolStripMenuItem.Enabled = true;
        }
        #region Method
        private void LoadCategory()
        {   cbbCategory.Items.Clear();
            List<Category> list = CategoryDAO.Instance.GetListCategory();
            cbbCategory.DataSource = list;
            cbbCategory.DisplayMember = "displayName";
        }
        private void LoadFoodByCategory(int idCategory)
        {
            List<Food> list = FoodDAO.Instance.GetListFoodbyIdCategory(idCategory);
            cbbFood.DataSource = list;
            cbbFood.DisplayMember = "displayName";
        }
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void LoadListTableExceptCurrentTable(int idTable)
        {
                List<Table> tableList = new List<Table>();
                tableList = TableDAO.Instance.LoadTableListExceptIDTable(idTable);
                cbbListTable.DataSource = tableList;
                cbbListTable.DisplayMember = "name";
        }
        private void fTableManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Bạn có chắc chắn muốn đăng xuất","Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
        private void LoadTable()
        {
            //add theo kiểu hiện ra các button
            flpTable.Controls.Clear();
            List<Table> tableList = TableDAO.Instance.LoadTableList();
            
            foreach(Table table in tableList)
            {
                Button btn = new Button() { Width = TableWidth ,Height = TableHeight } ;
                btn.Text = table.Name + "\n" + table.Status;//giải thích vì sao lại chuyển qua list cho dễ
                btn.Tag = table;
                switch (table.Status)
                {
                    case "Còn trống":
                        btn.BackColor = Color.Green;
                        break;
                    case "Có người":
                        btn.BackColor = Color.Red;
                        break;
                    
                }
                btn.Click += btnClick;
                btn.Tag = table;
                flpTable.Controls.Add(btn);
            }
        }
        //lấy ra thông tin Bill chưa thanh toán
        private void ShowBill(int tableID)
        {
            totalPrice = 0;
            lsvBill.Items.Clear();
            List<Personal_PBL.DTO.Menu> menuList = MenuDAO.Instance.GetListByTable(tableID);
            foreach(Personal_PBL.DTO.Menu item in menuList)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
             
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                lsvBill.Items.Add(lsvItem);
                totalPrice+=item.TotalPrice;
            }
            txttotalPrice.Text = totalPrice.ToString();
        }
        #endregion
        #region Event
        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfile f = new fAccountProfile(loginAccount);
            f.ShowDialog();
            LoginAccount = AccountDAO.Instance.GetAccountByUserName(loginAccount.UserName);
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }
        private void btnClick(object sender,EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).Id;//Dinkout
            lsvBill.Tag = (sender as Button).Tag;//lưu thông tin table vào lsvBill
            ShowBill(tableID);
            LoadListTableExceptCurrentTable(tableID);
            Table table = lsvBill.Tag as Table;
            lblNotify.Text = "Đang đặt món cho " + table.Name;
            lblNotify.Visible = true;
        }
        #endregion

        private void cbbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Category category = (Category)cbbCategory.SelectedItem;

            if (category == null) return;
            else
            {
                int idCategory = category.Id;
                LoadFoodByCategory(idCategory);
            }
        }

        private void cbbAddFood_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;//lấy bàn hiện tại đang chọn
            int idBill = BillDAO.Instance.GetUnCheckBillIDByTableID(table.Id);
            int idFood = (cbbFood.SelectedItem as Food).Id;
            int foodNum =(int) nmFoodCount.Value;
            if(idBill == -1)
            {
                //thêm mới bill
                BillDAO.Instance.InsertBill(table.Id);
                BillInforDAO.Instance.InsertBillInfor(BillDAO.Instance.GetMaxIdBill(), idFood, foodNum);
                LoadTable();
            }
            else
            {
                BillInforDAO.Instance.InsertBillInfor(idBill, idFood, foodNum);
            }
            ShowBill(table.Id);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table ;
            int idBill = BillDAO.Instance.GetUnCheckBillIDByTableID(table.Id) ;
            int discount = (int)nmrDiscount.Value;
            if(idBill != -1)
            {
                if(MessageBox.Show($"Bạn có chắc muốn thanh toán cho bàn {table.Name}","Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idBill,discount,totalPrice);
                    TableDAO.Instance.CheckOut(table.Id);
                    ShowBill(table.Id);
                    LoadTable();
                }
            }
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            float discount = (100 - (float)nmrDiscount.Value)/100;

            txttotalPrice.Text = (totalPrice * discount).ToString();
        }

        private void btnSwapTable_Click(object sender, EventArgs e)
        {
            Table tableFrom = lsvBill.Tag as Table;
            int idBillFrom = BillDAO.Instance.GetUnCheckBillIDByTableID(tableFrom.Id) ;

            Table tableTo = cbbListTable.SelectedItem as Table ;
            int idBillTo = BillDAO.Instance.GetUnCheckBillIDByTableID(tableTo.Id);
            if(idBillFrom == -1)
            {
                return;
            }
            else
            {
                if(MessageBox.Show($"Bạn có muốn chuyển từ {tableFrom.Name} sang {tableTo.Name} không?","Thông báo",MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    TableDAO.Instance.SwapTable(tableFrom.Id, tableTo.Id);
                    ShowBill(tableFrom.Id);
                    LoadTable();
                }           
            }
        }
    }
}
