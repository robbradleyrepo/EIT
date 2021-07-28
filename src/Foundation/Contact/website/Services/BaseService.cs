namespace LionTrust.Foundation.Contact.Services
{
    using LionTrust.Foundation.Contact.Repositories;
    public class BaseService
    {
        protected IViewModelFactory ViewModelFactory;

        public BaseService(IViewModelFactory viewModelFactory)
        {
            ViewModelFactory = viewModelFactory;
        }

    }
}