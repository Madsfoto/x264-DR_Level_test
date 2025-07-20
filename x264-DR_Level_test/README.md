This program will create a bat file with every combination in libx264 of profile (baseline, main, high, high10, high422 and high444), 
AVC level (4.2, 5, 5.1, 5.2, 6, 6.1, and 6.2) with the usual pixel formats (yuv420p, yuv422p, yuv444p, yuv420p10le, yuv422p10le, yuv44p10le),
in 1080p and 2160p resolution and finally with formats usually encountered (mov, mp4,mxf, mts, mkv).

The removal of invalid combinations (for example yuv420p10le in main) is left as an exercise to the reader.