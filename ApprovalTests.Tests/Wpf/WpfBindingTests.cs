#if !__MonoCS__
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;
using ApprovalTests.Reporters;
using ApprovalTests.Wpf;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalTests.Tests.Wpf
{
	[TestFixture]
	public class WpfBindingTests
	{
		[Test]
		[STAThread]
		public void TestFailedBindings()
		{
			var viewModel = new TestViewModel();
			Binding myBinding = new Binding(TestViewModel.MyPropertyPropertyName + "BOGUS");
			myBinding.Source = viewModel;
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
			get { return this._myProperty; }
			set
			{
				this._myProperty = value;
				RaisePropertyChanged();
			}
		}

		private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
#endif