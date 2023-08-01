using AutoMapper;
using Ecommerce.Application.Commands;
using Ecommerce.Application.Queries;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interface;
using Ecommerce.Domain.Models;
using MediatR;

namespace Ecommerce.Application.Handlers
{
    public class CartItemHandler
    {

        public class AddCartItemCommandHandler : IRequestHandler<CartItemCommand.AddCartItemCommand, CartItemModel>
        {
            private readonly ICartItemRepository _cartItemRepository;
            private readonly IMapper _mapper;

            public AddCartItemCommandHandler(ICartItemRepository cartItemRepository, IMapper mapper)
            {
                _cartItemRepository = cartItemRepository;
                _mapper = mapper;
            }

            public async Task<CartItemModel> Handle(CartItemCommand.AddCartItemCommand request, CancellationToken cancellationToken)
            {
                var newCartItemEntity = new CartItemEntity
                {
                    ShopperId = request.ShopperId,
                    ProductName = request.ProductName
                };

                var result = await _cartItemRepository.AddCartItem(newCartItemEntity);

                return _mapper.Map<CartItemModel>(result);
            }
        }


        // DeleteCartItemCommandHandler removes a specific cart item from the cart
        public class DeleteCartItemCommandHandler : IRequestHandler<CartItemCommand.DeleteCartItemCommand>
        {
            private readonly ICartItemRepository _cartItemRepository;

            public DeleteCartItemCommandHandler(ICartItemRepository cartItemRepository)
            {
                _cartItemRepository = cartItemRepository;
            }

            public async Task<Unit> Handle(CartItemCommand.DeleteCartItemCommand request, CancellationToken cancellationToken)
            {
                await _cartItemRepository.DeleteCartItem(request.CartItemId);
                return Unit.Value;
            }
        }

        // GetAllCartItemQueryHandler gets all cart items for a specific shopper
        public class GetAllCartItemQueryHandler : IRequestHandler<CartItemQuery.GetAllCartItemsQuery, List<CartItemModel>>
        {
            private readonly ICartItemRepository _cartRepository;

            public GetAllCartItemQueryHandler(ICartItemRepository cartRepository)
            {
                _cartRepository = cartRepository;
            }

            public async Task<List<CartItemModel>> Handle(CartItemQuery.GetAllCartItemsQuery request, CancellationToken cancellationToken)
            {
                return await _cartRepository.GetAllCartItems(request.ShopperId);
            }
        }

        // UpdateCartItemCommandHandler updates a specific cart item in the cart
        public class UpdateCartItemCommandHandler : IRequestHandler<CartItemCommand.UpdateCartItemCommand, CartItemModel>
        {
            private readonly ICartItemRepository _repository;
            private readonly IMapper _mapper;

            public UpdateCartItemCommandHandler(ICartItemRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<CartItemModel> Handle(CartItemCommand.UpdateCartItemCommand request, CancellationToken cancellationToken)
            {
                var cartItemEntity = _mapper.Map<CartItemEntity>(request);

                var updatedCartItemEntity = await _repository.UpdateCartItem(cartItemEntity);

                return _mapper.Map<CartItemModel>(updatedCartItemEntity);
            }
        }
    }
}
