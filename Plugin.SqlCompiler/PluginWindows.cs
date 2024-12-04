using System;
using SAL.Flatbed;
using SAL.Windows;
using System.Collections.Generic;
using System.Diagnostics;

namespace Plugin.sqlCompiler
{
	public class PluginWindows : IPlugin
	{
		private TraceSource _trace;
		private Dictionary<String, DockState> _documentTypes;

		internal TraceSource Trace { get => this._trace ?? (this._trace = PluginWindows.CreateTraceSource<PluginWindows>()); }

		private readonly IHostWindows _hostWindows;

		private IMenuItem CompilerMenu { get; set; }

		private Dictionary<String, DockState> DocumentTypes
		{
			get
			{
				if(this._documentTypes == null)
					this._documentTypes = new Dictionary<String, DockState>()
					{
						{typeof(DocumentSqlCompiler).ToString(),DockState.Document },
					};
				return this._documentTypes;
			}
		}

		public PluginWindows(IHostWindows hostWindows)
			=> this._hostWindows = hostWindows ?? throw new ArgumentNullException(nameof(hostWindows));

		public IWindow GetPluginControl(String typeName, Object args)
			=> this.CreateWindow(typeName, false, args);

		Boolean IPlugin.OnConnection(ConnectMode mode)
		{
			IMenuItem menuTools = this._hostWindows.MainMenu.FindMenuItem("Tools");
			if(menuTools == null)
			{
				this.Trace.TraceEvent(TraceEventType.Error, 10, "Menu item 'Tools' not found");
				return false;
			}

			IMenuItem menuSql = menuTools.FindMenuItem("Compilers");
			if(menuSql == null)
			{
				menuSql = menuTools.Create("Compilers");
				menuSql.Name = "Tools.Compilers";
				menuTools.Items.Add(menuSql);
			}

			this.CompilerMenu = menuSql.Create("&SQL Compiler");
			this.CompilerMenu.Name = "Tools.Compilers.SqlCompiler";
			this.CompilerMenu.Click += (sender, e) => { this.CreateWindow(typeof(DocumentSqlCompiler).ToString(), false); };
			menuSql.Items.Add(this.CompilerMenu);
			return true;
		}

		Boolean IPlugin.OnDisconnection(DisconnectMode mode)
		{
			if(this.CompilerMenu != null)
				this._hostWindows.MainMenu.Items.Remove(this.CompilerMenu);
			return true;
		}

		private IWindow CreateWindow(String typeName, Boolean searchForOpened, Object args = null)
			=> this.DocumentTypes.TryGetValue(typeName, out DockState state)
				? this._hostWindows.Windows.CreateWindow(this, typeName, searchForOpened, state, args)
				: null;

		private static TraceSource CreateTraceSource<T>(String name = null) where T : IPlugin
		{
			TraceSource result = new TraceSource(typeof(T).Assembly.GetName().Name + name);
			result.Switch.Level = SourceLevels.All;
			result.Listeners.Remove("Default");
			result.Listeners.AddRange(System.Diagnostics.Trace.Listeners);
			return result;
		}
	}
}