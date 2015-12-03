namespace Virgil.SDK.Keys.Domain
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class RecipientsCards : IEnumerable<RecipientCard>
    {
        private RecipientsCards()
        {
        }

        #region IEnumerable Implementation

        public IEnumerator<RecipientCard> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        public static RecipientsCards Search(string identityValue, params IdentityType[] identityTypes)
        {
            throw new NotImplementedException();
        }

        public string Encrypt(string text)
        {   
            throw new NotImplementedException();
        }

        public string EncryptAndSign(string text, PersonalCard card)
        {
            throw new NotImplementedException();
        }
    }
}