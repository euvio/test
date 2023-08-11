using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace 日期控件WPF.Toolkit
{
    public class MainViewModel
    {
        public DelegateCommand<Window> LoadedCommand { get; } = new DelegateCommand<Window>((view) =>
        {
            MessageBox.Show("ViewModel Loaded...");
        });

        public List<Product> Products { get; set; } = new List<Product>()
        {
            new Product()
            {
                Name = "11111111111111111111",
                Model = "One"
            },
            new Product()
            {
                Name = "22222222222222222222",
                Model = "Two"
            },
            new Product()
            {
                Name = "3333333333333333",
                Model = "Three"
            },
            new Product()
            {
                Name = "4444444444444444444",
                Model = "Four"
            },
            new Product()
            {
                Name = "555555555555555555555",
                Model = "Five"
            }
        };
    }
}
