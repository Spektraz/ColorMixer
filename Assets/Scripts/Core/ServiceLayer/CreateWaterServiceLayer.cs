using Core.Model;
using MVC.Service;

namespace Core.ServiceLayer
{
    public class CreateWaterServiceLayer : ServiceLayer<FruitsName,FruitsName>
    {
        public override FruitsName GetContext()
        {
            return dto;
        }
    }
}




