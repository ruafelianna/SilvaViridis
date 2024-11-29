using SilvaViridis.Common.Text.Extensions;
using System;

namespace SilvaViridis.Common.Localization.Abstractions
{
    public abstract class TranslatableException : ApplicationException
    {
        public TranslatableException(
            ITranslationUnit message,
            params object?[]? args
        ) : base()
        {
            _message = message;
            _messageArgs = args;
        }

        protected TranslatableException() : base()
        {
        }

        protected TranslatableException(string message) : base(message)
        {
        }

        protected TranslatableException(
            string message,
            Exception innerException
        ) : base(message, innerException)
        {
        }

        public override string Message =>
            _message?.Value.Format(_messageArgs)
            ?? base.Message;

        private readonly ITranslationUnit? _message;

        private readonly object?[]? _messageArgs;
    }
}
