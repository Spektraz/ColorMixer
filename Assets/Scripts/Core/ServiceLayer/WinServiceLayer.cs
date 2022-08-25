using MVC.Service;

namespace Core.ServiceLayer
{
    public class WinServiceLayer : ServiceLayer<bool,bool>
    {
        public override bool GetContext()
        {
            return dto;
        }
    }
}
