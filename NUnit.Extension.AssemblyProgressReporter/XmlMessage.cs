using System;
using System.IO;
using System.Xml;

namespace NUnit.Extension.AssemblyProgressReporter {

	internal enum MessageKind {
		StartSuite,
		TestSuite
	}

	internal abstract class XmlMessage {

		public string FullName { get; }

		public abstract MessageKind Kind { get; }

		public XmlMessage( XmlReader reader ) {
			FullName = reader.GetAttribute( "fullname" );
		}

		internal static XmlMessage Parse( string xml ) {
			using( var stream = new StringReader( xml ) )
			using( var reader = XmlReader.Create( stream ) ) {

				if( !reader.Read() ) {
					return null;
				}
				if( reader.NodeType != XmlNodeType.Element ) {
					return null;
				}

				var messageType = reader.Name;
				switch( messageType ) {
					case "start-suite":
						return new StartSuiteMessage( reader );
					case "test-suite":
						return new TestSuiteMessage( reader );
					default:
						return null;
				}

			}
		}

	}

}
