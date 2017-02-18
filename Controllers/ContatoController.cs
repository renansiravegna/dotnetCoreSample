using Microsoft.AspNetCore.Mvc;
using WebAPIApplication.ViewModels;
using WebAPIApplication.Daos;

namespace WebAPIApplication.Controllers
{
    [Route("api/[controller]")]
    public class ContatosController : Controller
    {
        private readonly IContatoDao _contatoDao;

        public ContatosController(IContatoDao contatoDao)
        {
            _contatoDao = contatoDao;
        }

        public string Get()
        {
            return _contatoDao.ObterTodos();
        }

        [HttpPost]
        public void Post([FromBody]ContatoViewModel novoContato)
        {
            _contatoDao.Adicionar(novoContato.Nome, novoContato.Telefone);
        }
    }
}