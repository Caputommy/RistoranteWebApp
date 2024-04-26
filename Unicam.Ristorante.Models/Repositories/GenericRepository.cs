using Unicam.Ristorante.Models.Context;

namespace Unicam.Ristorante.Models.Repositories
{
    public abstract class GenericRepository<T> where T : class
    {
        protected MyDbContext _ctx;
        public GenericRepository(MyDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AggiungiAsync(T entity)
        {
            await _ctx.Set<T>().AddAsync(entity);
        }

        public void Modifica(T entity)
        {
            _ctx.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public async Task<T> OttieniAsync(object id)
        {
            return await _ctx.Set<T>().FindAsync(id);
        }

        public async Task EliminaAsync(object id)
        {
            var entity = await OttieniAsync(id);
            _ctx.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public async Task SaveAsync()
        {
            await _ctx.SaveChangesAsync();
        }

    }
}
