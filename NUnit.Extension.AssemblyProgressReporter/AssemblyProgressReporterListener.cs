using System;
using System.Collections.Generic;
using NUnit.Engine;
using NUnit.Engine.Extensibility;

namespace NUnit.Extension.AssemblyProgressReporter {

	[Extension( Enabled = true, Description = "Logs assembly start and stop progress to Console.Out." )]
	public class AssemblyProgressReporterListener : ITestEventListener {

		private readonly HashSet<string> m_finishedAssemblies = new HashSet<string>();

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
			if( message.ParentId != "" ) {
				return;
			}

			Console.WriteLine( $"Starting { message.Name }" );
		}

		private void OnSuiteCompleted( TestSuiteMessage message ) {

			if( message.Type != TestSuiteType.Assembly ) {
				return;
			}

			if( !m_finishedAssemblies.Add( message.Id ) ) {
				return;
			}

			Console.WriteLine( $"Finished {message.Name}" );
		}
	}

}
