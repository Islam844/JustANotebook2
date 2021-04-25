using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JustANotebook2.View.Template
{
    static class Note
    {
        
        public static int width = 150; //ширина заметов
        public static int height = 120; //длина
        public static int SpaceX = 25; //растояние между заметками по X
        public static int SpaceY = 25; //растояние между заметками по Y
        public static int x = SpaceX;
        public static int y = SpaceY;        
        static Point location = new Point(x, y); //местоположение заметки

        public delegate void noteRemove();
        public static event noteRemove NoteUpdateEvent;

        public static int overallWidth;

        public static Panel Create(int id, string title, string desc)
        {
            Panel panel = new Panel();
            panel.Name = id.ToString() + "_panel";
            panel.Size = new Size(150, 120);
            panel.Location = location;
            panel.BackColor = ColorTranslator.FromHtml("#3366cc");
            SetRoundedShape(panel, 15);
            panel.Click += editNotePanel_Click;

            //Controls for noteButton
            Label titleLabel = new Label();
            titleLabel.Name = id.ToString()+"_title";
            titleLabel.Text = Char.ToUpper(title[0]) + title.Substring(1);
            titleLabel.Location = new Point(16, 16);
            titleLabel.ForeColor = Color.White;
            titleLabel.Font = new Font("Impact", 12f);
            titleLabel.Click += editNoteLabel_Click;

            Label descLabel = new Label();
            descLabel.Name = id.ToString()+"_desc";
            descLabel.Text = desc;
            descLabel.Location = new Point(16, 42);
            descLabel.AutoSize = false;
            descLabel.Size = new Size(120,60);
            descLabel.ForeColor = Color.White;
            descLabel.Font = new Font("Impact", 9f);
            descLabel.Click += editNoteLabel_Click;

            PictureBox removeNoteButton = new PictureBox();
            removeNoteButton.Name = id.ToString();
            removeNoteButton.Location = new Point(panel.Width - 22, 1);
            removeNoteButton.BackgroundImageLayout = ImageLayout.Stretch;
            removeNoteButton.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("removeButton"); 
            removeNoteButton.Size = new Size(20, 20);
            removeNoteButton.Click += removeNoteButton_Click;

            panel.Controls.Add(titleLabel);
            panel.Controls.Add(descLabel);
            panel.Controls.Add(removeNoteButton);

            location = GetLocation();

            return panel;
        }

        private static void editNotePanel_Click(object sender, EventArgs e)
        {
            Panel panel = (Panel)sender;
            string id = panel.Name.Split('_')[0];
            EditNoteForm editNoteForm = new EditNoteForm();
            editNoteForm.SetNote(id);
            editNoteForm.ShowDialog();
            if(editNoteForm.DialogResult == DialogResult.OK)
            {
                x = SpaceX;
                y = SpaceY;
                location = new Point(x, y);
                NoteUpdateEvent();
            }

        }
        private static void editNoteLabel_Click(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            string id = label.Name.Split('_')[0];
            EditNoteForm editNoteForm = new EditNoteForm();
            editNoteForm.SetNote(id);
            editNoteForm.ShowDialog();
            if(editNoteForm.DialogResult == DialogResult.OK)
            {
                x = SpaceX;
                y = SpaceY;
                location = new Point(x, y);
                NoteUpdateEvent();
            }
        }

        private static void removeNoteButton_Click(object sender, EventArgs e)
        {
            PictureBox b = (PictureBox)sender;
            int id = int.Parse(b.Name);
            if (Controller.Note.Remove(id.ToString())) //если удачно удалили из бд
            {
                x = SpaceX;
                y = SpaceY;
                location = new Point(x, y);
                NoteUpdateEvent();
            }

        }

        static void SetRoundedShape(Control control, int radius)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddLine(radius, 0, control.Width - radius, 0);
            path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
            path.AddLine(control.Width, radius, control.Width, control.Height - radius);
            path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
            path.AddLine(control.Width - radius, control.Height, radius, control.Height);
            path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
            path.AddLine(0, control.Height - radius, 0, radius);
            path.AddArc(0, 0, radius, radius, 180, 90);
            control.Region = new Region(path);
        }
        public static void nullifyLocation()
        {
            x = SpaceX;
            y = SpaceY;
            location = new Point(x, y);
        }

        static Point GetLocation()
        {
            x += width + SpaceX;
            if (x >= overallWidth - width)
            {
                y += height + SpaceY;
                x = SpaceX;
            }

            return new Point(x,y);
        }
    }
}
