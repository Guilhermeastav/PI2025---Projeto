using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PI2025___Projeto
{
    public partial class FormHistorico : Form
    {

        private string stringConexao = "server = 127.0.0.1; port=3306;database=aluguel_carros;uid=root;pwd=@uo&AY2k;SslMode=Disabled;";
        public FormHistorico()
        {
            InitializeComponent();
            EstilizarDGV();
            carregarHistorico();
        }

        private void carregarHistorico()
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(stringConexao))
                {
                    conexao.Open();

                    string query = @"
                    SELECT 
                    a.id_aluguel AS 'ID',
                    c.nome AS 'Cliente',
                    ca.modelo AS 'Carro',
                    a.data_inicio AS 'Data Início',
                    a.data_fim AS 'Data Fim',
                    a.valor_total AS 'Valor'
                    FROM alugueis a
                    JOIN clientes c ON a.id_cliente = c.id
                    JOIN carros ca ON a.id_carro = ca.id_carro
                    ORDER BY a.id_aluguel;
                    ";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conexao);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvHistorico.DataSource = dt;

                    dgvHistorico.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvHistorico.RowHeadersVisible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar histórico: " + ex.Message);
            }
        }

        private void EstilizarDGV()
        {
            Color headerBack = Color.FromArgb(90, 140, 250);
            Color headerFore = Color.White;
            Color rowBack = Color.White;
            Color rowFore = Color.Black;
            Color alternatingBack = Color.FromArgb(240, 243, 250);
            Color selectionBack = Color.FromArgb(210, 225, 255);
            Color selectionFore = Color.Black;

            Font headerFont = new Font("Segoe UI", 11, FontStyle.Bold);
            Font cellFont = new Font("Segoe UI", 10, FontStyle.Regular);
            Padding cellPadding = new Padding(5, 8, 5, 8);

            dgvHistorico.BackgroundColor = Color.White;
            dgvHistorico.BorderStyle = BorderStyle.None;
            dgvHistorico.RowHeadersVisible = false;
            dgvHistorico.CellBorderStyle = DataGridViewCellBorderStyle.None;

            dgvHistorico.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistorico.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistorico.AllowUserToResizeRows = false;
            dgvHistorico.AllowUserToAddRows = false;
            dgvHistorico.AllowUserToDeleteRows = false;
            dgvHistorico.ReadOnly = true;
            dgvHistorico.MultiSelect = false;

            dgvHistorico.EnableHeadersVisualStyles = false;
            dgvHistorico.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvHistorico.ColumnHeadersDefaultCellStyle.BackColor = headerBack;
            dgvHistorico.ColumnHeadersDefaultCellStyle.ForeColor = headerFore;
            dgvHistorico.ColumnHeadersDefaultCellStyle.Font = headerFont;
            dgvHistorico.ColumnHeadersDefaultCellStyle.Padding = cellPadding;
            dgvHistorico.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvHistorico.ColumnHeadersHeight = 40;

            dgvHistorico.DefaultCellStyle.BackColor = rowBack;
            dgvHistorico.DefaultCellStyle.ForeColor = rowFore;
            dgvHistorico.DefaultCellStyle.Font = cellFont;
            dgvHistorico.DefaultCellStyle.SelectionBackColor = selectionBack;
            dgvHistorico.DefaultCellStyle.SelectionForeColor = selectionFore;
            dgvHistorico.DefaultCellStyle.Padding = cellPadding;
            dgvHistorico.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvHistorico.AlternatingRowsDefaultCellStyle.BackColor = alternatingBack;
            dgvHistorico.AlternatingRowsDefaultCellStyle.ForeColor = rowFore;
            dgvHistorico.AlternatingRowsDefaultCellStyle.Font = cellFont;
            dgvHistorico.AlternatingRowsDefaultCellStyle.SelectionBackColor = selectionBack;
            dgvHistorico.AlternatingRowsDefaultCellStyle.SelectionForeColor = selectionFore;
            dgvHistorico.AlternatingRowsDefaultCellStyle.Padding = cellPadding;
            dgvHistorico.AlternatingRowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvHistorico.RowTemplate.Height = 38;
        }

        private void dgvHistorico_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
