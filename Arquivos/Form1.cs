using Campo_Minado.Properties;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Campo_Minado
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int[] LocationsX = {34, 80, 126, 172, 218, 264, 310, 356, 402, 448, 494, 540, 586, 632, 678, 724};
        int[] LocationsY = {21, 67, 113, 159, 205, 251, 297, 343, 389};
        Random Random = new Random();
        int Rodada = 0, BlocosNoJogo = 0, Marcadas = 0;
        bool VerificouInicio = false;
        string Modo = "Ativar";

        List<Button> BotoesPressionados = new List<Button>();
        List<string> Posicoes = new List<string>();

        private void RandomizarPosicoes()
        {
            foreach (Button Botao in Controls.OfType<Button>())
            {

            Rerollar:

                int X = LocationsX[Random.Next(0, LocationsX.Length)];
                int Y = LocationsY[Random.Next(0, LocationsY.Length)];



                string Verificacao = X.ToString() + Y.ToString();

                if (Posicoes.Contains(Verificacao))
                {
                    goto Rerollar;
                }
                else
                {
                    Botao.Location = new Point(X, Y);
                    Posicoes.Add(Verificacao);
                }
            }
        }

        private void Limpar()
        {
            foreach (Button Botao in Controls.OfType<Button>())
            {
                Botao.BackgroundImage = null;

                if (Botao.Tag.ToString() == "Nada" || Botao.Tag.ToString() == "Zero")
                {
                    Botao.Tag = "NadaVirada";
                    Botao.Text = null;
                    Botao.BackColor = Color.Silver;
                }
            }

            BotoesPressionados.Clear();
            Posicoes.Clear();
            Rodada = 0;
            Marcadas = 0;
            lblMarcadas.Text = Marcadas.ToString();
            VerificouInicio = false;
            RandomizarPosicoes();

            
        }

        private void ZeroAutomatico(Button Botao)
        {
            Botao.Tag = "Zero";
            int Y = Botao.Location.Y - 46;

            for (int i = 0; i < 3; i++)
            {
                int X = Botao.Location.X - 92;

                for (int j = 0; j < 3; j++)
                {
                    X += 46;

                    Point Posicao = new Point(X, Y);
                    foreach (Button butao in Controls.OfType<Button>())
                    {
                        if (butao.Location == Posicao)
                        {
                            int a = 0;
                            VerificarRedor(a, butao);
                        }
                    }

                }

                Y += 46;

            }
        }

        private void VerificarRedor(int a, Button Botao)
        {

            int Y = Botao.Location.Y - 46;

            for (int i = 0; i < 3; i++)
            {
                int X = Botao.Location.X - 92;

                for (int j = 0; j < 3; j++)
                {
                    X += 46;

                    Point Posicao = new Point(X, Y);
                    foreach (Button butao in Controls.OfType<Button>())
                    {
                        if (butao.Location == Posicao)
                        {
                            if (butao.Tag.ToString() == "BombaVirada")
                            {
                                a++;
                            }
                        }
                    }

                }

                Y += 46;

            }

            Botao.Text = a.ToString();

            ColorirTexto(a, Botao);

            Botao.BackColor = Color.White;
            Botao.Tag = "Nada";
        }
        
        private void VerificarInicio(int a, Button Botao)
        {

            int Y = Botao.Location.Y - 46;

            for (int i = 0; i < 3; i++)
            {
                int X = Botao.Location.X - 92;

                for (int j = 0; j < 3; j++)
                {
                    X += 46;

                    Point Posicao = new Point(X, Y);
                    foreach (Button butao in Controls.OfType<Button>())
                    {
                        if (butao.Location == Posicao)
                        {
                            if (butao.Tag.ToString() == "BombaVirada")
                            {
                                a++;
                            }
                            else if (butao.Tag.ToString() == "NadaVirada")
                            {
                                if (Random.Next(1, 5) <= 2)
                                {
                                    int BombaRedor = 0;
                                    VerificarRedor(BombaRedor, butao);
                                    BombaRedor = 0;
                                    if (VerificouInicio == false)
                                    {
                                        VerificarInicio(BombaRedor, butao);
                                        VerificouInicio = true;
                                    }
                                }
                            }
                        }
                    }

                }

                Y += 46;

            }

            Botao.Text = a.ToString();
            ColorirTexto(a, Botao);

            Botao.BackColor = Color.White;
            Botao.Tag = "Nada";

        }

        private void ColorirTexto(int a, Button Botao)
        {
            if (a == 0)
            {
                Botao.ForeColor = Color.White;
                Botao.Text = "";
            }
            else if (a == 1)
            {
                Botao.ForeColor = Color.Blue;
            }
            else if (a == 2)
            {
                Botao.ForeColor = Color.Green;
            }
            else if (a == 3)
            {
                Botao.ForeColor = Color.Red;
            }
            else if (a == 4)
            {
                Botao.ForeColor = Color.Purple;
            }
            else
            {
                Botao.ForeColor = Color.Orange;
            }
        }

        private void MarcarBlocos(Button Botao)
        {
            if(Botao.BackgroundImage == null)
            {
                Marcadas++;
                lblMarcadas.Text = Marcadas.ToString();
                Botao.BackgroundImage = Resources.Flag;
            }
            else
            {
                Botao.BackgroundImage = null;
                Marcadas--;
                lblMarcadas.Text = Marcadas.ToString();
            }
        }

        private void AlterarModos()
        {
            if (Modo == "Ativar")
            {
                Modo = "Marcar";
                picBoxMode.BackgroundImage = Resources.Flag;
            }
            else if (Modo == "Marcar")
            {
                Modo = "Ativar";
                picBoxMode.BackgroundImage = Resources.Pointer;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            RandomizarPosicoes();
            for (int j = 0; j<9; j++)
            {
                for(int i = 0; i<16; i++)
                {
                    foreach(Button butao in Controls.OfType<Button>())
                    {
                        Point Localizacao = new Point(LocationsX[i], LocationsY[j]);
                        if(butao.Location == Localizacao)
                        butao.Name = "btn" + j.ToString() + i.ToString();
                    }
                }
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                Close();
            }
            if(e.KeyCode == Keys.ShiftKey)
            {
                AlterarModos();
            }
        }

        private void picBoxApagar_Click(object sender, EventArgs e)
        {
            Limpar();
        }

        private void picBoxMode_Click(object sender, EventArgs e)
        {
            AlterarModos();
        }

        private void Botao_Click(object sender, EventArgs e)
        {
            Button Botao = sender as Button;

            if (BotoesPressionados.Contains(Botao) == false)
            {

                if (Botao.Tag.ToString() == "NadaVirada")
                {
                    

                    if(Rodada == 0)
                    {
                        if (Modo == "Ativar")
                        {
                            BotoesPressionados.Add(Botao);
                            Botao.BackgroundImage = null;
                            int BombasRedor = 0;
                            VerificarInicio(BombasRedor, Botao);
                            Rodada++;
                        }
                        else if (Modo == "Marcar")
                        {
                            MarcarBlocos(Botao);
                        }
                    }
                    else
                    {
                        if (Modo == "Ativar")
                        {
                            BotoesPressionados.Add(Botao);
                            Botao.BackgroundImage = null;
                            int BombasRedor = 0;

                            VerificarRedor(BombasRedor, Botao);
                            Rodada++;
                        }
                        else if(Modo == "Marcar")
                        {
                            MarcarBlocos(Botao);
                        }
                    }
                }
                else if (Botao.Tag.ToString() == "BombaVirada")
                {
                    if (Modo == "Ativar")
                    {
                        Botao.BackgroundImage = Resources.Bomb1;
                        if (MessageBox.Show("Você Perdeu. Deseja Continuar Jogando ?", "Campo Minado",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            Limpar();
                        }
                        else
                        {
                            Close();
                        }
                    }
                    else if (Modo == "Marcar")
                    {
                        MarcarBlocos(Botao);
                    }
                }
                for (int i = 0; i < 3; i++)
                {
                    foreach (Button Butao in Controls.OfType<Button>())
                    {
                        if (Butao.Text == "" && Butao.Tag.ToString() == "Nada")
                        {
                            ZeroAutomatico(Butao);
                        }
                    }
                }
            }

            BlocosNoJogo = 0;
            foreach (Button Botoes in Controls.OfType<Button>())
            {
                if(Botoes.Tag.ToString() == "NadaVirada")
                {
                    BlocosNoJogo++;
                }
            }
            if(BlocosNoJogo == 0)
            {
                if(MessageBox.Show("Você Ganhou. Deseja Continuar Jogando ?", "Campo Minado",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Limpar();
                }
                else
                {
                    Close();
                }
            }
        }
    }
}
