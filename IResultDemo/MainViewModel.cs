using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IResultDemo
{
    internal class MainViewModel
    {
        public IEnumerable<IResult> GoForward()
        {
            yield return Loader.Show("Downloading...");
            yield return Loader.Hide();
        }
    }
}
