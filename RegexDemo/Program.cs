using System;
using System.Diagnostics;
using RE;
namespace RegexDemo
{
	class Program
	{
		static void Main()
		{


			var sw = Stopwatch.StartNew();
			var a = RegexExpression.Parse(@"a.+a").ToFA<string>();
			Console.WriteLine(sw.ElapsedMilliseconds);
			a.RenderToFile("tmp.png");
			// _BuildArticleImages() // requires GraphViz
			// _RunCompiledLexCodeGen()
			/*_RunLexer();
			_RunMatch();
			_RunDom();
			// the following require GraphViz
			_RunStress();
			_RunStress2();*/
		}
	}
}
