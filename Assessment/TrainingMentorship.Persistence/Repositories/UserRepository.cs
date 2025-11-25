using System;
using Microsoft.EntityFrameworkCore;
using TrainingMentorship.Application.interfaces;
using TrainingMentorship.Domain.Entities;
using TrainingMentorship.Domain.Enums;
using TrainingMentorship.Persistence.context;

namespace TrainingMentorship.Persistence.Repositories;

public class UserRepository : IUserRepository
{

    private readonly ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<int> CreateAsync(User user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user.Id;
    }

    public async Task<bool> UpdatePasswordAsync(int userId, string newPasswordHash)
    {
        var user = await _db.Users.FindAsync(userId);
        if (user == null)
            return false;

        user.PasswordHash = newPasswordHash;
        user.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return true;

    }


}

