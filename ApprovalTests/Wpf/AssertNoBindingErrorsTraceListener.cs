using System;
using System.Diagnostics;
using System.Text;
using ApprovalTests.WindowsRegistry;

namespace ApprovalTests.Wpf
{
	public class AssertNoBindingErrorsTraceListener : TraceListener
	{
		private readonly StringBuilder messageBuilder = new StringBuilder();
        public const string RegEditText = @"
Just Save to file: wpf.reg and run
------------------------------------------------------
Windows Registry Editor Version 5.00

[HKEY_CURRENT_USER\Software\Microsoft\Tracing\WPF]
""ManagedTracing""=dword:00000001";


		private AssertNoBindingErrorsTraceListener(SourceLevels level)
		{

			WindowsRegistryAssert.HasDword(@"Software\Microsoft\Tracing\WPF", "ManagedTracing",1,"You need to add this key to your registry for Wpf report Binding Errors. \n" + RegEditText);
			PresentationTraceSources.DataBindingSource.Listeners.Add(this);
			PresentationTraceSources.DataBindingSource.Switch.Level = level;
		}

	  

	    public static IDisposable Start(SourceLevels level = SourceLevels.Warning)
		{
			
			var listener = new AssertNoBindingErrorsTraceListener(level);
			return new DisposableToken(listener);
		}

		private class DisposableToken : IDisposable
		{
			private readonly AssertNoBindingErrorsTraceListener listener;

			public DisposableToken(AssertNoBindingErrorsTraceListener listener)
			{
				this.listener = listener;
			}

			public void Dispose()
			{
				string message = this.listener.messageBuilder.ToString();
				this.listener.Flush();
				this.listener.Close();
				PresentationTraceSources.DataBindingSource.Listeners.Remove(this.listener);
				if (!String.IsNullOrEmpty(message))
				{
					message = message.Replace(";", "\r\n\t").Replace(". ", ".\r\n\t");
					throw new Exception(message);
				}
			}
		}

		public override void Write(string message)
		{
			messageBuilder.Append(message);
		}

		public override void WriteLine(string message)
		{
			messageBuilder.AppendLine(message);
		}
	}
}