namespace LR_3.TokenProccesing.Tokens_Auth
{
    public class RoleFlags
    {
        [Flags]
        public enum Roles
        {
            None = 0,
            User = 1 << 0, 
            Manager = 1 << 1,
            Admin = 1 << 2, 
            Super = 1 << 3 
        }

    }
}
