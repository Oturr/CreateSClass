using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GenerateSClasses
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string outputPath = @"C:\Users\Ars\Desktop\0\GeneratedClasses";

			bool exitState = true;
			while (exitState)
			{
				Console.Write("Название класса: ");
				string className = Console.ReadLine();
				Console.Write("Название методов: ");
				string methodName = Console.ReadLine();
				Console.Write("dt: ");
				string dtName = Console.ReadLine();
				Console.Write("Ошибка при получении чего?: ");
				string fErMsg = Console.ReadLine();
				Console.Write("Уже есть что?: ");
				string sErMsg = Console.ReadLine();
				Console.Write("Ошибка при чего?: ");
				string tErMsg = Console.ReadLine();

				Console.Write("Таблица: ");
				string bdTab = Console.ReadLine();
				Console.Write("Индекс таблицы: ");
				string tabInd = Console.ReadLine();
				Console.Write("Поле таблицы: ");
				string tabField = Console.ReadLine();
				Console.Write("Внешняя таблица: ");
				string fTab = Console.ReadLine();
				Console.Write("Внешний индекс: ");
				string fInd = Console.ReadLine();


				string path = $@"C:\Users\Ars\Desktop\0\GeneratedClasses\{className}.txt";

				string codeInnards = $@"using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PP25_Uchet.Classes.DBClasses.Sprav
{{
internal class {className} : DBConnectionClass
{{
static public DataTable {dtName} = new DataTable();
static public void Get{methodName}()
{{
	try
	{{
		myCommand.CommandText = $@""select * from {bdTab}"";
		{dtName}.Clear();
		myDataAdapter.Fill({dtName});
	}}
	catch
	{{
		MessageBox.Show(""Ошибка при получении {fErMsg}"",
			""Ошибка"",
			MessageBoxButtons.OK, MessageBoxIcon.Error);
	}}
}}

static public bool Add{methodName}(string value)
{{
	try
	{{
		myCommand.CommandText = $@""select {tabInd} from {bdTab} where {tabField}=value"";
		object result = myCommand.ExecuteScalar();
		if (result == null)
		{{
			myCommand.CommandText = $@""insert into {bdTab} values(NULL, '{{value}}')"";
			myCommand.ExecuteNonQuery();
			return true;
		}}
		MessageBox.Show(""Уже есть {sErMsg}"",
			""Ошибка"",
			MessageBoxButtons.OK, MessageBoxIcon.Error);
		return false;
	}}
	catch
	{{
		MessageBox.Show(""Ошибка при добавлении {tErMsg}"",
			""Ошибка"",
			MessageBoxButtons.OK, MessageBoxIcon.Error);
		return false;
	}}
}}

static public bool Update{methodName}(int index, string value)
{{
	try
	{{
		myCommand.CommandText = $@""select {tabInd} from {bdTab} where {tabField}='{{value}}' and {tabInd}!={{index}}"";
		object result = myCommand.ExecuteScalar();
		if (result == null)
		{{
			myCommand.CommandText = $@""update {bdTab} set {tabField}='{{value}}' where {tabInd}={{index}}"";
			myCommand.ExecuteNonQuery();
			return true;
		}}
		MessageBox.Show(""Уже есть {sErMsg}"",
			""Ошибка"",
			MessageBoxButtons.OK, MessageBoxIcon.Error);
		return false;
	}}
	catch
	{{
		MessageBox.Show(""Ошибка при обновлении {tErMsg}"",
			""Ошибка"",
			MessageBoxButtons.OK, MessageBoxIcon.Error);
		return false;
	}}
}}

static public bool Delete{methodName}(int index)
{{
	try
	{{
		myCommand.CommandText = $@""select {fInd} from {fTab} where {tabInd}={{index}}"";
		object result = myCommand.ExecuteScalar();
		if (result != null)
		{{
			MessageBox.Show(""Уже используется {sErMsg}"",
			""Ошибка"",
			MessageBoxButtons.OK, MessageBoxIcon.Error);
			return false;
		}}
		myCommand.CommandText = $@""delete from {bdTab} where {tabInd}={{index}}"";
		myCommand.ExecuteNonQuery();
		return true;
	}}
	catch
	{{
		MessageBox.Show(""Ошибка при удалении {tErMsg}"",
			""Ошибка"",
			MessageBoxButtons.OK, MessageBoxIcon.Error);
		return false;
	}}
}}
}}
}}";

				using (Stream myStream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write))
				{
					using (var sw = new StreamWriter(myStream))
					{
						sw.WriteLine(codeInnards);
						sw.Close();
					}
				}

				File.Move(path, Path.ChangeExtension(path, ".cs"));

				Console.WriteLine("Выход 'y'");
				if (Console.ReadLine() == "y")
				{
					exitState = false;
				}
			}
			

		}
	}
}
