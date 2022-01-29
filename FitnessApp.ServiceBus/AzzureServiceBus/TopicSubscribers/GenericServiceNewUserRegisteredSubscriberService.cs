using FitnessApp.Common.Abstractions.Db.Entities.Base;
using FitnessApp.Common.Abstractions.Models.Base;
using FitnessApp.Common.Abstractions.Services.Base;
using FitnessApp.Common.Serializer.JsonSerializer;
using FitnessApp.ServiceBus.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessApp.ServiceBus.AzzureServiceBus.TopicSubscribers
{
    public abstract class GenericServiceNewUserRegisteredSubscriberService<Entity, Model, GetItemsModel, CreateModel, UpdateModel> : ITopicSubscribers
        where Entity : IEntity
        where Model : ISearchableModel
        where GetItemsModel : IGetItemsModel
        where CreateModel : ICreateModel
        where UpdateModel : IUpdateModel
    {
        protected readonly IGenericService<Entity, Model, GetItemsModel, CreateModel, UpdateModel> _service;
        protected readonly IJsonSerializer _serializer;

        public IEnumerable<Tuple<string, string, Func<string, Task>>> TopicsSubscribers { get; }

        public GenericServiceNewUserRegisteredSubscriberService
        (
            IGenericService<Entity, Model, GetItemsModel, CreateModel, UpdateModel> service,
            string subscription,
            IJsonSerializer serializer
        )
        {
            _service = service;
            _serializer = serializer;
            TopicsSubscribers = new List<Tuple<string, string, Func<string, Task>>>
            {
                new Tuple<string, string, Func<string, Task>>(Topic.NEW_USER_REGISTERED, subscription, HandleNewUserRegisteredEvent)
            };
        }

        protected virtual async Task HandleNewUserRegisteredEvent(string data)
        {
            var integrationEvent = _serializer.DeserializeFromString<NewUserRegistered>(data);
            var model = Activator.CreateInstance<CreateModel>();
            model.UserId = integrationEvent.UserId;
            await _service.CreateItemAsync(model);
        }
    }
}
