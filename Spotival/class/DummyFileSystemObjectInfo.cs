using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotival.classes
{
    internal class DummyFileSystemObjectInfo : FileSystemObjectInfo
    {
        public DummyFileSystemObjectInfo()
            : base(new DirectoryInfo("DummyFileSystemObjectInfo"))
        {
        }
    }
}
