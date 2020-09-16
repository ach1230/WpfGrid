using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfGrid
{
    /// <summary>
    /// LargeImage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LargeImage : Window
    {
        public LargeImage()
        {
            InitializeComponent();
        }

        public void SetLargeImage(ImageSource imageSource)
        {
            imgLargeImage.Source = null;
            imgLargeImage.Source = imageSource;
            this.Width = imageSource.Width;
            this.Height = imageSource.Height;
        }
    }
}
