﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.IO;
using Microsoft.Win32;
using TiaFileViewer.Models;
using Prism.Mvvm;
using Prism.Commands;


namespace TiaFileViewer
{
    class MainWindowViewModel : BindableBase
    {
        private IEnumerable<Node> _nodes;
        private ObservableCollection<Tuple<string, int>> _types = new ObservableCollection<Tuple<string, int>>();

        public DelegateCommand LoadFileCommand { get; private set; }
        

        public ObservableCollection<Tuple<string, int>> Types
        {
            get => _types;
            set
            {
                SetProperty(ref _types, value);
            }
        }


        public IEnumerable<Node> Nodes
        {
            get => _nodes;
            set
            {
                SetProperty(ref _nodes, value);
                var types = value.Select(x => x.NodeType).Distinct();
                Types.Clear();
                Types.AddRange(types.Select(x => new Tuple<string, int>(x, value.Count(y => y.NodeType == x))));
            }
        }
        

        public MainWindowViewModel()
        {
            LoadFileCommand = new DelegateCommand(loadFile);
        }

        private void loadFile()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "tia-Dateien|*.tia";
            if (dialog.ShowDialog() == true)
                Nodes = Node.FromTiaFile(dialog.FileName);
        }

        
    }
}