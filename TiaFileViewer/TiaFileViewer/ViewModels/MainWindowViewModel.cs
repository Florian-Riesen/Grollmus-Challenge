using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using Microsoft.Win32;
using TiaFileViewer.Models;
using Prism.Mvvm;
using Prism.Commands;


namespace TiaFileViewer.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        private IEnumerable<Node> _allNodes;
        private ObservableCollection<Tuple<string, int>> _types = new ObservableCollection<Tuple<string, int>>();
        private Tuple<string, int> _selectedType;
        private Dictionary<string, int> _namedProperties;
        private string _windowTitle;
        private bool _useAllPropertiesForSummarization;

        public string WindowTitle
        {
            get => _windowTitle;
            set => SetProperty(ref _windowTitle, value);
        }

        public bool UseAllPropertiesForSummarization
        {
            get => _useAllPropertiesForSummarization;
            set
            {
                SetProperty(ref _useAllPropertiesForSummarization, value);
                SelectedType = SelectedType; // trigger a refresh of all properties when the radiobutton choice changed
            }
        }

        public Tuple<string, int> SelectedType
        {
            get => _selectedType;
            set
            {
                SetProperty(ref _selectedType, value);
                if (value == null)
                    return;
                else
                    NamedProperties = Node.SummarizeProperties(AllNodes.Where(x => x.NodeType == value.Item1), UseAllPropertiesForSummarization);
            }
        }

        public Dictionary<string, int> NamedProperties
        {
            get => _namedProperties;
            set
            {
                SetProperty(ref _namedProperties, value);
            }
        }
        public ObservableCollection<Tuple<string, int>> Types
        {
            get => _types;
            set
            {
                SetProperty(ref _types, value);
            }
        }

        public IEnumerable<Node> AllNodes
        {
            get => _allNodes;
            set
            {
                _allNodes = value ?? new List<Node>();
                if (value == null)
                    return;
                var types = value.Select(x => x.NodeType).Distinct();
                Types.Clear();
                Types.AddRange(types.Select(x => 
                    new Tuple<string, int>(
                        x, 
                        value.Count(y => y.NodeType == x))));
            }
        }

        public DelegateCommand LoadFileCommand { get; private set; }


        public MainWindowViewModel()
        {
            LoadFileCommand = new DelegateCommand(loadFile);
            WindowTitle = "TIA Selection Tool - Datei-Viewer";
            UseAllPropertiesForSummarization = true;
        }

        private void loadFile()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "tia-Dateien|*.tia";
            if (dialog.ShowDialog() == true)
            {
                AllNodes = Node.FromTiaFile(dialog.FileName) ?? AllNodes;
                var shortName = new FileInfo(dialog.FileName).Name;
                WindowTitle = "TIA Selection Tool - Datei-Viewer - \"" + shortName + "\"";
            }
        }
    }
}
