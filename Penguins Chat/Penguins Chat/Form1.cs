using MySql.Data.MySqlClient;

namespace Penguins_Chat
{
    public partial class Form1 : Form
    {
        Class1 a = new Class1();

        public Form1()
        {
            InitializeComponent();
            a.inicializa();

            if (a.open_connection())
            {
                MessageBox.Show("A ligação à base de dados " + a.data_base + " foi bem sucedida.");
            }
            else
            {
                MessageBox.Show("A ligação à base de dados " + a.data_base + " NÃO FOI POSSIVÉL.");
                a.close_connection();
                MessageBox.Show("A ligação à base de dados " + a.data_base + " foi desativada.");
            }
        }

        //Entrar
        private void button2_Click(object sender, EventArgs e)
        {
            label3.Visible = false;

            if (textBox1.Text == "" || textBox2.Text == "")
            {
                label3.Text = "*Todos os campos precisam de ser preenchidos";
                label3.Location = new Point(214, 12);
                label3.Visible = true;
            }

            else
            {
                string email = textBox1.Text;
                string pass = textBox2.Text;

                string query = "SELECT COUNT(*), email, pass, nickname FROM Pessoa WHERE email = @email and pass = @pass";

                // Criar o comando de verificação e associar a query com a ligação
                MySqlCommand verSeExisteConta = new MySqlCommand(query, a.connection);

                verSeExisteConta.Parameters.AddWithValue("@email", email);
                verSeExisteConta.Parameters.AddWithValue("@pass", pass);

                MySqlDataReader reader = verSeExisteConta.ExecuteReader();

                if (reader.Read())
                {
                    int contaExiste = reader.GetInt32(0);

                    if (contaExiste > 0)
                    {
                        string nickname= reader.GetString(3); //GetString(2) lê a coluna 3 da pesquisa

                        textBox1.Clear();
                        textBox2.Clear();

                        Form3 form3 = new Form3(nickname);
                        form3.Show();

                        reader.Close();
                    }
                    else if (contaExiste == 0)
                    {
                        textBox1.Clear();
                        textBox2.Clear();
                        label3.Visible = true;
                        label3.Text = "*Palavra-Pass ou Mail incorretos";
                        label3.Location = new Point(145, 12);
                    }
                }

                reader.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}
