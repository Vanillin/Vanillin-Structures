namespace Client
{
    partial class FormClient
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
            this.ListBoxNumbers = new System.Windows.Forms.ListBox();
            this.ButtonGet = new System.Windows.Forms.Button();
            this.ButtonPost = new System.Windows.Forms.Button();
            this.ButtonPut = new System.Windows.Forms.Button();
            this.ButtonDelete = new System.Windows.Forms.Button();
            this.CheckBoxGet = new System.Windows.Forms.CheckBox();
            this.LabelNumber = new System.Windows.Forms.Label();
            this.TextBoxNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TextBoxName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TextBoxSecondName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ListBoxNumbers
            // 
            this.ListBoxNumbers.BackColor = System.Drawing.SystemColors.Info;
            this.ListBoxNumbers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ListBoxNumbers.FormattingEnabled = true;
            this.ListBoxNumbers.ItemHeight = 20;
            this.ListBoxNumbers.Location = new System.Drawing.Point(12, 12);
            this.ListBoxNumbers.Name = "ListBoxNumbers";
            this.ListBoxNumbers.Size = new System.Drawing.Size(642, 544);
            this.ListBoxNumbers.TabIndex = 0;
            // 
            // ButtonGet
            // 
            this.ButtonGet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ButtonGet.Location = new System.Drawing.Point(671, 12);
            this.ButtonGet.Name = "ButtonGet";
            this.ButtonGet.Size = new System.Drawing.Size(177, 88);
            this.ButtonGet.TabIndex = 1;
            this.ButtonGet.Text = "Отобразить";
            this.ButtonGet.UseVisualStyleBackColor = true;
            this.ButtonGet.Click += new System.EventHandler(this.ButtonGet_Click);
            // 
            // ButtonPost
            // 
            this.ButtonPost.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ButtonPost.Location = new System.Drawing.Point(671, 155);
            this.ButtonPost.Name = "ButtonPost";
            this.ButtonPost.Size = new System.Drawing.Size(177, 88);
            this.ButtonPost.TabIndex = 2;
            this.ButtonPost.Text = "Добавить контакт";
            this.ButtonPost.UseVisualStyleBackColor = true;
            this.ButtonPost.Click += new System.EventHandler(this.ButtonPost_Click);
            // 
            // ButtonPut
            // 
            this.ButtonPut.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ButtonPut.Location = new System.Drawing.Point(671, 265);
            this.ButtonPut.Name = "ButtonPut";
            this.ButtonPut.Size = new System.Drawing.Size(177, 88);
            this.ButtonPut.TabIndex = 3;
            this.ButtonPut.Text = "Изменить контакт";
            this.ButtonPut.UseVisualStyleBackColor = true;
            this.ButtonPut.Click += new System.EventHandler(this.ButtonPut_Click);
            // 
            // ButtonDelete
            // 
            this.ButtonDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ButtonDelete.Location = new System.Drawing.Point(671, 375);
            this.ButtonDelete.Name = "ButtonDelete";
            this.ButtonDelete.Size = new System.Drawing.Size(177, 88);
            this.ButtonDelete.TabIndex = 4;
            this.ButtonDelete.Text = "Удалить контакт";
            this.ButtonDelete.UseVisualStyleBackColor = true;
            this.ButtonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // CheckBoxGet
            // 
            this.CheckBoxGet.AutoSize = true;
            this.CheckBoxGet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CheckBoxGet.Location = new System.Drawing.Point(671, 115);
            this.CheckBoxGet.Name = "CheckBoxGet";
            this.CheckBoxGet.Size = new System.Drawing.Size(167, 24);
            this.CheckBoxGet.TabIndex = 5;
            this.CheckBoxGet.Text = "Автоотображение";
            this.CheckBoxGet.UseVisualStyleBackColor = true;
            // 
            // LabelNumber
            // 
            this.LabelNumber.AutoSize = true;
            this.LabelNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelNumber.Location = new System.Drawing.Point(886, 34);
            this.LabelNumber.Name = "LabelNumber";
            this.LabelNumber.Size = new System.Drawing.Size(63, 20);
            this.LabelNumber.TabIndex = 6;
            this.LabelNumber.Text = "Номер:";
            // 
            // TextBoxNumber
            // 
            this.TextBoxNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TextBoxNumber.Location = new System.Drawing.Point(979, 31);
            this.TextBoxNumber.Name = "TextBoxNumber";
            this.TextBoxNumber.Size = new System.Drawing.Size(226, 26);
            this.TextBoxNumber.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(886, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Имя:";
            // 
            // TextBoxName
            // 
            this.TextBoxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TextBoxName.Location = new System.Drawing.Point(959, 80);
            this.TextBoxName.Name = "TextBoxName";
            this.TextBoxName.Size = new System.Drawing.Size(226, 26);
            this.TextBoxName.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(886, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "Фамилия:";
            // 
            // TextBoxSecondName
            // 
            this.TextBoxSecondName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TextBoxSecondName.Location = new System.Drawing.Point(1003, 127);
            this.TextBoxSecondName.Name = "TextBoxSecondName";
            this.TextBoxSecondName.Size = new System.Drawing.Size(226, 26);
            this.TextBoxSecondName.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1371, 588);
            this.Controls.Add(this.TextBoxSecondName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TextBoxName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextBoxNumber);
            this.Controls.Add(this.LabelNumber);
            this.Controls.Add(this.CheckBoxGet);
            this.Controls.Add(this.ButtonDelete);
            this.Controls.Add(this.ButtonPut);
            this.Controls.Add(this.ButtonPost);
            this.Controls.Add(this.ButtonGet);
            this.Controls.Add(this.ListBoxNumbers);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox ListBoxNumbers;
        private System.Windows.Forms.Button ButtonGet;
        private System.Windows.Forms.Button ButtonPost;
        private System.Windows.Forms.Button ButtonPut;
        private System.Windows.Forms.Button ButtonDelete;
        private System.Windows.Forms.CheckBox CheckBoxGet;
        private System.Windows.Forms.Label LabelNumber;
        private System.Windows.Forms.TextBox TextBoxNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextBoxName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TextBoxSecondName;
    }
}

