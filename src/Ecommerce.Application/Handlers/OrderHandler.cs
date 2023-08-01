using AutoMapper;
using Ecommerce.Application.Commands;
using Ecommerce.Application.Queries;
using Ecommerce.Domain.Interface;
using Ecommerce.Domain.Models;
using MediatR;

namespace Ecommerce.Application.Handlers
{
    public class OrderHandler
    {
        public class DeleteOrderCommandHandler : IRequestHandler<OrderCommand.DeleteOrderCommand>
        {
            private readonly IOrderRepository _repository;

            public DeleteOrderCommandHandler(IOrderRepository repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(OrderCommand.DeleteOrderCommand request, CancellationToken cancellationToken)
            {
                await _repository.DeleteOrderById(request.Id);
                return Unit.Value;
            }
        }
        public class UpdateOrderCommandHandler : IRequestHandler<OrderCommand.UpdateOrderCommand, OrderModel>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IMapper _mapper;

            public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
            {
                _orderRepository = orderRepository;
                _mapper = mapper;
            }

            public async Task<OrderModel> Handle(OrderCommand.UpdateOrderCommand request, CancellationToken cancellationToken)
            {
                var order = await _orderRepository.GetOrderById(request.OrderId);

                order = _mapper.Map(request.Order, order);

                await _orderRepository.UpdateOrder(order);

                return _mapper.Map<OrderModel>(order);
            }
        }
        public class GetOrderByIdQueryHandler : IRequestHandler<OrderQuery.GetOrderByIdQuery, OrderModel>
        {
            private readonly IOrderRepository _repository;

            public GetOrderByIdQueryHandler(IOrderRepository repository)
            {
                _repository = repository;
            }

            public async Task<OrderModel> Handle(OrderQuery.GetOrderByIdQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetOrderById(request.Id);
            }
        }

        public class GetOrdersQueryHandler : IRequestHandler<OrderQuery.GetOrdersQuery, List<OrderModel>>
        {
            private readonly IOrderRepository _repository;

            public GetOrdersQueryHandler(IOrderRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<OrderModel>> Handle(OrderQuery.GetOrdersQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetOrders(request.UserId);
            }
        }
    }
}
