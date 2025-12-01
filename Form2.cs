
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
    public partial class FormAdmin : Form
    {
        /*---------------------------------------------------------------------------------------
         Array contendo as TextBox dos campos de cliente.
         Permite manipular os campos como conjunto (validação, limpeza, etc).
        ----------------------------------------------------------------------------------------*/
        private TextBox[] camposTxtCliente;

        /*---------------------------------------------------------------------------------------
         String de conexão ao banco MySQL.
         Contém host, porta, banco, usuário, senha e modo SSL desabilitado.
        ----------------------------------------------------------------------------------------*/
        private string stringConexao = "server = 127.0.0.1; port=3306;database=aluguel_carros;uid=root;pwd=@uo&AY2k;SslMode=Disabled;";

        /*---------------------------------------------------------------------------------------
         Construtor do FormAdmin
         - Inicializa componentes
         - Configura o ComboBox de status dos carros
         - Configura arrays de campos de cliente
         - Carrega IDs dos clientes e carros
         - Carrega dados nos ComboBoxes utilizados para edição
         - Vincula eventos SelectedIndexChanged para atualização dinâmica
        ----------------------------------------------------------------------------------------*/
        public FormAdmin()
        {

            InitializeComponent();

            comboCarro_Status.Items.Add("Disponível");
            comboCarro_Status.Items.Add("Alugado");
            comboCarro_Status.Items.Add("Manutenção");

            comboCarro_Status.SelectedIndex = 0;

            camposTxtCliente = new TextBox[]
            {
                txtClientes_01, txtClientes_02, txtClientes_03, txtClientes_04,
                txtClientes_05, txtClientes_06, txtClientes_07, txtClientes_08, txtClientes_09
            };

            CarregarIDsClientes();
            CarregarIDsCarros();

            CarregarCarrosParaCombos();
            CarregarClientesParaCombos();

            // Eventos de atualização
            comboID_Clientes.SelectedIndexChanged += ComboID_Clientes_SelectedIndexChanged;
            comboID_Carros.SelectedIndexChanged += ComboID_Carros_SelectedIndexChanged;
            comboAlugueis_01.SelectedIndexChanged += comboAlugueis_01_SelectedIndexChanged;
            comboAlugueis_02.SelectedIndexChanged += comboAlugueis_02_SelectedIndexChanged;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        //======================================================================================
        //                                SESSÃO - CLIENTES
        //======================================================================================

        /*---------------------------------------------------------------------------------------
         CarregarIDsClientes()
         - Carrega do banco a lista de Nomes de Clientes
         - Preenche o ComboBox comboID_Clientes
         - Utilizado como base para seleção, edição e remoção
        ----------------------------------------------------------------------------------------*/
        private void CarregarIDsClientes()
        {
            try
            {

                if (comboID_Clientes == null)
                {
                    MessageBox.Show("Erro: comboBoxClientes não foi inicializada.");
                    return;
                }

                comboID_Clientes.Items.Clear();

                using (var conexao = new MySqlConnection(stringConexao))
                using (var comando = new MySqlCommand("SELECT nome FROM clientes ORDER BY id", conexao))
                {
                    conexao.Open();
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboID_Clientes.Items.Add(reader.GetString("nome"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar IDs dos clientes:\n{ex.Message}");
            }
        }

        private void btnAdicionar_0_Click(object sender, EventArgs e)
        {

            TextBox[] campos =
            {
                txtClientes_01,
                txtClientes_02,
                txtClientes_03,
                txtClientes_04,
                txtClientes_05,
                txtClientes_06,
                txtClientes_07,
                txtClientes_08,
                txtClientes_09
            };

            foreach (var campo in campos)
            {
                if (string.IsNullOrWhiteSpace(campo.Text))
                {
                    MessageBox.Show("Preencha todos os campos antes de adicionar o cliente.",
                                    "Campos vazios",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }
            }

            try
            {
                using (var conexao = new MySqlConnection(stringConexao))
                using (var comando = new MySqlCommand(
                    "INSERT INTO clientes (nome, idade, cpf, cnh, validade_cnh, endereco, cep, telefone, email) " +
                    "VALUES (@nome, @idade, @cpf, @cnh, @validade_cnh, @endereco, @cep, @telefone, @email)", conexao))
                {
                    comando.Parameters.AddWithValue("@nome", txtClientes_01.Text);
                    comando.Parameters.AddWithValue("@idade", txtClientes_02.Text);
                    comando.Parameters.AddWithValue("@cpf", txtClientes_03.Text);
                    comando.Parameters.AddWithValue("@cnh", txtClientes_04.Text);
                    comando.Parameters.AddWithValue("@validade_cnh", txtClientes_05.Text);
                    comando.Parameters.AddWithValue("@endereco", txtClientes_06.Text);
                    comando.Parameters.AddWithValue("@cep", txtClientes_07.Text);
                    comando.Parameters.AddWithValue("@telefone", txtClientes_08.Text);
                    comando.Parameters.AddWithValue("@email", txtClientes_09.Text);

                    conexao.Open();
                    comando.ExecuteNonQuery();
                }

                MessageBox.Show("Cliente cadastrado com sucesso!",
                                "Sucesso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                foreach (var campo in campos)
                    campo.Clear();

                CarregarIDsClientes();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar cliente:\n" + ex.Message,
                                "Erro",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void btnAlterar_0_Click(object sender, EventArgs e)
        {
            if (comboID_Clientes.SelectedItem == null)
            {
                MessageBox.Show("Selecione um ID de cliente antes de alterar.");
                return;
            }

            string id = comboID_Clientes.SelectedItem.ToString();

            List<string> campos = new List<string>();
            List<MySqlParameter> parametros = new List<MySqlParameter>();

            if (!string.IsNullOrWhiteSpace(txtClientes_01.Text))
            {
                campos.Add("nome = @nome");
                parametros.Add(new MySqlParameter("@nome", txtClientes_01.Text));
            }

            if (!string.IsNullOrWhiteSpace(txtClientes_02.Text))
            {
                campos.Add("idade = @idade");
                parametros.Add(new MySqlParameter("@idade", txtClientes_02.Text));
            }

            if (!string.IsNullOrWhiteSpace(txtClientes_03.Text))
            {
                campos.Add("cpf = @cpf");
                parametros.Add(new MySqlParameter("@cpf", txtClientes_03.Text));
            }

            if (!string.IsNullOrWhiteSpace(txtClientes_04.Text))
            {
                campos.Add("cnh = @cnh");
                parametros.Add(new MySqlParameter("@cnh", txtClientes_04.Text));
            }

            if (!string.IsNullOrWhiteSpace(txtClientes_05.Text))
            {
                campos.Add("validade_cnh = @validade_cnh");
                parametros.Add(new MySqlParameter("@validade_cnh", txtClientes_05.Text));
            }

            if (!string.IsNullOrWhiteSpace(txtClientes_06.Text))
            {
                campos.Add("endereco = @endereco");
                parametros.Add(new MySqlParameter("@endereco", txtClientes_06.Text));
            }

            if (!string.IsNullOrWhiteSpace(txtClientes_07.Text))
            {
                campos.Add("cep = @cep");
                parametros.Add(new MySqlParameter("@cep", txtClientes_07.Text));
            }

            if (!string.IsNullOrWhiteSpace(txtClientes_08.Text))
            {
                campos.Add("telefone = @telefone");
                parametros.Add(new MySqlParameter("@telefone", txtClientes_08.Text));
            }

            if (!string.IsNullOrWhiteSpace(txtClientes_09.Text))
            {
                campos.Add("email = @email");
                parametros.Add(new MySqlParameter("@email", txtClientes_09.Text));
            }

            if (campos.Count == 0)
            {
                MessageBox.Show("Preencha pelo menos um campo para atualizar.");
                return;
            }

            string query = $"UPDATE clientes SET {string.Join(", ", campos)} WHERE nome = @id";

            try
            {
                using (var conexao = new MySqlConnection(stringConexao))
                using (var comando = new MySqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@id", id);

                    foreach (var p in parametros)
                        comando.Parameters.Add(p);

                    conexao.Open();
                    comando.ExecuteNonQuery();
                }

                MessageBox.Show("Cliente atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar cliente:\n{ex.Message}");
            }
        }
        private void btnRemover_0_Click(object sender, EventArgs e)
        {
            if (comboID_Clientes.SelectedItem == null)
            {
                MessageBox.Show("Selecione um cliente para remover.");
                return;
            }

            string id = Convert.ToString(comboID_Clientes.SelectedItem);

            var confirm = MessageBox.Show(
                $"Tem certeza que deseja remover o cliente ID {id}?",
                "Confirmar Remoção",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                using (var conexao = new MySqlConnection(stringConexao))
                using (var comando = new MySqlCommand("DELETE FROM clientes WHERE id = @id", conexao))
                {
                    comando.Parameters.AddWithValue("@id", id);

                    conexao.Open();
                    comando.ExecuteNonQuery();
                }

                MessageBox.Show("Cliente removido com sucesso!");

                comboID_Clientes.Items.Remove(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao remover cliente:\n{ex.Message}");
            }
        }

        //======================================================================================
        //                                SESSÃO - CARROS
        //======================================================================================

        /*---------------------------------------------------------------------------------------
         CarregarIDsCarros()
         - Consulta o banco
         - Obtém IDs dos carros cadastrados
         - Preenche comboID_Carros
        ----------------------------------------------------------------------------------------*/
        private void CarregarIDsCarros()
        {
            try
            {
                comboID_Carros.Items.Clear();

                using (var conexao = new MySqlConnection(stringConexao))
                using (var comando = new MySqlCommand("SELECT id_carro FROM carros ORDER BY id_carro", conexao))
                {
                    conexao.Open();
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboID_Carros.Items.Add(reader.GetInt32("id_carro"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar IDs dos carros:\n{ex.Message}");
            }
        }

        private void btnAdicionar_1_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(txtCarro_01.Text) ||
                    string.IsNullOrWhiteSpace(txtCarro_02.Text) ||
                    string.IsNullOrWhiteSpace(txtCarro_03.Text) ||
                    string.IsNullOrWhiteSpace(txtCarro_04.Text) ||
                    string.IsNullOrWhiteSpace(txtCarro_05.Text) ||
                    string.IsNullOrWhiteSpace(txtCarro_06.Text) ||
                    string.IsNullOrWhiteSpace(txtCarro_07.Text) ||
                    string.IsNullOrWhiteSpace(txtCarro_08.Text) ||
                    string.IsNullOrWhiteSpace(txtCarro_09.Text) ||
                    comboCarro_Status.SelectedIndex < 0)
                {
                    MessageBox.Show("Preencha todos os campos antes de adicionar.");
                    return;
                }

                using (var conexao = new MySqlConnection(stringConexao))
                using (var comando = new MySqlCommand(
                    @"INSERT INTO carros (modelo, marca, ano, placa, cor, km_atual, tipo_combustivel,
                    categoria, valor_diaria, status)
                    VALUES (@modelo, @marca, @ano, @placa, @cor, @km, @combustivel, @categoria, @valor, @status)",
                    conexao))
                {
                    comando.Parameters.AddWithValue("@modelo", txtCarro_01.Text);
                    comando.Parameters.AddWithValue("@marca", txtCarro_02.Text);
                    comando.Parameters.AddWithValue("@ano", int.Parse(txtCarro_03.Text));
                    comando.Parameters.AddWithValue("@placa", txtCarro_04.Text);
                    comando.Parameters.AddWithValue("@cor", txtCarro_05.Text);
                    comando.Parameters.AddWithValue("@km", decimal.Parse(txtCarro_06.Text));
                    comando.Parameters.AddWithValue("@combustivel", txtCarro_07.Text);
                    comando.Parameters.AddWithValue("@categoria", txtCarro_08.Text);
                    comando.Parameters.AddWithValue("@valor", decimal.Parse(txtCarro_09.Text));
                    comando.Parameters.AddWithValue("@status", comboCarro_Status.Text);

                    conexao.Open();
                    comando.ExecuteNonQuery();
                }

                MessageBox.Show("Carro adicionado com sucesso!");

                CarregarIDsCarros();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao adicionar carro:\n{ex.Message}");
            }
        }

        private void btnAlterar_1_Click(object sender, EventArgs e)
        {
            if (comboID_Carros.SelectedItem == null)
            {
                MessageBox.Show("Selecione um carro.");
                return;
            }

            int idCarro = (int)comboID_Carros.SelectedItem;

            string[] camposBanco =
            {
                "modelo",
                "marca",
                "ano",
                "placa",
                "cor",
                "km_atual",
                "tipo_combustivel",
                "categoria",
                "valor_diaria"
            };

            TextBox[] txts =
            {
                txtClientes_01,
                txtClientes_02,
                txtClientes_03,
                txtClientes_04,
                txtClientes_05,
                txtClientes_06,
                txtClientes_07,
                txtClientes_08,
                txtClientes_09
            };

            List<string> campos = new List<string>();
            List<object> valores = new List<object>();

            for (int i = 0; i < txts.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(txts[i].Text))
                {
                    string campo = camposBanco[i];
                    campos.Add($"{campo} = @{campo}");

                    if (campo == "ano")
                        valores.Add(int.Parse(txts[i].Text));
                    else if (campo == "km_atual" || campo == "valor_diaria")
                        valores.Add(decimal.Parse(txts[i].Text));
                    else
                        valores.Add(txts[i].Text);
                }
            }

            if (comboCarro_Status.SelectedItem != null)
            {
                campos.Add("status = @status");
                valores.Add(comboCarro_Status.SelectedItem.ToString());
            }

            if (campos.Count == 0)
            {
                MessageBox.Show("Nenhum campo foi preenchido para alteração.");
                return;
            }

            string sql = "UPDATE Carros SET " + string.Join(", ", campos) + " WHERE id_carro = @id";

            valores.Add(idCarro);

            try
            {
                using (var conexao = new MySqlConnection(stringConexao))
                using (var comando = new MySqlCommand(sql, conexao))
                {
                    conexao.Open();

                    for (int i = 0; i < txts.Length; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(txts[i].Text))
                        {
                            string campo = camposBanco[i];

                            if (campo == "ano")
                                comando.Parameters.AddWithValue($"@{campo}", int.Parse(txts[i].Text));

                            else if (campo == "km_atual" || campo == "valor_diaria")
                                comando.Parameters.AddWithValue($"@{campo}", decimal.Parse(txts[i].Text));

                            else
                                comando.Parameters.AddWithValue($"@{campo}", txts[i].Text);
                        }
                    }

                    if (comboCarro_Status.SelectedItem != null)
                        comando.Parameters.AddWithValue("@status", comboCarro_Status.SelectedItem.ToString());

                    comando.Parameters.AddWithValue("@id", idCarro);

                    comando.ExecuteNonQuery();
                }

                MessageBox.Show("Carro atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar carro:\n" + ex.Message);
            }
        }

        private void btnRemover_1_Click(object sender, EventArgs e)
        {

            if (comboID_Carros.SelectedIndex < 0)
            {
                MessageBox.Show("Selecione um ID.");
                return;
            }

            string id = comboID_Carros.SelectedItem.ToString();

            try
            {
                using (var conexao = new MySqlConnection(stringConexao))
                using (var comando = new MySqlCommand("DELETE FROM carros WHERE id_carro=@id", conexao))
                {
                    comando.Parameters.AddWithValue("@id", id);

                    conexao.Open();
                    comando.ExecuteNonQuery();
                }

                MessageBox.Show("Carro removido com sucesso!");

                CarregarIDsCarros();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao remover carro:\n{ex.Message}");
            }
        }

        //======================================================================================
        //                              CARREGAR IDS PARA COMBOBOXES
        //======================================================================================

        /*---------------------------------------------------------------------------------------
         CarregarClientesParaCombos()
         - Preenche comboID_Clientes e comboAlugueis_01
         - Utiliza DataSource para exibir nome, vincular ID
        ----------------------------------------------------------------------------------------*/
        private void CarregarClientesParaCombos()
        {
            try
            {
                using (var conexao = new MySqlConnection(stringConexao))
                {
                    conexao.Open();
                    string sql = "SELECT id, nome FROM clientes ORDER BY nome";
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conexao);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    comboID_Clientes.DataSource = dt.Copy();
                    comboID_Clientes.DisplayMember = "nome";
                    comboID_Clientes.ValueMember = "id";
                    comboID_Clientes.SelectedIndex = -1;

                    comboAlugueis_01.DataSource = dt.Copy();
                    comboAlugueis_01.DisplayMember = "nome";
                    comboAlugueis_01.ValueMember = "id";
                    comboAlugueis_01.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar clientes: " + ex.Message);
            }
        }

        private void ComboID_Clientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboID_Clientes.SelectedValue == null) return;
            int id = Convert.ToInt32(comboID_Clientes.SelectedValue);
            CarregarDadosCliente(id);
        }
        private void CarregarDadosCliente(int idCliente)
        {
            try
            {
                using (var conexao = new MySqlConnection(stringConexao))
                {
                    conexao.Open();
                    string sql = "SELECT id, nome, idade, cpf, cnh, validade_cnh, endereco, cep, telefone, email FROM clientes WHERE id = @id LIMIT 1";
                    using (var cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@id", idCliente);
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                txtClientes_01.Text = dr["nome"]?.ToString();
                                txtClientes_02.Text = dr["idade"]?.ToString();
                                txtClientes_03.Text = dr["cpf"]?.ToString();
                                txtClientes_04.Text = dr["cnh"]?.ToString();
                                txtClientes_05.Text = dr["validade_cnh"] != DBNull.Value ? Convert.ToDateTime(dr["validade_cnh"]).ToString("yyyy-MM-dd") : "";
                                txtClientes_06.Text = dr["endereco"]?.ToString();
                                txtClientes_07.Text = dr["cep"]?.ToString();
                                txtClientes_08.Text = dr["telefone"]?.ToString();
                                txtClientes_09.Text = dr["email"]?.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados do cliente: " + ex.Message);
            }
        }


        private void CarregarCarrosParaCombos()
        {
            try
            {
                using (var conexao = new MySqlConnection(stringConexao))
                {
                    conexao.Open();
                    string sql = "SELECT id_carro, placa, modelo, CONCAT(placa, ' - ', modelo) AS exibicao FROM carros ORDER BY placa";
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conexao);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    comboID_Carros.DataSource = dt;
                    comboID_Carros.DisplayMember = "exibicao";
                    comboID_Carros.ValueMember = "id_carro";
                    comboID_Carros.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar carros: " + ex.Message);
            }
        }
        private void ComboID_Carros_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboID_Carros.SelectedValue == null) return;
            int id = Convert.ToInt32(comboID_Carros.SelectedValue);
            CarregarDadosCarro(id);
        }

        /*---------------------------------------------------------------------------------------
         CarregarDadosCarro()
         - Consulta informações do carro selecionado
         - Preenche campos do formulário
        ----------------------------------------------------------------------------------------*/
        private void CarregarDadosCarro(int idCarro)
        {
            try
            {
                using (var conexao = new MySqlConnection(stringConexao))
                {
                    conexao.Open();
                    string sql = "SELECT modelo, marca, ano, placa, cor, km_atual, tipo_combustivel, categoria, valor_diaria, status FROM carros WHERE id_carro = @id LIMIT 1";
                    using (var cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@id", idCarro);
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                txtCarro_01.Text = dr["modelo"]?.ToString();
                                txtCarro_02.Text = dr["marca"]?.ToString();
                                txtCarro_03.Text = dr["ano"]?.ToString();
                                txtCarro_04.Text = dr["placa"]?.ToString();
                                txtCarro_05.Text = dr["cor"]?.ToString();
                                txtCarro_06.Text = dr["km_atual"]?.ToString();
                                txtCarro_07.Text = dr["tipo_combustivel"]?.ToString();
                                txtCarro_08.Text = dr["categoria"]?.ToString();
                                txtCarro_09.Text = dr["valor_diaria"]?.ToString();
                                comboCarro_Status.Text = dr["status"]?.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados do carro: " + ex.Message);
            }
        }

        /*---------------------------------------------------------------------------------------
        GetSelectedIdFromCombo()
        - Interpreta corretamente valores de combos ligados a DataTable
        - Resolve problemas de SelectedValue retornando DataRowView
        ----------------------------------------------------------------------------------------*/
        private int GetSelectedIdFromCombo(ComboBox cb)
        {
            if (cb == null) return -1;
            if (cb.SelectedValue == null) return -1;

            if (cb.SelectedValue is DataRowView drv)
            {
                if (drv.DataView.Table.Columns.Contains("id_aluguel"))
                {
                    object v = drv["id_aluguel"];
                    if (v != DBNull.Value) return Convert.ToInt32(v);
                }
                if (drv.DataView.Table.Columns.Contains("id"))
                {
                    object v = drv["id"];
                    if (v != DBNull.Value) return Convert.ToInt32(v);
                }

                int tmp;
                if (int.TryParse(drv.Row.ItemArray.FirstOrDefault()?.ToString(), out tmp)) return tmp;
                return -1;
            }

            int id;
            if (int.TryParse(cb.SelectedValue.ToString(), out id)) return id;
            try { return Convert.ToInt32(cb.SelectedValue); }
            catch { return -1; }
        }

        //======================================================================================
        //                                   SESSÃO - ALUGUÉIS
        //======================================================================================

        private void btnAdicionar_2_Click(object sender, EventArgs e)
        {
            try
            {
                int idCliente = GetSelectedIdFromCombo(comboAlugueis_01);
                if (idCliente <= 0)
                {
                    MessageBox.Show("Selecione um cliente antes de adicionar o aluguel.");
                    return;
                }

                int idCarro = GetSelectedIdFromCombo(comboID_Carros);
                if (idCarro <= 0)
                {
                    MessageBox.Show("Selecione um carro antes de adicionar o aluguel.");
                    return;
                }

                DateTime dataInicio, dataFim;
                if (!DateTime.TryParse(txtAluguel_01.Text, out dataInicio) ||
                    !DateTime.TryParse(txtAluguel_02.Text, out dataFim))
                {
                    MessageBox.Show("Data de início/fim inválida. Use o formato aaaa-MM-dd.");
                    return;
                }

                decimal valorTotal = 0, kmInicial = 0, kmFinal = 0;
                decimal.TryParse(txtAluguel_05.Text, out valorTotal);
                decimal.TryParse(txtAluguel_03.Text, out kmInicial);
                decimal.TryParse(txtAluguel_04.Text, out kmFinal);

                using (var conexao = new MySqlConnection(stringConexao))
                using (var comando = new MySqlCommand(
                    @"INSERT INTO alugueis 
                    (id_cliente, id_carro, data_inicio, data_fim, valor_total, km_inicial, km_final)
                    VALUES (@id_cliente, @id_carro, @data_inicio, @data_fim, @valor_total, @km_inicial, @km_final)",
                    conexao))
                {
                    comando.Parameters.AddWithValue("@id_cliente", idCliente);
                    comando.Parameters.AddWithValue("@id_carro", idCarro);
                    comando.Parameters.AddWithValue("@data_inicio", dataInicio);
                    comando.Parameters.AddWithValue("@data_fim", dataFim);
                    comando.Parameters.AddWithValue("@valor_total", valorTotal);
                    comando.Parameters.AddWithValue("@km_inicial", kmInicial);
                    comando.Parameters.AddWithValue("@km_final", kmFinal);

                    conexao.Open();
                    comando.ExecuteNonQuery();
                }

                MessageBox.Show("Aluguel registrado com sucesso!");

                if (comboAlugueis_01.SelectedValue != null)
                    CarregarAlugueisDoCliente(Convert.ToInt32(comboAlugueis_01.SelectedValue));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao adicionar aluguel:\n{ex.Message}");
            }
        }
        private void comboAlugueis_01_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboAlugueis_01.SelectedValue == null) return;
            int idCliente = Convert.ToInt32(comboAlugueis_01.SelectedValue);
            CarregarAlugueisDoCliente(idCliente);
        }

        /*---------------------------------------------------------------------------------------
         CarregarAlugueisDoCliente()
         - Carrega aluguéis pertencentes ao cliente selecionado
         - Preenche comboAlugueis_02 com datas
        ----------------------------------------------------------------------------------------*/
        private void CarregarAlugueisDoCliente(int idCliente)
        {
            try
            {
                comboAlugueis_02.SelectedIndexChanged -= comboAlugueis_02_SelectedIndexChanged;

                using (var conexao = new MySqlConnection(stringConexao))
                {
                    conexao.Open();
                    string sql = "SELECT id_aluguel, DATE_FORMAT(data_inicio, '%Y-%m-%d') AS data_inicio FROM alugueis WHERE id_cliente = @id ORDER BY data_inicio DESC";
                    using (var cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@id", idCliente);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        comboAlugueis_02.DataSource = null;
                        comboAlugueis_02.DisplayMember = "data_inicio";
                        comboAlugueis_02.ValueMember = "id_aluguel";
                        comboAlugueis_02.DataSource = dt;
                        comboAlugueis_02.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar aluguéis do cliente: " + ex.Message);
            }
            finally
            {
                comboAlugueis_02.SelectedIndexChanged += comboAlugueis_02_SelectedIndexChanged;
            }

        }

        private void comboAlugueis_02_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var cb = (ComboBox)sender;
                if (cb.SelectedIndex < 0) return;

                int idAluguel = -1;

                if (cb.SelectedValue != null && !(cb.SelectedValue is DataRowView))
                {
                    if (!int.TryParse(cb.SelectedValue.ToString(), out idAluguel))
                        idAluguel = Convert.ToInt32(cb.SelectedValue);
                }
                else if (cb.SelectedItem is DataRowView drv)
                {
                    object val = drv["id_aluguel"];
                    if (val != DBNull.Value) int.TryParse(val.ToString(), out idAluguel);
                }

                if (idAluguel <= 0) return;

                CarregarDadosAluguel(idAluguel);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao selecionar aluguel: " + ex.Message);
            }
        }
        private void CarregarDadosAluguel(int idAluguel)
        {
            try
            {
                using (var conexao = new MySqlConnection(stringConexao))
                {
                    conexao.Open();
                    string sql = @"SELECT id_aluguel, id_cliente, id_carro, data_inicio, data_fim, km_inicial, km_final, valor_total
                           FROM alugueis WHERE id_aluguel = @id LIMIT 1";
                    using (var cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@id", idAluguel);
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                txtAluguel_01.Text = dr["data_inicio"] != DBNull.Value ? Convert.ToDateTime(dr["data_inicio"]).ToString("yyyy-MM-dd") : "";
                                txtAluguel_02.Text = dr["data_fim"] != DBNull.Value ? Convert.ToDateTime(dr["data_fim"]).ToString("yyyy-MM-dd") : "";
                                txtAluguel_03.Text = dr["km_inicial"]?.ToString();
                                txtAluguel_04.Text = dr["km_final"]?.ToString();
                                txtAluguel_05.Text = dr["valor_total"]?.ToString();

                                object idCliente = dr["id_cliente"];
                                object idCarro = dr["id_carro"];
                                if (idCliente != DBNull.Value) comboAlugueis_01.SelectedValue = Convert.ToInt32(idCliente);
                                if (idCarro != DBNull.Value) comboID_Carros.SelectedValue = Convert.ToInt32(idCarro);
                            }
                            else
                            {
                                txtAluguel_01.Clear();
                                txtAluguel_02.Clear();
                                txtAluguel_03.Clear();
                                txtAluguel_04.Clear();
                                txtAluguel_05.Clear();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados do aluguel: " + ex.Message);
            }
        }
        private void btnAlterar_2_Click(object sender, EventArgs e)
        {
            try
            {
                int idAluguel = GetSelectedIdFromCombo(comboAlugueis_02);
                if (idAluguel <= 0)
                {
                    MessageBox.Show("Selecione um aluguel (data) para alterar.");
                    return;
                }

                List<string> campos = new List<string>();
                var parametros = new List<MySqlParameter>();

                DateTime tempDt;
                if (!string.IsNullOrWhiteSpace(txtAluguel_01.Text) && DateTime.TryParse(txtAluguel_01.Text, out tempDt))
                {
                    campos.Add("data_inicio = @data_inicio");
                    parametros.Add(new MySqlParameter("@data_inicio", tempDt));
                }
                if (!string.IsNullOrWhiteSpace(txtAluguel_02.Text) && DateTime.TryParse(txtAluguel_02.Text, out tempDt))
                {
                    campos.Add("data_fim = @data_fim");
                    parametros.Add(new MySqlParameter("@data_fim", tempDt));
                }

                decimal tmpDec;
                if (!string.IsNullOrWhiteSpace(txtAluguel_05.Text) && decimal.TryParse(txtAluguel_05.Text, out tmpDec))
                {
                    campos.Add("valor_total = @valor_total");
                    parametros.Add(new MySqlParameter("@valor_total", tmpDec));
                }
                if (!string.IsNullOrWhiteSpace(txtAluguel_03.Text) && decimal.TryParse(txtAluguel_03.Text, out tmpDec))
                {
                    campos.Add("km_inicial = @km_inicial");
                    parametros.Add(new MySqlParameter("@km_inicial", tmpDec));
                }
                if (!string.IsNullOrWhiteSpace(txtAluguel_04.Text) && decimal.TryParse(txtAluguel_04.Text, out tmpDec))
                {
                    campos.Add("km_final = @km_final");
                    parametros.Add(new MySqlParameter("@km_final", tmpDec));
                }

                if (campos.Count == 0)
                {
                    MessageBox.Show("Preencha ao menos um campo para alterar.");
                    return;
                }

                string sql = $"UPDATE alugueis SET {string.Join(", ", campos)} WHERE id_aluguel = @id";
                using (var conexao = new MySqlConnection(stringConexao))
                using (var comando = new MySqlCommand(sql, conexao))
                {
                    foreach (var p in parametros) comando.Parameters.Add(p);
                    comando.Parameters.AddWithValue("@id", idAluguel);

                    conexao.Open();
                    comando.ExecuteNonQuery();
                }

                MessageBox.Show("Aluguel atualizado com sucesso!");

                if (comboAlugueis_01.SelectedValue != null)
                    CarregarAlugueisDoCliente(Convert.ToInt32(comboAlugueis_01.SelectedValue));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao alterar aluguel:\n{ex.Message}");
            }
        }

        private void btnRemover_2_Click(object sender, EventArgs e)
        {
            try
            {
                int idAluguel = GetSelectedIdFromCombo(comboAlugueis_02);
                if (idAluguel <= 0)
                {
                    MessageBox.Show("Selecione um aluguel (data) para remover.");
                    return;
                }

                var confirm = MessageBox.Show($"Deseja remover o aluguel ID {idAluguel}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm != DialogResult.Yes) return;

                using (var conexao = new MySqlConnection(stringConexao))
                using (var comando = new MySqlCommand("DELETE FROM alugueis WHERE id_aluguel = @id", conexao))
                {
                    comando.Parameters.AddWithValue("@id", idAluguel);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                }

                MessageBox.Show("Aluguel removido com sucesso!");

                if (comboAlugueis_01.SelectedValue != null)
                    CarregarAlugueisDoCliente(Convert.ToInt32(comboAlugueis_01.SelectedValue));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao remover aluguel:\n{ex.Message}");
            }
        }
    }
}



