using System.Xml;

namespace NUnit.Extension.AssemblyProgressReporter {

	internal sealed class StartSuiteMessage : XmlMessage {

		public override MessageKind Kind => MessageKind.StartSuite;


		public StartSuiteMessage( XmlNode node )
			: base( node ) { }

	}

}
