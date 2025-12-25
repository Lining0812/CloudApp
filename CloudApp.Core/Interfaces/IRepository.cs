using CloudApp.Core.Entities;

namespace CloudApp.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        #region 查询方法

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
        public T GetById(int id);

        public bool Exists(int id);
        #endregion

        #region 同步操作方法
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
        /// 通过主键删除实体
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id);
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity);
        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="entities"></param>
        public void DeleteRange(IEnumerable<T> entities);
        #endregion
    }
}
