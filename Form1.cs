using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PI2025___Projeto
{
    public partial class FormAlugueis : Form
    {
        private string stringConexao = "server = 127.0.0.1; port=3306;database=aluguel_carros;uid=root;pwd=@uo&AY2k;SslMode=Disabled;";

        public FormAlugueis()
        {
            InitializeComponent();
            EstilizarDGV();
            CarregarAlugueis();
        }

        private void CarregarAlugueis()
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(stringConexao))
                {
                    conexao.Open();
                    string query = @"
                      SELECT 
                        categoria AS 'Categoria',
                        modelo AS 'Modelo',
                        marca AS 'Marca',
                        placa AS 'Placa',
                        ano AS 'Ano',
                        cor AS 'Cor',
                        km_atual AS 'Kilometragem (Km)',
                        valor_diaria AS 'Valor Diaria'
                    FROM carros;
                    ";

                    MySqlDataAdapter adaptador = new MySqlDataAdapter(query, conexao);
                    DataTable tabela = new DataTable();
                    adaptador.Fill(tabela);

                    dgvAlugueis.DataSource = tabela;
                }

                EstilizarDGV();

                if (dgvAlugueis.Columns.Count > 0)
                {
                    // Criamos uma CÉLULA DE CABEÇALHO nova e limpa
                    var newHeaderCell = new DataGridViewColumnHeaderCell();

                    // Pegamos o estilo exato que queremos
                    Color headerBack = Color.FromArgb(90, 140, 250);
                    Color headerFore = Color.White;
                    Font headerFont = new Font("Segoe UI", 11, FontStyle.Bold);
                    Padding cellPadding = new Padding(5, 8, 5, 8);

                    // Aplicamos o estilo diretamente nela
                    newHeaderCell.Style.BackColor = headerBack;
                    newHeaderCell.Style.ForeColor = headerFore;
                    newHeaderCell.Style.Font = headerFont;
                    newHeaderCell.Style.Padding = cellPadding;
                    newHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

                    dgvAlugueis.Columns[0].HeaderCell = newHeaderCell;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar aluguéis: " + ex.Message);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvAlugueis_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

            dgvAlugueis.BackgroundColor = Color.White;
            dgvAlugueis.BorderStyle = BorderStyle.None;
            dgvAlugueis.RowHeadersVisible = false;
            dgvAlugueis.CellBorderStyle = DataGridViewCellBorderStyle.None;

            dgvAlugueis.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAlugueis.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAlugueis.AllowUserToResizeRows = false;
            dgvAlugueis.AllowUserToAddRows = false;
            dgvAlugueis.AllowUserToDeleteRows = false;
            dgvAlugueis.ReadOnly = true;
            dgvAlugueis.MultiSelect = false;

            dgvAlugueis.EnableHeadersVisualStyles = false;
            dgvAlugueis.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvAlugueis.ColumnHeadersDefaultCellStyle.BackColor = headerBack;
            dgvAlugueis.ColumnHeadersDefaultCellStyle.ForeColor = headerFore;
            dgvAlugueis.ColumnHeadersDefaultCellStyle.Font = headerFont;
            dgvAlugueis.ColumnHeadersDefaultCellStyle.Padding = cellPadding;
            dgvAlugueis.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvAlugueis.ColumnHeadersHeight = 40;

            dgvAlugueis.DefaultCellStyle.BackColor = rowBack;
            dgvAlugueis.DefaultCellStyle.ForeColor = rowFore;
            dgvAlugueis.DefaultCellStyle.Font = cellFont;
            dgvAlugueis.DefaultCellStyle.SelectionBackColor = selectionBack;
            dgvAlugueis.DefaultCellStyle.SelectionForeColor = selectionFore;
            dgvAlugueis.DefaultCellStyle.Padding = cellPadding;
            dgvAlugueis.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvAlugueis.AlternatingRowsDefaultCellStyle.BackColor = alternatingBack;
            dgvAlugueis.AlternatingRowsDefaultCellStyle.ForeColor = rowFore;
            dgvAlugueis.AlternatingRowsDefaultCellStyle.Font = cellFont;
            dgvAlugueis.AlternatingRowsDefaultCellStyle.SelectionBackColor = selectionBack;
            dgvAlugueis.AlternatingRowsDefaultCellStyle.SelectionForeColor = selectionFore;
            dgvAlugueis.AlternatingRowsDefaultCellStyle.Padding = cellPadding;
            dgvAlugueis.AlternatingRowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvAlugueis.RowTemplate.Height = 38;
        }
    }
}
