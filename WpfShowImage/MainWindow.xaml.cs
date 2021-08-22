using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfShowImage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel vm;

        public MainWindow()
        {
            InitializeComponent();

            vm = new ViewModel();
            DataContext = vm;

#if false //ここを削除
            using (var ms = new MemoryStream(File.ReadAllBytes("baseImage.jpg")))
            {
                vm.Img = new WriteableBitmap(BitmapFrame.Create(ms));
                ms.Close();
            }
#endif
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            FrameworkElement frameworkElement = Content as FrameworkElement;
            vm.ImgHeight = frameworkElement.ActualHeight - menu.ActualHeight; //ここを修正
            vm.ImgWidth = frameworkElement.ActualWidth;
        }
    }
}