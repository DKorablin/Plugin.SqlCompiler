using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Plugin.sqlCompiler.Bll;
using Plugin.sqlCompiler.Compiler;
using Plugin.sqlCompiler.UI;
using SAL.Flatbed;
using SAL.Windows;
using System.Diagnostics;

namespace Plugin.sqlCompiler
{
	internal partial class DocumentSqlCompiler : UserControl, IPluginSettings<DocumentSqlCompilerSettings>
	{
		private enum TreeImageIndex
		{
			Method = 0,
			Class = 1,
			Field = 2,
		}

		private DocumentSqlCompilerSettings _settings;
		private ProjectBll _project;
		private const String Caption = "XML to SQL Compiler";
		private const String ProjectFileFilter = "Sql/Xml Project|*.sqlxproj|All Files|*.*";

		#region Properties
		Object IPluginSettings.Settings => this.Settings;

		public DocumentSqlCompilerSettings Settings => this._settings == null ? this._settings = new DocumentSqlCompilerSettings() : this._settings;

		private PluginWindows Plugin => (PluginWindows)this.Window.Plugin;

		private IWindow Window => (IWindow)base.Parent;

		private Boolean Initialized { get; set; }

		private ProjectBll Project
		{
			get
			{
				if(this._project == null)
				{
					if(this.Settings.ProjectFilePath != null)
						this.Project = new ProjectBll(this.Settings.ProjectFilePath);
				}
				return this._project;
			}
			set
			{
				Boolean loaded = value != null;
				tsbnCompile.Enabled = loaded;
				tsmiProjectSave.Enabled = loaded;
				this._project = value;
				if(loaded)
				{
					this.Settings.ProjectFilePath = this._project.ConfigFileName;
					this.Window.Caption = String.Join(" - ", new String[] { value.ProjectName, DocumentSqlCompiler.Caption, });
					this.ReloadMethods();
				} else
				{
					this.Settings.ProjectFilePath = null;
					this.Window.Caption = DocumentSqlCompiler.Caption;
				}
			}
		}

		/// <summary>Текущий проект открыт</summary>
		private Boolean IsProjectOpened => this.Project != null;

		private ProjectDataSet.MethodRow SelectedMethod
			=> tvMethods.SelectedNode == null ? null : tvMethods.SelectedNode.Tag as ProjectDataSet.MethodRow;

		private ProjectDataSet.ClassRow SelectedClass
			=> tvMethods.SelectedNode == null ? null : tvMethods.SelectedNode.Tag as ProjectDataSet.ClassRow;

		private ProjectDataSet.ClassParameterRow SelectedParameter
			=> tvMethods.SelectedNode == null ? null : tvMethods.SelectedNode.Tag as ProjectDataSet.ClassParameterRow;

		#endregion Properties

		public DocumentSqlCompiler()
			=> InitializeComponent();

		protected override void OnCreateControl()
		{
			this.Window.Caption = Caption;
			//this.Window.SetTabPicture(Resources.IDE);
			this.Window.SetDockAreas(DockAreas.Float | DockAreas.Document);

			base.OnCreateControl();
			if(this.Settings.ProjectFilePath != null && File.Exists(this.Settings.ProjectFilePath))
				this.Project = new ProjectBll(this.Settings.ProjectFilePath);
		}

		private void tsbnProject_ButtonClick(Object sender, EventArgs e)
		{
			if(this.Project == null)
				this.tsbnProject_DropDownItemClicked(sender, new ToolStripItemClickedEventArgs(tsmiProjectOpen));
			else
				this.tsbnProject_DropDownItemClicked(sender, new ToolStripItemClickedEventArgs(tsmiProjectSave));
		}

