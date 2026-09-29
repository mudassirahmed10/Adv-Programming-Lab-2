using System.Data;

namespace lab2CrudOperation
{
    public partial class Form1 : Form
    {
        // Database wali class ka object
        EmployeeDBConn db = new EmployeeDBConn();

        public Form1()
        {
            InitializeComponent();
        }

        // INSERT button
        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                bool success = db.Insert(txtId.Text, txtName.Text, txtCell.Text, txtAddress.Text);

                if (success)
                    MessageBox.Show("Employee data inserted successfully.");
                else
                    MessageBox.Show("Insert failed.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // UPDATE button
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                bool success = db.Update(txtId.Text, txtName.Text, txtCell.Text, txtAddress.Text);

                if (success)
                    MessageBox.Show("Employee updated successfully.");
                else
                    MessageBox.Show("Update failed.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // DELETE button
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                bool success = db.Delete(txtId.Text);

                if (success)
                    MessageBox.Show("Employee deleted successfully.");
                else
                    MessageBox.Show("Delete failed.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // VIEW button - saara data grid mein dikhata hai
        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                dgvEmployees.DataSource = db.FetchAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Form load hone par kuch karna ho to yahan likhein
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Id khali to nahi check karein
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Please enter the id for search", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                EmployeeDBConn db = new EmployeeDBConn();
                DataTable dt = db.SearchById(txtId.Text.Trim());

                if (dt.Rows.Count > 0)
                {
                    // 1. DataGridView mein show karwana
                    dgvEmployees.DataSource = dt;

                    // 2. Textboxes ke andar bhi values bharna (optional magar user-friendly)
                    txtName.Text = dt.Rows[0]["Name"].ToString();
                    txtCell.Text = dt.Rows[0]["Cell"].ToString();
                    txtAddress.Text = dt.Rows[0]["Address"].ToString();

                    MessageBox.Show("Record found!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Sorry don't found the record for this id", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Agar record na miley toh baki text boxes clear kar dein
                    txtName.Clear();
                    txtCell.Clear();
                    txtAddress.Clear();
                    dgvEmployees.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
