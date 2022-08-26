using System.Diagnostics;
namespace battleshipdeneme
{
    public partial class Gamearea : Form
    {
        List<Button> PlayerPstBtns;
        List<Button> EnmyPstBtns;
        Random rnd =new Random();
        int TotalShips = 3;
        int PlayerScore;
        int EnmyScore;
        int EnmyShipsNmbr=3;
        int PlayerShipsNmbr=3;

        public Gamearea()
        {
            InitializeComponent();
            RstrtGame();
        }

        private void Gamearea_Load(object sender, EventArgs e)
        {

        }
        private void EnemyPlayTimerEvent(object sender, EventArgs e)
        {

        }

        private void AttackBtnEvent(object sender, EventArgs e)
        {
            if (EnmyLctLstBox.Text!="")
            {
                var atckPst=EnmyLctLstBox.Text.ToUpper();
                int index = EnmyPstBtns.FindIndex(A => A.Name == atckPst);
                if (EnmyPstBtns[index].Enabled)
                {
                    if ((string)EnmyPstBtns[index].Tag=="enemyShip")
                    {
                        EnmyPstBtns[index].Enabled= false;
                        EnmyPstBtns[index].BackColor = Color.Red;
                        PlayerScore += 1;
                        Playerscoree.Text = PlayerScore.ToString();
                        EnmyShipsNmbr--;
                        enmyshipleft.Text = EnmyShipsNmbr.ToString();
                    }
                    else
                    {
                        EnmyPstBtns[index].Enabled = false;
                        EnmyPstBtns[index].BackColor=Color.Blue;
                    }
                }
            }
            else
            {
                MessageBox.Show("select target point","information");
            }
        }

        private void PlayerBtnPstEvent(object sender, EventArgs e)
        {
            if (TotalShips>0)
            {
                var button = (Button)sender;
                button.Enabled=false;
                button.Tag = "playerShip";
                button.BackColor=Color.Orange;
                TotalShips -= 1;
            }
            if (TotalShips==0)
            {
                btnattack.Enabled = true;
                btnattack.BackColor = Color.Red;
                btnattack.ForeColor = Color.White;
                
            }
        }




        private void RstrtGame()
        {
            PlayerPstBtns = new List<Button>{A1,A2,A3,A4,A5,B1,B2,B3,B4,B5,C1,C2,C3,C4,C5
                                            ,D1,D2,D3,D4,D5,E1,E2,E3,E4,E5 };
            EnmyPstBtns = new List<Button> {V1,V2,V3,V4,V5,W1,W2,W3,W4,W5,X1,X2,X3,X4,X5
                                            ,Y1,Y2,Y3,Y4,Y5,Z1,Z2,Z3,Z4,Z5 };

            EnmyLctLstBox.Items.Clear();
            EnmyLctLstBox.Text = null;

            for (int i = 0; i < EnmyPstBtns.Count; i++)
            {
                EnmyPstBtns[i].Enabled = true;
                EnmyPstBtns[i].Tag=null;
                EnmyPstBtns[i].BackColor = Color.White;
                EnmyPstBtns[i].BackgroundImage = null;
                EnmyLctLstBox.Items.Add(EnmyPstBtns[i].Text);
            }
            for (int i = 0; i < PlayerPstBtns.Count;i++)
            {
                PlayerPstBtns[i].Enabled = true;
                PlayerPstBtns[i].Tag=null;
                PlayerPstBtns[i].BackColor = Color.White;
                PlayerPstBtns[i].BackgroundImage = null;
            }

            PlayerScore = 0;
            EnmyScore = 0;
            TotalShips = 3;

            Playerscoree.Text=PlayerScore.ToString();
            EnemyScoree.Text=EnmyScore.ToString();
            enmymove.Text = "A1";
            btnattack.Enabled = false;
            EnmyLctPicker();
        }

        private void EnmyLctPicker()
        {
            for (int i = 0; i < 3; i++)
            {
                int index = rnd.Next(EnmyPstBtns.Count);
                if (EnmyPstBtns[index].Enabled == true && (string)EnmyPstBtns[index].Tag==null)
                {
                    EnmyPstBtns[index].Tag = "enemyShip";
                    Debug.WriteLine("enemy position " + EnmyPstBtns[index].Text);
                }
                else
                {
                    index=rnd.Next(EnmyPstBtns.Count);
                }
            }
        }

        private void BotbtnEvent(object sender, EventArgs e)
        {
            int index = rnd.Next(PlayerPstBtns.Count);
            if ((string)PlayerPstBtns[index].Tag == "playerShip")
            {
                PlayerPstBtns[index].BackColor = Color.Red;
                enmymove.Text = PlayerPstBtns[index].Text;
                PlayerPstBtns[index].Enabled = false;
                PlayerPstBtns.RemoveAt(index);
                EnmyScore += 1;
                EnemyScoree.Text = EnmyScore.ToString();
                PlayerShipsNmbr -- ;
                playershipleft.Text = PlayerShipsNmbr.ToString();
                
            }
            else
            {
                PlayerPstBtns[index].Enabled = false;
                PlayerPstBtns[index].BackColor = Color.Blue;
                enmymove.Text = PlayerPstBtns[index].Text;
                PlayerPstBtns.RemoveAt(index);
                
            }
            if (EnmyScore>2||PlayerScore>2)
            {
                if (PlayerScore>EnmyScore)
                {
                    MessageBox.Show("You won", "VICTORY");
                    RstrtGame();
                }
                else if (EnmyScore>PlayerScore)
                {
                    MessageBox.Show("You lost", "DEFEAT");
                    RstrtGame();
                }
            }
        }
    }
}