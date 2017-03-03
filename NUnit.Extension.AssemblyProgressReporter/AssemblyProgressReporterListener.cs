using NUnit.Engine;
using NUnit.Engine.Extensibility;
using System;
using System.IO;

namespace NUnit.Extension.AssemblyProgressReporter {
	[Extension( Enabled = true, Description = "Logs assembly start and stop progress to Console.Out." )]
	public class AssemblyProgressReporterListener : ITestEventListener {
		void ITestEventListener.OnTestEvent( string report ) {
			if( string.IsNullOrWhiteSpace( report ) ) {
				return;
			}

			var message = XmlMessage.Parse( report );

			if( message == null ) {
				return;
			}

			switch( message.Kind ) {

				case MessageKind.StartSuite:
					OnSuiteStarted( message as StartSuiteMessage );
					break;
				case MessageKind.TestSuite:
					OnSuiteCompleted( message as TestSuiteMessage );
					break;

				default:
					break;
			}
		}

		private void OnSuiteStarted( StartSuiteMessage message ) {
			var suite = message.FullName;

			if( !Path.IsPathRooted( suite ) ) {
				return;
			}

			var assembly = Path.GetFileName( suite );
			Console.WriteLine( $"Starting {assembly}" );
		}

		private void OnSuiteCompleted( TestSuiteMessage message ) {
			if( message.Type != TestSuiteType.Assembly ) {
				return;
			}

			var suite = message.FullName;
			if( File.Exists( suite ) ) {
				var assembly = Path.GetFileName( suite );
				Console.WriteLine( $"Finished {assembly}" );
			}
		}
	}

}
