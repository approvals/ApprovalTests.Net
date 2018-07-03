#if !__MonoCS__
using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ApprovalUtilities.Wpf
{
    public class WpfUtils
    {
        public static void ScreenCapture(Window window, string filename)
        {
            try
            {
                window.Show(); // make sure it is ready for rendering

                // The BitmapSource that is rendered with a Visual.
                var rtb = new RenderTargetBitmap((int) window.ActualWidth, (int) window.ActualHeight, 96, 96,
                    PixelFormats.Pbgra32);
                rtb.Render(window);

                // Encoding the RenderBitmapTarget as a PNG file.
                var png = new PngBitmapEncoder();
                png.Frames.Add(BitmapFrame.Create(rtb));
                using (Stream stm = File.Create(filename))
                {
                    png.Save(stm);
                }
            }
            finally
            {
                window.Close();
            }
        }

        public static string ScreenCaptureInStaThread(string received, Func<Window> loader)
        {
            return ScreenCaptureInStaThread(received, () => ScreenCapture(loader(), received));
        }
        public static string ScreenCaptureInStaThread(string received, Func<Control> loader)
        {
            return ScreenCaptureInStaThread(received, () => ScreenCapture(loader(), received));
        }

        private static string ScreenCaptureInStaThread(string received, Action screenCapture)
        {
            Exception caught = null;
            var t = new Thread(() =>
            {
                try
                {
                    screenCapture();
                }
                catch (Exception e)
                {
                    caught = e;
                }
            });

            t.SetApartmentState(ApartmentState.STA); //Many WPF UI elements need to be created inside STA
            t.Start();
            t.Join();

            if (caught != null)
            {
                throw new Exception("Creating window failed.", caught);
            }

            return received;
        }

        public static void ScreenCapture(Control control, string filename)
        {
            // The BitmapSource that is rendered with a Visual.
            control.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            var size = control.DesiredSize;
            var width = (int) size.Width;
            var height = (int) size.Height;
            control.Arrange(new Rect(0, 0, width, height));
            var rtb = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(control);

            // Encoding the RenderBitmapTarget as a PNG file.
            var png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(rtb));
            using (Stream stm = File.Create(filename))
            {
                png.Save(stm);
            }
        }
    }
}
#endif