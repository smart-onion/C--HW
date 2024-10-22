namespace LocalLibrary
{
    public struct Message
    {
        User user;
        public Message(User user)
        {
            this.user = user;
        }

        public override string ToString()
        {
            return $"From {user.Name}: {user.Message}\0";
        }

    }
}
