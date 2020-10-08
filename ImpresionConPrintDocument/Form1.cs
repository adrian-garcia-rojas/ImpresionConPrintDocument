using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImpresionConPrintDocument
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnImprime_Click(object sender, EventArgs e)
        {
            // crearemos el objeto printDocument1 y le colocaremos la configuracion para implementar el metodo Imprimir
            printDocument1 = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            printDocument1.PrinterSettings = ps;
            printDocument1.PrintPage += Imprimir;
            printDocument1.Print();
        }

        private void Imprimir(object sender, PrintPageEventArgs e)
        {          
            //constantes para formato
            int ancho = 250;
            int axisY = 20;
            System.Drawing.Font font = new System.Drawing.Font("Arial", 11, FontStyle.Regular, GraphicsUnit.Point);

            //texto
            e.Graphics.DrawString("Texto del ticket", font, Brushes.Black, new RectangleF(0, axisY+=20, ancho, 20));
            e.Graphics.DrawString("Texto del ticket2", font, Brushes.Black, new RectangleF(0, axisY+=20, ancho, 20));

            //recuepraciond de directorio de la imagen QR
            string pathBinIn = Directory.GetCurrentDirectory();
            string pathBin = Directory.GetParent(pathBinIn).ToString();
            string pathProject = Directory.GetParent(pathBin).ToString();
            System.Drawing.Image img = System.Drawing.Image.FromFile(pathProject + "/Images/ejemploQR1.png");
            e.Graphics.DrawImage(img, new System.Drawing.Rectangle(0, axisY += 20, 150, 150));
        }

        
        private void btn_ZenBarcode_Click(object sender, EventArgs e)
        {
            printDocument1 = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            printDocument1.PrinterSettings = ps;
            printDocument1.PrintPage += ImprimirZenBarcode;
            printDocument1.Print();
        }

        private void ImprimirZenBarcode(object sender, PrintPageEventArgs e)
        {
            System.Drawing.Image imgZenBarcode;         
            Zen.Barcode.CodeQrBarcodeDraw qrCode = Zen.Barcode.BarcodeDrawFactory.CodeQr;           
            imgZenBarcode = qrCode.Draw("|MAPR860101SL1|3017169|1|200|ROX|01/01/2011 09:05:37|31/12/2012|", 1);

            //constantes para formato
            int ancho = 250;
            int axisY = 20;
            System.Drawing.Font font = new System.Drawing.Font("Arial", 11, FontStyle.Regular, GraphicsUnit.Point);

            //texto
            e.Graphics.DrawString("Texto del ticket", font, Brushes.Black, new RectangleF(0, axisY += 20, ancho, 20));
            e.Graphics.DrawString("Texto del ticket2", font, Brushes.Black, new RectangleF(0, axisY += 20, ancho, 20));
            e.Graphics.DrawImage(imgZenBarcode, new System.Drawing.Rectangle(0, axisY += 20, 75, 75));
        }

    }
}