		private void tsbnProject_DropDownItemClicked(Object sender, ToolStripItemClickedEventArgs e)
		{
			tsbnProject.DropDown.Close();
			if(e.ClickedItem == tsmiProjectOpen)
			{
				using(OpenFileDialog dlg = new OpenFileDialog() { Filter = ProjectFileFilter, RestoreDirectory = true, CheckFileExists = true, })
					if(dlg.ShowDialog(this) == DialogResult.OK)
						this.Project = new ProjectBll(dlg.FileName);
			} else if(e.ClickedItem == tsmiProjectNew)
			{
				using(SaveFileDialog dlg = new SaveFileDialog() { Filter = ProjectFileFilter, DefaultExt = ".sqlxproj", RestoreDirectory = true, CheckPathExists = true, })
					if(dlg.ShowDialog(this) == DialogResult.OK)
						this.Project = new ProjectBll(dlg.FileName);
			} else if(e.ClickedItem == tsmiProjectSave)
				this.Project.Save();
			else
				throw new NotImplementedException(e.ClickedItem.ToString());
		}

		private void tvMethods_BeforeExpand(Object sender, TreeViewCancelEventArgs e)
		{
			if(e.Action == TreeViewAction.Expand
				&& e.Node.Nodes.Count == 1
				&& String.IsNullOrEmpty(e.Node.Nodes[0].Text)
				&& e.Node.Nodes[0].ImageIndex != (Int32)TreeImageIndex.Field)
			{
				e.Node.Nodes[0].Remove();
				ProjectDataSet.MethodRow methodRow = (ProjectDataSet.MethodRow)e.Node.Tag;
				ProjectDataSet.ClassRow[] classRow = this.Project.GetMethodClass(methodRow.MethodID);
				switch(classRow.Length)
				{
				case 0:
					return;
				case 1:
					TreeNode classNode = e.Node.Nodes.Add(classRow[0].Name);
					classNode.Tag = classRow[0];
					classNode.ImageIndex = classNode.SelectedImageIndex = (Int32)TreeImageIndex.Class;

					foreach(ProjectDataSet.ClassParameterRow row in this.Project.GetClassParameters(classRow[0].ClassID))
					{
						TreeNode node = new TreeNode(row.DisplayName)
						{
							Tag = row
						};
						node.ImageIndex = node.SelectedImageIndex = (Int32)TreeImageIndex.Field;

						classNode.Nodes.Add(node);
					}
					break;
				default:
					throw new NotSupportedException();
				}
			}
		}

		private void cmsMethod_Opening(Object sender, CancelEventArgs e)
		{
			e.Cancel = this.Project == null;
			tsmiMethodAdd.Visible = tvMethods.SelectedNode == null || this.SelectedMethod != null;
			tsmiMethodRemove.Visible = this.SelectedMethod != null;
			tsmiMethodRename.Visible = this.SelectedMethod != null;
			tsmiParameterAdd.Visible = this.SelectedClass != null;
			tsmiParameterRemove.Visible = this.SelectedParameter != null;
			tsmiParameterChange.Visible = this.SelectedParameter != null;
		}

		private void tsmiMethod_DropDownItemClicked(Object sender, ToolStripItemClickedEventArgs e)
		{
			if(e.ClickedItem == tsmiMethodAdd)
			{
				TreeNode node = new TreeNode();
				tvMethods.Nodes.Add(node);
				node.BeginEdit();
			} else if(e.ClickedItem == tsmiMethodRemove)
			{
				ProjectDataSet.MethodRow methodRow = this.SelectedMethod;
				this.Project.RemoveMethod(methodRow);
				tvMethods.SelectedNode.Remove();
			} else if(e.ClickedItem == tsmiMethodRename)
				tvMethods.SelectedNode.BeginEdit();
			else
				throw new NotSupportedException();
		}

		private void tsmiParameter_DropDownItemClicked(Object sender, ToolStripItemClickedEventArgs e)
		{
			if(e.ClickedItem == tsmiParameterAdd)
			{
				TreeNode node = new TreeNode() { ImageIndex = (Int32)TreeImageIndex.Field, SelectedImageIndex = (Int32)TreeImageIndex.Field, };
				TreeNode selectedNode = tvMethods.SelectedNode;
				selectedNode = this.SelectedParameter == null ? selectedNode : selectedNode.Parent;
				selectedNode.Nodes.Add(node);
				selectedNode.Expand();
				node.BeginEdit();
			} else if(e.ClickedItem == tsmiParameterRemove)
			{
				ProjectDataSet.ClassParameterRow parameterRow = this.SelectedParameter;
				this.Project.RemoveClassParameter(parameterRow);
				tvMethods.SelectedNode.Remove();
			} else if(e.ClickedItem == tsmiParameterChange)
				tvMethods.SelectedNode.BeginEdit();
			else
				throw new NotSupportedException();
		}

