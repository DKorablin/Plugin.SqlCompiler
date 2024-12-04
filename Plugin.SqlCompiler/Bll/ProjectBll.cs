using System;
using System.Data;
using System.IO;
using System.Linq;
using AlphaOmega.Bll;

namespace Plugin.sqlCompiler.Bll
{
	internal class ProjectBll : BllBase<ProjectDataSet, ProjectDataSet.MethodRow>
	{
		public String ProjectName { get; private set; }

		public ProjectBll(String configFileName)
			: base(configFileName, 0)
		{
			this.ProjectName = Path.GetFileNameWithoutExtension(configFileName);
			base.Load(false);
		}

		/// <summary>Получить информацию о методе сервиса</summary>
		/// <param name="methodId">Идентификатор метода о котором получить информацию</param>
		/// <returns>Информация о методе</returns>
		public ProjectDataSet.MethodRow GetMethodRow(Int32 methodId)
			=> base.DataSet.Method.Where(p => p.MethodID == methodId).FirstOrDefault();

		/// <summary>Получить информацию о мтоде сервиса по наименованию</summary>
		/// <param name="methodName">Наименование метода сервиса</param>
		/// <returns>Информация о методе</returns>
		public ProjectDataSet.MethodRow GetMethodRow(String methodName)
			=> String.IsNullOrEmpty(methodName)
				? null
				: base.DataSet.Method.Where(p => p.Name.Equals(methodName)).FirstOrDefault();

		public ProjectDataSet.MethodRow[] GetMethodRows()
			=> base.DataSet.Method.ToArray();

		/// <summary>Добавить или изменть метод</summary>
		/// <param name="methodId">Идентификатор изменяемого метода</param>
		/// <param name="name">Наименование метода</param>
		/// <param name="connection">Наименование строки подключения к источнику данных</param>
		public ProjectDataSet.MethodRow ModifyMethod(Int32? methodId, String name, String description)
		{
			ProjectDataSet.MethodRow row = methodId == null
				? base.DataSet.Method.NewMethodRow()
				: this.GetMethodRow(methodId.Value);

			_ = row ?? throw new ArgumentNullException(nameof(methodId));

			row.BeginEdit();
			row.Name = name;
			row.Description = description;
			row.EndEdit();

			if(row.RowState == DataRowState.Detached)
				base.DataSet.Method.AddMethodRow(row);
			else
				row.AcceptChanges();
			return row;
		}

		public ProjectDataSet.ClassRow GetClassRow(Int32 classId)
			=> base.DataSet.Class.Where(p => p.ClassID.Equals(classId)).FirstOrDefault();

		public ProjectDataSet.ClassRow GetClassRow(String className)
			=> base.DataSet.Class.Where(p => p.Name.Equals(className)).FirstOrDefault();

		/// <summary>Получить все классы</summary>
		/// <returns>Все пользовательские классы</returns>
		public ProjectDataSet.ClassRow[] GetClassRows()
			=> base.DataSet.Class.ToArray();

		/// <summary>Получить список всех классов метода</summary>
		/// <param name="methodId">Идентификатор метода, классы которого необходимо получить</param>
		/// <returns></returns>
		public ProjectDataSet.ClassRow[] GetMethodClass(Int32 methodId)
			=> base.DataSet.Class.Where(p => p.MethodID.Equals(methodId)).ToArray();

		/// <summary>Изменить информацию о классе</summary>
		/// <param name="classId">Идентификатор изменяемого класса или null, если добавить новый класс</param>
		/// <param name="methodId">Идентификатор метода, которому принадлежит класс</param>
		/// <param name="name">Наименование метода</param>
		/// <returns>Созданный или изменённый ряд описателя метода</returns>
		public ProjectDataSet.ClassRow ModifyMethodClass(Int32? classId, Int32 methodId, String name)
		{
			ProjectDataSet.ClassRow row = classId.HasValue
				? this.GetClassRow(classId.Value)
				: base.DataSet.Class.NewClassRow();

			_ = row ?? throw new ArgumentNullException(nameof(classId));

			row.BeginEdit();
			row.MethodID = methodId;
			row.Name = name;

			if(row.RowState == DataRowState.Detached)
				base.DataSet.Class.AddClassRow(row);
			else
				row.AcceptChanges();
			return row;
		}

		public ProjectDataSet.ClassParameterRow ModifyClassParameterRow(String baseName, Int32 classId, String name, DbType type, Boolean canBeNull, String defaultValue, UInt16? size)
		{
			ProjectDataSet.ClassParameterRow row = baseName == null
				? base.DataSet.ClassParameter.NewClassParameterRow()
				: this.GetClassParameter(classId, baseName);

			_ = row ?? throw new ArgumentNullException(nameof(baseName), "classId");

			row.BeginEdit();
			row.ClassID = classId;
			row.Name = name.ToLowerInvariant();
			row.Type = type;
			row.CanBeNull = canBeNull;
			row.DefaultI = defaultValue;
			row.SizeI = size;

			if(row.RowState == DataRowState.Detached)
				base.DataSet.ClassParameter.AddClassParameterRow(row);
			else
				row.AcceptChanges();
			return row;
		}

		/// <summary>Удалить метод</summary>
		/// <param name="methodRow">Метод</param>
		public void RemoveMethod(ProjectDataSet.MethodRow methodRow)
		{
			this.RemoveMethodClasses(methodRow.MethodID);
			this.DataSet.Method.RemoveMethodRow(methodRow);
		}

		/// <summary>Удалить классы метода</summary>
		/// <param name="methodId">Идентификатор метода классы которого необходимо удалить</param>
		public void RemoveMethodClasses(Int32 methodId)
		{
			foreach(ProjectDataSet.ClassRow classRow in this.GetMethodClass(methodId))
				this.RemoveClass(classRow);
		}

		public void RemoveClass(ProjectDataSet.ClassRow classRow)
		{
			this.RemoveClassParameters(classRow.ClassID);
			base.DataSet.Class.RemoveClassRow(classRow);
		}

		/// <summary>Удалить все элементы определённого класса</summary>
		/// <param name="classId">Идентификатор класса методы которого удалить</param>
		public void RemoveClassParameters(Int32 classId)
		{
			foreach(ProjectDataSet.ClassParameterRow classParameter in this.GetClassParameters(classId))
				this.RemoveClassParameter(classParameter);
		}

		/// <summary>Удалить параметры класса</summary>
		/// <param name="classParameter">Параметр класса</param>
		public void RemoveClassParameter(ProjectDataSet.ClassParameterRow classParameter)
			=> base.DataSet.ClassParameter.Rows.Remove(classParameter);

		/// <summary>Получить все параметры класса</summary>
		/// <param name="classId">Идентификатор класса</param>
		/// <returns>Массив параметров класса</returns>
		public ProjectDataSet.ClassParameterRow[] GetClassParameters(Int32 classId)
			=> base.DataSet.ClassParameter.Where(p => p.ClassID.Equals(classId)).ToArray();

		/// <summary>Получить параметр класса по идентификатору класса и наименованию параметра</summary>
		/// <param name="classId">Идентификатор класса</param>
		/// <param name="name">Наименование параметра</param>
		/// <returns>Параметр класса</returns>
		public ProjectDataSet.ClassParameterRow GetClassParameter(Int32 classId, String name)
			=> this.GetClassParameters(classId).FirstOrDefault(p => p.Name == name);
	}
}