using System;
using Xunit;
using ConsoleApp2;

namespace FormaterTests
{
	public class UnitTest1
	{
		[Fact]
		public void Test1()
		{
			string input = "Green metal stick ";
			int length = 13;

			LineFormater f = new LineFormater();
			var res = f.FormatLine(input, length);

			Assert.Equal("Green metal ", res[0]);
			Assert.Equal("stick", res[1]);


		}

		[Fact]
		public void Test2()
		{
			string input = @"Establishment of the
church";
			int length = 7;

			LineFormater f = new LineFormater();
			var res = f.FormatLine(input, length);

			Assert.Equal("Establi", res[0]);
			Assert.Equal("shment", res[1]);
			Assert.Equal("of the", res[2]);
			Assert.Equal("church", res[3]);



		}


		[Fact]
		public void Test3()
		{
			string input = "Lorem ipsum\r\ndolor sit amet";
			int length = 9999;

			LineFormater f = new LineFormater();
			var res = f.FormatLine(input, length);

			Assert.Equal(input, res[0]);
		}

		[Fact]
		public void Test4()
		{
			string input = "1234\r\n1\r\n1234";
			int length = 3;

			LineFormater f = new LineFormater();
			var res = f.FormatLine(input, length);

			Assert.Equal("123", res[0]);
			Assert.Equal("4", res[1]);
			Assert.Equal("1", res[2]);
			Assert.Equal("123", res[3]);
			Assert.Equal("4", res[4]);
		}
	}
}
