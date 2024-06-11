using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;

namespace DominicoBus.Utils
{
    public class DOMHelper
    {
        private readonly IJSRuntime _js;
        public DOMHelper(IJSRuntime JS)
        {
            _js = JS;
        }

        public async Task<string[]> GetSelectedValues(ElementReference selectReference)
        {
            var results = (await _js.InvokeAsync<string[]>("getSelectedValues", selectReference)).ToArray();
            if (results is null) return new string[0];
            return results;
        }
    }
}