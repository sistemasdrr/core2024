﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRRCore.Transversal.Common.JsonReader
{
    public class SftpSettings
    {
        public string? Host { get; set; }
        public int Port { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? RemotePath { get; set; }
        public string? FileName { get; set; }
    }
}
