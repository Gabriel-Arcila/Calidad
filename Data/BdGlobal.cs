using Calidad.Model;
using Microsoft.EntityFrameworkCore;

namespace Global.Data
{
    public interface IDataPais
    {
        Task<List<Pai>> ObtenerTodosLosPaises();
    }
    public class DataPais : IDataPais
    {
        private readonly DbNeoContext _cotext;

        public DataPais(DbNeoContext context)
        {
            this._cotext = context;
        }
        public async Task<List<Pai>> ObtenerTodosLosPaises(){
            return await _cotext.Pais.Where(p => p.Pestado == true).ToListAsync();
        }
    }

    public interface IDataEmpresa
    {
        Task<List<Empresa>> ObtenerEmpresasPorPaies(int idPais);
    }

    public class DataEmpresa : IDataEmpresa
    {
        private readonly DbNeoContext _cotext;

        public DataEmpresa(DbNeoContext context)
        {
            this._cotext = context;
        }
        public async Task<List<Empresa>> ObtenerEmpresasPorPaies(int idPais){
            return await this._cotext.Empresas.Where(e => e.IdPais == idPais && e.Eestado == true).ToListAsync();
        }
    }

    public interface IDataCentro
    {
        Task<List<Centro>> ObtenerTodosLosCentro();

        Task<List<Centro>> ObtenerCentrosPorEmpresa(int idEmpresa);
    }

    public class DataCentro : IDataCentro
    {
        private readonly DbNeoContext _cotext;

        public DataCentro(DbNeoContext context)
        {
            this._cotext = context;
        }
        public async Task<List<Centro>> ObtenerTodosLosCentro(){
            return await _cotext.Centros.Where(x => x.Cestado == true).ToListAsync();
        }
        public async Task<List<Centro>> ObtenerCentrosPorEmpresa(int idEmpresa){
            return await _cotext.Centros.Where(x => x.Cestado == true && x.IdEmpresa == idEmpresa).ToListAsync();
        }
    }

    public interface IDataDivision
    {
        Task<List<Division>> ObtenerDivisionDelCentro(int idCentro);
    }
    public class DataDivision : IDataDivision
    {
        private readonly DbNeoContext _cotext;

        public DataDivision(DbNeoContext context)
        {
            this._cotext = context;
        }
        public async Task<List<Division>> ObtenerDivisionDelCentro(int idCentro){
            return await this._cotext.Divisions.Where(d => d.IdCentro == idCentro && d.Destado == true).ToListAsync();
        }
    }

    public interface IDataLinea
    {
        Task<List<Linea>?> ObtenerLasLineasPorCentro(int idCentro);
        Task<List<Linea>> ObtenerTodasLasLineas();
        Task<List<Linea>> ObtenerLasLineasPorDivision(int idDivision);
        Task<List<Linea>> ObtenerTodasLasLineasDeLaEmpresa(int IdEmpresa);
    }
    public class DataLinea : IDataLinea
    {
        private readonly DbNeoContext _cotext;

        public DataLinea(DbNeoContext context)
        {
            this._cotext = context;
        }
        public async Task<List<Linea>?> ObtenerLasLineasPorCentro(int idCentro)
        {
            if(idCentro == 0){
                return await _cotext.Lineas.Where(x => x.Lestado == true).ToListAsync();
            }else if(idCentro == -1){
                return null;
            }else{
                return await _cotext.Lineas.Include(l => l.IdDivisionNavigation).Where(l => l.IdDivisionNavigation.IdCentro == idCentro && l.Lestado == true).ToListAsync();
            }
        }
        public async Task<List<Linea>> ObtenerTodasLasLineas(){
            return await _cotext.Lineas.OrderBy(l => l.Lnom).ToListAsync();
        }

        public async Task<List<Linea>> ObtenerTodasLasLineasDeLaEmpresa(int IdEmpresa){
            
            return await _cotext.Lineas.Where(l => l.IdDivisionNavigation.IdCentroNavigation.IdEmpresa == IdEmpresa).OrderBy(l => l.Lnom).ToListAsync();
        }

        public async Task<List<Linea>> ObtenerLasLineasPorDivision(int idDivision){
            return await _cotext.Lineas.Where(l => l.IdDivision == idDivision).ToListAsync();
        }
    }

    
}