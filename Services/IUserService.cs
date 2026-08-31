namespace learn_asp_clean_structure.Services;

using learn_asp_clean_structure.DTOs;

public interface IUserService
{
    public List<UserResponse> GetAll();
    public UserResponse? GetById(int id);
    public UserResponse Create(UserCreateRequest user);
    public UserResponse? Update(int id, UserUpdateRequest updateUser);
    public bool Delete(int id);
}