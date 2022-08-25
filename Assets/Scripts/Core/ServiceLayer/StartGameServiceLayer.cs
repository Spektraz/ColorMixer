using MVC.Service;

namespace Core.ServiceLayer
{
    public class StartGameServiceLayer : ServiceLayer<bool,bool>
    {
        public override bool GetContext()
        {
            return dto;
        }
    }
}

