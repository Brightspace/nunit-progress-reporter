using System;
using System.IO;
using System.Xml;

namespace NUnit.Extension.AssemblyProgressReporter {

	internal enum MessageKind {
		StartSuite,
		TestSuite
	}

	internal abstract class XmlMessage {

		public string Name { get; }

		public string Id { get; }

		public abstract MessageKind Kind { get; }

		public XmlMessage( XmlReader reader ) {
			Name = reader.GetAttribute( "name" );
			Id = reader.GetAttribute( "id" );
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
