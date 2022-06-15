using Personal_PBL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Personal_PBL.DAO;


namespace Personal_PBL
{
    public partial class fAccountProfile : Form
    {
        private Account loginAccount;
        
        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; LoadData(); }
        }
        public fAccountProfile(Account loginAccount)
        {
            InitializeComponent();
            LoginAccount = loginAccount;
        }
        public void LoadData()
        {
            this.txtUserName.Text = loginAccount.UserName;
            //this.txtPassWord.Text = loginAccount.Password;
            this.txtDisplayName.Text = loginAccount.DisplayName;

            cbbSex.Text = loginAccount.Sex;
            this.txtPhoneNum.Text = loginAccount.PhoneNum;
            this.txtAddress.Text = loginAccount.Address;
        }
        private void UpdateAccount()
        {
            try
            {
                string userName = txtUserName.Text.Trim();
                string displayName = txtDisplayName.Text.Trim();
                string passWord = LoginAccount.Password.Trim();
                string newPassWord = txtNewPassWord.Text.Trim();
                string confirm = txtConfirm.Text.Trim();

                string sex = cbbSex.Text.Trim();
                string phoneNum = txtPhoneNum.Text.Trim();
                string address = txtAddress.Text.Trim();

                string query = $"update Account set passWord = '{newPassWord}',displayName = N'{displayName}',sex = N'{sex}',phoneNum = '{phoneNum}',address = N'{address}' where userName = N'{userName}'";
                if (string.IsNullOrEmpty(displayName) || string.IsNullOrEmpty(passWord) || string.IsNullOrEmpty(newPassWord) || string.IsNullOrEmpty(confirm) || string.IsNullOrEmpty(sex) || string.IsNullOrEmpty(phoneNum) || string.IsNullOrEmpty(address))
                {
                    throw new ArgumentNullException();
                }
                else if(txtPassWord.Text != passWord)
                {
                    throw new KeyNotFoundException();
                }
                else if (newPassWord != confirm)
                {
                    throw new InvalidEnumArgumentException();
                }
                else
                {
                    int number = Convert.ToInt32(phoneNum);

                    DataProvider.Instance.ExecuteNonQuery(query);
                    MessageBox.Show("Cập nhật thông tin thành công");
                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
            catch(KeyNotFoundException ex)
            {
                MessageBox.Show("Mật khẩu cũ sai");
                txtPassWord.Clear();
                txtNewPassWord.Clear();
                txtConfirm.Clear();
                txtPassWord.Focus();
            }
            catch (InvalidEnumArgumentException ex) 
            {
                MessageBox.Show("Xác nhận mật khẩu không đúng");
                txtNewPassWord.Clear();
                txtConfirm.Clear();
                txtNewPassWord.Focus();
            }
            catch(FormatException ex)
            {
                MessageBox.Show("Kiểu dữ liệu nhập vào không đúng");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.GetType().Name);
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccount();
            Close();
        }
    }
}
