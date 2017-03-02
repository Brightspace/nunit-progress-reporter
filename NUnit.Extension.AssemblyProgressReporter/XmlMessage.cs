using System;
using System.Xml;

namespace NUnit.Extension.AssemblyProgressReporter {

	internal enum MessageKind {
		StartSuite,
		TestSuite
	}

	internal abstract class XmlMessage {

		public string Id { get; private set; }

		public string Name { get; private set; }

		public string FullName { get; private set; }

		public abstract MessageKind Kind { get; }
	
		public XmlMessage( XmlNode node ) {
			Id = node.Attributes["id"].Value;
			Name = node.Attributes["name"].Value;
			FullName = node.Attributes["fullname"].Value;
		}

		internal static XmlMessage Parse( string xml ) {
			var node = ReadXml( xml );
			switch( node.Name ) {
				case "start-suite":
					return new StartSuiteMessage( node );
				case "test-suite":
					return new TestSuiteMessage( node );
				default:
					return null;
			}
		}

		private static XmlNode ReadXml( string xml ) {
			var doc = new XmlDocument();
			doc.LoadXml( xml );
			return doc.FirstChild;
		}
	}

}
