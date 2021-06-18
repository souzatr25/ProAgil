using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        public readonly ProAgilContexto _context;

        public ProAgilRepository(ProAgilContexto context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        //Gerais
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }        
        
        //Eventos

        public async Task<Evento[]> getAllEventosAsync(bool includePalestrantes = false)
        {
           IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedeSociais);

            if(includePalestrantes)
            {
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            query = query.AsNoTracking()
                         .OrderByDescending(c => c.DataEvento);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> getAllEventosAsyncByTema(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedeSociais);

            if(includePalestrantes)
            {
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            query = query.AsNoTracking()
                        .OrderByDescending(c => c.DataEvento)
                        .Where(c => c.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }
     

        public async Task<Evento> getAllEventosAsyncByID(int EventoId, bool includePalestrantes)
        {
             IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedeSociais);

            if(includePalestrantes)
            {
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            query = query.AsNoTracking()
                        .OrderByDescending(c => c.DataEvento)
                        .Where(c => c.Id == EventoId);

            return await query.FirstOrDefaultAsync();
        }

        

        //Palestrantes
        public async Task<Palestrante> getAllPalestranteAsyncByID(int PalestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes                
                .Include(c => c.RedeSociais);

            if(includeEventos)
            {
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(e => e.Evento);
            }

            query = query.AsNoTracking()
                       .OrderBy(p => p.Nome)
                       .Where(p => p.Id == PalestranteId);
                        

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> getAllPalestrantesAsyncByName(string name, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes                
                .Include(c => c.RedeSociais);

            if(includeEventos)
            {
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(e => e.Evento);
            }

            query = query.AsNoTracking()
                       .OrderBy(p => p.Nome)
                       .Where(p => p.Nome.ToLower().Contains(name.ToLower()));
                        

            return await query.ToArrayAsync();
        }

        

        
    }
}