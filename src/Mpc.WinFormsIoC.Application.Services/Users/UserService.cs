﻿namespace Mpc.WinFormsIoC.Application.Services.Users
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Mpc.WinFormsIoC.Application.Dto;
    using Mpc.WinFormsIoC.Application.Services.Mappings;
    using Mpc.WinFormsIoC.Domain.Core;

    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(UserDto user)
        {
            var userModel = user.ToModel();

            await _unitOfWork.UsersRepository.InsertAsync(userModel).ConfigureAwait(false);

            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await _unitOfWork.UsersRepository.GetByFilterAsync(1, 10).ConfigureAwait(false);

            return users.ToDto().ToList();
        }

        public async Task<UserDto> GetByUsernameAsync(string username)
        {
            var user = await _unitOfWork.UsersRepository.GetByUsernameAsync(username).ConfigureAwait(false);

            return user.ToDto();
        }
    }
}
