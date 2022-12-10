using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Model.Context;
using RestWithASPNETUdemy.Repository;

namespace RestWithASPNETUdemy.Business.Implementations
{

    public class PersonBusinessImplementation : IPersonBusiness
    {
        private IPersonRepository
            _repository;
        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
        }
        public Person Create(Person person)
        {
        
            return _repository.Create(person);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);


        }

        public List<Person> FindAll()
        {
            return _repository.FindAll();
        }

        public Person FindByID(long id)
        {
            return _repository.FindByID(id);
        }

        public Person Update(Person person)
        {
            return _repository.Update(person);
        }


    }
}
