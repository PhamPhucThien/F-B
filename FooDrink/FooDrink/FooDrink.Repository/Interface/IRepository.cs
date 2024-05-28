﻿using FooDrink.DTO.Request;

namespace FooDrink.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<T> AddAsync(T entity);
        Task<bool> EditAsync(T entity);
        Task<bool> DeleteByIdAsync(Guid id);
        IEnumerable<T> GetWithPaging(IPagingRequest pagingRequest);
    }
}
