namespace CloudApp.Core.Exceptions
{
    /// <summary>
    /// 实体未找到异常
    /// </summary>
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityName, int id) 
            : base($"未找到 {entityName} (ID: {id})")
        {
            EntityName = entityName;
            EntityId = id;
        }

        public EntityNotFoundException(string entityName, int id, Exception innerException) 
            : base($"未找到 {entityName} (ID: {id})", innerException)
        {
            EntityName = entityName;
            EntityId = id;
        }

        public string EntityName { get; }
        public int EntityId { get; }
    }
}
