using NUnit.Framework;

namespace NUnit.Extension.AssemblyProgressReporter.Tests {

	[TestFixture]
	public class AssemblyProgressReporterListenerTests {

		[Test]
		public void TryGetAssembly_WhenAssemblyPath() {

			string assembly;
			Assert.IsTrue( AssemblyProgressReporterListener.TryGetAssembly( @"C:\test\assembly.dll", out assembly ) );
			Assert.AreEqual( "assembly.dll", assembly );
		}

		[Test]
		public void TryGetAssembly_WhenTestFixture() {

			string assembly;
			Assert.IsFalse( AssemblyProgressReporterListener.TryGetAssembly( @"Assembly.Namespace.TestFixture", out assembly ) );
			Assert.IsNull( assembly );
		}

		[Test]
		public void TryGetAssembly_WhenInvalidPathChars() {

			string assembly;
			Assert.IsFalse( AssemblyProgressReporterListener.TryGetAssembly( @"C:\weird-stuff\<|>TestCase", out assembly ) );
			Assert.IsNull( assembly );
		}
	}
}
