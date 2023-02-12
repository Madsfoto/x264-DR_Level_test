using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace x264_DR_Level_test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputFilename = "";
            if (args.Length == 0)
            {
                Console.WriteLine("Defaulting to Source-FFv1.mkv, use 1st argument for other name");
                Console.WriteLine("Press filename and enter for new source name OR just enter to create default files");
                string consoleline = Console.ReadLine();
                if(consoleline.Length==0)
                {
                    inputFilename = "Source-FFv1.mkv"; // what my standard source file happens to be named.
                }
                else
                {
                    inputFilename = consoleline;
                }
                
            }
            else
            {
                inputFilename = args[1];
            }
            string[] profileArr = { "baseline", "main", "high", "high10", "high422", "high444" }; // profile is the total color space and depth available 
            //string[] presetArr = { "ultrafast", "superfast", "veryfast", "faster", "fast", "medium", "slow", "slower", "veryslow", "placebo" }; // every option
            string[] presetArr = { "superfast"}; // ultrafast forces baseline profile. The results are identical for the other presets.
            string[] levelsArr = { "1", "1b", "1.1", "1.2", "1.3", "2", "2.1", "2.2", "3", "3.1", "3.2", "4", "4.1", "4.2", "5", "5.1", "5.2", "6", "6.1", "6.2" };
            string[] pix_fmtArr = { "yuv420p", "yuv422p", "yuv444p", "yuv420p10le", "yuv422p10le", "yuv444p10le" }; // All colorspaces
            

            string inputFilenameNoExt = inputFilename.Substring(0, inputFilename.Length - 4); // remove the last 4 characters from input: often ".mov"

            string ff = "ffmpeg -i " + inputFilename + " -c:v libx264 ";
            string profileStr = "-profile:v ";
            string presetStr = "-preset ";
            string levelStr = "-level:v ";
            string CRFStr = "-crf 51";
            string pix_fmtStr = "-pix_fmt ";
            string noaudio = "-an";
            

            List<string> writeStrList = new List<string>();
            string outputFilename = "";

            for (int profileInt = 0; profileInt < profileArr.Length; profileInt++)
            {
                for (int presetInt = 0; presetInt < presetArr.Length; presetInt++)
                {
                    for (int levelsInt = 0; levelsInt < levelsArr.Length; levelsInt++)
                    {
                        for (int pix_fmtInt = 0; pix_fmtInt < pix_fmtArr.Length; pix_fmtInt++)
                        {

                            string FF_encode_cmd = ff + profileStr + profileArr[profileInt] + " " + presetStr + presetArr[presetInt] + " " + 
                                levelStr + levelsArr[levelsInt] + " " + pix_fmtStr + pix_fmtArr[pix_fmtInt]+ " "+CRFStr + " "+noaudio;

                            
                            outputFilename = inputFilenameNoExt + "_" + profileInt + profileArr[profileInt] + "_" + 
                                presetInt + presetArr[presetInt] + "_" + "L" + levelsArr[levelsInt] + "_" + pix_fmtArr[pix_fmtInt] + ".mov";

                            writeStrList.Add(FF_encode_cmd + " " + outputFilename);


                        }


                    }

                    File.WriteAllLines("0-" + profileInt + profileArr[profileInt] + "-" + presetInt + presetArr[presetInt] + ".bat", writeStrList);
                    writeStrList.Clear();
                }
            }

            }
    }
}
