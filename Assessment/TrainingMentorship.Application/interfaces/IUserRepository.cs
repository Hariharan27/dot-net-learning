using System;
using TrainingMentorship.Domain.Entities;
using TrainingMentorship.Domain.Enums;

namespace TrainingMentorship.Application.interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<int> CreateAsync(User user);
    Task<bool> UpdatePasswordAsync(int userId, string newPasswordHash);

}

