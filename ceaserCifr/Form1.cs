using System.Numerics;
using System.Text;

namespace ceaserCifr
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static readonly string cyrillic = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя0123456789";
        private static readonly string latin = "abcdefghijklmnopqrstuvwxyz0123456789";

        public static void Is_key_correct(string key)
        {
            if (!BigInteger.TryParse(key, out _))
            {
                throw new Exception("Ключ должен состоять из чесел");
            }
        }

        private static string Remove_spaces(string text)
        {
            string[] res = text.Split();
            return string.Join(' ', res);
        }

        public static void Is_text_correct(string text, string al)
        {
            text = Remove_spaces(text);
            if (text.Length == 0)
            {
                throw new Exception("Введите текст для шифрования.");
            }
            for (int i = 0; i < text.Length; i++)
            {
                if (!cyrillic.Contains(text[i]) && !latin.Contains(text[i]))
                {
                    continue;
                }
                if (!al.Contains(text[i]))
                {
                    throw new Exception($"Текст содержит не соответсвтующие алфавиту символы.");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string step = textBox1.Text;
            string text = textBox2.Text.ToLower();
            bool is_сyrillic = checkBox1.Checked;

            if (is_сyrillic)
            {
                textBox3.Text = Encrypt(cyrillic, text, step);
            }
            else
            {
                textBox3.Text = Encrypt(latin, text, step);
            }
        }

        public static string Encrypt(string al, string text, string step)
        {
            try
            {
                Is_key_correct(step);
                Is_text_correct(text, al);
                StringBuilder code = new StringBuilder();
                BigInteger key = BigInteger.Parse(step);

                for (int i = 0; i < text.Length; i++)
                {
                    for (int j = 0; j < al.Length; j++)
                    {
                        if (Char.ToLower(text[i]) == al[j])
                        {
                            int ind = (int)((j + key) % al.Length);
                            code.Append(al[ind]);
                            break;
                        }
                        else if (!cyrillic.Contains(text[i]) && !latin.Contains(text[i]))
                        {
                            code.Append(text[i]);
                            break;
                        }
                    }
                }
                return code.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error message");
                return "Error";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string step = textBox1.Text;
            string text = textBox3.Text.ToLower();
            bool is_сyrillic = checkBox1.Checked;

            if (is_сyrillic)
            {
                textBox2.Text = Decrypt(cyrillic, text, step);
            }
            else
            {
                textBox2.Text = Decrypt(latin, text, step);
            }
        }

        public static string Decrypt(string al, string text, string step)
        {
            try
            {
                Is_key_correct(step);
                Is_text_correct(text, al);
                StringBuilder code = new StringBuilder();
                BigInteger key = BigInteger.Parse(step);

                for (int i = 0; i < text.Length; i++)
                {
                    for (int j = 0; j < al.Length; j++)
                    {
                        if (Char.ToLower(text[i]) == al[j])
                        {
                            int ind = (int)((j - key + al.Length) % al.Length);
                            if (ind < 0)
                            {
                                code.Append(al[al.Length + ind]);
                            }
                            else
                            {
                                code.Append(al[ind]);
                            }
                            break;
                        }
                        else if (!cyrillic.Contains(text[i]) && !latin.Contains(text[i]))
                        {
                            code.Append(text[i]);
                            break;
                        }
                    }
                }
                return code.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error message");
                return "Error";
            }
        }
    }
}