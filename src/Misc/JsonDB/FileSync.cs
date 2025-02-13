using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal class FileSync
{
	public string pathFileName = string.Empty;

	public FileSync(string pathFileName)
	{
		this.pathFileName = pathFileName;
	}

	public string Read()
	{
		if(File.Exists(pathFileName)) return ReadFromFile();
			
		return Constants.EMPTY_JSON;
	}

	public bool Write(string json)
	{
		return WriteToFile(json);
	}

	public void Delete()
	{
		try
		{
			File.Delete(pathFileName);
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private string ReadFromFile()
	{
		try
		{
			var file = File.Open(pathFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			var streamReader = new StreamReader(file);
			var content = streamReader.ReadToEnd();

			streamReader.Close();

			return content;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
			return Constants.EMPTY_JSON;
		}

	}

	private bool WriteToFile(string json)
	{
		try
		{
			Directory.CreateDirectory(Path.GetDirectoryName(pathFileName));
			var file = File.Open(pathFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);

			var streamWriter = new StreamWriter(file, Encoding.UTF8);
			streamWriter.AutoFlush = true;

			file.SetLength(0);

			streamWriter.Write(json);
			streamWriter.Close();

			return true;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
			return false;
		}
	}
}
