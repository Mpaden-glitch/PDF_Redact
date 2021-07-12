using System;
using System.Collections.Generic;
using System.IO;
using CommandLine;
using iText.Kernel.Pdf;
using iText.PdfCleanup;
using iText.Kernel.Geom;
using iText.Kernel.Colors;
using iText.PdfCleanup.Autosweep;
using System.Text.RegularExpressions;

namespace Redact
{
    class Program
    {
        public class Opitions
        {
            [Option('r', "verbose", Required = false, HelpText = "enter r to run process")]
            public bool Verbose { get; set; }

            [Option('h', "height", Required = false, HelpText = "This is the height of the barcode")]
            public float Height { get; set; }

            [Option('w', "width", Required = false, HelpText = "This is the width of the barcode")]
            public float Width { get; set; }

            [Option('x', "xaxis", Required = false, HelpText = "X-axis with 0,0 being bottom left")]
            public float Xaxis { get; set; }

            [Option('y', "yaxis", Required = false, HelpText = "Y-axis with 0,0 being bottom left")]
            public float Yaxis { get; set; }
        }
        static void Main(string[] args)
        {
            _ = Parser.Default.ParseArguments<Opitions>(args)
                .WithParsed(o =>
                {
                    if (o.Verbose)
                    {
                        //float height = 40;
                        //float width = 383;
                        //float xaxis = 97;
                        //float yaxis = 405;
                        //PdfWriter outStream = new PdfWriter("/Users/matthewpaden/Downloads/White_and_Blue_Illustrative_Menu4.pdf");
                        //PdfReader inStream = new PdfReader("/Users/matthewpaden/Downloads/White_and_Blue_Illustrative_Menu.pdf");
                        //var pdfdoc = new PdfDocument(inStream, outStream);
                        float height = o.Height;
                        float width = o.Width;
                        float xaxis = o.Xaxis;
                        float yaxis = o.Yaxis;
                        Stream outStream = Console.OpenStandardOutput();
                        Stream inStream = Console.OpenStandardInput();
                        var writeProp = new WriterProperties();
                        var readProp = new ReaderProperties();
                        var writer = new PdfWriter(outStream, writeProp);
                        var reader = new PdfReader(inStream, readProp);
                        var pdfdoc = new PdfDocument(reader, writer);
                        PdfCleanUpLocation cleanUpLocations = new PdfCleanUpLocation(1,
                             new Rectangle(xaxis, yaxis, width, height), ColorConstants.WHITE);
                        PdfCleanUpTool cleaner = new PdfCleanUpTool(pdfdoc);
                        cleaner.AddCleanupLocation(cleanUpLocations);
                        cleaner.CleanUp();
                        pdfdoc.Close();
                        Environment.Exit(0);

                    }
                    else
                    {
                        Console.WriteLine("Something went wrong");
                    }
                });
        }
    }
}
