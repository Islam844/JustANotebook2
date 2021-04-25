
namespace JustANotebook2.View
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panelForNotes = new System.Windows.Forms.Panel();
            this.noteAdd = new System.Windows.Forms.Button();
            this.taskAddButton = new System.Windows.Forms.Button();
            this.toDoPanel = new System.Windows.Forms.Panel();
            this.LabelsListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // panelForNotes
            // 
            this.panelForNotes.AutoScroll = true;
            this.panelForNotes.Location = new System.Drawing.Point(206, 0);
            this.panelForNotes.Name = "panelForNotes";
            this.panelForNotes.Size = new System.Drawing.Size(438, 409);
            this.panelForNotes.TabIndex = 7;
            // 
            // noteAdd
            // 
            this.noteAdd.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.noteAdd.FlatAppearance.BorderSize = 0;
            this.noteAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.noteAdd.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.noteAdd.ForeColor = System.Drawing.Color.White;
            this.noteAdd.Location = new System.Drawing.Point(206, 413);
            this.noteAdd.Name = "noteAdd";
            this.noteAdd.Size = new System.Drawing.Size(438, 47);
            this.noteAdd.TabIndex = 6;
            this.noteAdd.Text = "Добавить";
            this.noteAdd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.noteAdd.UseVisualStyleBackColor = false;
            this.noteAdd.Click += new System.EventHandler(this.noteAdd_Click);
            // 
            // taskAddButton
            // 
            this.taskAddButton.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.taskAddButton.FlatAppearance.BorderSize = 0;
            this.taskAddButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.taskAddButton.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.taskAddButton.ForeColor = System.Drawing.Color.White;
            this.taskAddButton.Location = new System.Drawing.Point(650, 413);
            this.taskAddButton.Name = "taskAddButton";
            this.taskAddButton.Size = new System.Drawing.Size(254, 47);
            this.taskAddButton.TabIndex = 8;
            this.taskAddButton.Text = "Добавить";
            this.taskAddButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.taskAddButton.UseVisualStyleBackColor = false;
            this.taskAddButton.Click += new System.EventHandler(this.taskAddButton_Click);
            // 
            // toDoPanel
            // 
            this.toDoPanel.AutoScroll = true;
            this.toDoPanel.Location = new System.Drawing.Point(650, 0);
            this.toDoPanel.Name = "toDoPanel";
            this.toDoPanel.Size = new System.Drawing.Size(254, 409);
            this.toDoPanel.TabIndex = 0;
            // 
            // LabelsListBox
            // 
            this.LabelsListBox.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.LabelsListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LabelsListBox.Font = new System.Drawing.Font("Impact", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelsListBox.ForeColor = System.Drawing.Color.White;
            this.LabelsListBox.FormattingEnabled = true;
            this.LabelsListBox.ItemHeight = 26;
            this.LabelsListBox.Items.AddRange(new object[] {
            "Сегодня",
            "Завтра",
            "Неделя"});
            this.LabelsListBox.Location = new System.Drawing.Point(0, 0);
            this.LabelsListBox.Name = "LabelsListBox";
            this.LabelsListBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LabelsListBox.Size = new System.Drawing.Size(200, 1638);
            this.LabelsListBox.TabIndex = 0;
            this.LabelsListBox.SelectedIndexChanged += new System.EventHandler(this.LabelsListBox_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(901, 463);
            this.Controls.Add(this.panelForNotes);
            this.Controls.Add(this.noteAdd);
            this.Controls.Add(this.taskAddButton);
            this.Controls.Add(this.LabelsListBox);
            this.Controls.Add(this.toDoPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(917, 502);
            this.MinimumSize = new System.Drawing.Size(917, 502);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button noteAdd;
        private System.Windows.Forms.Panel panelForNotes;
        private System.Windows.Forms.Button taskAddButton;
        private System.Windows.Forms.Panel toDoPanel;
        private System.Windows.Forms.ListBox LabelsListBox;
    }
}