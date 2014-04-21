using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;
using Roslyn.Scripting.CSharp;

namespace Brainfuck {
	public static class BrainfuckExtension {
		public static bool Brainfuck(this string src, out string code, out string result) {
			var sb = new StringBuilder();
			sb.AppendLine(@"var ptr = 0;");
			sb.AppendLine(@"var buffer = new char[30000];");
			sb.AppendLine(@"var sb = new StringBuilder();");
			foreach (var c in src) {
				switch (c) {
					case '>':
						sb.AppendLine(@"++ptr;");
						break;
					case '<':
						sb.AppendLine(@"--ptr;");
						break;
					case '+':
						sb.AppendLine(@"buffer[ptr] = (char)(buffer[ptr] + 1);");
						break;
					case '-':
						sb.AppendLine(@"buffer[ptr] = (char)(buffer[ptr] - 1);");
						break;
					case '.':
						sb.AppendLine(@"sb.Append(buffer[ptr]);");
						break;
					case ',':
						//not supported
						//sb.AppendLine(@"buffer[ptr] = (char)Console.Read();");
						break;
					case '[':
						sb.AppendLine(@"while(buffer[ptr] != 0) {");
						break;
					case ']':
						sb.AppendLine(@"}");
						break;
					default:
						break;
				}
			}
			sb.AppendLine(@"sb.ToString();");

			code = sb.ToString();
			//run 
			var engine = new ScriptEngine();
			engine.ImportNamespace("System");
			engine.ImportNamespace("System.Text");
			var session = engine.CreateSession();
			try {
				result = session.Execute<string>(code);
			} catch(Exception ex) {
				result = ex.ToString();
				return false;
			}

			return true;
		}
	}
}
