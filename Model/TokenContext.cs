
namespace hw5.Model
{
    public class TokenContext 
    {
        List<string> _tokens = new();

        public void AddToken(string token)
        {
            _tokens.Add(token);
        }

        public bool IsRegisteredToken(string token)
        {
            if (_tokens.Contains(token)) return true;
            return false;
        }

        public async Task RemoveToken(string token)
        {
           _tokens.Remove(token);
        }

       
    }
}
