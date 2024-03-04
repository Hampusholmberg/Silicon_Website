using Infrastructure.Contexts;
using Infrastructure.Factories;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public abstract class Repo<TEntity> where TEntity : class
{
    private readonly DataContext _context;

    public Repo(DataContext context)
    {
        _context = context;
    }

    public virtual async Task<ResponseResult> CreateAsync(TEntity entity)
    {
        try
        {
            var result = await _context.Set<TEntity>().AddAsync(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Handle specific DbUpdateException
                Console.WriteLine($"DbUpdateException during SaveChangesAsync: {ex}");
                throw; // Rethrow the exception to propagate it upwards
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"Exception during SaveChangesAsync: {ex}");
                throw; // Rethrow the exception to propagate it upwards
            }



            return ResponseFactory.Ok(result);
        }
        catch (Exception ex) 
        {
            if (ex.InnerException != null)
            {
                // Handle the inner exception
                Console.WriteLine($"Inner exception message: {ex.InnerException.Message}");
                Console.WriteLine($"Inner exception type: {ex.InnerException.GetType()}");
            }
            return ResponseFactory.Error(ex.Message);
        }
    }

    public virtual async Task<ResponseResult> GetAllAsync()
    {
        try
        {
            IEnumerable<TEntity> result = await _context.Set<TEntity>().ToListAsync();
            return ResponseFactory.Ok(result);

        }
        catch (Exception ex) 
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    public virtual async Task<ResponseResult> GetOneAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var result = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (result == null)
                return ResponseFactory.NotFound();

            return ResponseFactory.Ok(result);
        }
        catch (Exception ex) 
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    public virtual async Task<ResponseResult> UpdateAsync(Expression<Func<TEntity, bool>> predicate, TEntity entity)
    {
        try
        {
            var result = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (result != null)
            {
                _context.Entry(result).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
                return ResponseFactory.Ok(result);
            }

            return ResponseFactory.NotFound();

        }
        catch (Exception ex) 
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    public virtual async Task<ResponseResult> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var result = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (result != null)
            {
                _context.Set<TEntity>().Remove(result);
                await _context.SaveChangesAsync();
                return ResponseFactory.Ok(result);
            }

            return ResponseFactory.NotFound();

        }
        catch (Exception ex) 
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    public virtual async Task<ResponseResult> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var result = await _context.Set<TEntity>().AnyAsync(predicate);
            if (result == true)
            {
                return ResponseFactory.Exists();
            }
            return ResponseFactory.NotFound();
        }   
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

}
