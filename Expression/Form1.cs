using Expression.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Expression
{
    public partial class Form1 : Form
    {
        private Equation equation;
        private string expression;
        private string[] identificatorNames;
        private decimal[] identificatorValues;


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox2.Text != "")
            {
                // Parse names of identificators.
                identificatorNames = textBox2.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            }
            else
            {
                identificatorNames = new string[0];
            }

            if (textBox3.Text != "")
            {
                // Parse values of identificators.
                identificatorValues = textBox3.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).Select(l => Decimal.Parse(l)).ToArray();
            }
            else
            {
                identificatorValues = new decimal[0];
            }


            if (identificatorNames.Length != identificatorValues.Length)
            {
                MessageBox.Show("Invalid identificators, please try again", "Error", MessageBoxButtons.OK);
                return;
            }

            textBox1.Enabled = true;
            button2.Visible = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {

            // Itialize the identificators.
            List<Identificator> tempList = new List<Identificator>();

            for (int i = 0; i < identificatorNames.Length; i++)
            {
                tempList.Add(new Identificator(identificatorNames[i], identificatorValues[i]));
            }

            // Parse expression.
            expression = textBox1.Text;

            try
            {
                equation = new Equation(expression, tempList);
                textBox4.Text = equation.Evaluate().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                return;
            }

            button2.Visible = false;

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Reading the expression from the txt file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }

            MessageBox.Show(fileContent, "File Content at path: " + filePath, MessageBoxButtons.OK);
            textBox1.Text = fileContent;
        }
    }
}
