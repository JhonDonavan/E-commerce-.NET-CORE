using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LojaVirtual.Repositories.Contracts
{
    public interface IClienterepository
    {
        Cliente Login(String email, string senha);

        void Create(Cliente cliente);

        void Update(Cliente cliente);

        void Delete(int id);

        Cliente Get(int id);

        List<Cliente> GetAll();

    }
}
