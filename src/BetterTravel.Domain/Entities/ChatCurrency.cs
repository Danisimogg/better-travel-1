using BetterTravel.Domain.Entities.Base;
using CSharpFunctionalExtensions;

namespace BetterTravel.Domain.Entities
{
    public class ChatCurrency : Entity
    {
        protected ChatCurrency()
        {
        }

        private ChatCurrency(Chat chat, Currency currency)
        {
            Chat = chat;
            Currency = currency;
        }
        
        public virtual Chat Chat { get; private set; }
        public virtual Currency Currency { get; private set; }

        public Result<ChatCurrency> Create(Maybe<Chat> maybeChat, Maybe<Currency> maybeCurrency)
        {
            var chatResult = maybeChat.ToResult("Chat was not provided.");
            var currencyResult = maybeCurrency.ToResult("Currency was not provided.");

            return Result
                .Combine(chatResult, currencyResult)
                .Bind(() => Result.Success(new ChatCurrency(chatResult.Value, currencyResult.Value)));
        }
    }
}