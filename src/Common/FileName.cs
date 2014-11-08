using System;
using System.IO;
using System.Linq;

namespace Common {
    public class FileName {
        public string FileNameString;

        public FileName() {
            FileNameString = "";
        }

        public FileName(string fileNameString) {
            var invalidFileNameChars = Path.GetInvalidFileNameChars().Except(new[]{'\\'}).ToArray();
            if (fileNameString.IndexOfAny(invalidFileNameChars) != -1)
                throw new ArgumentException("file name contains illegal characters", "fileNameString");
            FileNameString = fileNameString;
        }

        public FileInfo GetFile() {
            return new FileInfo(FileNameString);
        }

        public override string ToString() {
            return FileNameString;
        }
    }
}