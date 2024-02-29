using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class UserSavedItemRepository : Repo<UserSavedItemEntity>
{
    public UserSavedItemRepository(DataContext context) : base(context)
    {
    }
}