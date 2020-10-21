using System;
using Framework.Shared.Models.Interfaces;

namespace Framework.EntityFrameworkCore.Models
{
    public interface IBaseEntityModel : ISoftDeletable
    {
        Guid Id { get; set; }
        bool IsActive { get; set; }
        DateTime CreatedOnUtc { get; set; }
        Guid CreatedBy { get; set; }
        DateTime? UpdatedOnUtc { get; set; }
        Guid? UpdatedBy { get; set; }
    }

    public interface IBaseEntityModel<T> :  ISoftDeletable
    {
        T Id { get; set; }
        bool IsActive { get; set; }
        DateTime CreatedOnUtc { get; set; }
        Guid CreatedBy { get; set; }
        DateTime? UpdatedOnUtc { get; set; }
        Guid? UpdatedBy { get; set; }
    }
}
