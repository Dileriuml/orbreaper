// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MS-PL license.
// See the LICENSE file in the project root for more information.

using System;

namespace MvvmCross.Base
{
#nullable enable
    public sealed class MvxStopWatch
        : IDisposable
    {
        private readonly string _message;
        private readonly int _startTickCount;

        private MvxStopWatch(string text, params object[] args)
        {
            _startTickCount = Environment.TickCount;
            _message = string.Format(text, args);
        }

        private MvxStopWatch(string tag, string text, params object[] args)
        {
            _startTickCount = Environment.TickCount;
            _message = string.Format(text, args);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public static MvxStopWatch Create(string text, params object[] args)
        {
            return new MvxStopWatch(text, args);
        }

        public static MvxStopWatch CreateWithTag(string tag, string text, params object[] args)
        {
            return new MvxStopWatch(tag, text, args);
        }
    }
#nullable restore
}
