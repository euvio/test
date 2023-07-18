using Prism.Mvvm;
using System;
using System.Collections.Generic;

namespace 折叠板
{
    public class ConfigItem
    {
        public string Description { get; set; }
        public string JsonValue { get; set; }
        public string Name { get; set; }
        public Type Type => typeof(int);
        public string Unit { get; set; }
    }

    public class MainViewModel : BindableBase
    {
        public MainViewModel()
        {
            TreeNode root = new TreeNode();
            root.Name = "Root";
            root.Children = new List<TreeNode>();
            root.Children.Add(new TreeNode() { Name = "A" });
            root.Children.Add(new TreeNode()
            {
                Name = "B",
                Children = new List<TreeNode>()
                {
                    new TreeNode()
                    {
                        Name = "2"
                    }
                }
            });
            root.Children.Add(new TreeNode()
            {
                Name = "C",
                Children = new List<TreeNode>()
                {
                    new TreeNode()
                    {
                        Name = "3",
                        Children = new List<TreeNode>()
                        {
                            new TreeNode(){Name = "c"}
                        }
                    }
                }
            });
            Tree = new List<TreeNode>();
            Tree.Add(root);
        }

        public IList<TreeNode> Tree { get; }
    }

    public class TreeNode
    {
        public IList<TreeNode> Children { get; set; }
        public string Name { get; set; }
    }
}