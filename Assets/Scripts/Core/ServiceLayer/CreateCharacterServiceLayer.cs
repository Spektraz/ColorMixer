using MVC.Service;

namespace Core.ServiceLayer
{
    public class CreateCharacterServiceLayer: ServiceLayer<bool,bool>
    {
        public override bool GetContext()
        {
            return dto;
        }
    }
}



