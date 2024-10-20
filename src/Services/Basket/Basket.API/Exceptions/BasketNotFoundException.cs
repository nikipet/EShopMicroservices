﻿using BuildingBlocks.Exceptions;

namespace Basket.API.Exceptions;

public class BasketNotFoundException : NotFoundException
{
    public BasketNotFoundException(string username) : base(nameof(ShoppingCart), username)
    {
    }
}
