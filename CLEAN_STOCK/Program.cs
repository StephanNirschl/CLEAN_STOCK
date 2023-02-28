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
                string logfile = Settings.Default.logfile1;
                string path = Settings.Default.path;
                string extension1 = Settings.Default.extension1;
                int days1 = Settings.Default.days1;
                string extension2 = Settings.Default.extension2;
                int days2 = Settings.Default.days2;
                DirectoryInfo di = new DirectoryInfo(@path);
                float length;
                float alllength = 0;
                float lengthNOTUSE;
                float alllengthNOTUSE = 0;
                StringBuilder sblogger = new StringBuilder();

                if (!Directory.Exists(@logfile))
                {
                    Directory.CreateDirectory(@logfile);
                }

                DateTime today = DateTime.Now;
                today.ToString(@"yyyy-MM-dd_HH");
                sblogger.Append(today + "\n");

                string info = ("Suche in " + path + " und lösche alle " + 
                    "\n" + extension1 + " älter als " + days1 + " Tage und alle " + extension2 + " älter als " + days2 + " Tage" +
                    "\n" + "delete = " + Settings.Default.delete + 
                    "\n" + "see Settingsfile" + 
                    "\n" + 
                    "\n"
                    );

                Console.WriteLine(info);
                sblogger.Append(info);
                


                sblogger.Append("***** search files " + extension1  + " Wait...");
                Console.WriteLine("\n" + "***** search files " + extension1 + " Wait...");
                foreach (var fi in di.GetFiles("*.stl", SearchOption.AllDirectories))
                {
                    DateTime dtFile = fi.CreationTime;
                    TimeSpan t = DateTime.Now - dtFile;
                    double NrOfDays = t.TotalDays;
                    int iAlter = Convert.ToInt32(NrOfDays);

                    // Lösche die Dateien
                    if (iAlter >= days1)
                    {
                        string info1 = ("*** gelöscht = " + Settings.Default.delete + " --- " + fi.CreationTime + " --- " + fi.Name);
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
                        lengthNOTUSE = (fi.Length) / 1024 / 1024;
                        alllengthNOTUSE = (alllengthNOTUSE + lengthNOTUSE);
                    }
                }

                sblogger.Append("\n" + "***** search files " + extension2 +  " Wait...");
                Console.WriteLine("\n" + "***** search files " + extension2 + " Wait...");
                foreach (var fi in di.GetFiles("*.vis", SearchOption.AllDirectories))
                {
                    DateTime dtFile = fi.CreationTime;
                    TimeSpan t = DateTime.Now - dtFile;
                    double NrOfDays = t.TotalDays;
                    int iAlter = Convert.ToInt32(NrOfDays);

                    // Lösche die Dateien
                    if (iAlter >= days2)
                    {
                        string info1 = ("*** gelöscht " + Settings.Default.delete + " --- " + fi.CreationTime + " --- " + fi.Name);
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
                        lengthNOTUSE = (fi.Length) / 1024 / 1024;
                        alllengthNOTUSE = (alllengthNOTUSE + lengthNOTUSE);
                    }
                }



                Console.WriteLine();
                sblogger.Append("\n");
                float alllengthGB = alllength / 1024;
                float alllengthTB = alllengthGB / 1024;
                string info2 = ("\n" + "***** " + Math.Round(alllength, 3) + " MB (" + Math.Round(alllengthGB, 3) + " GB --- " + Math.Round(alllengthTB,3) + " TB) zum löschen");
                Console.WriteLine(info2);
                sblogger.Append(info2);

                float alllengthGBNOTUSE = alllengthNOTUSE / 1024;
                float alllengthTBNOTUSE = alllengthGBNOTUSE / 1024;
                string info3 = ("\n" + "***** " + Math.Round(alllengthNOTUSE,3) + " MB (" + Math.Round(alllengthGBNOTUSE,3) + " GB --- " + Math.Round(alllengthTBNOTUSE,3) + " TB) bleiben übrig");
                Console.WriteLine(info3);
                sblogger.Append(info3 + "\n");




                sblogger.Append("\n");



                // Schreibe logfiles
                DateTime dt = DateTime.Now;
                string date = dt.ToString("dd-MM-yyyy_HH-mm-ss");
                File.AppendAllText(@logfile + date + ".log", sblogger + Environment.NewLine);


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







