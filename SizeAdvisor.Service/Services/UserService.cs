using SizeAdvisor.Data.IRepositories;
using SizeAdvisor.Data.Repositories;
using SizeAdvisor.Domain.Entities;
using SizeAdvisor.Service.Dtos;
using SizeAdvisor.Service.Exseptions;
using SizeAdvisor.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SizeAdvisor.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository = new Repository<User>();
        public async Task<bool> RemoveAsync(long id)
        {
            var user = userRepository.SelecttByIdAsync(id);
            if (user is null)
                throw new CustomException(404, "User is not found");
            await userRepository.DeleteAsync(id);

            return true;
        }
        public async Task<bool> IsThereAsync(ForCheckedDto dto)
        {
            var users = userRepository.SelectAll();
            var available = users.FirstOrDefault(u => u.Email == dto.email && u.Password == dto.password);
            if (available is null)
                throw new CustomException(404, "User is not found");
            return true;
        }
        public async Task<List<UserForResultDto>> GetAllAsync()
        {
            IQueryable<User> users =  userRepository.SelectAll();
            List<UserForResultDto> userRezults = new List<UserForResultDto>();
            foreach (var user in users)
            {
                UserForResultDto result = new UserForResultDto()
                {
                    Email = user.Email,
                    LastName = user.LastName,
                    FirstName = user.FirstName,
                };
                userRezults.Add(result);
            }
            return userRezults;
        }
        public async Task<UserForResultDto> GetByIdAsync(long id)
        {
            var user = await userRepository.SelecttByIdAsync(id);

            if (user is null)
                throw new CustomException(404, "User is not found");
            var userResult = new UserForResultDto()
            {
                Email = user.Email,
                LastName = user.LastName,
                FirstName = user.FirstName,
            };
            return userResult;
        }

        public async Task<UserForResultDto> UpdateAsync(UserForUpdate dto)
        {
            var user = await userRepository.SelecttByIdAsync(dto.Id);
            if (user is null)
                throw new CustomException(404, "User is not found");
            var user1 = new User()
            {
                Email = dto.Email,
                LastName = dto.LastName,
                FirstName = dto.FirstName,
                UpdatedAt = DateTime.UtcNow,
            };
            await userRepository.UpdateAsync(user1);
            var userResult = new UserForResultDto()
            {
                Id = user1.Id,
                Email = dto.Email,
                LastName = dto.LastName,
                FirstName = dto.FirstName,
            };
            return userResult;
        }
        public async Task<UserForResultDto> CreateAsync(UserForCreationDto dto)
        {
            var user =  userRepository.SelectAll().
                FirstOrDefault(u => u.Email == dto.Email);
            if (user is not null)
                throw new CustomException(409, "User is already exist");

            User user1 = new User()
            {
                Email = dto.Email,
                LastName = dto.LastName,
                FirstName = dto.FirstName,
                Password = dto.Password,
            };
            await userRepository.InsertAsync(user1);
            UserForResultDto result = new UserForResultDto()
            {
                Id = user1.Id,
                Email = dto.Email,
                LastName = dto.LastName,
                FirstName = dto.FirstName,
                Password = dto.Password,
            };
            return result;
        }
    }
}
