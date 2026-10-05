using DulcesPro.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;

namespace DulcesPro.Service
{
    public interface IDulcesService<TDulces, TInsert, TUpdate> 
    {
        public List<String> Errors { get; }
       public Task<IEnumerable<TDulces>> Get();

        public Task<TDulces?> GetById(int id);
        public Task<TDulces?> Update(int id, TUpdate dto);
        public Task<TDulces> Insert(TInsert PostDto);
        public Task <bool>Delete(int Id);
        public Task<bool> ValidateUpdate(TUpdate dto, int id);
        public Task<bool> ValidateInsert(TInsert dto);
        public Task<bool> ValidateDelete(int id);
        

    }
}
