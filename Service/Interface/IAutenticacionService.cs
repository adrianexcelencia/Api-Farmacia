using APIformbuilder.Domains.DTOs;
using APIformbuilder.Models;

namespace APIformbuilder.Service.Interface
{
    public interface IAutenticacionService
    {
        Task<ValidarDTO> validar(LogUsuario request);
    }
}
