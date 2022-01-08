using Firebase.Auth;
using Firebase.Storage;
using FireSharp;
using FireSharp.Interfaces;
namespace Movie.Core.Interfaces
{
    public interface IFirebaseManager
    {
        IFirebaseClient Database();
        FirebaseStorage Storage();
        FirebaseAuthProvider Authentication();
    }
}
