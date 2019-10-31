﻿using AutoMapper;
using Dermayon.Common.CrossCutting;
using Dermayon.Common.Infrastructure.EventMessaging;
using Dermayon.Infrastructure.EvenMessaging.Kafka.Contracts;
using Dermayon.Sample.SocialMedia.User.CqrsEventSourcing.Commands;
using Dermayon.Sample.SocialMedia.User.Framework.BLL.Contracts;
using Dermayon.Sample.SocialMedia.User.Framework.DTO;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dermayon.Sample.SocialMedia.User.CqrsEventSourcing.EventHanders
{
    public class UserCreatedEventHandler : IServiceEventHandler
    {
        private readonly IMapper _mapper;
        private readonly IAccountUserBLL _accountUserBLL;
        private readonly IKakfaProducer _producer;

        public UserCreatedEventHandler(IMapper mapper,IAccountUserBLL accountUserBLL, IKakfaProducer producer)
        {
            _mapper = mapper;
            _accountUserBLL = accountUserBLL;
            _producer = producer;
        }

        public async Task Handle(JObject jObject, ILog log, CancellationToken cancellationToken)
        {
            log.Info("Handled / consumer User created event");
            var dataConsumed = jObject.ToObject<CreateUserCommand>();

            var data = _mapper.Map<AccountUserCreateRequest>(dataConsumed);
            await _accountUserBLL.AddAsync(data);
        }
    }
    
}
