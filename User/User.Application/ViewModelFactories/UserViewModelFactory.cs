using User.Application.ViewModelFactories.Base;
using User.Contract.Models.User;

namespace User.Application.ViewModelFactories
{
    public class UserViewModelFactory : IViewModelFactory
    {
        public UserViewModel Create(Domain.User user)
        {
            return new UserViewModel
            {

                Id = user.Id,
                CreatedBy = user.CreatedBy,
                CreatedOnUtc = user.CreatedOnUtc,
                UpdatedOnUtc = user.UpdatedOnUtc,
                UpdatedBy = user.UpdatedBy,
                IsActive = user.IsActive,
                IsDeleted = user.IsDeleted,
                Name = user.Name,
                IdentificationNumber = user.IdentificationNumber,
                PhoneNumber = user.PhoneNumber
            };
        }
    }
}