using System;
using System.Collections;
using System.Collections.Generic;

namespace Plugin.sqlCompiler.Compiler
{
	internal class AssemblyCollection : IEnumerable<String>
	{
		/// <summary>Список ссылок</summary>
		private Dictionary<String, String[]> References { get; set; }

		/// <summary>Получение пространств имён в определнной сборки</summary>
		/// <param name="assembly">Сборка в которой получить список всех пространств имён</param>
		/// <returns>Массив пространств имён</returns>
		public String[] this[String assembly]
		{
			get => this.References[assembly];
			private set => this.References[assembly] = value;
		}

		internal AssemblyCollection()
			=> this.References = new Dictionary<String, String[]>();

		/// <summary>Установить массив сборок, которые будут добавлены в исходный код</summary>
		/// <param name="assemblies">Список сборок для добавления в компиляцию</param>
		public void AddAssemblies(params String[] assemblies)
		{
			_ = assemblies ?? throw new ArgumentNullException(nameof(assemblies));

			this.References.Clear();
			foreach(String asm in assemblies)
				this.AddAssembly(asm);
		}

		/// <summary>Добавить сборку в компиляцию</summary>
		/// <param name="assembly">Сборка, которая будет добавлена в компиляцию</param>
		public void AddAssembly(String assembly)
		{
			if(String.IsNullOrEmpty(assembly))
				throw new ArgumentNullException(nameof(assembly));

			if(!this.IsAssemblyAdded(assembly))
				this.References.Add(assembly, new String[] { });
		}

		/// <summary>Проверка нахождения сборки в компиляции</summary>
		/// <param name="assembly">Ссылка на сборку, которую необходимо добавить в список сборок</param>
		/// <returns>Сборка уже добавлена в список</returns>
		public Boolean IsAssemblyAdded(String assembly)
		{
			if(String.IsNullOrEmpty(assembly))
				throw new ArgumentNullException(nameof(assembly));

			foreach(String key in this.References.Keys)
				if(key.Equals(assembly))
					return true;
			return false;
		}

		/// <summary>Проверка нахождения пространства имён в компиляции</summary>
		/// <param name="referencedNamespace">Ссылка на пространство имён, которое будет добавлено в исходный код</param>
		/// <returns>Пространство имён уже добавлено в список</returns>
		public Boolean IsNamespaceAdded(String assembly, String referencedNamespace)
		{
			if(String.IsNullOrEmpty(assembly))
				throw new ArgumentNullException(nameof(assembly));
			if(String.IsNullOrEmpty(referencedNamespace))
				throw new ArgumentNullException(nameof(referencedNamespace));

			foreach(String ns in this.References[assembly])
				if(ns.Equals(referencedNamespace))
					return true;
			return false;
		}

		/// <summary>Добавить пространство имён в компиляцию</summary>
		/// <param name="referencedNamespaces">Пространства имён, которые будут добавлены в компиляцию</param>
		public void AddNamespace(String assembly, params String[] referencedNamespaces)
		{
			if(String.IsNullOrEmpty(assembly))
				throw new ArgumentNullException(nameof(assembly));
			_ = referencedNamespaces ?? throw new ArgumentNullException(nameof(referencedNamespaces));

			this.AddAssembly(assembly);
			foreach(String ns in referencedNamespaces)
				if(!this.IsNamespaceAdded(assembly, ns))
				{
					String[] namespaces = this.References[assembly];
					Array.Resize<String>(ref namespaces, namespaces.Length + 1);
					namespaces[namespaces.Length - 1] = ns;
					this.References[assembly] = namespaces;
				}
		}
		public void RemoveAssembly(String assembly)
			=> this.References.Remove(assembly);

		public void RemoveNamespace(String assembly, String referencedNamespace)
		{
			if(String.IsNullOrEmpty(assembly))
				throw new ArgumentNullException(nameof(assembly));
			if(String.IsNullOrEmpty(referencedNamespace))
				throw new ArgumentNullException(nameof(referencedNamespace));

			if(this.IsNamespaceAdded(assembly, referencedNamespace))
			{
				List<String> namespaces = new List<String>(this[assembly]);
				namespaces.Remove(referencedNamespace);
				if(namespaces.Count == 0)
					this.RemoveAssembly(assembly);
				else
					this[assembly] = namespaces.ToArray();
			}
		}
		public IEnumerator<String> GetEnumerator()
			=> this.References.Keys.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
			=> this.References.Keys.GetEnumerator();
	}
}