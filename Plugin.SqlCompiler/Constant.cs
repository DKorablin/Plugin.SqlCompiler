using System;
using System.Data;
using System.Collections.Generic;

namespace Plugin.sqlCompiler
{
	internal static class Constant
	{
		public static String CombineDisplayName(String name, DbType type, UInt16? size, Boolean canBeNull, String defaultValue)
			=> String.Format("{0} : {1}{2}{3}{4}",
				name,
				type,
				size == null ? String.Empty : "(" + size.ToString() + ")",
				canBeNull ? "?" : String.Empty,
				defaultValue == null ? String.Empty : "=" + defaultValue);

		public static Boolean SplitDisplayName(String text, out String name,out DbType type, out UInt16? size, out Boolean canBeNull, out String defaultValue)
		{
			name = null;
			type = DbType.Object;
			size = null;
			canBeNull = false;
			defaultValue = null;

			if(String.IsNullOrEmpty(text))
				return false;

			String[] nameValue = text.Split(new Char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

			name = nameValue[0].Trim();
			if(name.Length == 0)
				return false;

			if(nameValue.Length != 2)
				return false;//Параметры не найдены. Разбор успешен, но не полон

			String value = nameValue[1].Trim();
			if(String.IsNullOrEmpty(value))
				return false;//тип данных не найден. Но наименование поля есть

			Int32 index = value.IndexOfAny(new Char[] { '(', '?', '=', });

			type = (DbType)Enum.Parse(typeof(DbType), index > -1 ? value.Substring(0, index).Trim() : value);

			index = value.IndexOf('(');
			if(index > -1)//Найден размер
				size = UInt16.Parse(value.Substring(index + 1, value.IndexOf(')') - (index + 1)).Trim());
			canBeNull = value.Contains("?");//Может быть null
			index = value.IndexOf('=');
			if(index > -1)//Найдено значение по умолчанию
				defaultValue = value.Substring(index + 1);

			return true;
		}

		public static TypeCode GetTypeCode(DbType type)
		{
			switch(type)
			{
			case DbType.Boolean:				return TypeCode.Boolean;
			case DbType.AnsiString:
			case DbType.AnsiStringFixedLength:
			case DbType.String:
			case DbType.StringFixedLength:		return TypeCode.String;
			case DbType.Byte:					return TypeCode.Byte;
			case DbType.Currency:
			case DbType.Decimal:				return TypeCode.Decimal;
			case DbType.Double:					return TypeCode.Double;
			case DbType.Guid:					return TypeCode.Object;
			case DbType.Int16:					return TypeCode.Int16;
			case DbType.Int32:					return TypeCode.Int32;
			case DbType.Int64:					return TypeCode.Int64;
			case DbType.Single:					return TypeCode.Single;
			case DbType.Date:
			case DbType.DateTime:
			case DbType.DateTime2:
			case DbType.DateTimeOffset:			return TypeCode.DateTime;
			default:
				throw new NotSupportedException();
			}
		}

		/// <summary>Получить тип данных SQL</summary>
		/// <param name="type">Тип данных в data provider'е</param>
		/// <param name="size">Максимальный размер значения</param>
		/// <returns></returns>
		public static String GetSqlType(DbType type, UInt16? size)
		{
			switch(type)
			{
			case DbType.Binary:					return String.Format("VarBinary({0})", size == null ? "MAX" : size.ToString());
			case DbType.Boolean:				return "Bit";
			case DbType.Byte:					return "TinyInt";
			case DbType.Currency:				return "Money";
			case DbType.Date:					return "SmallDateTime";
			case DbType.DateTime:				return "DateTime";
			case DbType.DateTime2:				return String.Format("DateTime2({0})", size == null ? 7 : size.Value);
			case DbType.DateTimeOffset:			return String.Format("DateTimeOffset({0})", size == null ? 7 : size.Value);
			case DbType.Decimal:				return String.Format("Decimal({0}, 0)", size == null ? 18 : size.Value);
			case DbType.Single:					return "Float";
			case DbType.Guid:					return "UniqueIdentifier";
			case DbType.Int16:					return "SmallInt";
			case DbType.Int32:					return "Int";
			case DbType.Int64:					return "BigInt";
			case DbType.AnsiString:
			case DbType.String:					return String.Format("NVarChar({0})", size == null ? "MAX" : size.Value.ToString());
			case DbType.AnsiStringFixedLength:
			case DbType.StringFixedLength:		return String.Format("NVarChar({0})", size == null ? 255 : size.Value);
			case DbType.Xml:					return "Xml";
			default:							throw new NotImplementedException();
			}
		}

		public static String GetSqlNetType(DbType type)
		{
			switch(type)
			{
			case DbType.AnsiString:				return "SqlString";
			case DbType.AnsiStringFixedLength:	return "SqlString";
			case DbType.Binary:					return "SqlBinary";
			case DbType.Boolean:				return "SqlBoolean";
			case DbType.Byte:					return "SqlByte";
			case DbType.Currency:				return "SqlMoney";
			case DbType.Date:					return "SqlDateTime";
			case DbType.DateTime:				return "SqlDateTime";
			case DbType.DateTime2:				return "SqlDateTime";
			case DbType.DateTimeOffset:			return "SqlDateTime";
			case DbType.Decimal:				return "SqlDecimal";
			case DbType.Single:					return "SqlDouble";
			case DbType.Guid:					return "SqlGuid";
			case DbType.Int16:					return "SqlInt16";
			case DbType.Int32:					return "SqlInt32";
			case DbType.Int64:					return "SqlInt64";
			case DbType.String:					return "SqlString";
			case DbType.StringFixedLength:		return "SqlString";
			case DbType.Xml:					return "SqlXml";
			default:							throw new NotImplementedException();
			}
		}

		public static String GetNetType(DbType type)
		{
			switch(type)
			{
			case DbType.AnsiString:				return "String";
			case DbType.AnsiStringFixedLength:	return "String";
			case DbType.Binary:					return "Byte[]";
			case DbType.Boolean:				return "Boolean";
			case DbType.Byte:					return "Byte";
			case DbType.Currency:				return "Decimal";
			case DbType.Date:					return "DateTime";
			case DbType.DateTime:				return "DateTime";
			case DbType.DateTime2:				return "DateTime";
			case DbType.DateTimeOffset:			return "TimeSpan";
			case DbType.Decimal:				return "Decimal";
			case DbType.Single:					return "Single";
			case DbType.Guid:					return "Guid";
			case DbType.Int16:					return "Int16";
			case DbType.Int32:					return "Int32";
			case DbType.Int64:					return "Int64";
			case DbType.String:					return "String";
			case DbType.StringFixedLength:		return "String";
			case DbType.Xml:					return "String";
			default:							throw new NotImplementedException();
			}
		}

		internal static class Template
		{
			public const String ClassTemplate = @"
using System;
using System.Globalization;
using System.Reflection;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using Microsoft.SqlServer.Server;

public partial class XmlParserClass
{
";
			public const String ItemTemplateArgs2 = @"
public struct {0}
{{
{1}
}}";
			public const String DecoderTemplateArgs3 = @"
public static void Decode{0}XML(Object row{1})
{{
	{0} item = ({0})row;
{2}
}}";
			public const String ParserTemplateArgs4 = @"
[SqlFunction(TableDefinition = ""{0}"", DataAccess = DataAccessKind.Read, FillRowMethodName = ""Decode{1}XML"")]
	public static IEnumerable {1}_cf(SqlString xpath, SqlString xml)
	{{
		using(TextReader stream = new StringReader(xml.Value))
		{{
			XPathDocument document = new XPathDocument(stream);
			XPathNavigator navigator = document.CreateNavigator();
			XPathNodeIterator it = navigator.Select(xpath.IsNull ? ""{2}"" : xpath.ToString());

			while(it.MoveNext())
			{{
				XPathUtils utils = new XPathUtils(it.Current);
				{1} item = new {1}();
				{3}

				yield return item;
			}}
		}}
	}}";
			public const String UtilsTemplate = @"
internal class XPathUtils
{
	private readonly Dictionary<String, String> _attributes = new Dictionary<String, String>();
	public XPathUtils(XPathNavigator navigator)
	{
		if(navigator.MoveToFirstAttribute())
			do
			{
				String value = navigator.Value;
				if(!String.IsNullOrEmpty(value))
					this._attributes.Add(navigator.LocalName.ToLowerInvariant(), value);
			} while(navigator.MoveToNextAttribute());
	}
	public String this[String localName]
	{
		get
		{
			String result;
			return this._attributes.TryGetValue(localName, out result) ? result : null;
		}
	}
	public String GetString(String localName)
	{
		return this[localName];
	}
	public Boolean? GetBoolean(String localName)
	{
		String value = this[localName];
		if(value == null) return null;

		return XmlConvert.ToBoolean(value);
	}
	public SByte? GetSByte(String localName)
	{
		String value = this[localName];
		if(value == null) return null;

		return XmlConvert.ToSByte(value);
	}
	public Byte? GetByte(String localName)
	{
		String value = this[localName];
		if(value == null) return null;

		return XmlConvert.ToByte(value);
	}
	public Decimal? GetDecimal(String localName)
	{
		String value = this[localName];
		if(value == null) return null;

		return XmlConvert.ToDecimal(value);
	}
	public Single? GetSingle(String localName)
	{
		String value = this[localName];
		if(value == null) return null;

		return XmlConvert.ToSingle(value);
	}
	public Guid? GetGuid(String localName)
	{
		String value = this[localName];
		if(value == null) return null;

		return XmlConvert.ToGuid(value);
	}
	public Int16? GetInt16(String localName)
	{
		String value = this[localName];
		if(value == null) return null;

		return XmlConvert.ToInt16(value);
	}
	public Int32? GetInt32(String localName)
	{
		String value = this[localName];
		if(value == null) return null;

		return XmlConvert.ToInt32(value);
	}
	public Int64? GetInt64(String localName)
	{
		String value = this[localName];
		if(value == null) return null;

		return XmlConvert.ToInt64(value);
	}
	public UInt16? GetUInt16(String localName)
	{
		String value = this[localName];
		if(value == null) return null;

		return XmlConvert.ToUInt16(value);
	}
	public UInt32? GetUInt32(String localName)
	{
		String value = this[localName];
		if(value == null) return null;

		return XmlConvert.ToUInt32(value);
	}
	public UInt64? GetUInt64(String localName)
	{
		String value = this[localName];
		if(value == null) return null;

		return XmlConvert.ToUInt64(value);
	}
	public DateTime? GetDateTime(String localName)
	{
		String value = this[localName];
		if(value == null) return null;

		return XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.Local);
	}
	public TimeSpan? GetTimeSpan(String localName)
	{
		String value = this[localName];
		if(value == null) return null;

		return XmlConvert.ToTimeSpan(value);
	}
}";
		}
	}
}