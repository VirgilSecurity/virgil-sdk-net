namespace Virgil.SDK.Keys.Clients
{
    using System;

    public class KnownKeyIds
    {
        public static Guid IdentityService { get; } = Guid.Parse("D7C6E363-04B5-412E-AD49-AFD80F2665B7");
        public static Guid PrivateService { get; } = Guid.Parse("D7C6E363-04B5-412E-AD49-AFD80F2665B6");
        public static Guid PublicService { get; } = Guid.Parse("D7C6E363-04B5-412E-AD49-AFD80F2665B5");
    }
}