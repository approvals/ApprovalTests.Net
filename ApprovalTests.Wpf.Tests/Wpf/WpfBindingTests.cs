using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using ApprovalTests.Reporters;
using ApprovalTests.Wpf;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalTests.Tests.Wpf
{
    [TestFixture]
    [UseReporter(typeof(DiffReporter))]
    public class WpfBindingTests
    {
        [Test]
        [RequiresThread(ApartmentState.STA)]
        public void TestFailedBindings()
        {
            var viewModel = new TestViewModel();
            var myBinding = new Binding(TestViewModel.MyPropertyPropertyName + "BOGUS")
            {
                Source = viewModel
            };
            var e = ExceptionUtilities.GetException(() => WpfBindingsAssert.BindsWithoutError(viewModel, () =>
            {
                var textBox = new TextBox();
                textBox.SetBinding(TextBox.TextProperty, myBinding);
                return textBox;
            }));
            Approvals.Verify(e.Message, s => Regex.Replace(s, @"\(HashCode=\d+\)", "(Hashcode)"));
        }
    }

    internal class TestViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public const string MyPropertyPropertyName = "MyProperty";
        private string _myProperty;

        public string MyProperty
        {
            get => _myProperty;
            set
            {
                _myProperty = value;
                RaisePropertyChanged();
            }
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}