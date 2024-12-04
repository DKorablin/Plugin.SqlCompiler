using System;
using System.Data;
using System.Windows.Forms;
using Plugin.sqlCompiler.Bll;
using System.ComponentModel;

namespace Plugin.sqlCompiler.UI
{
	internal partial class ParameterDlg : Form
	{
		private ProjectBll Project { get; set; }

		private ProjectDataSet.ClassRow ClassRow { get; set; }

		private ProjectDataSet.ClassParameterRow ParameterRow { get; set; }

		/// <summary>Наименование параметра</summary>
		public String ParameterName
		{
			get => txtName.Text.Trim();
			set => txtName.Text = value;
		}

		/// <summary>Default value</summary>
		public String Default
		{
			get => txtDefault.Text.Length == 0 ? null : txtDefault.Text;
			set => txtDefault.Text = value;
		}

		/// <summary>can be null</summary>
		public Boolean IsNullable
		{
			get => cbNullable.Checked;
			set => cbNullable.Checked = value;
		}

		/// <summary>Тип параметра</summary>
		public DbType Type
		{
			get => (DbType)ddlType.SelectedItem;
			set => ddlType.SelectedItem = value;
		}

		/// <summary>Размер поля в БД</summary>
		public UInt16? ParameterSize
		{
			get => udSize.Value >= 0 ? (UInt16)udSize.Value : (UInt16?)null;
			set => udSize.Value = value == null ? -1 : value.Value;
		}

		public ParameterDlg(ProjectBll project, ProjectDataSet.ClassRow classRow)
			: this(project, classRow, null)
		{ }

		public ParameterDlg(ProjectBll project, ProjectDataSet.ClassRow classRow, ProjectDataSet.ClassParameterRow parameterRow)
		{
			InitializeComponent();
			this.Project = project;
			this.ClassRow = classRow;
			this.ParameterRow = parameterRow;
			ddlType.DataSource = Enum.GetValues(typeof(DbType));

			if(parameterRow != null)
			{
				this.ParameterName = parameterRow.Name;
				this.Default = parameterRow.DefaultI;
				this.IsNullable = parameterRow.CanBeNull;
				this.Type = parameterRow.Type;
				this.ParameterSize = parameterRow.SizeI;
			}
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			if(base.DialogResult == DialogResult.OK)
			{
				Boolean cancel = false;

				//Проверка на валидные символы
				if(Array.Exists(this.ParameterName.ToCharArray(), (Char ch) => { return !Char.IsLetterOrDigit(ch) && !Char.IsPunctuation(ch); }))
				{
					cancel = true;
					error.SetError(txtName, "Only digit or letter characters allowed");
				} else
					error.SetError(txtName, String.Empty);

				//Проверка на дубль
				foreach(ProjectDataSet.ClassParameterRow parameterRow in this.Project.GetClassParameters(this.ClassRow.ClassID))
					if(parameterRow.Name.Equals(this.ParameterName, StringComparison.OrdinalIgnoreCase)
						&& (this.ParameterRow == null || parameterRow.Name != this.ParameterRow.Name))
					{
						cancel = true;
						error.SetError(txtName, "Parameter with this name already exists");
						break;
					}

				//Проверка на возможность установки размера поля
				if(this.ParameterSize != null)
					switch(this.Type)
					{
					case DbType.Binary:
					case DbType.DateTime2:
					case DbType.DateTimeOffset:
					case DbType.Decimal:
					case DbType.AnsiString:
					case DbType.AnsiStringFixedLength:
					case DbType.String:
					case DbType.StringFixedLength:
						error.SetError(udSize, String.Empty);
						break;
					default:
						cancel = true;
						error.SetError(udSize, "This type is unsizable");
						break;
					}

				//Проверка значения по умолчанию
				if(this.Default != null)
				{
					try
					{
						Object test;
						TypeCode code = Constant.GetTypeCode(this.Type);
						switch(code)
						{
						case TypeCode.Object:
							test = new Guid(this.Default);
							break;
						default:
							test = Convert.ChangeType(this.Default, code);
							break;
						}
					} catch(Exception exc)
					{
						e.Cancel = true;
						error.SetError(txtDefault, exc.Message);
					}
				}
				e.Cancel = cancel;
			}
			base.OnClosing(e);
		}
	}
}