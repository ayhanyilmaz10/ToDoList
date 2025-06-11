using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace To_Do_List_App
{
    public partial class ToDoList : Form
    {
        public ToDoList()
        {
            InitializeComponent();
        }

        DataTable todoList = new DataTable();
        bool isEditing = false;
        private void ToDoList_Load(object sender, EventArgs e)
        {
            // Create Columns
            todoList.Columns.Add("Title");
            todoList.Columns.Add("Description");

            // Point our datagridview to our datasource
            toDoListView.DataSource = todoList;

        }

        private void newButton_Click(object sender, EventArgs e)
        {
            titleTextBox.Text = "";
            descriptionTextBox.Text = "";
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            isEditing = true;
            // Fill text fields with data from table
            titleTextBox.Text = todoList.Rows[toDoListView.CurrentCell.RowIndex].ItemArray[0].ToString();
            descriptionTextBox.Text = todoList.Rows[toDoListView.CurrentCell.RowIndex].ItemArray[1].ToString();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            // It is good practice to first check if there is a selected row.
            // If no row is selected, we may get an error or perform the wrong operation.
            if (toDoListView.CurrentCell == null)
            {
                MessageBox.Show("Please select a note you want to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Show the popup asking the user a confirmation question
            DialogResult resul = MessageBox.Show("Are you sure you want to delete the selected note?", "Note Deletion Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Let's check the user's response
            if (resul == DialogResult.Yes) //If user click on YES button
            {
                try
                {
                    todoList.Rows[toDoListView.CurrentCell.RowIndex].Delete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error" + ex);
                }
            }
            else
            {
                MessageBox.Show("Deletion canceled", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if(isEditing)
            {
                todoList.Rows[toDoListView.CurrentCell.RowIndex]["Title"] = titleTextBox.Text;
                todoList.Rows[toDoListView.CurrentCell.RowIndex]["Description"] = descriptionTextBox.Text;
            }
            else
            {
                todoList.Rows.Add(titleTextBox.Text, descriptionTextBox.Text);
            }
            // Clear fields
            titleTextBox.Text = "";
            descriptionTextBox.Text = "";
            isEditing = false;
        }
    }
}
