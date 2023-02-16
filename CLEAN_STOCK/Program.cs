using System;
using System.IO;
using System.Text;
using CLEAN_STOCK.Properties;

namespace CLEAN_STOCK
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string logfile = Settings.Default.logfile;
                string path = Settings.Default.path;
                string extension1 = Settings.Default.extension1;
                string extension2 = Settings.Default.extension2;
                int days = Settings.Default.days;
                DirectoryInfo di = new DirectoryInfo(@path);
                float length;
                float alllength = 0;
                StringBuilder sblogger = new StringBuilder();

                if (!Directory.Exists(@logfile))
                {
                    Directory.CreateDirectory(@logfile);
                }

                DateTime today = DateTime.Now;
                today.ToString(@"yyyy-MM-dd_HH");
                sblogger.Append(today + "\n");

                string info = ("Suche " + extension1 + " und " + extension2 + " Dateien in " + path + " und lösche alle die älter sind als " + days + " Tage" + "\n" + "delete = " + Settings.Default.delete + "\n" + "see Settingsfile" + "\n" + "\n");
                Console.WriteLine(info);
                sblogger.Append(info);

                Console.WriteLine();


                sblogger.Append("***** search files " + extension1 + "\n");
                Console.WriteLine("\n" + "***** search files " + extension1 + "\n");
                foreach (var fi in di.GetFiles("*.stl", SearchOption.AllDirectories))
                {
                    DateTime dtFile = fi.CreationTime;
                    TimeSpan t = DateTime.Now - dtFile;
                    double NrOfDays = t.TotalDays;
                    int iAlter = Convert.ToInt32(NrOfDays);

                    // Lösche die Dateien
                    if (iAlter >= days)
                    {
                        string info1 = (fi.Name + "*** gelöscht " + fi.CreationTime);
                        Console.WriteLine(info1);
                        sblogger.Append(info1 + "\n");

                        length = (fi.Length) / 1024 / 1024;
                        alllength = (alllength + length);

                        if (Settings.Default.delete == true)
                        {
                            fi.Delete();
                        }
                    }

                    else
                    {
                        // string info11 = (fi.Name + " nicht gelöscht");
                        // Console.WriteLine (info11);
                        // sblogger.Append(info11 + "\n");
                    }
                }

                sblogger.Append("\n" + "***** search files " + extension2 + "\n");
                Console.WriteLine("\n" + "***** search files " + extension2 + "\n");
                foreach (var fi in di.GetFiles("*.vis", SearchOption.AllDirectories))
                {
                    DateTime dtFile = fi.CreationTime;
                    TimeSpan t = DateTime.Now - dtFile;
                    double NrOfDays = t.TotalDays;
                    int iAlter = Convert.ToInt32(NrOfDays);

                    // Lösche die Dateien
                    if (iAlter >= days)
                    {
                        string info1 = (fi.Name + "*** gelöscht " + fi.CreationTime);
                        Console.WriteLine(info1);
                        sblogger.Append(info1 + "\n");

                        length = (fi.Length) / 1024 / 1024;
                        alllength = (alllength + length);

                        if (Settings.Default.delete == true)
                        {
                            fi.Delete();
                        }
                    }

                    else
                    {
                        // string info11 = (fi.Name + " nicht gelöscht");
                        // Console.WriteLine(info11);
                        // sblogger.Append(info11 + "\n");
                    }
                }



                Console.WriteLine();
                float alllengthGB = alllength / 1024;
                string info2 = ("\n" + "***** found " + alllength + " MB (" + alllengthGB + " GB)");
                Console.WriteLine(info2);
                sblogger.Append(info2 + "\n");

                sblogger.Append("\n");


                // Schreibe logfiles
                File.AppendAllText(@logfile + "" + "file.log", sblogger + Environment.NewLine);


                Console.WriteLine();
                Console.ReadLine();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                
            }


        }
    }
}







