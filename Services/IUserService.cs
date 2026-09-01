namespace learn_asp_clean_structure.Services;

using learn_asp_clean_structure.DTOs;

public interface IUserService
{
    public Task<UserResponse?> GetById(int id);
    public Task<UserResponse> Create(UserCreateRequest user);
    public Task<UserResponse?> Update(int id, UserUpdateRequest updateUser);
    public Task<bool> Delete(int id);
}