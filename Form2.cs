
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

        private TextBox[] camposTxtCliente;

        private string stringConexao = "server = 127.0.0.1; port=3306;database=aluguel_carros;uid=root;pwd=@uo&AY2k;SslMode=Disabled;";
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
            CarregarIdsAlugueis();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        //===============================//
        // SESSAO DO CODIGO - CLIENTES   //
        //===============================//
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

        //==============================//
        // SESSAO DO CODIGO - CARROS    //
        //==============================//

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

            // Mapeamento dos campos do banco
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

            // Suas TextBoxes genéricas
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

                    // Conversão automática
                    if (campo == "ano")
                        valores.Add(int.Parse(txts[i].Text));
                    else if (campo == "km_atual" || campo == "valor_diaria")
                        valores.Add(decimal.Parse(txts[i].Text));
                    else
                        valores.Add(txts[i].Text);
                }
            }

            // STATUS (combobox)
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

        // ================================
        // ===== CARREGAR IDS NO COMBO ====
        // ================================

        private void CarregarIdsAlugueis()
        {
            try
            {
                comboBox_Alugueis.Items.Clear();

                using (var conexao = new MySqlConnection(stringConexao))
                using (var comando = new MySqlCommand("SELECT id_aluguel FROM alugueis ORDER BY id_aluguel", conexao))
                {
                    conexao.Open();
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox_Alugueis.Items.Add(reader.GetInt32("id_aluguel"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar IDs de aluguéis:\n{ex.Message}");
            }
        }

        private void btnAdicionar_2_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conexao = new MySqlConnection(stringConexao))
                using (var comando = new MySqlCommand(
                    @"INSERT INTO alugueis 
                    (id_cliente, id_carro, data_inicio, data_fim, valor_total, km_inicial, km_final)
                    VALUES (@id_cliente, @id_carro, @data_inicio, @data_fim, @valor_total, @km_inicial, @km_final)",
                    conexao))
                {
                    comando.Parameters.AddWithValue("@data_inicio", txtAluguel_01.Text);
                    comando.Parameters.AddWithValue("@data_fim", txtAluguel_02.Text);
                    comando.Parameters.AddWithValue("@valor_total", txtAluguel_03.Text);
                    comando.Parameters.AddWithValue("@km_inicial", txtAluguel_04.Text);
                    comando.Parameters.AddWithValue("@km_final", txtAluguel_05.Text);

                    conexao.Open();
                    comando.ExecuteNonQuery();

                    MessageBox.Show("Aluguel registrado com sucesso!");

                    CarregarIdsAlugueis();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao adicionar aluguel:\n{ex.Message}");
            }
        }

        private void btnAlterar_2_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox_Alugueis.SelectedItem == null)
                {
                    MessageBox.Show("Selecione um ID de aluguel para alterar.");
                    return;
                }

                int id = (int)comboBox_Alugueis.SelectedItem;

                List<string> campos = new List<string>();
                MySqlCommand comando;

                using (var conexao = new MySqlConnection(stringConexao))
                {
                    conexao.Open();

                    comando = new MySqlCommand();
                    comando.Connection = conexao;

                    if (!string.IsNullOrWhiteSpace(txtAluguel_01.Text))
                    {
                        campos.Add("data_inicio = @data_inicio");
                        comando.Parameters.AddWithValue("@data_inicio", txtAluguel_01.Text);
                    }

                    if (!string.IsNullOrWhiteSpace(txtAluguel_02.Text))
                    {
                        campos.Add("data_fim = @data_fim");
                        comando.Parameters.AddWithValue("@data_fim", txtAluguel_02.Text);
                    }

                    if (!string.IsNullOrWhiteSpace(txtAluguel_03.Text))
                    {
                        campos.Add("valor_total = @valor_total");
                        comando.Parameters.AddWithValue("@valor_total", txtAluguel_03.Text);
                    }

                    if (!string.IsNullOrWhiteSpace(txtAluguel_04.Text))
                    {
                        campos.Add("km_inicial = @km_inicial");
                        comando.Parameters.AddWithValue("@km_inicial", txtAluguel_04.Text);
                    }

                    if (!string.IsNullOrWhiteSpace(txtAluguel_05.Text))
                    {
                        campos.Add("km_final = @km_final");
                        comando.Parameters.AddWithValue("@km_final", txtAluguel_05.Text);
                    }

                    if (campos.Count == 0)
                    {
                        MessageBox.Show("Preencha ao menos um campo para alterar.");
                        return;
                    }

                    string sql = $"UPDATE alugueis SET {string.Join(", ", campos)} WHERE id_aluguel = @id";

                    comando.CommandText = sql;
                    comando.Parameters.AddWithValue("@id", id);

                    comando.ExecuteNonQuery();
                }

                MessageBox.Show("Aluguel atualizado com sucesso!");
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
                if (comboBox_Alugueis.SelectedItem == null)
                {
                    MessageBox.Show("Selecione um aluguel para remover.");
                    return;
                }

                int id = (int)comboBox_Alugueis.SelectedItem;

                using (var conexao = new MySqlConnection(stringConexao))
                using (var comando = new MySqlCommand(
                    "DELETE FROM alugueis WHERE id_aluguel = @id", conexao))
                {
                    comando.Parameters.AddWithValue("@id", id);

                    conexao.Open();
                    comando.ExecuteNonQuery();

                    MessageBox.Show("Aluguel removido com sucesso!");

                    CarregarIdsAlugueis();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao remover aluguel:\n{ex.Message}");
            }
        }
    }
}



