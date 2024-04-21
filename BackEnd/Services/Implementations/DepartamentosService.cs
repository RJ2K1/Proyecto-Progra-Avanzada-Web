using BackEnd.Models;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Services.Implementations
{
    public class DepartamentosService : IDepartamentosService
    {

        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public DepartamentosService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task<bool> Add(DepartamentosModel departamentomodel)
        {
            var departamento = Convertir(departamentomodel);
            await _unidadDeTrabajo.DepartamentosDAL.AddAsync(departamento);
            var result = _unidadDeTrabajo.Complete();
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var departamento = new Departamentos { Id = id };
            await _unidadDeTrabajo.DepartamentosDAL.RemoveAsync(departamento);
            var result = _unidadDeTrabajo.Complete();
            return result;
        }

        public async Task<DepartamentosModel> GetById(int id)
        {
            var departamento = await _unidadDeTrabajo.DepartamentosDAL.GetAsync(id);
            return Convertir(departamento);
        }

        public async Task<List<DepartamentosModel>> GetDepartamentos()
        {
  
            var departamentos = await _unidadDeTrabajo.DepartamentosDAL.GetAllAsync();
            var departamentosModelList = departamentos.Select(Convertir).ToList();
            return departamentosModelList;
        }

        public async Task<bool> Update(DepartamentosModel departamentoModel)
        {
            var departamento = await _unidadDeTrabajo.DepartamentosDAL.GetAsync(departamentoModel.Id);
            if (departamento == null)
            {
             
                return false;
            }

          
            departamento.Nombre = departamentoModel.NombreDepartamento; 
            departamento.Descripcion = departamentoModel.Descripcion;

            await _unidadDeTrabajo.DepartamentosDAL.UpdateAsync(departamento);
            var result = await _unidadDeTrabajo.CompleteAsync();
            return result;
        }


        private DepartamentosModel Convertir(Departamentos departamento)
        {

            return new DepartamentosModel
            {
                Id = departamento.Id,
                NombreDepartamento = departamento.Nombre,
                Descripcion =departamento.Descripcion
            };
        }
        private Departamentos Convertir(DepartamentosModel departamentoModel)
        {

            return new Departamentos
            {
                Id = departamentoModel.Id,
                Nombre = departamentoModel.NombreDepartamento,
                Descripcion = departamentoModel.Descripcion

            };
        }


    }
}
