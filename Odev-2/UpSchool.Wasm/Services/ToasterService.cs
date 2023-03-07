using Blazored.Toast.Services;
using UpSchool.Domain.Services;

namespace UpSchool.Wasm.Services
{
    public class ToasterService : IToasterService
    {
        private readonly IToastService _toastService;

        public ToasterService(IToastService toastService)
        {
            _toastService = toastService;
        }

        public void ShowSuccess(string message)
        {
            throw new NotImplementedException();
        }
    }
}
