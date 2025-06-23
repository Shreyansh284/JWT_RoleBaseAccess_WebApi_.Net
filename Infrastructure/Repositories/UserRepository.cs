using Core.Interfaces;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository(AppDbContext dbContext):IUserRepository
    {
        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await dbContext.Users.SingleAsync(u => u.Id == id);
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await dbContext.Users.ToListAsync();
        }
        public async Task<User> AddUserAsync(User user)
        {
            user.Id = Guid.NewGuid();
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(Guid userId, User user)
        {
            var userToUpdate= await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (userToUpdate != null)
            {
                userToUpdate.Name = user.Name;
                userToUpdate.Email = user.Email;
                userToUpdate.Password = user.Password;
                await dbContext.SaveChangesAsync();
                return userToUpdate;
            }

            return user;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var userToDelete = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (userToDelete != null)
            {
                dbContext.Users.Remove(userToDelete);
                return await dbContext.SaveChangesAsync()>0;
            }
            return false;
        }


    }
}