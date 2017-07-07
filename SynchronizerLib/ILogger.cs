using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronizerLib
{
    public interface ISyncLogger
    {
        void Debug(string message);
    }
}
