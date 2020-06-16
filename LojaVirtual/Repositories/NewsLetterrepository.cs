using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories
{
    public class NewsLetterrepository : INewsLetterRepository
    {
        private LojaVirtualContext _banco;
        public NewsLetterrepository(LojaVirtualContext banco) 
        {
            _banco = banco;
        }

        public void Create(NewsLetterEmail newsLetterEmaill)
        {
            _banco.newsLetterEmails.Add(newsLetterEmaill);
            _banco.SaveChanges();
        }

        public IEnumerable<NewsLetterEmail> GetAll()
        {
            return _banco.newsLetterEmails.ToList();
        }
    }
}
