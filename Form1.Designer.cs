using System.Drawing;
using System.Windows.Forms;
using System;

namespace TicTacToeClient
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 420); 
            this.Text = "Крестики-Нолики";

            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnDraw = new System.Windows.Forms.Button();
            this.btnResign = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbGameMode = new System.Windows.Forms.ComboBox(); 

            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top; 
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.Size = new System.Drawing.Size(450, 450);
            this.tableLayoutPanel.TabIndex = 0;

            // Создание кнопок для игрового поля
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Button button = new Button();
                    button.Size = new Size(100, 100);
                    button.Font = new Font("Arial", 24, FontStyle.Bold);
                    button.Click += new EventHandler(Button_Click);
                    this.tableLayoutPanel.Controls.Add(button, j, i);
                }
            }

            // 
            // btnRestart
            // 
            this.btnRestart.Location = new System.Drawing.Point(350, 15);
            this.btnRestart.Size = new System.Drawing.Size(100, 50);
            this.btnRestart.Text = "Перезапустить";
            this.btnRestart.Click += new EventHandler(BtnRestart_Click);
            this.Controls.Add(this.btnRestart);

            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(350, 75);
            this.btnDraw.Size = new System.Drawing.Size(100, 50);
            this.btnDraw.Text = "Ничья";
            this.btnDraw.Click += new EventHandler(BtnDraw_Click);
            this.Controls.Add(this.btnDraw);

            // 
            // btnResign
            // 
            this.btnResign.Location = new System.Drawing.Point(350, 135);
            this.btnResign.Size = new System.Drawing.Size(100, 50);
            this.btnResign.Text = "Сдаться";
            this.btnResign.Click += new EventHandler(BtnResign_Click);
            this.Controls.Add(this.btnResign);

            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(100, 380);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 20);
            this.Controls.Add(this.lblStatus);

            // 
            // cmbGameMode
            // 
            this.cmbGameMode.Items.AddRange(new object[] {
        "Человек-Человек",
        "Человек-Компьютер",
        "Компьютер-Компьютер"});
            this.cmbGameMode.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbGameMode.Location = new System.Drawing.Point(10, 330);
            this.cmbGameMode.Name = "cmbGameMode";
            this.cmbGameMode.Size = new System.Drawing.Size(200, 28);
            this.cmbGameMode.TabIndex = 1;
            this.Controls.Add(this.cmbGameMode);

            // 
            // MainForm
            // 
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "MainForm";
            this.Text = "Крестики-Нолики";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(MainForm_FormClosing);
        }


        #endregion
    }
}