		private void tvMethods_AfterLabelEdit(Object sender, NodeLabelEditEventArgs e)
		{
			if(e.Label == null)//Отмена редактирования
			{
				if(e.Node.Tag == null)
					e.Node.Remove();
				return;
			}

			if(e.Node.Parent == null)//Method
			{
				foreach(var method in this.Project.GetMethodRows())//Проверка на дубль
					if(method.Name.Equals(e.Label, StringComparison.OrdinalIgnoreCase))
					{
						e.CancelEdit = true;
						if(e.Node.Tag == null)
							e.Node.Remove();
						this.Plugin.Trace.TraceEvent(TraceEventType.Warning, 1, "Method with the name '{0}' already exists.", method.Name);
						return;
					}
				foreach(Char ch in e.Label)//Проверка на ошибочные символы
					if(!Char.IsLetterOrDigit(ch))
					{
						e.CancelEdit = true;
						if(e.Node.Tag == null)
							e.Node.Remove();
						this.Plugin.Trace.TraceEvent(TraceEventType.Warning, 1, "You canspecify only letter or digit symbols in method name.");
						return;
					}

				Int32? methodId = e.Node.Tag == null ? (Int32?)null : ((ProjectDataSet.MethodRow)e.Node.Tag).MethodID;
				ProjectDataSet.MethodRow methodRow = this.Project.ModifyMethod(methodId, e.Label, null);
				e.Node.Tag = methodRow;
				e.Node.Text = methodRow.Name;
				if(methodId == null)
				{
					ProjectDataSet.ClassRow classRow = this.Project.ModifyMethodClass(null, methodRow.MethodID, "/datapacket");
					TreeNode classNode = new TreeNode(classRow.Name);
					classNode.ImageIndex = classNode.SelectedImageIndex = (Int32)TreeImageIndex.Class;
					classNode.Tag = classRow;
					e.Node.Nodes.Add(classNode);
					e.Node.Expand();
				}
			} else if(e.Node.Parent.Tag is ProjectDataSet.MethodRow)//Class
			{
				ProjectDataSet.ClassRow classRow = (ProjectDataSet.ClassRow)e.Node.Tag;
				this.Project.ModifyMethodClass(classRow.ClassID, classRow.MethodID, e.Label);
			} else if(e.Node.Parent.Tag is ProjectDataSet.ClassRow)//Parameter
			{
				String name, defaultValue;
				Boolean canBeNull;
				DbType type;
				UInt16? size;
				Boolean result = Constant.SplitDisplayName(e.Label, out name, out type, out size, out canBeNull, out defaultValue);
				ProjectDataSet.ClassRow classRow = (ProjectDataSet.ClassRow)e.Node.Parent.Tag;
				ProjectDataSet.ClassParameterRow parameterRow = (ProjectDataSet.ClassParameterRow)e.Node.Tag;

				using(ParameterDlg dlg = new ParameterDlg(this.Project, classRow, parameterRow))
				{
					dlg.ParameterName = name;
					if(result)
					{
						dlg.Type = type;
						dlg.ParameterSize = size;
						dlg.Default = defaultValue;
						dlg.IsNullable = canBeNull;
					}

					if(dlg.ShowDialog(this) == DialogResult.OK)
					{
						parameterRow = this.Project.ModifyClassParameterRow(parameterRow == null ? null : parameterRow.Name, classRow.ClassID, dlg.ParameterName, dlg.Type, dlg.IsNullable, dlg.Default, dlg.ParameterSize);
						e.Node.Text = parameterRow.DisplayName;
						e.Node.Tag = parameterRow;
					} else
					{
						e.CancelEdit = true;
						if(e.Node.Tag == null)
							e.Node.Remove();
					}
				}
			} else
				throw new NotImplementedException();
		}

