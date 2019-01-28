using System;
using Xunit;

namespace Naticon.Tests
{
	public class MultiJoinTests
	{
		/// <summary>Defines the test method CanParseData.</summary>
		/// <param name="testName">Name of the test.</param>
		/// <param name="to">To.</param>
		[Theory]
		[MemberData(nameof(TestObjectGenerator.TestData), MemberType = typeof(TestObjectGenerator))]
		public void CanParseData(string testName, in MemberDataSerializer<TestObject> to)
		{
			Assert.NotNull(testName);
			Assert.NotNull(to.Object);
			Assert.IsType<TestObject>(to.Object);

			var testData = to.Object;

			Assert.IsType<DateTime>(testData.StartDate);
			Assert.IsType<DateTime>(testData.EndDate);

	

			if (!string.IsNullOrEmpty(testData.Time1))
			{
				Assert.IsType<string>(testData.Time1);				
			}

		}
	}
}