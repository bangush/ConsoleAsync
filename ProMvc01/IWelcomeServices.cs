namespace ProMvc01
{
    public interface IWelcomeServices
    {
        string GetMessgse();
    }

    public class WelcomeServices : IWelcomeServices
    {
       
        public string GetMessgse()
        {
            return " form  IWelcomeServices";
        }
    }
}