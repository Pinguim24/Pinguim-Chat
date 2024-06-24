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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Penguins_Chat
{
    public partial class Form2 : Form
    {
        Class1 a = new Class1();

        public Form2()
        {
            InitializeComponent();
            a.inicializa();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                label2.Visible = true;
            }
            else
            {
                if (textBox2.Text != textBox4.Text)
                {
                    label2.Visible = true;
                    label2.Text = "*As palavavas-passe não coincidem";
                }
                else
                {
                    label2.Visible = false;

                    string email = textBox1.Text;
                    string nkn = textBox3.Text;

                    //definir a query / pesquisa
                    string query = "INSERT INTO Pessoa (email, nome,  nickname, pass) " + "VALUES (@email, @nome,  @nickname, @pass)";
                    string query2 = "SELECT COUNT(*) FROM Pessoa WHERE email = @email OR nickname = @nickname";

                    //abrir a ligação à BD
                    if (a.open_connection())
                    {
                        // Criar o comando de verificação e associar a query com a ligação
                        MySqlCommand verSeExiste = new MySqlCommand(query2, a.connection);

                        verSeExiste.Parameters.AddWithValue("@email", email);
                        verSeExiste.Parameters.AddWithValue("@nickname", nkn);

                        // Executar o comando de verificação e obter o resultado
                        int contaExistente = Convert.ToInt32(verSeExiste.ExecuteScalar());



                        if (contaExistente > 0)
                        {
                            MessageBox.Show("Email ou Nickname já estão registados.");
                        }
                        else
                        {
                            //criar o comando e associar a query com a ligação através do construtor 
                            MySqlCommand cmd = new MySqlCommand(query, a.connection);

                            cmd.Parameters.AddWithValue("@email", textBox1.Text);
                            cmd.Parameters.AddWithValue("@nome", textBox5.Text);
                            cmd.Parameters.AddWithValue("@pass", textBox2.Text);
                            cmd.Parameters.AddWithValue("@nickname", textBox3.Text);

                            //executar o comando 
                            cmd.ExecuteNonQuery();

                            //fechar a ligação à BD 
                            a.close_connection();

                            Close();

                            MessageBox.Show("Registro feito com sucesso!");

                        }
                    }
                }
            }
        }
    }
}
