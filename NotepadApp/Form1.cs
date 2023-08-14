using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace NotepadApp
{
    public partial class Form1 : Form
    {
        string currentFileName; // Şu anki işlenmekte olan dosyanın adı

        public Form1()
        {
            InitializeComponent();
            InitializeEvents(); // Olayları bağlamak için metodu çağırır
        }

        private void InitializeEvents()
        {
            newToolStripMenuItem.Click += newToolStripMenuItem_Click; // Yeni belge menü öğesi tıklanma olayı
            openToolStripMenuItem.Click += openToolStripMenuItem_Click; // Aç menü öğesi tıklanma olayı
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click; // Kaydet menü öğesi tıklanma olayı
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click; // Çıkış menü öğesi tıklanma olayı
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click; // Hakkında menü öğesi tıklanma olayı
            infoToolStripMenuItem.Click += infoToolStripMenuItem_Click; // Bilgi menü öğesi tıklanma olayı
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Yeni belge oluşturur
            myRichTextBox.Text = ""; // RichTextBox içeriğini temizler
            currentFileName = null; // Şu anki dosyanın adını sıfırlar
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Kaydedilmiş bir belge açar
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Metin Dosyaları|*.txt|Tüm Dosyalar|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFileName = openFileDialog.FileName; // Şu anki dosya adını kaydeder
                    myRichTextBox.Text = File.ReadAllText(currentFileName); // Dosya içeriğini okur ve RichTextBox'a yazar
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Mevcut belgeyi kaydeder veya yeni belgeyse farklı kaydedebilir
            if (string.IsNullOrEmpty(currentFileName)) // Eğer mevcut dosya adı boşsa
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Metin Dosyaları|*.txt|Tüm Dosyalar|*.*";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        currentFileName = saveFileDialog.FileName; // Yeni dosya adını kaydeder
                        File.WriteAllText(currentFileName, myRichTextBox.Text); // Dosyayı kaydeder
                    }
                }
            }
            else
            {
                File.WriteAllText(currentFileName, myRichTextBox.Text); // Mevcut dosyayı kaydeder
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Uygulamayı kapatır
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Hakkında menü öğesi tıklandığında tarayıcıyı açar ve belirtilen web sitesine yönlendirir
            string url = "https://tahayasinkoksal.com.tr";
            Process.Start(url); // Tarayıcıyı belirtilen URL'ye yönlendirir
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Bilgi menü öğesi tıklandığında metni gösteren bir iletişim kutusu açar
            string metin = "Bu uygulama Taha Yasin KÖKSAL tarafından yapılmıştır. Sınırsız şekilde kullanılabilir.";
            MessageBox.Show(metin, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
