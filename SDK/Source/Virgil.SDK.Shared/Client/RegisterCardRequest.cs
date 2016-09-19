namespace Virgil.SDK.Client
{
    using Virgil.SDK.Client.Models;

    public class RegisterCardRequest : ClientRequest
    {
        private readonly VirgilCardModel card;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterCardRequest"/> class.
        /// </summary>
        public RegisterCardRequest(VirgilCardModel cardModel)
        {
            this.card = cardModel;  
        }
    }
}