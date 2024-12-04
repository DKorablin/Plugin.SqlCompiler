using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using System.Reflection;
using System.IO;
using AlphaOmega.Reflection;

namespace Plugin.sqlCompiler.Compiler
{
	internal class DynamicCompiler
	{
		private String _className;

		/// <summary>Список ссылок и пространств имён, которые будут добавлены в исходный код</summary>
		public AssemblyCollection References { get; private set; }

		/// <summary>Наименование плагина, который добавляется в исходный код</summary>
		public String ClassName
		{
			get => this._className;
			internal set
			{
				if(String.IsNullOrEmpty(value))
					throw new ArgumentNullException("DynamicCompiler.ClassName");
				else
					this._className = value.Trim().Replace(' ', '_').Replace('.', '_');
			}
		}

		/// <summary>Исходный код для компиляции</summary>
		public String SourceCode { get; private set; }

		/// <summary>Скомпилированная сборка</summary>
		public Assembly CompiledAssembly { get; set; }

		/// <summary>Путь к файлу куда компилировать сборку</summary>
		public String CompiledAssemblyFilePath { get; set; }

		/// <summary>Создание экземпляра класса</summary>
		public DynamicCompiler()
			: this(String.Empty)
		{ }

		/// <summary>Создание экземпляра класса с указанием стартового префикса класса</summary>
		/// <param name="pluginName">Наименование плагина для которого компилируется код</param>
		public DynamicCompiler(String pluginName)
		{
			this.ClassName = String.IsNullOrEmpty(pluginName) ? "Undefined" : pluginName;
			this.References = new AssemblyCollection();
		}

		/// <summary>Получить список поддерживаемых компиляторов</summary>
		/// <returns>Список поддерживаемых компиляторов</returns>
		public static CompilerInfo[] GetSupportedCompilers()
			=> CodeDomProvider.GetAllCompilerInfo();

		/// <summary>Получить поддерживаемый язык из информации о компиляторе</summary>
		/// <param name="compiler">Информация о компиляторе</param>
		/// <returns>Наименование языка из информации о компиляторе</returns>
		public String GetSupportedLanguage(CompilerInfo info)
		{
			String[] names = info.GetLanguages();
			Int32 length = names.Length;
			return length > 0 ? names[length - 1] : String.Empty;
		}

		/// <summary>Получить список поддерживаемых языков</summary>
		/// <returns>Массив поддерживаемых языков компилятором</returns>
		public String[] GetSupportedLanguages()
		{
			CompilerInfo[] languages = DynamicCompiler.GetSupportedCompilers();
			List<String> result = new List<String>();
			foreach(var language in languages)
			{
				String[] names = language.GetLanguages();
				if(names.Length > 0)
					result.Add(names[names.Length - 1]);
			}
			return result.ToArray();
		}

		public Assembly CompileAssembly(String sourceCode)
			=> this.CompileAssembly(CodeDomProvider.GetCompilerInfo("csharp"), sourceCode);

		/// <summary>Скомпилировать сборку из класса, полностью написанного пользователем</summary>
		/// <param name="language">Язык на котором написан код</param>
		/// <param name="fullCode">Полный исходный код для генерации сборки</param>
		/// <returns>Сгенерированная сборка</returns>
		public Assembly CompileAssembly(CompilerInfo info, String sourceCode)
		{
			var compiler = info.CreateProvider();
			//var compiler = CodeDomProvider.CreateProvider(language);
			CompilerParameters parameters = info.CreateDefaultCompilerParameters();

			foreach(String assembly in this.References)
				if(File.Exists(assembly))
					parameters.ReferencedAssemblies.Add(assembly);
				else
				{
					String path = AssemblyCache.QueryAssemblyInfo(assembly);
					parameters.ReferencedAssemblies.Add(path);
				}

			parameters.GenerateInMemory = true;
			parameters.CompilerOptions += "/optimize";
			parameters.OutputAssembly = this.CompiledAssemblyFilePath;
			/*if(compiler.Supports(GeneratorSupport.EntryPointMethod))
				parameters.MainClass = String.Format("{0}.{1}", Constant.ClassNamespace, this.ClassName);*/

			CompilerResults result = compiler.CompileAssemblyFromSource(parameters, sourceCode);
			if(result.Errors.HasErrors)
				throw new CompilerException(sourceCode, result);

			return result.CompiledAssembly;
		}
	}
}