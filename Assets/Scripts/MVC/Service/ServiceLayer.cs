using System;
using BaseService.ModelEntity;
using EventHandlerUtils;
using MVC.Model;

namespace MVC.Service
{

    
    public abstract class ServiceLayer<MODEL, DTO, CONTEXT> : ServiceLayer<DTO,CONTEXT> where MODEL : IModel
    {
        private MODEL model;

        protected MODEL Model
        {
            get
            {
                if (model == null)
                {
                    model = ModelService.GetModel<MODEL>();
                }

                return model;
            }
        }
    }

    
    public abstract class ServiceLayer<Dto, Context> : BaseServiceLayer
    {
        protected Dto dto;
        public event Action OnReset;

        public abstract Context GetContext();

        public virtual void UpdateDto(Dto dto, bool checkEquals = false)
        {
            if (checkEquals && IsInited)
            {
                if (Equals(dto, this.dto))
                {
                    return;
                }
                
            }
            this.dto = dto;
            IsInited = true;
            DtoHandler.Invoke();
        }

        public override void Reset()
        {
            dto = default;
            IsInited = false;
            DtoHandler = new Handler();
            OnReset?.Invoke();
        }

        public override bool IsInited { get; protected set; }

    
    }
}