using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
//using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace WpfGrid
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly LargeImage wndLargeImage = new LargeImage();

        private Point point = new Point();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                //grdImage.ShowGridLines = true;                
                dialog.SelectedPath = @"Z:\folder\JAV\Y\Yumeno Aika(夢乃あいか)★★★";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string path = dialog.SelectedPath;
                    var directoryInfo = new DirectoryInfo(path);
                    int row = -1;
                    grdImage.RowDefinitions.Clear();
                    for (int i = 0; i < directoryInfo.GetFiles().Length; i++)
                    {
                        FileInfo file = directoryInfo.GetFiles()[i];

                        if (file.Extension == ".jpg")
                        {
                            row++;
                            RowDefinition rowDef = new RowDefinition();
                            var converter = new GridLengthConverter();
                            rowDef.Height = (GridLength)converter.ConvertFromString("150");
                            grdImage.RowDefinitions.Add(rowDef);

                            Image image = new Image();
                            image.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                            image.VerticalAlignment = VerticalAlignment.Stretch;

                            image.MouseEnter += new System.Windows.Input.MouseEventHandler(this.Image_MouseEvent);
                            image.MouseLeave += new System.Windows.Input.MouseEventHandler(this.Image_MouseLeave);
                            image.MouseMove += new System.Windows.Input.MouseEventHandler(this.Image_MouseMove);

                            BitmapImage bitmap = new BitmapImage();
                            bitmap.BeginInit();
                            bitmap.UriSource = new Uri(file.FullName);
                            bitmap.EndInit();
                            image.Source = bitmap;

                            Grid.SetColumn(image, 0);
                            Grid.SetRow(image, row);
                            grdImage.Children.Add(image);

                            var label = new System.Windows.Controls.Label();
                            //Thickness margin = label.Margin;
                            //margin.Left = grdImage.RenderSize.Width + 10;
                            //label.Margin = margin;
                            label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
                            label.VerticalContentAlignment = VerticalAlignment.Center;
                            label.Content = file.Name.Replace(file.Extension, string.Empty);

                            Grid.SetColumn(label, 1);
                            Grid.SetRow(label, row);
                            grdImage.Children.Add(label);
                        }
                    }
                }
            }
        }

        private void Image_MouseEvent(object sender, MouseEventArgs e)
        {
            wndLargeImage.SetLargeImage((sender as Image).Source);
            wndLargeImage.Show();
            Console.WriteLine("Enter");
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            wndLargeImage.Hide();
            Console.WriteLine("Leave");
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            this.point = e.GetPosition(this);

            wndLargeImage.Left = this.Left + point.X + 20;
            if (this.Top + point.Y + 30 + wndLargeImage.Height > 1080)
            {
                wndLargeImage.Top = this.Top + point.Y + 30 - wndLargeImage.Height;
            }
            else
            {
                wndLargeImage.Top = this.Top + point.Y + 30;
            }
        }
    }
}
