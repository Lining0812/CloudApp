

namespace CloudApp.Core.Interface
{
    public interface IRepository<T>
    {
        #region 查询方法

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAllEntities();

        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetEntityById(int id);
        #endregion

        public void AddEntity(T entity);
        public void UpdateEntity(T entity);
        public void DeleteEntity(int id);
        public bool FindEntity(int id);
    }
}
