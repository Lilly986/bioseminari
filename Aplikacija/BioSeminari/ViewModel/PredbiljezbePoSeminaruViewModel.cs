using MSDNRoles.Models;
using System.Collections.Generic;

namespace MSDNRoles.ViewModel
{
    public class PredbiljezbePoSeminaruViewModel
    {
        public List<Seminar> SviSeminari { get; set; }
        public List<Predbiljezba> PredbiljezbePoSeminaru { get; set; }
    }
}