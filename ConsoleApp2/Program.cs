using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;

namespace ConsoleApp2
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length < 2)
			{
				Console.WriteLine("Formatter!");
				Console.WriteLine("usage:./ConsoleApp2 <inputFile> <outputFile> <maxLength>  ");
				Console.WriteLine();
				return;
			}
			
			
			var inputFile = args[0];
			var outputFile = args[1];
			int maxLength = int.Parse(args[2]);

#if DEBUG
			Console.WriteLine("InputFile: {0} ", inputFile);
			Console.WriteLine("outputFile: {0}", outputFile);
			Console.WriteLine("MaxLenght: {0}", maxLength);
#endif

			var p = new EagerProcess();

			try
			{
				p.ProcessFile(inputFile, outputFile, maxLength);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine(ex.Message);
				Console.Error.WriteLine("-- Process terminated --");

			}
		}

	
	}

	interface IProcess
	{
		void ProcessFile(string inputFile, string outputFile, int maxLength);
	}

	public class EagerProcess :IProcess
	{
		public void ProcessFile(string inputFile, string outputFile, int maxLength)
		{
			if (maxLength == 0)
				throw new ArgumentException("max length is zero");
			if (maxLength < 1)
				throw new ArgumentException("max length is negative.");

			if(!File.Exists(inputFile))
				throw new ArgumentException(string.Format("Input file {0} does not exist.",inputFile));

			ILineFormat f = new LineFormater();
			var input = File.ReadAllLines(inputFile);

			var res = new List<string>();

#if DEBUG
			Console.WriteLine("Number of lines {0}", input.Length);
#endif

			foreach (var line in input)
			{
				foreach (string formatedLine in f.FormatLine(line, maxLength))
				{
					res.Add(formatedLine);
				}
			}
			var result = res.ToArray();
			File.WriteAllLines(outputFile, result);
		}
	}

	public class LineFormater :ILineFormat
	{
		public string[] FormatLine(string line, int maxLength)
		{
			if (line.Length < maxLength)
				return new string[] { line };
			List<string> res = new List<string>();

			var first = line.Substring(0, maxLength).Trim();
			var rest = line.Substring(maxLength);
			res.Add(first);
			if (rest.Length < maxLength)
				res.Add(rest);
			else
			{
				foreach (var substring in FormatLine(rest, maxLength))
				{
					res.Add(substring.Trim());

				}
			}
			return res.ToArray();
		}
	}

	interface ILineFormat
	{
		string[] FormatLine(string line, int maxLength);
	}

}
