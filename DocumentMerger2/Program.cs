//Document Merger 2 Challenge. Zach Swartz. INFOTC2040
using System;
using System.IO;
using System.Collections.Generic;

namespace DocumentMerger2
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                //Error messages when less than three armuments are entered. 
                Console.WriteLine("DocumentMerger2 <input_file_1> <input_file_2> ... <input_file_n> <output_file>");
                Console.WriteLine("Supply a list of text files to merge followed by the name of the resulting merged text file as command line arguments.");
            }
            //If three or more arguments are entered, this is executed.
            else
            {
                Console.WriteLine("<<** Document Merger 2 **>>");
                //Read arguments and create merged document name.
                List<string> fileNames = CheckFiles(new List<string>(args).GetRange(0, args.Length - 1));
                string mergedDocName = extensionCheck(args[args.Length - 1]);

                try
                {
                    //Count characters in files and print number of characters and confirmaton that files has been merged and saved. 
                    int characterCount = mergeDocuments(fileNames, mergedDocName);
                    Console.WriteLine("{0} was successfully saved. The document contains {1} characters.", mergedDocName, characterCount);
                }
                catch (Exception error)
                {
                    //If exception is thrown, print to console. 
                    Console.WriteLine(error.Message);
                }
            }
            Console.ReadLine();
        }


        // Function to merge all documents specified into a new document that is named by the user. 
        static int mergeDocuments(List<string> documents, string newDocumentName)
        {
            StreamWriter sw = null;
            int characterCount = 0;
            try
            {
                sw = new StreamWriter(newDocumentName);
                foreach (string name in documents)
                {
                    characterCount += writeContentToFile(readDocumentText(name), sw);
                }
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
            return characterCount;
        }


        //Function to read lines from specified files. 
        static List<string> readDocumentText(string documentName)
        {
            List<string> lines = new List<string>();
            StreamReader sr = null;
            //Try to read lines in document until null.
            try
            {
                sr = new StreamReader(documentName);
                string line = sr.ReadLine();
                while (line != null)
                {
                    lines.Add(line);
                    line = sr.ReadLine();
                }
                return lines;
            }
            //Close as long as sr is not null. 
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }
        }

        //Function to write content from specified files to new merged file. 
        static int writeContentToFile(List<string> lines, StreamWriter sw)
        {
            int characterCount = 0;
            foreach (string line in lines)
            {
                characterCount += line.Length;
                sw.WriteLine(line);
            }
            return characterCount;
        }

        //Check list of files and make they have an extension name. 
        static List<string> CheckFiles(List<string> names)
        {
            List<string> fileChecked = new List<string>();
            names.ForEach(name => fileChecked.Add(extensionCheck(name)));
            return fileChecked;
        }

        //Function to check for file extension. If it is not there, a .txt extension will be added. 
        static string extensionCheck(string fileName)
        {
            if (Path.HasExtension(fileName))
            {
                return fileName;
            }
            else
            {
                return fileName + ".txt";
            }
        }
    }
}