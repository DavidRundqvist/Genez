using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Infrastructure.IO {
    public static class DirectoryInfoExtensions {
        public static DirectoryInfo GetSubFolder(this DirectoryInfo self, string folderName) {
            return new DirectoryInfo(Path.Combine(self.FullName, folderName));
        }

        public static FileInfo GetFile(this DirectoryInfo self, string fileName) {
            return new FileInfo(Path.Combine(self.FullName, fileName));
        }
    }
}