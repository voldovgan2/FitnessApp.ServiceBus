using FitnessApp.Common.Abstractions.Db.Entities.Collection;
using FitnessApp.Common.Abstractions.Models.Collection;
using FitnessApp.Common.Abstractions.Services.Collection;
using FitnessApp.Common.Serializer.JsonSerializer;
using FitnessApp.ServiceBus.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessApp.ServiceBus.AzzureServiceBus.TopicSubscribers
{
    public abstract class CollectionServiceNewUserRegisteredSubscriberService<Entity, CollectionItemEntity, Model, CollectionItemModel, CreateModel, UpdateModel> : ITopicSubscribers
        where Entity : ICollectionEntity
        where CollectionItemEntity : ICollectionItemEntity
        where Model : ICollectionModel
        where CollectionItemModel : ISearchableCollectionItemModel
        where CreateModel : ICreateCollectionModel
        where UpdateModel : IUpdateCollectionModel
    {
        protected readonly ICollectionService<Entity, CollectionItemEntity, Model, CollectionItemModel, CreateModel, UpdateModel> _service;
        protected readonly IJsonSerializer _serializer;

        public IEnumerable<Tuple<string, string, Func<string, Task>>> TopicsSubscribers { get; }

        public CollectionServiceNewUserRegisteredSubscriberService
        (
            ICollectionService<Entity, CollectionItemEntity, Model, CollectionItemModel, CreateModel, UpdateModel> service,
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
