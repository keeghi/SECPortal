using AutoMapper;
using System.Collections.Generic;

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

        public IList<TViewModel> MapModelToViewModel<TViewModel>(IList<TModel> models)
        {
            return _mapper.Map<IList<TModel>, IList<TViewModel>>(models);
        }

        public IList<TModel> MapViewModelToModel<TViewModel>(IList<TViewModel> viewModels)
        {
            return _mapper.Map<IList<TViewModel>, IList<TModel>>(viewModels);
        }
    }
}
