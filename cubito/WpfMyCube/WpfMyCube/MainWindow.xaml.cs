using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;
using System.IO;

namespace WpfMyCube
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int x;
        public int y;
        public int z;

        public byte r;
        public byte g;
        public byte b;

       
        public int[] arrayX = new int[100];
        public int[] arrayY = new int[100];
        public int[] arrayZ = new int[100];
        public byte[] arrayR = new byte[101];
        public byte[] arrayG = new byte[101];
        public byte[] arrayB = new byte[101];


        public MainWindow()
        {
            InitializeComponent();
            Inicializar();
            leerArchivo();
                            
        }

        public void mostrararreglo()
        {
            for (int i = 0; i < arrayG.Length; i++)
            {
                MessageBox.Show(arrayX[i] + "," + arrayY[i] + "," + arrayZ[i] + "," + arrayR[i] + "," + arrayG[i] + "," + arrayB[i]);
            }
        }

        private void rotateX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RotateX(e.NewValue);
        }

        private void rotationY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RotateY(e.NewValue);
        }

        private void rotationZ_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RotateZ(e.NewValue);
        }

        public Color CubeColor { get; set; }

        public void RotateX(double angle)
        {
            rotX.Angle = angle;
        }

        public void RotateY(double angle)
        {
            rotY.Angle = angle;
        }

        public void RotateZ(double angle)
        {
            rotZ.Angle = angle;
        }

        public void Render()
        {

            CubeBuilder cubeBuilder2 = new CubeBuilder(CubeColor);
            CubeColor = System.Windows.Media.Color.FromRgb(arrayR[0], arrayG[0], arrayB[0]);
            mainViewport.Children.Add(cubeBuilder2.Create(0, 0, 0));

            for (int i = 0; i < arrayY.Length; i++)
            {
                if (arrayX[i] == 0 && arrayY[i] == 0 && arrayZ[i] == 0 && arrayR[i] == 0 && arrayR[i] == 0 && arrayB[i] == 0)
                {
                    i++;
                }
                else
                {
                    r = arrayR[i + 1];
                    b = arrayB[i + 1];
                    g = arrayG[i + 1];

                    CubeBuilder cubeBuilder = new CubeBuilder(CubeColor);
                    CubeColor = System.Windows.Media.Color.FromRgb(r, g, b);
                    mainViewport.Children.Add(cubeBuilder.Create(arrayX[i] * 6, arrayY[i] * 6, arrayZ[i] * 6));
                    MessageBox.Show(arrayX[i] + "," + arrayY[i] + "," + arrayZ[i] + "," + arrayR[i] + "," + arrayG[i] + "," + arrayB[i]);
                }
            }
        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mCamera.Position = new System.Windows.Media.Media3D.Point3D(
                mCamera.Position.X,
                mCamera.Position.Y,
                mCamera.Position.Z - e.Delta / 250D);

        }

        private void mBtn_Click(object sender, RoutedEventArgs e)
        {
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Inicializar();
        }
        private void Inicializar()
        {           
            rotateX.Value = 0;
            rotationY.Value = 0;
            rotationZ.Value = 0;            
        }

        public void leerArchivo()
        {
            FileStream fs = new FileStream(@"C:\Users\MyDam\desktop\archivo.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);

            string[] campos;
            string linea;

            int i = 0;

            try
            {

                while (!sr.EndOfStream)
                {
                    linea = sr.ReadLine();
                    campos = linea.Split(',');
                    int.TryParse(campos[0], out x);
                    int.TryParse(campos[1], out y);
                    int.TryParse(campos[2], out z);

                    byte.TryParse(campos[3], out r);
                    byte.TryParse(campos[4], out g);
                    byte.TryParse(campos[5], out b);

                    arrayX[i] = x;
                    arrayY[i] = y;
                    arrayZ[i] = z;

                    arrayR[i] = r;
                    arrayB[i] = b;
                    arrayG[i] = g;


                    i++;

                }

            }
            catch (IOException e)
            {
                MessageBox.Show("El error es: " + e);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Render();           
        }


    }
}