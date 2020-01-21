using System;
using System.IO;
using System.IO.Compression;

namespace CopyMap
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initilising");

            /*Defining directories and mapname parameters*/

            string startingDir;
            string startingHammerDir;
            string destinationDir;
            string mapname;

            /*So that the compiler doesnt complain*/

            startingDir = @"";
            startingHammerDir = @"";
            destinationDir = @"";
            mapname = "";

            /*Setting the directory because of file readers*/


            /*The fun stuff*/

            if (File.Exists(@"Config.txt"))
            {
                String line;
                try
                {
                    //Pass the file path and file name to the StreamReader constructor
                    StreamReader sr = new StreamReader(@"Config.txt");

                    //Read the first line of text
                    line = sr.ReadLine();

                    //Continue to read until you reach end of file
                    while (line != null)
                    {
                        //write the line to console window
                        Console.WriteLine(line);
                        //Read the next line
                        startingDir = sr.ReadLine();

                        //write the line to console window
                        Console.WriteLine(startingDir);
                        //Read the next line
                        startingHammerDir = sr.ReadLine();

                        //write the line to console window
                        Console.WriteLine(startingHammerDir);
                        //Read the next line
                        destinationDir = sr.ReadLine();

                        //write the line to console window
                        Console.WriteLine(destinationDir);
                        //Read the next line
                        mapname = sr.ReadLine();

                        //write the line to console window
                        Console.WriteLine(mapname);
                        //Read the next line
                        line = sr.ReadLine();
                        
                    }

                    //close the file
                    sr.Close();
                   
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
                finally
                {
                }
            }
            else
            {
                Console.WriteLine("ERROR : Config doesnt exit");
                Program.Exit();
            }

            string fileBsp = mapname + ".bsp";
            string fileVmf = mapname + ".vmf";
            string fileNav = mapname + ".nav";


            //Get date

            DateTime date = DateTime.Now;
            string dateWithFormat = date.ToUniversalTime().ToString("dd-MM-yyyy-hh-mm");


            //Combining parameters to get usable directories

            string sourceBspFile = System.IO.Path.Combine(startingDir, fileBsp);
            string sourceVmfFile = System.IO.Path.Combine(startingHammerDir, fileVmf);
            string sourceNavFile = System.IO.Path.Combine(startingDir, fileNav);
            string pastingDir = System.IO.Path.Combine (destinationDir, mapname + "@" + dateWithFormat);
            string pastingDir2 = System.IO.Path.Combine(destinationDir, mapname + "@" + dateWithFormat + @"\");

            string pastingBspFile = Path.Combine(pastingDir2, fileBsp);
            string pastingVmfFile = Path.Combine(pastingDir2, fileVmf);
            string pastingNavFile = Path.Combine(pastingDir, fileNav);

            /*Feedback for initialisation completion*/

            Console.WriteLine("Initialisation Complete");

            /*Debug directories*/

            Console.WriteLine(sourceBspFile);
            Console.WriteLine(sourceVmfFile);
            Console.WriteLine(sourceNavFile);
            Console.WriteLine(pastingDir2);
            Console.WriteLine(pastingDir);
            Console.WriteLine(pastingBspFile);
            Console.WriteLine(pastingVmfFile);
            Console.WriteLine(pastingNavFile);

           

            /*Checking if the startingDir exists*/

            if (Directory.Exists(startingDir))
            {

                Directory.CreateDirectory(pastingDir2);

                Console.WriteLine("Created directory", pastingDir2);

                if (System.IO.Directory.Exists(pastingDir2))
                {
                    Console.WriteLine("Starting BSP copy"); /*Copying BSP*/
                    System.IO.File.Copy(sourceBspFile, pastingBspFile);
                    Console.WriteLine("Coping of BSP finnished");

                    Console.WriteLine("Starting VMF copy"); /*Copying VMF*/
                    System.IO.File.Copy(sourceVmfFile, pastingVmfFile);
                    Console.WriteLine("Coping of VMF finnished");

                    Console.WriteLine("Starting NAV copy"); /*Copying NAV*/
                    System.IO.File.Copy(sourceNavFile, pastingNavFile);
                    Console.WriteLine("Coping of NAV finnished");
                }
                else {

                    Console.WriteLine("ERROR: Pasting directory doesnt exist");
                    Program.Exit();

                }


            }
            else
            {

                Console.WriteLine("ERROR: Copying directory doesnt exist");
                Program.Exit();

            }

            //Zip

            string zipPath = (pastingDir + ".zip");
            Console.WriteLine("Starting zipping file " + pastingDir + " Into : " + zipPath );

            try {
                ZipFile.CreateFromDirectory(pastingDir, zipPath);
            }
            catch (Exception e) {
                Console.WriteLine("EXEPTION : " + e); /*Catch and contain errors and exeptions*/
                Program.Exit();
            }

            Console.WriteLine("Zipping ended");

            //Deleating the unziped file
            Console.WriteLine("Deleating temp files");
            string[] filelist = Directory.GetFiles(pastingDir); //get the files
            try 
            {
                foreach (string f in filelist) {
                    Console.WriteLine(f); //list the files
                    File.Delete(f); //deleate the files because c# is a bitch
                }
            }
            catch (Exception e) /*catch the shitstorm*/
            {
                Console.WriteLine("EXEPTION : " + e);
            }

            try
            {
                Directory.Delete(pastingDir); //Finaly deleate the temparary directory
            }
            catch (Exception e) /*catch the shitstorm*/
            {
                Console.WriteLine("EXEPTION : " + e.Message);
                Program.Exit();
            }


            Console.WriteLine("Finished deleating temp files");


            //User friendliness
            Console.WriteLine("Finished the backup to : " + zipPath);

            Program.Exit();

        }

        static void Exit() {

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            Environment.Exit(0);

        }


    }

}
