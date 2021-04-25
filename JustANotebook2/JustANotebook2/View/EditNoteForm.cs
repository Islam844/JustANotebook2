using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JustANotebook2.View
{
    public partial class EditNoteForm : Form
    {
        Model.Note _note = new Model.Note();
        bool isNew = true;
        public EditNoteForm()
        {
            InitializeComponent();
        }
        public void SetNote(string id)
        {
            _note = Controller.Note.GetById(id);
            Title.Text = _note.Title;
            Title.ForeColor = Color.Black;
            Description.Text = _note.Description;
            Description.ForeColor = Color.Black;
            isNew = false;
        }

        private void button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Title.ForeColor == Color.Gray || String.IsNullOrWhiteSpace(Title.Text))//если заголовок пуст
                {
                    if(Description.ForeColor == Color.Black)//если описание заполнено
                    {
                        _note.Title = Description.Lines[0];
                        _note.Description = Description.Text;
                    }
                    else
                    {
                        throw new Exception("Заполните поля");
                    }
                }
                else
                {
                    _note.Title = Title.Lines[0];

                    if(Description.ForeColor == Color.Black)//если описание заполнено
                        _note.Description = Description.Text;
                    else
                        _note.Description = "";

                }
                if (isNew)
                {
                    if (Controller.Note.Add(_note))
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    if (Controller.Note.Update(_note))
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void RemoveHelp(object sender, EventArgs e)
        {
            TextBox title = (TextBox)sender;
            if (title.ForeColor == Color.Gray)
            {
                title.Text = "";
                title.ForeColor = Color.Black;
            }

        }
        private void Title_Leave(object sender, EventArgs e)
        {
            TextBox title = (TextBox)sender;
            if(String.IsNullOrEmpty(title.Text))
            {
                title.Text = "Заголовок";
                title.ForeColor = Color.Gray;
            }
        }

        private void Description_Leave(object sender, EventArgs e)
        {
            TextBox desc = (TextBox)sender;
            if(String.IsNullOrEmpty(desc.Text))
            {
                desc.Text = "Описание";
                desc.ForeColor = Color.Gray;
            }
        }

    }
}
