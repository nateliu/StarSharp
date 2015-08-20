using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
using System.IO;
using System.Windows.Forms;

namespace StarSharp.Core.Utility
{
	public class ZipHelper
	{
		/// <summary>
		/// Create a zip archive.
		/// </summary>
		/// <param name="filename">The filename.</param>
		/// <param name="directory">The directory to zip.</param> 
		public static void PackFiles(string filename, string directory)
		{
			try
			{
				FastZip fz = new FastZip();
				fz.CreateEmptyDirectories = true;
				fz.CreateZip(filename, directory, true, "");
				fz = null;
			}
			catch (Exception)
			{
				throw;
			}
		}

		/**/
		/// <summary>
		/// Unpacks the files.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <returns>if succeed return true,otherwise false.</returns>
		public static bool UnpackFiles(string file, string dir)
		{
			try
			{
				if (!Directory.Exists(dir))
					Directory.CreateDirectory(dir);

				using (ZipInputStream s = new ZipInputStream(File.OpenRead(file)))
				{

					ZipEntry theEntry;
					while ((theEntry = s.GetNextEntry()) != null)
					{

						string directoryName = Path.GetDirectoryName(theEntry.Name);
						string fileName = Path.GetFileName(theEntry.Name);

						if (directoryName != String.Empty)
							Directory.CreateDirectory(Path.Combine(dir, directoryName));

						if (fileName != String.Empty)
						{
							string p = Path.Combine(dir, theEntry.Name.Replace("/", "\\"));
							//MessageBox.Show(p + ": p.Length=" + p.Length);
							try
							{
								using (FileStream streamWriter = File.Create(p))
								{

									int size = 2048;
									byte[] data = new byte[2048];
									while (true)
									{
										size = s.Read(data, 0, data.Length);
										if (size > 0)
										{
											streamWriter.Write(data, 0, size);
										}
										else
										{
											break;
										}
									}

								}
							}
							catch (PathTooLongException)
							{
                                //ShellUtils.ShowWarn("The path too LONG. Please insure the paths must be less than 248 characters");
								continue;
							}
						}
					}
				}

				return true;
			}
			catch (Exception ex)
			{
                //ShellUtils.ShowError(ex.Message + "\r\n" + ex.StackTrace);
				throw;
			}
		}
	}
}
