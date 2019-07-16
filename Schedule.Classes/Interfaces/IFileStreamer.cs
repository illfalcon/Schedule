using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Classes.Interfaces
{
    public interface IFileStreamer
    {
        void SaveList<T>(string fileName, List<T> list);
        List<T> RestoreList<T>(string fileName);
    }
}
