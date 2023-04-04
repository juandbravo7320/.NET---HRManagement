using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace HRManagement.Utilities
{
	public static class Csv
	{
		public static MemoryStream ExportCSV<T>(string fileName, List<T> dataList)
		{
			var CurrentDirectory = Directory.GetCurrentDirectory() + "\\" + fileName + ".csv";

			var stream = new MemoryStream();
			using (var writer = new StreamWriter(stream, Encoding.UTF8, 1024, true))
			{
				using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
				{
					csv.WriteRecords(dataList);
					writer.Flush();
				}
			}
			stream.Position = 0;
			return stream;
		}
	}
}