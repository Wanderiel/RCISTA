using ApplicationCore.Dtos.Users;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Domain.Models.Users;

namespace ApplicationCore.Services;

public class AuthService
{
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(IUserRepository repository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task Register(RegisteredUserDto userDto, CancellationToken cancellationToken = default)
    {
        FullName fullName = FullName.Create(userDto.FirstName, userDto.LastName, userDto.Patronymic);
        string passwordHash = _passwordHasher.CreateHash(userDto.Password1);
        User user = new User(userDto.Login.ToLower(), fullName, passwordHash);
        _repository.Insert(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> Login(LoginUserDto loginUser, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(loginUser.Login))
            return false;

        User user = await _repository.GetByLoginAsync(loginUser.Login.ToLower(), cancellationToken)
            ?? throw new MissingUserException();

        return _passwordHasher.Compare(loginUser.Password, user.PasswordHash);
    }
}
