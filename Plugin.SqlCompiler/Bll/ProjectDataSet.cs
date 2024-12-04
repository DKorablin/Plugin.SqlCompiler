using System;
using System.Data;

namespace Plugin.sqlCompiler.Bll
{
	partial class ProjectDataSet
	{
		partial class ClassParameterRow
		{
			public String DefaultI
			{
				get => this.IsDefaultNull() ? null : this.Default;
				set
				{
					if(String.IsNullOrEmpty(value))
						this.SetDefaultNull();
					else
						this.Default = value;
				}
			}
			public UInt16? SizeI
			{
				get => this.IsSizeNull() ? (UInt16?)null : this.Size;
				set
				{
					if(value == null)
						this.SetSizeNull();
					else
						this.Size = value.Value;
				}
			}
			public String DefaultValue
			{
				get
				{
					String defaultValue = this.DefaultI;
					if(defaultValue != null)
					{
						switch(this.Type)
						{
						case DbType.String:
						case DbType.StringFixedLength:
						case DbType.AnsiString:
						case DbType.AnsiStringFixedLength:
						case DbType.Guid:
							defaultValue = String.Format("\"{0}\"", defaultValue.Replace("\"", "\\\""));
							break;
						}
					}
					return defaultValue;
				}
			}
			public String DisplayName
				=> Constant.CombineDisplayName(this.Name, this.Type, this.SizeI, this.CanBeNull, this.DefaultValue);

			public String SqlNetType
				=> Constant.GetSqlNetType(this.Type);

			public String NetType
				=> Constant.GetNetType(this.Type);

			public String SqlType
				=> Constant.GetSqlType(this.Type, this.SizeI);

			/// <summary>Код описателя переменной</summary>
			public String FieldCode
			{
				get
				{
					String defaultValue = this.DefaultValue;
					if(defaultValue != null)
						defaultValue = String.Format("[DefaultValue({0})]", defaultValue);

					Char? nullableKey = null;
					if(this.CanBeNull)
						switch(this.Type)
						{
						case DbType.AnsiString:
						case DbType.AnsiStringFixedLength:
						case DbType.Binary:
						case DbType.Object:
						case DbType.String:
						case DbType.StringFixedLength:
						case DbType.Xml:
							break;//Nullable<T> на объектах не используется
						default:
							nullableKey = '?';
							break;
						}
					return String.Format(@"{0} public {1}{2} {3};", defaultValue,
						this.NetType,
						nullableKey,
						this.Name);
				}
			}
			/// <summary>TableDefinition member name</summary>
			public String DefinitionMemberName
				=> String.Format("{0} {1}", this.Name, this.SqlType);

			/// <summary>Наименование out параметра в методе декодера</summary>
			public String OutMemberName
				=> String.Format("out {0} {1}", this.SqlNetType, this.Name);

			public String AssigmentGetOperator
			{
				get
				{
					String defaultValue = this.DefaultValue;
					String template;
					if(defaultValue != null)
						template = "item.{0}=utils.Get{1}(\"{0}\")??" + defaultValue + ";";

					switch(this.Type)
					{
					case DbType.String:
					case DbType.StringFixedLength:
					case DbType.AnsiString:
					case DbType.AnsiStringFixedLength:
						template = "item.{0}=utils.Get{1}(\"{0}\");";
						break;
					default:
						if(this.CanBeNull)
							template = "item.{0}=utils.Get{1}(\"{0}\");";
						else
							template = "item.{0}=utils.Get{1}(\"{0}\").GetValueOrDefault();";
						break;
					}
					return String.Format(template, this.Name, this.NetType);
				}
			}

			/// <summary>Операция присваивания при передачи в SQL тип</summary>
			public String AssignmentFillOperation
			{
				get
				{
					String template;
					switch(this.Type)
					{
					case DbType.String:
					case DbType.StringFixedLength:
					case DbType.AnsiString:
					case DbType.AnsiStringFixedLength:
						template = "{0} = item.{0}==null?new {1}():new {1}(item.{0});";
						break;
					default:
						if(this.CanBeNull)
							template = "{0} = item.{0}.HasValue?new {1}(item.{0}.Value):new {1}();";
						else
							template = "{0} = new {1}(item.{0});";
						break;
					}

					return String.Format(template, this.Name, this.SqlNetType);
				}
			}
		}
	}
}