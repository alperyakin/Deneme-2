using Microsoft.AspNetCore.Http;

namespace Deneme2.BuildingBlocks.Presentation.SessionContexts;

public sealed class DefaultSessionContext(IHttpContextAccessor httpContextAccessor) : SessionContext(httpContextAccessor);
