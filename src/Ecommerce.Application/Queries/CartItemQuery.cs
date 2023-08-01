using Ecommerce.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Queries
{
    public class CartItemQuery
    {
        public class GetAllCartItemsQuery : IRequest<List<CartItemModel>>
        {
            public Guid ShopperId { get; }

            public GetAllCartItemsQuery(Guid shopperId)
            {
                ShopperId = shopperId;
            }
        }

    }
}
