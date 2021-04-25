using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JustANotebook2.View.Template
{
    class Task
    {
        public static int width = 215; //ширина задания
        public static int height = 44; //длина
        public static int SpaceY = 25; //растояние между заметками по Y
        public static int x = 14;
        public static int y = SpaceY;        
        static Point location = new Point(x, y); //местоположение заметки

        public delegate void taskUpdate();
        public static event taskUpdate TaskUpdateEvent;
        /*
        public delegate void noteRemove();
        public static event noteRemove NoteUpdateEvent;
        */

        public static int overallWidth;

        public static Panel Create(int id, string desc, string indicator)
        {
            Panel panel = new Panel();
            panel.Name = id.ToString() + "_panel";
            panel.Size = new Size(width, height);
            panel.Location = location;
            panel.BackColor = ColorTranslator.FromHtml("#3366cc");

            PictureBox indicatorButton = new PictureBox();
            indicatorButton.Name = id.ToString()+"_indicatorButton";
            indicatorButton.Location = new Point(10, 10);
            indicatorButton.BackgroundImageLayout = ImageLayout.Stretch;
            indicatorButton.BorderStyle = BorderStyle.FixedSingle;
            switch(indicator)
            {
                case "0": indicatorButton.BackColor = Color.Yellow;break;
                case "1": indicatorButton.BackColor = Color.Transparent;break;
                case "2": indicatorButton.BackColor = Color.Red;break;
            }
            indicatorButton.Size = new Size(23, 23);
            indicatorButton.Click += Indicator_Click;

            //Controls for noteButton
            TextBox descTextBox = new TextBox();
            descTextBox.Name = id.ToString()+"_desc";
            descTextBox.BorderStyle = BorderStyle.None;
            descTextBox.BackColor = ColorTranslator.FromHtml("#3366cc");
            descTextBox.ForeColor = Color.White;
            descTextBox.Location = new Point(41, 10);
            descTextBox.Size = new Size(133, 22);
            descTextBox.Font = new Font("Impact", 12f, indicator == "2"?FontStyle.Strikeout:FontStyle.Regular);
            descTextBox.Text = desc;
            descTextBox.Leave += DescTextBox_Leave;

            PictureBox removeTaskButton = new PictureBox();
            removeTaskButton.Name = id.ToString()+"_remove";
            removeTaskButton.Location = new Point(panel.Width - 33, 10);
            removeTaskButton.BackgroundImageLayout = ImageLayout.Stretch;
            removeTaskButton.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("removeButton"); 
            removeTaskButton.Size = new Size(23, 23);
            removeTaskButton.Click += RemoveTaskButton_Click;

            panel.Controls.Add(indicatorButton);
            panel.Controls.Add(descTextBox);
            panel.Controls.Add(removeTaskButton);

            location = GetLocation();

            return panel;
        }

        private static void Indicator_Click(object sender, EventArgs e)
        {
            y = SpaceY;
            location = new Point(x, y);
            PictureBox indicator = (PictureBox)sender;
            string id = indicator.Name.Split('_')[0];
            Model.Task task = Controller.Task.GetById(id);
            if (indicator.BackColor == Color.Transparent)
            {
                task.Indicator = "0";
                indicator.BackColor = Color.Yellow;
            }
            else if (indicator.BackColor == Color.Yellow)
            { 
                task.Indicator = "2";
                indicator.BackColor = Color.Red;
            }
            else if (indicator.BackColor == Color.Red)
            {
                task.Indicator = "1";
                indicator.BackColor = Color.Transparent;
            }
            Controller.Task.Update(task);
            TaskUpdateEvent();
        }

        private static void DescTextBox_Leave(object sender, EventArgs e)
        {
            TextBox desc = (TextBox)sender;
            string id = desc.Name.Split('_')[0];
            Model.Task task = Controller.Task.GetById(id);
            task.Description = desc.Text;
            Controller.Task.Update(task);
        }

        private static void RemoveTaskButton_Click(object sender, EventArgs e)
        {
            PictureBox b = (PictureBox)sender;
            string id = b.Name.Split('_')[0];
            if (Controller.Task.Remove(id.ToString()))
            {
                y = SpaceY;
                location = new Point(x, y);
                TaskUpdateEvent();
            }
        }

        public static void nullifyLocation()
        {
             y = SpaceY;
             location = new Point(x, y);
        }

        static Point GetLocation()
        {
            y += height + SpaceY;
            return new Point(x,y);
        }

    }
}
