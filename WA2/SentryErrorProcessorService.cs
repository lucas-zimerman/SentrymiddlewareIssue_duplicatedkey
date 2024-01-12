using Microsoft.Extensions.Primitives;
using Sentry;
using Sentry.Extensibility;
    public class SentryErrorProcessorService : ISentryEventProcessor
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly Dictionary<string, string> _patterns;

        public SentryErrorProcessorService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
            _patterns = new Dictionary<string, string>()
            {
                { "CorrecaoId", "correcao" },
                { "OcorrenciaId", "ocorrencia" },
                { "AnomaliaId", "ocorrencia" }
            };
        }

        public SentryEvent Process(SentryEvent @event)
        {
            if (_httpContext.HttpContext == null) return @event;

            @event.SetTag("Empresa", "1");
            @event.SetTag("Unidade", "2");

            // Here I can modify the event, while taking dependencies via DI
            if (_httpContext.HttpContext.Request.Headers.ContainsKey("X-Transaction-Id"))
            {
                @event.SetTag("transaction_id", _httpContext.HttpContext.Request.Headers["X-Transaction-Id"]);
                _httpContext.HttpContext.Request.Headers.Remove("X-Transaction-Id");
            }

            if (@event.Request.Method == "GET")
            {
                SetQueryTags(@event);
            }
            else if (@event.Request.Data != null)
            {
            }

            return @event;
        }

        private void SetQueryTags(SentryEvent @event)
        {
            foreach (var pattern in _patterns)
            {
                if (_httpContext.HttpContext.Request.Query.TryGetValue(pattern.Key, out StringValues str))
                {
                    @event.SetTag(pattern.Value, str.ToString());
                }
            }
        }
    }