using Microsoft.JSInterop;

namespace FoodBlazor.Services.Extensions
{
    public static class IJSRuntimeExtension
    {
        public static async Task ToastrSuccess(this IJSRuntime js,string message)
        {
            await js.InvokeVoidAsync("ShowToaster", "success", message);
        }

        public static async Task ToastrError(this IJSRuntime js, string message)
        {
            await js.InvokeVoidAsync("ShowToaster", "message", message);
        }
    }
}
