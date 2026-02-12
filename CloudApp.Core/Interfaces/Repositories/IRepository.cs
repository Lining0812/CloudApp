using CloudApp.Core.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace CloudApp.Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        #region 同步方法

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll();
        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T? GetById(int id);
        /// <summary>
        /// 判断实体是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Exists(int id);
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity);
        /// <summary>
        /// 批量添加实体
        /// </summary>
        /// <param name="entities"></param>
        public void AddRange(IEnumerable<T> entities);
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity);
        /// <summary>
        /// 批量更新实体
        /// </summary>
        /// <param name="entities"></param>
        public void UpdateRange(IEnumerable<T> entities);
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity);
        /// <summary>
        /// 通过主键删除实体
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id);
        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="entities"></param>
        public void DeleteRange(IEnumerable<T> entities);
        /// <summary>
        /// 统一保存修改
        /// </summary>
        public int SaveChange();
        /// <summary>
        /// 获取实体数量
        /// </summary>
        public int Count();
        // 事务相关方法
        IDbContextTransaction BeginTransaction();
        #endregion

        #region 异步方法
        /// <summary>
        /// 获取所有实体（异步）
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync();
        /// <summary>
        /// 根据Id获取实体（异步）
        /// </summary>
        Task<T?> GetByIdAsync(int id);
        /// <summary>
        /// 判断实体是否存在（异步）
        /// </summary>
        Task<bool> ExistsAsync(int id);
        /// <summary>
        /// 添加实体（异步）
        /// </summary>
        Task AddAsync(T entity);
        /// <summary>
        /// 批量添加实体（异步）
        /// </summary>
        Task AddRangeAsync(IEnumerable<T> entities);
        /// <summary>
        /// 更新实体（异步）
        /// </summary>
        Task UpdateAsync(T entity);
        /// <summary>
        /// 批量更新实体（异步）
        /// </summary>
        Task UpdateRangeAsync(IEnumerable<T> entities);
        /// <summary>
        /// 删除实体（异步）
        /// </summary>
        Task DeleteAsync(T entity);
        /// <summary>
        /// 通过主键删除实体（异步）
        /// </summary>
        Task DeleteAsync(int id);
        /// <summary>
        /// 批量删除实体（异步）
        /// </summary>
        Task DeleteRangeAsync(IEnumerable<T> entities);
        /// <summary>
        /// 统一保存修改（异步）
        /// </summary>
        Task<int> SaveChangeAsync();
        /// <summary>
        /// 获取实体数量（异步）
        /// </summary>
        Task<int> CountAsync();

        Task<IDbContextTransaction> BeginTransactionAsync();
        #endregion
    }
}
