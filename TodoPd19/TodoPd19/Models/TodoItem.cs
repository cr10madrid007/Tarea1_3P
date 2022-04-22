﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;

namespace TodoPd19.Models
{
    public class TodoItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string apellidos { get; set; }
        public string edad { get; set; }
        public string direccion { get; set; }
        public string puesto { get; set; }

        public string Notes { get; set; }
        public bool Done { get; set; }

    }
}
