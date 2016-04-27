using System;
using System.IO;

namespace ImageLoader
{
    public class Loader
    {

        /*
        Baby Drawing
        3 5
        255, 6, 65, 78, 99
        100, 25, 0, 45, 66
        88, 190, 88, 76, 50
        */

        
        public static Image LoadImageFromFile(string prmImgFilePath)
        {
            ValidateFilePath(prmImgFilePath);
            var img = new Image();
            StreamReader rdrFile = null;
            try
            {
                rdrFile = new StreamReader(prmImgFilePath);
            }
            catch (Exception ex)
            {

                throw new Exception("Error reading the input file.", ex);
            }
            using (rdrFile)
            {
                string line;
                var count = 0;
                var rowCount = 0;
                while ((line = rdrFile.ReadLine()) != null)
                {

                    ++count;
                    switch (count)
                    {
                        //Process file name ......
                        case 1:
                        {
                            ProcessName(img, line);
                            break;
                        }
                        //Process the number of rows and columns...
                        case 2:
                        {
                            ProcessMetadata(line, img);

                            break;
                        }
                        //Process th data now..
                        default:
                        {
                                //TODO: Check if the file actually contians actual or more no. of rows...
                                if (rowCount < img.Rows)
                            {
                                ProcessData(line, img, rowCount);
                            }
                            ++rowCount;
                            break;
                        }
                    }
                }
            }

            return img;
        }

        #region Private Methods.....

        /// <summary>
        /// Utility method to validate the file path.........
        /// </summary>
        /// <param name="prmFileName"></param>
        private static void ValidateFilePath(string prmFileName)
        {
            if(!File.Exists(prmFileName)) throw new Exception("Invalid file path. The fiven file does not exist..");
        }

        /// <summary>
        /// Utility method to process the Name inside the file.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="line"></param>
        private static void ProcessName(Image img, string line)
        {
            if(string.IsNullOrEmpty(line))
                throw new Exception("Invalid file name..");
            img.Name = $"{line}.img";
        }

        /// <summary>
        /// Utility method to process the actual pixel data..
        /// </summary>
        /// <param name="prmLine"></param>
        /// <param name="img"></param>
        /// <param name="rowCount"></param>
        private static void ProcessData(string prmLine, Image img, int rowCount)
        {
            var ary = prmLine.Split(new char[] {','});
            if (ary.Length < img.Columns)
                throw new Exception("Invalid number of columns...");
            for (var i = 0; i < img.Columns; ++i)
            {
                int val;
                int.TryParse(ary[i], out val);
                img.Data[rowCount, i] = val;
            }
        }

        /// <summary>
        /// Utility method to process the rows / column information.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="img"></param>
        private static void ProcessMetadata(string line, Image img)
        {
            var ary = line.Split(new char[] {' '});
            if(ary.Length < 2)
                throw new Exception("Invlaid metadata.. Cannot determine the number of rows and columns..");

            int rows, columns;
            if (int.TryParse(ary[0], out rows))
            {
                img.Rows = rows;
            }
            if (int.TryParse(ary[1], out columns))
            {
                img.Columns = columns;
            }

            img.Data = new int[rows,columns];
        }
        #endregion
    }
}
