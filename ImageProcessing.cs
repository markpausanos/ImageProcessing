using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ImageProcessing
{
    public partial class ImageProcessing : Form
    {
        Bitmap loaded1, loaded2, processed;
        public ImageProcessing()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "PNG|*.png|BMP|*.bmp|JPEG|*.jpg";
            ImageFormat format;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string extension = System.IO.Path.GetExtension(saveFileDialog1.FileName);
                Console.WriteLine(extension);
                switch (extension)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                    default:
                        format = ImageFormat.Png;
                        break;
                }

                pictureBox3.Image.Save(saveFileDialog1.FileName, format); 
            }
        }

        private void basicCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageProcesses.BasicCopy(ref loaded1, ref processed);
            pictureBox3.Image = processed;
            button3.Enabled = true;
        }

        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageProcesses.Grayscale(ref loaded1, ref processed);
            pictureBox3.Image = processed;
            button3.Enabled = true;
        }

        private void colorInversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageProcesses.ColorInversion(ref loaded1, ref processed);
            pictureBox3.Image = processed;
            button3.Enabled = true;
        }
        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageProcesses.Histogram(ref loaded1, ref processed);
            pictureBox3.Image = processed;
            button3.Enabled = true;
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageProcesses.Sepia(ref loaded1, ref processed);
            pictureBox3.Image = processed;
            button3.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void subtractionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            button4.Enabled = true;
        }

        private void dIPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button4.Enabled = false;
        }
        private void subtraction_Click(object sender, EventArgs e)
        {
            try
            {
                ImageProcesses.Subtraction(ref loaded1, ref loaded2, ref processed, trackBar1.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            pictureBox3.Image = processed;
            button3.Enabled = true;
        }

        private void clearImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            loaded2 = new Bitmap(openFileDialog2.FileName);
            pictureBox2.Image = loaded2;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            loaded1 = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = loaded1;
        }
    }
}
    