using CloudApp.Core.Entities;

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
        public T GetById(int id);
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
        #endregion
    }
}
