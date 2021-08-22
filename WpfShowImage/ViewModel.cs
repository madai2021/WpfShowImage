using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace WpfShowImage
{
    class ViewModel : INotifyPropertyChanged
    {
        private string baseImgFilePath = string.Empty;
        private string overlayImgFilePath = string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private WriteableBitmap img;
        public WriteableBitmap Img
        {
            get
            {
                return img;
            }

            set
            {
                img = value;
                NotifyPropertyChanged();
            }
        }

        private double imgHeight;
        public double ImgHeight
        {
            get
            {
                return imgHeight;
            }

            set
            {
                imgHeight = value;
                NotifyPropertyChanged();
            }
        }

        private double imgWidth;
        public double ImgWidth
        {
            get
            {
                return imgWidth;
            }

            set
            {
                imgWidth = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand OpenBaseImgFile
        {
            get
            {
                return new BaseCommand(new Action(() =>
                {
                    var filePath = Utility.OpenFile("Open Base Img File", "All files (*.*)|*.*");
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        baseImgFilePath = filePath;
                        MessageBox.Show("Set Base Img File", "Info", MessageBoxButton.OK);
                    }
                }));
            }
        }
        public ICommand OpenOverlayImgFile
        {
            get
            {
                return new BaseCommand(new Action(() =>
                {
                    var filePath = Utility.OpenFile("Open Overlay Img File", "All files (*.*)|*.*");
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        overlayImgFilePath = filePath;
                        MessageBox.Show("Set Overlay Img File", "Info", MessageBoxButton.OK);
                    }
                }));
            }
        }

        public ICommand ViewImgFiles
        {
            get
            {
                return new BaseCommand(new Action(() =>
                {
                    if (!string.IsNullOrEmpty(baseImgFilePath))
                    {
                        try
                        {
                            var bitmap = OpenCVWrapper.OpenCVWrapperCls.GetImageBitmap(baseImgFilePath, overlayImgFilePath);
                            using (var ms = new MemoryStream())
                            {
                                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                                ms.Seek(0, SeekOrigin.Begin);
                                Img = new WriteableBitmap(BitmapFrame.Create(ms));
                            }
                        }
                        catch
                        {
                        }
                    }
                }));
            }
        }
    }
}