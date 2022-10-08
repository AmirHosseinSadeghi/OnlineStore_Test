using Store_Test.Application.Interfaces.Contexts;
using Store_Test.Common.Dto;
using Store_Test.Domain.Entities.Users;

namespace Store_Test.Application.Services.Users.Commands.RegisterUser
{
    public class RegisterUserService : IRegisterUserService
    {
        private readonly IDataBaseContext _context;
        public RegisterUserService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultRegisterUserDto> Execute(RequestRegisterUserDto request)
        {
            User user = new User
            {
                Email = request.Email,
                FullName = request.FullName,
            };
            List<UserInRole> userInRoles = new List<UserInRole>();
            foreach (var item in request.Roles)
            {
                var roles = _context.Roles.Find(item.Id);
                userInRoles.Add(new UserInRole
                {
                    Role = roles,
                    RoleId = roles.Id,
                    User = user,
                    UserId = user.Id
                });
            }
            user.UserInRoles = userInRoles;
            _context.Users.Add(user);
            _context.SaveChanges();
            return new ResultDto<ResultRegisterUserDto>
            {
                IsSuccess = true,
                Message="ثبت نام کاربر انجام شد",
                Data =new ResultRegisterUserDto
                {
                    UserId = user.Id,
                }
            };
        }
    }
}
