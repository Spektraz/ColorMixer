using MVC.Service;

namespace Core.ServiceLayer
{
    public class FinishAnimatorServiceLayer: ServiceLayer<bool,bool>
    {
        public override bool GetContext()
        {
            return dto;
        }
    }
}



