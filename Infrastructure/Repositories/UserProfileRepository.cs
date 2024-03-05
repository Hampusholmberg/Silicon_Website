using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class UserProfileRepository : Repo<UserProfileEntity>
{
    private readonly DataContext _context;

    public UserProfileRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<ResponseResult> GetAllAsync()
    {
        try
        {
            IEnumerable<UserProfileEntity> result = await _context.UserProfiles
                .Include(x => x.Address)
                //.Include(x => x.SavedItems)
                .Include(x => x.ProfilePicture)               
                .ToListAsync();
            return ResponseFactory.Ok(result);
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    public override async Task<ResponseResult> GetOneAsync(Expression<Func<UserProfileEntity, bool>> predicate)
    {
        try
        {
            var result = await _context.UserProfiles
                .Include(x => x.Address)
                //.Include(x => x.SavedItems)
                .Include(x => x.ProfilePicture)
                .FirstOrDefaultAsync(predicate);
            if (result == null)
                return ResponseFactory.NotFound();

            return ResponseFactory.Ok(result);
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }
}
