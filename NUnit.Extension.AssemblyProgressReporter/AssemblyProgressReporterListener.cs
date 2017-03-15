using System;
using System.IO;
using NUnit.Engine;
using NUnit.Engine.Extensibility;

namespace NUnit.Extension.AssemblyProgressReporter {

	[Extension( Enabled = true, Description = "Logs assembly start and stop progress to Console.Out." )]
	public class AssemblyProgressReporterListener : ITestEventListener {

		void ITestEventListener.OnTestEvent( string report ) {

			try {
				XmlMessage message = XmlMessage.Parse( report );
				if( message == null ) {
					return;
				}

				switch( message.Kind ) {

					case MessageKind.StartSuite:
						OnSuiteStarted( (StartSuiteMessage)message );
						break;

					case MessageKind.TestSuite:
						OnSuiteCompleted( (TestSuiteMessage)message );
						break;

					default:
						break;
				}

			} catch( Exception err ) {
				Console.Error.WriteLine( "Unhandled exception in assembly progress reporter extension: {0}", err );
			}
		}

		private void OnSuiteStarted( StartSuiteMessage message ) {

			string assembly;
			if( TryGetAssembly( message.FullName, out assembly ) ) {
				Console.WriteLine( $"Starting {assembly}" );
			}
		}

		private void OnSuiteCompleted( TestSuiteMessage message ) {

			if( message.Type != TestSuiteType.Assembly ) {
				return;
			}

			string assembly;
			if( TryGetAssembly( message.FullName, out assembly ) ) {
				Console.WriteLine( $"Finished {assembly}" );
			}
		}

		private static readonly char[] InvalidPathChars = Path.GetInvalidPathChars();

		internal static bool TryGetAssembly(
				string fullName,
				out string assembly
			) {

			if( fullName.IndexOfAny( InvalidPathChars ) >= 0 ) {
				assembly = null;
				return false;
			}

			if( !Path.IsPathRooted( fullName ) ) {
				assembly = null;
				return false;
			}

			assembly = Path.GetFileName( fullName );
			return true;
		}
	}

}
