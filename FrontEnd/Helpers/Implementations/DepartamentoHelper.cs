using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEnd.Helpers.Implementations
{
    public class DepartamentoHelper : IDepartamentoHelper
    {
        private readonly IServiceRepository _repository;

        public DepartamentoHelper(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<DepartamentoViewModel>> GetDepartamentos()
        {
            var responseMessage = await _repository.GetResponse("api/Departamentos");
            if (responseMessage.IsSuccessStatusCode)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                var departamentos = JsonConvert.DeserializeObject<List<Departamento>>(content);
                return departamentos.ConvertAll(d => Convertir(d));
            }
            return new List<DepartamentoViewModel>();
        }

        public async Task<DepartamentoViewModel> GetDepartamento(int id)
        {
            var responseMessage = await _repository.GetResponse($"api/Departamentos/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                var departamento = JsonConvert.DeserializeObject<Departamento>(content);
                return Convertir(departamento);
            }
            return null;
        }

        public async Task AddDepartamento(DepartamentoViewModel departamentoViewModel)
        {
            var departamentoApi = Convertir(departamentoViewModel);
            await _repository.PostResponse("api/Departamentos", departamentoApi);
        }

        public async Task UpdateDepartamento(DepartamentoViewModel departamentoViewModel)
        {
            var departamentoApi = Convertir(departamentoViewModel);
            await _repository.PutResponse($"api/Departamentos/{departamentoViewModel.Id}", departamentoApi);
        }

        public async Task DeleteDepartamento(int id)
        {
            await _repository.DeleteResponse($"api/Departamentos/{id}");
        }

        private Departamento Convertir(DepartamentoViewModel departamentoViewModel)
        {
            return new Departamento
            {
                Id = departamentoViewModel.Id,
                NombreDepartamento = departamentoViewModel.NombreDepartamento,
                Descripcion = departamentoViewModel.Descripcion
            };
        }

        private DepartamentoViewModel Convertir(Departamento departamento)
        {
            return new DepartamentoViewModel
            {
                Id = departamento.Id,
                NombreDepartamento = departamento.NombreDepartamento,
                Descripcion = departamento.Descripcion
            };
        }
    }
}
