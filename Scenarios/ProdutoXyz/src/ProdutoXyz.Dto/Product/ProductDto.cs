﻿using System;
using Tnf.Dto;

namespace ProdutoXyz.Dto.Product
{
    public class ProductDto : BaseDto
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public float Value { get; set; }
    }
}
