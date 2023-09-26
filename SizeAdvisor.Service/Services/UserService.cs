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
        private long _id;
        public async Task GenerateIdAsync()
        {
            var users = await userRepository.SelectAllAsync();
            if (users.Count == 0)
            {
                this._id = 1;
            }
            else
            {
                var user = users[users.Count - 1];
                this._id = ++user.Id;
            }
        }
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
            List<User> users = await userRepository.SelectAllAsync();
            var available = users.FirstOrDefault(u => u.Email == dto.email && u.Password == dto.password);
            if (available is null)
                throw new CustomException(404, "User is not found");
            return true;
        }
        public async Task<List<UserForResultDto>> GetAllAsync()
        {
            List<User> users = await userRepository.SelectAllAsync();
            List<UserForResultDto> userRezults = new List<UserForResultDto>();
            foreach (var user in users)
            {
                UserForResultDto result = new UserForResultDto()
                {
                    Id = user.Id,
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
                Id = user.Id,
                Email = user.Email,
                LastName = user.LastName,
                FirstName = user.FirstName,
            };

            throw new NotImplementedException();
        }
        public async Task<UserForResultDto> UpdateAsync(UserForUpdate dto)
        {
            var user = await userRepository.SelecttByIdAsync(dto.Id);
            if (user is null)
                throw new CustomException(404, "User is not found");
            var user1 = new User()
            {
                Id = dto.Id,
                Email = dto.Email,
                LastName = dto.LastName,
                FirstName = dto.FirstName,
                UpdatedAt = DateTime.UtcNow,
            };
            await userRepository.UpdateAsync(user1);
            var userResult = new UserForResultDto()
            {
                Id = dto.Id,
                Email = dto.Email,
                LastName = dto.LastName,
                FirstName = dto.FirstName,
            };
            return userResult;
        }
        public async Task<UserForResultDto> CreateAsync(UserForCreationDto dto)
        {
            var user = (await userRepository.SelectAllAsync()).
                FirstOrDefault(u => u.Email == dto.Email);
            if (user is not null)
                throw new CustomException(409, "User is already exist");
            await GenerateIdAsync();
            User user1 = new User()
            {
                Id = _id,
                Email = dto.Email,
                LastName = dto.LastName,
                FirstName = dto.FirstName,
                Password = dto.Password,
               
            };
            await userRepository.InsertAsync(user1);
            UserForResultDto result = new UserForResultDto()
            {
                Id = _id,
                Email = dto.Email,
                LastName = dto.LastName,
                FirstName = dto.FirstName,
                Password = dto.Password,
            };
            return result;
        }
    }
}
