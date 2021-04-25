using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace JustANotebook2.Model
{
    class Note
    {
        public string id { get; set; }
        public string Title { set; get; }
        public string Description { set; get; }
    }
}
