using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics.CodeAnalysis;

namespace QuickReplicate
{
    class Compare : IEqualityComparer<FileInfo>
    {
        public bool Equals(FileInfo sourceFile, FileInfo destinationFile)
        {
            return (sourceFile.FullName.Equals(destinationFile.FullName) && sourceFile.FullName.Length.Equals(destinationFile.FullName.Length));
        }

        public int GetHashCode(FileInfo obj)
        {
            return (obj.FullName + " ").GetHashCode();
        }
    }
}
