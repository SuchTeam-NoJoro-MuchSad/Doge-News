﻿using System;

using WebFormsMvp;

namespace DogeNews.Web.Mvp.Default
{
    public interface IDefaultView : IView<DefaultViewModel>
    {
        event EventHandler PageLoad;
    }
}