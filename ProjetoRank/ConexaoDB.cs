using MySql.Data.MySqlClient;
using System.Windows;

namespace ProjetoRank
{
    internal class ConexaoDB
    {
        public static MySqlConnection? Conexao { get; private set; }

        public static void AbrirConexao()
        {
            try
            {
                if (Conexao == null)
                {
                    Conexao = new MySqlConnection("server=localhost;uid=root;pwd=Lsm@1596357;database=quiz");
                    Conexao.Open();
                }
            }
            catch (Exception ex)
            {
                Conexao = null;
                MessageBox.Show(ex.ToString());
            }
        }

        public static void FecharConexao()
        {
            if (Conexao != null && Conexao.State == System.Data.ConnectionState.Open)
                Conexao.Close();
        }
    }
}