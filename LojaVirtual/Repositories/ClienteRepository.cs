using LojaVirtual.Database;
using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories.Contracts
{
    public class ClienteRepository : IClienterepository
    {

        private LojaVirtualContext _banco;

        public ClienteRepository(LojaVirtualContext banco) 
        {
            _banco = banco;    
        }

        public void Create(Cliente cliente)
        {
            _banco.Add(cliente);
            _banco.SaveChanges();
        }

        public void Delete(int id)
        {
            _banco.Remove(Get(id));
            _banco.SaveChanges();
        }

        public Cliente Get(int id)
        {
            return _banco.clientes.Find(id);
        }

        public IEnumerable<Cliente> GetAll()
        {
            return _banco.clientes.ToList(); 
        }

        public Cliente Login(string email, string senha)
        {
            Cliente cliente = _banco.clientes.Where(m => m.email == email && m.senha == senha).FirstOrDefault();

            return cliente;
        }

        public void Update(Cliente cliente)
        {
            _banco.Update(cliente);
            _banco.SaveChanges();
        }

        List<Cliente> IClienterepository.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
