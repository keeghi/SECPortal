using AutoMapper;

namespace SecPortal.Services.Infrastructures
{
    public abstract class AutoMapperBase<TModel>
    {
        private readonly IMapper _mapper;

        protected AutoMapperBase(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TViewModel MapModelToViewModel<TViewModel>(TModel model)
        {
            return _mapper.Map<TModel, TViewModel>(model);
        }

        public TModel MapViewModelToModel<TViewModel>(TViewModel viewModel)
        {
            return _mapper.Map<TViewModel, TModel>(viewModel);
        }

        public void AssignValue<TViewModel>(TModel model, TViewModel viewModel)
        {
            _mapper.Map<TViewModel, TModel>(viewModel, model);
        }
    }
}
