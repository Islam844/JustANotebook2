using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests
{
    class Program
    {
        static void Main(string[] args)
        {
            using(DBContext db = new DBContext())
            {
                var notes = db.Notes;
                foreach(var note in notes)
                {
                    Console.WriteLine(note.Id+" "+note.Title+" "+note.Description);
                }
            }
            Console.Read();
        }
    }
}