		private void tvMethods_KeyDown(Object sender, KeyEventArgs e)
		{
			switch(e.KeyData)
			{
			case Keys.F2:
				if(tvMethods.SelectedNode != null)
				{
					tvMethods.SelectedNode.BeginEdit();
					e.Handled = true;
				}
				break;
			case Keys.Delete:
			case Keys.Back:
				if(tvMethods.SelectedNode == null)
					e.Handled = false;
				else if(this.SelectedMethod != null)
					this.tsmiMethod_DropDownItemClicked(sender, new ToolStripItemClickedEventArgs(tsmiMethodRemove));
				else if(this.SelectedClass != null)
					e.Handled = false;
				else if(this.SelectedParameter != null)
					this.tsmiParameter_DropDownItemClicked(sender, new ToolStripItemClickedEventArgs(tsmiParameterRemove));
				else
					throw new NotSupportedException();
				e.Handled = true;
				break;
			case Keys.Insert:
			case Keys.I | Keys.Control:
				if(tvMethods.SelectedNode == null || this.SelectedMethod != null)
					this.tsmiMethod_DropDownItemClicked(sender, new ToolStripItemClickedEventArgs(tsmiMethodAdd));
				else if(this.SelectedClass != null)
					this.tsmiParameter_DropDownItemClicked(sender, new ToolStripItemClickedEventArgs(tsmiParameterAdd));
				else
					throw new NotSupportedException();
				e.Handled = true;
				break;
			}
		}

		private void ReloadMethods()
		{
			tvMethods.Nodes.Clear();
			foreach(ProjectDataSet.MethodRow row in this.Project.GetMethodRows())
			{
				TreeNode node = new TreeNode();
				tvMethods.Nodes.Add(node);
				this.ReloadMethod(node, row);
			}
		}

		private void ReloadMethod(Int32 methodId)
		{
			foreach(TreeNode node in tvMethods.Nodes)
				if(((ProjectDataSet.MethodRow)node.Tag).MethodID == methodId)
				{
					this.ReloadMethod(node, null);
					return;
				}
			ProjectDataSet.MethodRow row = this.Project.GetMethodRow(methodId);
			if(row != null)
			{
				TreeNode node = new TreeNode();
				tvMethods.Nodes.Add(node);
				this.ReloadMethod(node, row);
			}
		}

		private void ReloadMethod(TreeNode node)
			=> this.ReloadMethod(node, (ProjectDataSet.MethodRow)node.Tag);

		private void ReloadMethod(TreeNode node, ProjectDataSet.MethodRow row)
		{
			if(row == null)
			{//Если ряд равен null, то обновить всю информацию из хранилища
				row = (ProjectDataSet.MethodRow)node.Tag;
				row = this.Project.GetMethodRow(row.MethodID);
			}
			String parameters = String.Empty;
			/*foreach(ProjectDataSet.MethodParametersRow parameterRow in this.Project.GetMethodParameters(row.MethodID))
				parameters += String.Format("{0} {1},", parameterRow.Type, parameterRow.Name);*/

			//node.Text = String.Format("{0}({1}) : {2}", row.Name, parameters.TrimEnd(','), "IEnumerable");
			node.ImageIndex = node.SelectedImageIndex = (Int32)TreeImageIndex.Method;
			node.Text = row.Name;
			node.Tag = row;
			node.Nodes.Clear();
			node.Nodes.Add(String.Empty);
			node.Nodes[0].Collapse();
		}

		private void tsbnCompile_ButtonClick(Object sender, EventArgs e)
			=> this.tsbnCompile_DropDownItemClicked(sender, new ToolStripItemClickedEventArgs(tsmiCompile));

