using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace JustANotebook2.View
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Template.Note.NoteUpdateEvent += NoteUpdateEvent;
            Template.Task.TaskUpdateEvent += TaskUpdateEvent;
            //
            LabelsListBox.SelectedIndex = 0;

            //Внешний вид
            panelForNotes.BackColor = ColorTranslator.FromHtml("#3399ff");
            toDoPanel.BackColor = ColorTranslator.FromHtml("#3399ff");


            //notes
            Template.Note.overallWidth = panelForNotes.Width;
            NotesUpdate();

            //task
            Template.Task.overallWidth = panelForNotes.Width;
            TasksUpdate();

        }

        private void noteAdd_Click(object sender, EventArgs e)
        {
            EditNoteForm editNoteForm = new EditNoteForm();
            if (editNoteForm.ShowDialog() == DialogResult.OK)
            {
                Model.Note note = Controller.Note.GetLast();
                var panel = Template.Note.Create(int.Parse(note.id), note.Title, note.Description);
                panelForNotes.Controls.Add(panel);
            }
        }
        private void taskAddButton_Click(object sender, EventArgs e)
        {
            Model.Task task = new Model.Task() { Description = "", Indicator = "1" };
            if(Controller.Task.Add(task))
                TasksUpdate();
        }

        private void NoteUpdateEvent()
        {
            NotesUpdate();
        }
        void NotesUpdate()
        {
            Template.Note.nullifyLocation();
            panelForNotes.Controls.Clear();

            List<Model.Note> notes = Controller.Note.Get();

            foreach (Model.Note note in notes)
            {
                int id = int.Parse(note.id);
                Panel panel = Template.Note.Create(int.Parse(note.id), note.Title, note.Description);

                panelForNotes.Controls.Add(panel);
            }
        }
        private void TaskUpdateEvent()
        {
            TasksUpdate();
        }
        void TasksUpdate()
        {
            Template.Task.nullifyLocation();
            toDoPanel.Controls.Clear();

            List<Model.Task> tasks = Controller.Task.Get();
            tasks.Sort(delegate (Model.Task x, Model.Task y)
            {
                return x.Indicator.CompareTo(y.Indicator);
            });

            foreach (Model.Task task in tasks)
            {
                int id = int.Parse(task.id);
                Panel panel = Template.Task.Create(int.Parse(task.id), task.Description, task.Indicator);

                toDoPanel.Controls.Add(panel);
            }
        }

        private void LabelsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            string dbName = "";
            switch(listBox.SelectedIndex)
            {
                case 0: dbName = "today.db" ; break;
                case 1: dbName = "yesterday.db" ; break;
                case 2: dbName = "week.db" ; break;
                default: dbName = "week.db"; LabelsListBox.SelectedIndex = 2; break;
            }
            Controller.Labels.Create(dbName);
            TasksUpdate();
            NotesUpdate();
        }
    }

}
