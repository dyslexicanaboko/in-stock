﻿using FluentValidation;
using InStock.Lib.Exceptions;

namespace InStock.Lib.Validation
{
  public static class FluentValidationExtensions
  {
    public static IRuleBuilderOptions<T, TProperty> WithMessageAndErrorCode<T, TProperty>(
      this IRuleBuilderOptions<T, TProperty> rule,
      InvalidArgumentException exception)
      =>
        rule.WithMessage(exception.Message)
          .WithErrorCode(exception.ErrorCode.ToString());
  }
}
