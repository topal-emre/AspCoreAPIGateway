using System;

namespace Tulpar.Core.ModelsBase
{
    public class ModelBase<T>
    {
        public T Id { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
