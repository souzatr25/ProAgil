using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
        //Geral
         void Add<T>(T entity) where T : class;

         void Update<T>(T entity) where T : class;

         void Delete<T>(T entity) where T : class;

        Task<bool> SaveChangesAsync();

        //Eventos

        Task<Evento[]> getAllEventosAsyncByTema(string tema, bool includePalestrantes);

        Task<Evento[]> getAllEventosAsync(bool includePalestrantes);

        Task<Evento> getAllEventosAsyncByID(int EventoId, bool includePalestrantes);

        //Palestrante
        Task<Palestrante[]> getAllPalestrantesAsyncByName(string name, bool includeEventos);

        Task<Palestrante> getAllPalestranteAsyncByID(int PalestranteId, bool includeEventos);


    }
}