﻿using System;
using VisualRust.Cargo;

namespace VisualRust.Project.Controls
{
    internal class BinaryAutoOutputTargetViewModel : AutoOutputTargetViewModel
    {
        public BinaryAutoOutputTargetViewModel(Manifest m) : base(m)
        {
        }

        public override OutputTargetType Type { get { return OutputTargetType.Binary; } }
        public override string TabName { get { return "Autodetect application"; } }
        public override string Name { get { return Manifest.Name; } }
        public override string Path { get { return @"src\main.rs"; } }
        public override bool Doc { get { return true; } }
    }
}