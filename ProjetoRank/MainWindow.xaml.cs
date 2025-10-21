using MySql.Data.MySqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ProjetoRank
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        

        public MainWindow()
        {
            InitializeComponent();

            // Abrir conexão com o banco quiz_jogador
            ConexaoDB.AbrirConexao();

            AtualizarPontos();
            // Inicia o timer
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(10); // A cada 10 segundos
            timer.Tick += (sender, e) => AtualizarPontos();
            timer.Start();

            this.WindowStyle = WindowStyle.None;
            this.WindowState = WindowState.Maximized;
            this.ResizeMode = ResizeMode.NoResize;

        }
        private void AtualizarPontos()
        {
            try
            {
                var comando = ConexaoDB.Conexao.CreateCommand();
                comando.CommandText = "SELECT Nome, Pontos FROM jogadores ORDER BY Pontos DESC LIMIT 5";

                using (var reader = comando.ExecuteReader())
                {
                    int posicao = 1;

                    while (reader.Read())
                    {
                        string nome = reader.GetString(0);
                        int pontos = reader.GetInt32(1);

                        // Atualiza a label correspondente
                        switch (posicao)
                        {
                            case 1:
                                J1.Content = nome;
                                Pt1.Content = pontos;
                                break;

                            case 2:
                                J2.Content = nome;
                                Pt2.Content = pontos;
                                break;

                            case 3:
                                J3.Content = nome;
                                Pt3.Content = pontos;
                                break;

                            case 4:
                                J4.Content = nome;
                                Pt4.Content = pontos;
                                break;

                            case 5:
                                J5.Content = nome;
                                Pt5.Content = pontos;
                                break;
                        }

                        posicao++;
                    }

                    // Caso haja menos de 5 jogadores, limpa as labels restantes
                    if (posicao <= 2) J2.Content = "";
                    if (posicao <= 3) J3.Content = "";
                    if (posicao <= 4) J4.Content = "";
                    if (posicao <= 5) J5.Content = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar pontos: " + ex.Message);
            }
        }
    }

}