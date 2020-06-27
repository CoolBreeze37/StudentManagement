namespace StudentManagement
{
    class WelcomeService :IWelcomService
    {
        public string GetMessage()
        {
            return "Hello,this is the method!";
        }
    }
}