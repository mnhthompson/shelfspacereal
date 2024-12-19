
namespace ShelfSpace;

public class UserState {
    public User user { get; set; } = new();

    public void ResetUser(){
        user = new User();
   }
}