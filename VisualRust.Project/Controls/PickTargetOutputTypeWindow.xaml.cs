﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VisualRust.Cargo;

namespace VisualRust.Project.Controls
{
    partial class PickTargetOutputTypeWindow : ChildWindow
    {
        public IList<OutputTargetType> Targets { get; private set; }
        public OutputTargetType SelectedTarget { get; private set; }

        public PickTargetOutputTypeWindow()
        {
            InitializeComponent();
        }

        public PickTargetOutputTypeWindow(Window owner, OutputTargetSectionViewModel viewModel) : base(owner)
        {
            InitializeComponent();
            Title = "Select type";
            DataContext = this;
            IOutputTargetViewModel libraryVm = viewModel.Targets.First(vm => vm.Type == OutputTargetType.Library);
            if(libraryVm.IsAutoGenerated)
            {
                Targets = new OutputTargetType[]
                {
                    OutputTargetType.Library,
                    OutputTargetType.Binary,
                    OutputTargetType.Benchmark,
                    OutputTargetType.Test,
                    OutputTargetType.Example
                };
            }
            else
            {
                Targets = new OutputTargetType[]
                {
                    OutputTargetType.Binary,
                    OutputTargetType.Benchmark,
                    OutputTargetType.Test,
                    OutputTargetType.Example
                };
            }
            SelectedTarget = Targets[0];
        }

        public static OutputTargetType? Start(OutputTargetSectionViewModel viewModel)
        {
            var window = new PickTargetOutputTypeWindow(Application.Current.MainWindow, viewModel);
            bool? result = window.ShowDialog();
            if(result == true)
                return window.SelectedTarget;
            else
                return null;
        }

        void OnOk(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        void OnCancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
