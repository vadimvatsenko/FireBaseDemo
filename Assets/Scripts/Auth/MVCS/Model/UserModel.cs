using Firebase.Auth;

namespace Auth
{

    public class UserModel
    {
        public Observable<FirebaseUser> UserInfo { get; private set; } = new Observable<FirebaseUser>();    
        
    }
}