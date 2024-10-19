using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace TicTacToeClient
{
    public partial class MainForm : Form
    {
        private TcpClient client;
        private NetworkStream stream;

        private char currentPlayer = 'X'; 
        private char[,] gameBoard = new char[3, 3]; 
        private bool gameActive = true; 
        private string gameMode = "";
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.Button btnResign;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbGameMode;
        public MainForm()
        {
            InitializeComponent();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            client = new TcpClient("127.0.0.1", 5500);
            stream = client.GetStream();
            SendGameMode();
            WaitForGameStart();
        }

        private void SendGameMode()
        {
            string selectedMode = cmbGameMode.SelectedItem.ToString();
            byte[] data = Encoding.UTF8.GetBytes(selectedMode);
            stream.Write(data, 0, data.Length);
        }
        private void WaitForGameStart()
        {
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            gameMode = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            MessageBox.Show($"Игра в режиме: {gameMode}");
            gameActive = true; 
        }

        private void SendMove(int x, int y)
        {
            byte[] data = new byte[2] { (byte)(x + '0'), (byte)(y + '0') };
            stream.Write(data, 0, data.Length);
            ReadBoardState();
        }

        private void ReadBoardState()
        {
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string boardState = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            UpdateBoard(boardState);
        }

        private void UpdateBoard(string boardState)
        {
            string[] rows = boardState.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    char mark = rows[i][j];
                    Button button = tableLayoutPanel.GetControlFromPosition(j, i) as Button;
                    if (button != null)
                    {
                        button.Text = mark == ' ' ? "" : mark.ToString();
                    }
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            client?.Close();
        }

        
        private void Button_Click(object sender, EventArgs e)
        {
            if (!gameActive) return; 

            Button clickedButton = sender as Button;

            if (clickedButton.Text != "")
            {
                MessageBox.Show("Эта ячейка уже занята!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            clickedButton.Text = currentPlayer.ToString();
            int row = tableLayoutPanel.GetRow(clickedButton);
            int col = tableLayoutPanel.GetColumn(clickedButton);
            gameBoard[row, col] = currentPlayer;

            if (CheckGameStatus())
            {
                lblStatus.Text = $"Игрок {currentPlayer} выиграл!";
                gameActive = false; 
            }
            else if (IsBoardFull())
            {
                lblStatus.Text = "Ничья!";
                gameActive = false; 
            }
            else
            {
                currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
            }
        }

        private bool CheckGameStatus()
        {
            // Проверка строк, столбцов и диагоналей
            for (int i = 0; i < 3; i++)
            {
                if ((gameBoard[i, 0] == currentPlayer && gameBoard[i, 1] == currentPlayer && gameBoard[i, 2] == currentPlayer) ||
                    (gameBoard[0, i] == currentPlayer && gameBoard[1, i] == currentPlayer && gameBoard[2, i] == currentPlayer))
                {
                    return true;
                }
            }
            // Проверка диагоналей
            if ((gameBoard[0, 0] == currentPlayer && gameBoard[1, 1] == currentPlayer && gameBoard[2, 2] == currentPlayer) ||
                (gameBoard[0, 2] == currentPlayer && gameBoard[1, 1] == currentPlayer && gameBoard[2, 0] == currentPlayer))
            {
                return true;
            }
            return false;
        }

        private bool IsBoardFull()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (gameBoard[i, j] == '\0') 
                        return false;
                }
            }
            return true; 
        }

        private void BtnRestart_Click(object sender, EventArgs e)
        {
            currentPlayer = 'X';
            gameActive = true;
            gameBoard = new char[3, 3]; 
            lblStatus.Text = ""; 

            foreach (Control control in tableLayoutPanel.Controls)
            {
                if (control is Button button)
                {
                    button.Text = ""; 
                }
            }
        }

        private void BtnDraw_Click(object sender, EventArgs e)
        {
            if (gameActive)
            {
                lblStatus.Text = "Игра завершена. Предложена ничья.";
                gameActive = false; 
            }
            else
            {
                MessageBox.Show("Игра уже завершена!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnResign_Click(object sender, EventArgs e)
        {
            if (gameActive)
            {
                lblStatus.Text = $"Игрок {currentPlayer} признал поражение. Победитель: {(currentPlayer == 'X' ? 'O' : 'X')}";
                gameActive = false; 
            }
            else
            {
                MessageBox.Show("Игра уже завершена!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }

}
