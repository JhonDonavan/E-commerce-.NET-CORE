using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LojaVirtual.Repositories.Contracts
{
    public interface INewsLetterRepository
    {
        void Create(NewsLetterEmail newsLetterEmaill);

        IEnumerable<NewsLetterEmail> GetAll();
    }
}
