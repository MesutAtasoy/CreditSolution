using System;

namespace User.Contract.Models.Base
{
    public class BaseViewModel<T>
    {
        public T Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
    
    public class BaseViewModel : BaseViewModel<Guid>
    {
    }
}