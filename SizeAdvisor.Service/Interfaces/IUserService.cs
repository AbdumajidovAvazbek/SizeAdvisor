using SizeAdvisor.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SizeAdvisor.Service.Interfaces
{
    public interface IUserService
    {
        public Task<bool> RemoveAsync(long id);
        public Task<List<UserForResultDto>> GetAllAsync();
        public Task<UserForResultDto> GetByIdAsync(long id);
        public Task<UserForResultDto> UpdateAsync(UserForUpdate dto);
        public Task<UserForResultDto> CreateAsync(UserForCreationDto dto);
        public Task<bool> IsThereAsync(ForCheckedDto dto);
    }
}
