using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using PruebaAFP.Models;
using Dapper;

namespace PruebaAFP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly string _connectionString;

        public EmpresaController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpPost]
        public IActionResult RegistrarEmpresa(Empresa empresa)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = @"INSERT INTO Empresa (EmpresaID, Nombre, RazonSocial, FechaRegistro, DetallesBitacora) 
                               VALUES (@EmpresaID, @Nombre, @RazonSocial, @FechaRegistro, @DetallesBitacora)";
                db.Execute(sql, empresa);
            }
            return Ok();
        }

        [HttpGet("{empresaId}")]
        public IActionResult GetInfoEmpresa(int empresaId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var empresa = db.QueryFirstOrDefault<Empresa>("GetInfoEmpresaID", new { EmpresaID = empresaId }, commandType: CommandType.StoredProcedure);
                if (empresa == null)
                    return NotFound();
                return Ok(empresa);
            }
        }

        [HttpGet("{empresaId}/departamentos")]
        public IActionResult GetDepartamentos(int empresaId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var departamentos = db.Query<Departamento>("GetDepartamentos", new { EmpresaID = empresaId }, commandType: CommandType.StoredProcedure);
                return Ok(departamentos);
            }
        }
    }
}
