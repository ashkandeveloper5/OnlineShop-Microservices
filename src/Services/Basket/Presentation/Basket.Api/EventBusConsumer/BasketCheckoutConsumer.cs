using AutoMapper;
using Basket.Application.UOW;
using Basket.Core.Entities.Basket;
using Basket.Core.Entities.Order;
using EventBus.Message.Events.CheckoutEvents;
using MassTransit;
using System.Net.Http;
using System.Text.Json;

namespace Basket.Api.EventBusConsumer
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;

        public BasketCheckoutConsumer(IUnitOfWork unitOfWork, IHttpClientFactory httpClientFactory,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            Order order = _unitOfWork.BasketService.GetOrderAsync(o => o.Id == context.Message.OrderId).Result[0];
            var orderDetails = _unitOfWork.BasketService.GetOrderDetailAsync(o => o.OrderId == order.Id).Result;
            foreach (var item in orderDetails)
            {
                order.TotalPrice += item.Price;
            }
            order.IsFinally = true;
            _unitOfWork.Save();
            var checkout = _unitOfWork.BasketService.AddCkeckout(_mapper.Map<Core.Entities.Basket.BasketCheckout>(context.Message)).Result;
            var result =_mapper.Map<GetOrderForCheckout>(checkout);
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5003/api/v1/Order/CheckoutOrder");
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync(context.Message.OrderId, JsonSerializer.Serialize(result));
        }
    }
}