		private void tsbnCompile_DropDownItemClicked(Object sender, ToolStripItemClickedEventArgs e)
		{
			tsbnCompile.DropDown.Close();
			lvErrors.Items.Clear();
			splitMain.Panel2Collapsed = true;

			StringBuilder code = new StringBuilder(Constant.Template.ClassTemplate);
			StringBuilder structItems = new StringBuilder();//Item
			StringBuilder parserItems = new StringBuilder();//Parser
			StringBuilder decoderItems = new StringBuilder();//Decoder
			foreach(ProjectDataSet.MethodRow methodRow in this.Project.GetMethodRows())
			{
				ProjectDataSet.ClassRow classRow = this.Project.GetMethodClass(methodRow.MethodID)[0];
				StringBuilder structItem = new StringBuilder();//Item
				StringBuilder structAssignOperator = new StringBuilder();//Assign
				StringBuilder decoderParams = new StringBuilder();//Decoder params
				StringBuilder decoderItem = new StringBuilder();//Decoder items
				StringBuilder defString = new StringBuilder();//TableDefinition

				foreach(ProjectDataSet.ClassParameterRow parameterRow in this.Project.GetClassParameters(classRow.ClassID))
				{
					//Создание определятеля
					defString.AppendFormat("{0},", parameterRow.DefinitionMemberName);
					//Создание декодера
					decoderParams.AppendFormat(", {0}", parameterRow.OutMemberName);
					decoderItem.AppendLine(parameterRow.AssignmentFillOperation);

					//Создание структуры
					structItem.AppendLine(parameterRow.FieldCode);
					structAssignOperator.AppendLine(parameterRow.AssigmentGetOperator);
				}

				parserItems.AppendFormat(Constant.Template.ParserTemplateArgs4,
					defString.ToString().TrimEnd(','),
					methodRow.Name,
					classRow.Name,
					structAssignOperator.ToString());
				decoderItems.AppendFormat(Constant.Template.DecoderTemplateArgs3,
					methodRow.Name,
					decoderParams,
					decoderItem);
				structItems.AppendFormat(Constant.Template.ItemTemplateArgs2, methodRow.Name, structItem.ToString());
			}
			code.AppendLine(parserItems.ToString());
			code.AppendLine(decoderItems.ToString());
			code.AppendLine(structItems.ToString());
			code.AppendLine("}");
			code.AppendLine(Constant.Template.UtilsTemplate);

			if(e.ClickedItem == tsmiCompile)
			{
				using(SaveFileDialog dlg = new SaveFileDialog() { DefaultExt = ".dll", FileName = this.Project.ProjectName + ".dll", Filter = "All files (*.*)|*.*", OverwritePrompt = true, RestoreDirectory = true, CheckPathExists = true, })
					if(dlg.ShowDialog(this) == DialogResult.OK)
						try
						{
							DynamicCompiler compiler = new DynamicCompiler();
							compiler.References.AddAssembly("System");
							compiler.References.AddAssembly("System.Data");
							compiler.References.AddAssembly("System.XML");
							compiler.CompiledAssemblyFilePath = dlg.FileName;
							compiler.CompileAssembly(code.ToString());
						} catch(CompilerException exc)
						{
							splitMain.Panel2Collapsed = false;
							for(Int32 loop = 0; loop < exc.Result.Errors.Count; loop++)
							{
								var error = exc.Result.Errors[loop];
								ListViewItem item = lvErrors.Items.Add(error.Line.ToString("n0"));
								if(error.IsWarning)
									item.ImageIndex = item.StateImageIndex = 0;
								else
									item.ImageIndex = item.StateImageIndex = 1;
								item.SubItems.Add(error.ErrorNumber);
								item.SubItems.Add(error.ErrorText);
							}
							lvErrors.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
						}
			} else if(e.ClickedItem == tsmiCompileSave)
			{
				using(SaveFileDialog dlg = new SaveFileDialog() { DefaultExt = ".cs", FileName = this.Project.ProjectName + ".cs", Filter = "All files (*.*)|*.*", OverwritePrompt = true, RestoreDirectory = true, CheckPathExists = true, })
					if(dlg.ShowDialog(this) == DialogResult.OK)
						File.WriteAllText(dlg.FileName, code.ToString());
			} else
				throw new NotSupportedException();
		}

		private void tvMethods_DragDrop(Object sender, DragEventArgs e)
		{
			if(e.Data.GetDataPresent(DataFormats.FileDrop))
			{//TODO: Необходимо проверить на ошибки
				String[] files = (String[])e.Data.GetData(DataFormats.FileDrop);
				this.Project = new ProjectBll(files[0]);
			} else throw new NotSupportedException("DataFormats.FileDrop supported only");
		}

		private void tvMethods_DragEnter(Object sender, DragEventArgs e)
			=> e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop)
				? DragDropEffects.Copy
				: DragDropEffects.None;
	}
}