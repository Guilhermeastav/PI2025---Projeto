using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PI2025___Projeto
{
    public partial class Form1 : Form
    {
        //CONEXAO COM O BANCO DE DADOS

        private MySqlConnection conexao;
        private string stringConexao = "server=127.0.0.1;port=3306;database=aluguel_carros;uid=root;pwd=@uo&AY2k;SslMode=Disabled;";
        public Form1()
        {
            InitializeComponent();
        }

        private void VerificarConexao()
        {
            try
            {
                conexao = new MySqlConnection(stringConexao);
                conexao.Open();

                lblConexaoBDA.Text = "Conectado";
                lblConexaoBDA.ForeColor = Color.LimeGreen;
            }

            catch (Exception ex)
            {
                lblConexaoBDA.Text = "Não Conectado" + ex.Message;
                lblConexaoBDA.ForeColor = Color.Red;
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            VerificarConexao();
        }
        private void btnHistorico_Click(object sender, EventArgs e)
        {

        }

        private void btnDeslogar_Click(object sender, EventArgs e)
        {
            if (conexao != null && conexao.State == ConnectionState.Open)
                conexao.Close();

            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

     
        }

        private void CarregarAlugueis()
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(stringConexao))
                {
                    conexao.Open();
                    string query = "SELECT * FROM alugueis;";

                    MySqlDataAdapter adaptador = new MySqlDataAdapter(query, conexao);
                    DataTable tabela = new DataTable();
                    adaptador.Fill(tabela);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }
        private DataTable BuscarAlugueis()
        {
            DataTable tabela = new DataTable();
            string query = @"
            SELECT 
                a.id_aluguel,
                c.nome AS cliente,
                c.cpf,
                c.telefone,
                ca.modelo AS carro,
                ca.marca,
                ca.placa,
                ca.cor,
                ca.tipo_combustivel,
                a.data_inicio,
                a.data_fim,
                a.valor_total,
                a.km_inicial,
                a.km_final
            FROM alugueis a
            JOIN clientes c ON a.id_cliente = c.id
            JOIN carros ca ON a.id_carro = ca.id_carro;";

            try
            {
                using (MySqlConnection conexao = new MySqlConnection(stringConexao))
                {
                    conexao.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conexao))
                    {
                        MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd);
                        adaptador.Fill(tabela);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar alugueis: " + ex.Message);
            }

            return tabela;
        }

        private void btnAluguel_Click(object sender, EventArgs e)
        {
            FormAlugueis tela = new FormAlugueis();
            tela.ShowDialog(); 
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Color corSemitransparente = Color.FromArgb(128, 30, 30, 30);

            pnlBackground.BackColor = corSemitransparente;
        }
    }
}
