using System;
using System.IO;

namespace NUnit.Extensions.Forms
{
	///<summary>
	/// Assertions for files.
	///</summary>
	public static class FileAssert
	{
		///<summary>
		/// Compare two files for binary equality.
		///</summary>
		///<param name="filePathOne">The path to the first file to compare.</param>
		///<param name="filePathTwo">The path to the second file to compare.</param>
		///<returns>True if the given files have the same contents.</returns>
		public static bool AreBinaryEqual(string filePathOne, string filePathTwo)
		{
			if (!File.Exists(filePathOne) || !File.Exists(filePathTwo))
				return false;

			using (BinaryReader fileOne = new BinaryReader(File.OpenRead(filePathOne)))
			using (BinaryReader fileTwo = new BinaryReader(File.OpenRead(filePathTwo)))
			{
				while (fileOne.PeekChar() != -1 && fileTwo.PeekChar() != -1)
				{
					if (fileOne.ReadByte() != fileTwo.ReadByte())
						return false;
				}

				return true;
			}
		}
	}
}
