#if !__MonoCS__

using System;
using System.Windows;
using System.Windows.Controls;

namespace ApprovalTests.Wpf
{
    public class WpfBindingsAssert
    {
        public static void BindsWithoutError(object viewModel, Func<Control> process)
        {
            BindsWithoutError(viewModel, () => new Window {Content = process()});
        }

        public static void BindsWithoutError(object viewModel, Func<Window> process)
        {
            using (AssertNoBindingErrorsTraceListener.Start())
            {
                var window = process();
                window.DataContext = viewModel;
                window.Show(); // force binding
                window.Close();
            }
        }
    }
}
#endif