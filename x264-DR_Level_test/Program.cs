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
            Console.WriteLine("Creating a bat file with all the profiles, levels, pixel formats and containers that H.264 supports in 1080p and 2160p");
            Console.WriteLine("By generating testsrc we get a known good video output and are therefore independent of any video on the system");

            string[] profileArr = { "baseline", "main", "high", "high10", "high422", "high444" }; // profile is the total color space and depth available 
            string[] levelsArr = new string[] { "4.2", "5", "5.1", "5.2", "6", "6.1", "6.2" };

            string[] pix_fmtArr = new string[] { "yuv420p", "yuv422p", "yuv444p", "yuv420p10le", "yuv422p10le", "yuv444p10le" }; // All colorspaces
            string[] resolutionArr = new string[] { "1920x1080", "3840x2160" };
            string[] containerArr = new string[] { ".mov", ".mp4", ".mxf", ".mts", ".mkv" };

            string ff = "ffmpeg -n -f lavfi -i testsrc=size=";
            string codec = "-c:v libx264";
            string pix_fmtStr = "-pix_fmt";
            string noaudio = "-an";
            string time = "-t 30";
            string profileStr = "-profile:v";
            string presetStr = "-preset superfast";
            string levelStr = "-level:v";


            List<string> writeStrList = new List<string>();

            for (int profileInt = 0; profileInt < profileArr.Length; profileInt++)
            {
                for (int levelsInt = 0; levelsInt < levelsArr.Length; levelsInt++)
                {
                    for (int pix_fmtInt = 0; pix_fmtInt < pix_fmtArr.Length; pix_fmtInt++)
                    {
                        for (int resolutionInt = 0; resolutionInt < resolutionArr.Length; resolutionInt++)
                        {
                            for (int containerInt = 0; containerInt < containerArr.Length; containerInt++)
                            {
                                string ffEncode = ff+resolutionArr[resolutionInt]+ " "+ codec+ " "+ presetStr+ " "+ profileStr+ " "+ profileArr[profileInt]+ " "+ levelStr+ " "+ levelsArr[levelsInt]+ " "+ pix_fmtStr+ " "+ pix_fmtArr[pix_fmtInt]+ " "+ noaudio+ " "+ time+ " ";
                                string outputFilename = "testsrc_"+ resolutionArr[resolutionInt]+ "_"+ profileArr[profileInt]+ "_"+ levelsArr[levelsInt]+ "_"+ pix_fmtArr[pix_fmtInt]+ containerArr[containerInt];
                                writeStrList.Add(ffEncode + outputFilename);
                            }
                        }
                    }
                }
            }
            File.WriteAllLines("0.bat", writeStrList);
        }
    }
}



