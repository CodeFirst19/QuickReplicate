using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace QuickReplicate
{
    class Replicate
    {
        private string sourceDirectory;
        private string destinationDirectory;
        private bool subDirChecked;
        private bool mirrorChecked;
        bool DirareSimilar;

        public string Message { get; set; }

        IEnumerable<FileInfo> sourceDirectorylist;
        IEnumerable<FileInfo> destinationDirectorylist;

        Compare compare;

        public Replicate(string sourceDirectory, string destinationDirectory, bool subDirChecked, bool mirrorChecked)
        {
            this.sourceDirectory = sourceDirectory;
            this.destinationDirectory = destinationDirectory;
            this.subDirChecked = subDirChecked;
            this.mirrorChecked = mirrorChecked;
        }

        public void Compare()
        {
            if(sourceDirectory != destinationDirectory)
            {
                if (subDirChecked)
                {
                    CompareSubDir();
                }
                else
                {
                    DirectoryInfo SourceDirInfo = new DirectoryInfo(sourceDirectory);
                    DirectoryInfo DestinationDirInfo = new DirectoryInfo(destinationDirectory);

                    sourceDirectorylist = SourceDirInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly);
                    destinationDirectorylist = DestinationDirInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly);

                    DirareSimilar = sourceDirectorylist.SequenceEqual(destinationDirectorylist, compare = new Compare());

                    if (!DirareSimilar)
                    {
                        string[] sourceFiles = Directory.GetFiles(sourceDirectory, "*", SearchOption.TopDirectoryOnly);
                        string[] destinationFiles = Directory.GetFiles(sourceDirectory, "*", SearchOption.TopDirectoryOnly);
                        destinationFiles = sourceFiles;
                        Message = "File successfully replicated";
                    }
                }
            }
        }
        public void CompareSubDir()
        {
            DirectoryInfo SourceDirInfo = new DirectoryInfo(sourceDirectory);
            DirectoryInfo DestinationDirInfo = new DirectoryInfo(destinationDirectory);

            sourceDirectorylist = SourceDirInfo.GetFiles("*.*", SearchOption.AllDirectories);
            destinationDirectorylist = DestinationDirInfo.GetFiles("*.*", SearchOption.AllDirectories);

            DirareSimilar = sourceDirectorylist.SequenceEqual(destinationDirectorylist, compare = new Compare());

            if (!DirareSimilar)
            {
                string[] sourceFiles = Directory.GetFiles(sourceDirectory, "*", SearchOption.AllDirectories);
                string[] destinationFiles = Directory.GetFiles(sourceDirectory, "*", SearchOption.AllDirectories);
                destinationFiles = sourceFiles;
                Message = "File successfully replicated";
            }
        }
    }
}
