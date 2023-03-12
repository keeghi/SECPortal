using System.Collections.Generic;

namespace SecPortal.Services.Infrastructures
{
    public interface IAutoMapperService<TModel>
    {
        TViewModel MapModelToViewModel<TViewModel>(TModel model);

        TModel MapViewModelToModel<TViewModel>(TViewModel viewModel);

        void AssignValue<TViewModel>(TModel model, TViewModel viewModel);
        IList<TViewModel> MapModelToViewModel<TViewModel>(IList<TModel> models);

    }
}
