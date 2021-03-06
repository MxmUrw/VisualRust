﻿using System;
using System.Runtime.CompilerServices;
using VisualRust.Cargo;

namespace VisualRust.Project.Controls
{
    public class OutputTargetViewModel : ViewModelBase, IOutputTargetViewModel
    {
        public OutputTargetType Type { get; private set; }

        readonly Manifest manifest;

        public string TabName { get { return this.Name; } }

        public string DefaultName { get { return manifest.Name; } }
        private string name;
        public string Name
        {
            get { return name ?? manifest.Name; }
            set
            {
                if(SetInvalidate(ref name, value))
                {
                    RaisePropertyChanged("IsNameOverriden");
                    RaisePropertyChanged("Path");
                    RaisePropertyChanged("TabName");
                }
            }
        }
        public string RawName { get { return name; } }
        public bool IsNameOverriden
        {
            get
            {
                return name != null;
            }
            set
            {
                if(value)
                    Name = DefaultName;
                else
                    Name = null;
            }
        }


        public string DefaultPath { get { return Type.DefaultPath(Name ?? DefaultName); } }
        private string path;
        public string Path
        {
            get { return path; }
            set
            {
                if(SetInvalidate(ref path, value))
                    RaisePropertyChanged("IsPathOverriden");
            }
        }
        public string RawPath { get { return path; } }
        public bool IsPathOverriden
        {
            get
            {
                return path != null;
            }
            set
            {
                if(value)
                    Path = DefaultPath;
                else
                    Path = null;
            }
        }
        
        private bool? test;
        public bool Test
        {
            get { return test ?? Type.DefaultTest() ; }
            set { SetInvalidate(ref test, value); }
        }
        public bool? RawTest { get { return test; } }
        
        private bool? doctest;
        public bool Doctest
        {
            get { return doctest ?? Type.DefaultDoctest(); }
            set { SetInvalidate(ref doctest, value); }
        }
        public bool? RawDoctest { get { return doctest; } }

        private bool? bench;
        public bool Bench
        {
            get { return bench ?? Type.DefaultBench(); }
            set { SetInvalidate(ref bench, value); }
        }
        public bool? RawBench { get { return bench; } }

        private bool? doc;
        public bool Doc
        {
            get { return doc ?? Type.DefaultDoc(); }
            set { SetInvalidate(ref doc, value); }
        }
        public bool? RawDoc { get { return doc; } }

        private bool? plugin;
        public bool Plugin
        {
            get { return plugin ?? Type.DefaultPlugin(); }
            set { SetInvalidate(ref plugin, value); }
        }
        public bool? RawPlugin { get { return plugin; } }

        private bool? harness;
        public bool Harness
        {
            get { return harness ?? Type.DefaultHarness(); }
            set { SetInvalidate(ref harness, value); }
        }
        public bool? RawHarness { get { return harness; } }

        public bool IsAutoGenerated { get { return false; } }
        public RelayCommand Remove { get; private set; }
        public UIntPtr? Handle { get; internal set; }

        private OutputTargetSectionViewModel parent;

        public OutputTargetViewModel(Manifest m, OutputTargetSectionViewModel parent, OutputTarget t)
        {
            this.manifest = m;
            Type = t.Type;
            name = t.Name;
            path = t.Path;
            test = t.Test;
            doctest = t.Doctest;
            bench = t.Bench;
            doc = t.Doc;
            plugin = t.Plugin;
            harness = t.Harness;
            Handle = t.Handle;
            this.parent = parent;
            Remove = new RelayCommand(() => parent.Remove(this));
        }

        private bool SetInvalidate<T>(ref T field, T value, [CallerMemberName] string property = null)
        {
            if(!Set(ref field, value, property))
                return false;
            parent.IsDirty = true;
            return true;
        }

        public OutputTarget ToOutputTarget()
        {
            return new OutputTarget(this.Type)
            {
                Name = this.name,
                Path = this.path,
                Test = this.test,
                Doctest = this.doctest,
                Bench = this.bench,
                Doc = this.doc,
                Plugin = this.plugin,
                Harness = this.harness
            };
        }
    }
}