using System;
using Xunit;

namespace Naticon.Tests
{
	public class MultiJoinTests : ParsingTestsBase
	{
		private DateTime _now;

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

			_now = testData.StartDate;


			if (!string.IsNullOrEmpty(testData.Time1))
			{
				Assert.IsType<string>(testData.Time1);
				var span = Parse(testData.Time1);
				Assert.True(span != null, testData.Time1);
			}

		}

		protected override DateTime Now() => _now;
	}
}