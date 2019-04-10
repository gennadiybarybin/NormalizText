using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabInformDefendModule1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Нормализация текста
        /// </summary>
        /// <param name="text">Входной текст для обработки</param>
        /// <returns>Нормализированный текст (лишь с русскими символами и цифрами)</returns>
        public string Normalize(string text)
        {
            text = text.ToLower();
            text = new string((from c_text in text where char.IsWhiteSpace(c_text) || char.IsLetterOrDigit(c_text) select c_text).ToArray());          
            text = ReplaceCharEnglishToRussian(text);
            text = Regex.Replace(text, @"[a-zA-Z]", "");
            return text;
        }

        /// <summary>
        /// Замена английских символов на русских
        /// </summary>
        /// <param name="text">Входной текст для замены символов</param>
        /// <returns>Текст с замененными символами</returns>
        private string ReplaceCharEnglishToRussian(string text)
        {
            Dictionary<char, char> replaces = Replacers();
            StringBuilder string_b = new StringBuilder(text);
            foreach (var replaсe in replaces)
            {
                string_b.Replace(replaсe.Key, replaсe.Value);
            }
            return string_b.ToString();
        }

        /// <summary>
        /// Словарь символов заменяемых на русские аналоги
        /// </summary>
        /// <returns>Заполненный словарь заменяемых символов</returns>
        private Dictionary<char, char> Replacers()
        {
            Dictionary<char, char> replaces = new Dictionary<char, char>();
            replaces.Add('a', 'а');
            replaces.Add('b', 'б');
            replaces.Add('c', 'с');
            replaces.Add('e', 'е');
            replaces.Add('o', 'о');
            replaces.Add('u', 'и');
            replaces.Add('r', 'г');
            replaces.Add('x', 'х');
            replaces.Add('y', 'у');
            return replaces;
        }

        /// <summary>
        /// Событие при нажатии на элемент меню "Открыть" открывающий входной текст программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text=Open(openFileDialog1);
        }

        /// <summary>
        /// Событие при нажатии на кнопку "->" вызывающий нормализацию входного текста в выходную
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = Normalize(richTextBox1.Text);
        }

        /// <summary>
        /// Событие при нажатии на элемент меню "Сохранить" сохраняющий выходной текст программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(saveFileDialog1, richTextBox2.Text);
        }

        /// <summary>
        /// Сохранение текста в .txt в кодировке UTF8
        /// </summary>
        /// <param name="saveFile">Элемент управления сохранением файла</param>
        /// <param name="text">сохраняемый текст</param>
        public void Save(SaveFileDialog saveFile, string text)
        {
            saveFile.FileName = "OutputText.txt";
            saveFile.Filter = "Text File | *.txt";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                StreamWriter s_writer = new StreamWriter(saveFile.OpenFile(), Encoding.UTF8);
                s_writer.WriteLine(text);
                s_writer.Dispose();
                s_writer.Close();
            }
        }

        /// <summary>
        /// Открытие текстового файла для вывода текста
        /// </summary>
        /// <param name="openFile">Элемент управления открытием файла</param>
        /// <returns>Выходной текст из файла</returns>
        public string Open(OpenFileDialog openFile)
        {
            string output_text = "";
            openFile.FileName = "InputText.txt";
            openFile.Title = "Открыть текстовой файл";
            openFile.Filter = "TXT files|*.txt";
            openFile.InitialDirectory = @"C:\";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string filename = openFile.FileName;
                string[] lines = File.ReadAllLines(filename);

                for (int a = 0; a < lines.Length; a++)
                {
                    output_text += lines[a] + Environment.NewLine;                   
                }
            }
            return output_text;
        }
    }
}
