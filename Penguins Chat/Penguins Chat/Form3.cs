using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Penguins_Chat
{
    public partial class Form3 : Form
    {
        Class1 a = new Class1();

        public string Nickname;

        public Form3(string nickname)
        {
            InitializeComponent();
            a.inicializa();

            CarregarPessoas();

            Nickname = nickname;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            CarregarPessoas();
        }

        private void CarregarPessoas()
        {
            string query = "SELECT nickname FROM Pessoa WHERE nickname != @Nickname";

            if (a.open_connection())
            {
                MySqlCommand pessoas = new MySqlCommand(query, a.connection);
                pessoas.Parameters.AddWithValue("@Nickname", Nickname);

                MySqlDataReader reader = pessoas.ExecuteReader();

                while (reader.Read())
                {
                    string nickname = reader.GetString("nickname");
                    AdicionarAoFlow(nickname);
                }

                reader.Close();

                a.close_connection();
            }

        }

        private void AdicionarAoFlow(string nickname)
        {
            Button buttonContacto = new Button
            {
                Text = nickname,
                Width = 239,
                Height = 28,
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat
            };

            // Additional configurations for the button
            buttonContacto.FlatAppearance.BorderSize = 1;
            buttonContacto.FlatAppearance.MouseDownBackColor = Color.Transparent;
            buttonContacto.FlatAppearance.MouseOverBackColor = Color.Transparent;

            // Add the button to the FlowLayoutPanel
            flowLayoutPanel1.Controls.Add(buttonContacto);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            CarregarPessoas();
        }
    }
}
