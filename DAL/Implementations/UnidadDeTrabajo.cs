using DAL.Interfaces;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class UnidadDeTrabajo : IUnidadDeTrabajo
    {

      

        public IUsuariosDAL _usuariosDAL { get; }
        public ITicketsDAL _ticketsDAL { get; }

    private readonly ProyectoWebContext _context;

    public UnidadDeTrabajo(ProyectoWebContext proyectoWebContext,
                            IUsuariosDAL usuariosDAL,
                            ITicketsDAL ticketsDAL)
    {
        _context = proyectoWebContext;
        _usuariosDAL = usuariosDAL;
        _ticketsDAL = ticketsDAL;

    }


    public bool Complete()
    {
        try
        {
            _context.SaveChanges();
            return true;
        }
        catch (Exception)
        {

            return false;
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
}

