using System;
using Microsoft.JSInterop;

namespace DocuitWeb.Models
{
    public class MyBlocker
    {
        private readonly IJSRuntime _myRunTime;
        public  string Text { get; set; }

        public MyBlocker(IJSRuntime iJSRunTime)
        {
            _myRunTime = iJSRunTime;
        }

        public void Show()
        {
            _myRunTime.InvokeVoidAsync(identifier: "MyLib.BlockerShow", Text);
        }

        public void Hide()
        {
            _myRunTime.InvokeVoidAsync(identifier: "MyLib.BlockerHide");
        }

    }
}
