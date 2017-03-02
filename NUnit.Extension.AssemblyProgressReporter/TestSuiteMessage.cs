using System;
using System.Xml;

namespace NUnit.Extension.AssemblyProgressReporter {

	internal enum TestSuiteType {
		Assembly,
		Namespace,
		TestFixture,
		ParameterizedTest
	}

	internal sealed class TestSuiteMessage : XmlMessage {

		public TestSuiteType? Type { get; private set; }

		public override MessageKind Kind => MessageKind.TestSuite;

		public TestSuiteMessage( XmlNode node ) : base( node ) {
			TestSuiteType tst;
			Enum.TryParse( node.Attributes["type"].Value, out tst );
			Type = tst;
		}

	}

}
