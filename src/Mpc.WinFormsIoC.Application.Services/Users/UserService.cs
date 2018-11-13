﻿namespace Mpc.WinFormsIoC.Application.Services.Users
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Mpc.WinFormsIoC.Application.Dto;
    using Mpc.WinFormsIoC.Domain.Core;
    using Mpc.WinFormsIoC.Domain.Models;

    public class UserService : IUserService
    {
        private static readonly List<UserDto> _users = new List<UserDto>();
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(UserDto user)
        {
            var model = new UserModel
            {
                Email = user.Email,
                Name = user.Name,
                Password = user.Password,
                Username = user.Username
            };

            await _unitOfWork.UsersRepository.InsertAsync(model).ConfigureAwait(false);

            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await _unitOfWork.UsersRepository.GetByFilterAsync(1, 10).ConfigureAwait(false);

            var usersDto = users.Select(x => new UserDto
            {
                Id = x.Id,
                Email = x.Email,
                Name = x.Name,
                Password = x.Password,
                Username = x.Username
            }).ToList();

            return usersDto;
        }

        public async Task<UserDto> GetByUsernameAsync(string username)
        {
            var user = await _unitOfWork.UsersRepository.GetByUsernameAsync(username).ConfigureAwait(false);

            var userDto = new UserDto
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                Password = string.Empty,
                Username = user.Username
            };

            return userDto;
        }
    }
}
