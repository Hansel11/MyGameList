using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MyGameList.WebService.Services
{
    public abstract class BaseService : ControllerBase
    {
        private ILogger<BaseService> _logger;

        public BaseService(ILogger<BaseService> logger)
        {
            _logger = logger;
        }

        protected void SetLoggerInfo(string log)
        {
            if (_logger != null)
            {
                _logger.LogInformation(log);
            }
        }

        protected void SetLoggerInfo<T>(T log)
        {
            if (_logger != null)
            {
                _logger.LogInformation(JsonConvert.SerializeObject(log));
            }
        }

        protected void SetLoggerWarn(string log)
        {
            if (_logger != null)
            {
                _logger.LogWarning(log);
            }
        }

        protected void SetLoggerWarn<T>(T log)
        {
            if (_logger != null)
            {
                _logger.LogWarning(JsonConvert.SerializeObject(log));
            }
        }

        protected void SetLoggerError(string log)
        {
            if (_logger != null)
            {
                _logger.LogError(log);
            }
        }

        protected void SetLoggerError<T>(T log)
        {
            if (_logger != null)
            {
                _logger.LogError(JsonConvert.SerializeObject(log));
            }
        }
    }
}
